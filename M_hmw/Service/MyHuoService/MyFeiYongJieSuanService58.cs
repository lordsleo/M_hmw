using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.MyHuoService
{
    /// <summary>
    /// 我的费用结算
    /// </summary>
    public class MyFeiYongJieSuanService58 : BaseServiceH
    {
        protected override string getSql()
        {
            //0为服务传入参数船代ID
            //默认查询5条
            return String.Format(SQL.MyFeiYongJieSuanService58, this.Params[0], this.Params.Length > 1 ? this.Params[1] : "5");
        }
    }
}