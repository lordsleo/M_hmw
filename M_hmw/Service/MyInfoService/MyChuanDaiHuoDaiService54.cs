using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.MyInfoService
{
    /// <summary>
    /// 我的船代货代
    /// </summary>
    public class MyChuanDaiHuoDaiService54 : BaseServiceMC
    {
        protected override string getSql()
        {
            //0为传入参数用户ID
            //默认查询5条
            return String.Format(SQL.MyChuanDaiHuoDaiService54, this.Params[0], this.Params.Length > 1 ? this.Params[1] : "5");
        }
    }
}