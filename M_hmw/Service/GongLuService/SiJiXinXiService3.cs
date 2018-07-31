using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.GongLuService
{
    /// <summary>
    /// 司机信息
    /// </summary>
    public class SiJiXinXiService3 : BaseServiceT
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.SiJiXinXiService3, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}