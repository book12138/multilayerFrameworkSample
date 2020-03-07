using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultilayerFrameworkSample.Common.ValueMapping
{
    /// <summary>
    /// 在通过映射的方式进行赋值时，对源的属性进行名称标记，以便在映射的时候可以找到
    /// </summary>
    public class ValueMappingPropertyName : Attribute
    {
        public string Name { get; set; }

        public ValueMappingPropertyName(string name)
        {
            this.Name = name;
        }
    }
}
