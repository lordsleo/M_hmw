using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.ZiXunService.BenDiXinWen
{
    /// <summary>
    /// 本地新闻 新疆
    /// </summary>
    public class BenDiXinWenXinJiangService62 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.BenDiXinWenXinJiangService62, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}