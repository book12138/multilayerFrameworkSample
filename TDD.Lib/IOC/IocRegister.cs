using Autofac;
using MultilayerFrameworkSample.DAL;
using MultilayerFrameworkSample.DAL.Base;
using MultilayerFrameworkSample.IDAL;
using MultilayerFrameworkSample.IDAL.Base;

namespace TDD.Lib.IOC
{
    public class IocInstanceResolve
    {
        private IocInstanceResolve() { }            

        private ContainerBuilder builder = new ContainerBuilder();

        private IContainer container;

        private readonly static object obj = new object();

        private static IocInstanceResolve Instance { get; set; }

        /// <summary>
        /// ioc服务实例获取
        /// </summary>
        public static IocInstanceResolve IocInstance
        {
            get
            {
                if (Instance == null)
                    lock (obj)
                        if (Instance == null)  // 并发情况下的双重检查锁
                            Instance = new IocInstanceResolve();

                return Instance;
            }
        }

        //public void Register(ContainerBuilder builder)
        //{
        //    //this.builder = builder;
        //    this.container = builder.Build();
        //}

        /// <summary>
        /// ioc注册
        /// </summary>
        public void Register()
        {
            builder.RegisterType<IUserInfoDal>().As<UserInfoDal>();
            this.container = this.builder.Build();
        }

        /// <summary>
        /// 从ioc容器中创建实例
        /// </summary>
        public T Resolve<T>()
            => this.container.Resolve<T>();
    }
}
