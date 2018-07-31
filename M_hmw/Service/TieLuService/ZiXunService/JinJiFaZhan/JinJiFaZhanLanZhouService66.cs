using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.ZiXunService.JinJiFaZhan
{
    /// <summary>
    /// 经济发展 兰州
    /// </summary>
    public class JinJiFaZhanLanZhouService66 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.JinJiFaZhanLanZhouService66, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}