using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.ZiXunService.WenHuaHuoDong
{
    /// <summary>
    /// 文化活动 西安
    /// </summary>
    public class WenHuaHuoDongXiAnService82 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.WenHuaHuoDongXiAnService82, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}