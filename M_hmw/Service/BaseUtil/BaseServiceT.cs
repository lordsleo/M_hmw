using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.BaseUtil
{
    /// <summary>
    /// 服务基类，身份账号为“transit”
    /// </summary>
    public abstract class BaseServiceT : BaseService
    {
        /// <summary>
        /// 重写数据库身份
        /// </summary>
        /// <returns>返回“transit”身份配置</returns>
        protected override string getDataBaseIdentity()
        {
            return "transit";
        }
    }
}