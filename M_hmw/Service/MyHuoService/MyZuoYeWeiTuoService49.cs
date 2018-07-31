using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.MyHuoService
{
    /// <summary>
    /// 我的作业委托
    /// </summary>
    public class MyZuoYeWeiTuoService49 : BaseServiceH
    {
        protected override string getSql()
        {
            //0为传入参数船代客户ID
            //默认查询5条
            return String.Format(SQL.MyZuoYeWeiTuoService49, this.Params[0], this.Params.Length > 1 ? this.Params[1] : "5");
        }
    }
}