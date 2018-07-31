using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.CangChuService
{
    /// <summary>
    /// 园区仓储
    /// </summary>
    public class YuanQuCangChuService23 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.YuanQuCangChuService23, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}