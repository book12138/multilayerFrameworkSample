using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultilayerFrameworkSample.Common
{
    /// <summary>
    /// id生成帮助类
    /// </summary>
    public class CombineIdHelper
    {
        public string LastId { get; set; }

        /// <summary>
        /// 创建id
        /// </summary>
        /// <returns></returns>
        public string CreateId()
        {
            LastId = Guid.NewGuid().ToString("N").Substring(0, 10) + DateTime.Now.ToString("mmssfff").Substring(1);
            return LastId;
        }

        public string CreateNewId() => Guid.NewGuid().ToString("N").Substring(0, 10) + DateTime.Now.ToString("mmssfff").Substring(1);
    }
}
