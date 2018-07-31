using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.ZiXunService.WenHuaHuoDong
{
    /// <summary>
    /// 文化活动 山西
    /// </summary>
    public class WenHuaHuoDongSanXiService81 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.WenHuaHuoDongSanXiService81, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}