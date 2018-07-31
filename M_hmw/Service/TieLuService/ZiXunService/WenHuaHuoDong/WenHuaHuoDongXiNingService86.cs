using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.ZiXunService.WenHuaHuoDong
{
    /// <summary>
    /// 文化活动 西宁
    /// </summary>
    public class WenHuaHuoDongXiNingService86 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.WenHuaHuoDongXiNingService86, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}