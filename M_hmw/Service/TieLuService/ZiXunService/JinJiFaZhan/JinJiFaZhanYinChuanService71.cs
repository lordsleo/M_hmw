using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.ZiXunService.JinJiFaZhan
{
    /// <summary>
    /// 经济发展 银川
    /// </summary>
    public class JinJiFaZhanYinChuanService71 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.JinJiFaZhanYinChuanService71, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}