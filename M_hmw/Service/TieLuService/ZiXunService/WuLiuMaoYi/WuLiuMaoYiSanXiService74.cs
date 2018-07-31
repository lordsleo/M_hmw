using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.ZiXunService.WuLiuMaoYi
{
    /// <summary>
    /// 物流贸易 山西
    /// </summary>
    public class WuLiuMaoYiSanXiService74 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.WuLiuMaoYiSanXiService74, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}