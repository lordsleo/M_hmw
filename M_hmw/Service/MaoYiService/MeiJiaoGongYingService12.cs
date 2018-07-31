using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.MaoYiService
{
    /// <summary>
    /// 煤焦供应
    /// </summary>
    public class MeiJiaoGongYingService12 : BaseServiceLYG
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.MeiJiaoGongYingService12, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}