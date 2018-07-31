using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.ZiXunService.WenHuaHuoDong
{
    /// <summary>
    /// 文化活动 新疆
    /// </summary>
    public class WenHuaHuoDongXinJiangService83 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.WenHuaHuoDongXinJiangService83, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}