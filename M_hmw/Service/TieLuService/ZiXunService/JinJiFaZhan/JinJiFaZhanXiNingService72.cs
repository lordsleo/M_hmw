using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.ZiXunService.JinJiFaZhan
{
    /// <summary>
    /// 经济发展 西宁
    /// </summary>
    public class JinJiFaZhanXiNingService72 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.JinJiFaZhanXiNingService72, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}