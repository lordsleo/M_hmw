using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.MaoYiService
{
    /// <summary>
    /// 煤焦求购
    /// </summary>
    public class MeiJiaoQiuGouService13 : BaseServiceLYG
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.MeiJiaoQiuGouService13, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}