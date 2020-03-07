using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MultilayerFrameworkSample.Common.ValueMapping
{
    /// <summary>
    /// 将DTO的值映射到一个现有的实体模型中
    /// </summary>
    public static class ValueMappingSet
    {
        /// <summary>
        /// 通过映射的方式，将源数据实例的数据，赋值到现有的目标实例上
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TTarget">目标类型</typeparam>
        /// <param name="source">源数据实例</param>
        /// <param name="target">目标</param>
        public static void ValueMapping<TSource, TTarget>(this TSource source, TTarget target)
        {
            Type sourceType = typeof(TSource);
            Type targetType = typeof(TTarget);
            foreach (var item in sourceType.GetProperties())
            {
                string propertyName = item.Name;
                
                if (item.IsDefined(typeof(ValueMappingPropertyName), false))
                    propertyName = sourceType.GetCustomAttribute<ValueMappingPropertyName>().Name;
                PropertyInfo property = targetType.GetProperty(propertyName);
                if (property != null)
                    property.SetValue(target, item.GetValue(source));
            }
        }
    }
}
