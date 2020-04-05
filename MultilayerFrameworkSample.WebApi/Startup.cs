using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using MultilayerFrameworkSample.DAL;
using MultilayerFrameworkSample.DAL.Base;
using System.Data;
using Microsoft.Data.SqlClient;
using MultilayerFrameworkSample.WebApi.Filter;
using MultilayerFrameworkSample.BLL;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerGen;
using MultilayerFrameworkSample.DAL.Interface;
using MultilayerFrameworkSample.BLL.Interface;
using System.Reflection;

namespace MultilayerFrameworkSample.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        #region autofac ioc
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // 在这里添加服务注册
            builder.RegisterType<UserInfoDal>().As<IUserInfoDal>();
            builder.RegisterType<UserInfoBll>().As<IUserInfoBll>();
            builder.RegisterInstance<IDbConnection>(new SqlConnection(Configuration.GetConnectionString("myConnectonStr")));
        }
        #endregion

        private readonly string apiName = "***";
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()

            #region newtonsoft
                .AddNewtonsoftJson(options =>
                {
                    //修改属性名称的序列化方式，首字母小写
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                    //修改时间的序列化方式
                    options.SerializerSettings.Converters.Add(new IsoDateTimeConverter() { DateTimeFormat = "yyyy/MM/dd HH:mm:ss" });
                }
            );
            #endregion

            services.AddControllersWithViews().AddControllersAsServices();

            services.AddMvc(options =>
                options.Filters.Add(typeof(GlobalException))
                );

            #region swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new OpenApiInfo
                {
                    Version = "V1",//版本号
                    Title = $"{apiName} 接口文档——dotnetcore 3.1",//编辑标题
                    Description = $"{apiName} HTTP API V1",//编辑描述
                    Contact = new OpenApiContact { Name = apiName, Email = "sogood12138@163.com" },//编辑联系方式
                    License = new OpenApiLicense { Name = apiName }//编辑许可证
                });
                c.OrderActionsBy(o => o.RelativePath);
                c.CustomOperationIds(u => u.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : u.GroupName);

                var xmlPath = Path.Combine(Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath, "MultilayerFrameworkSample.WebApi.xml");// 配置接口文档文件路径
                var dtoXmlPath = Path.Combine(Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath, "MultilayerFrameworkSample.Dto.xml");
                c.IncludeXmlComments(xmlPath, true); // 把接口文档的路径配置进去。第二个参数表示的是是否开启包含对Controller的注释容纳
                c.IncludeXmlComments(dtoXmlPath, true);                
            });
            #endregion

            #region cors
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region swagger

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/V1/swagger.json", $"{apiName} V1");
                c.RoutePrefix = "";
                c.DisplayOperationId();
            });

            #endregion

            app.UseRouting();

            #region cors

            app.UseCors(MyAllowSpecificOrigins);

            #endregion

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
