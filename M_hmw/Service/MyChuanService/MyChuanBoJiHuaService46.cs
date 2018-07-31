using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.MyChuanService
{
    /// <summary>
    /// 我的船舶计划
    /// </summary>
    public class MyChuanBoJiHuaService46 : BaseServiceBS
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.MyChuanBoJiHuaService46, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}