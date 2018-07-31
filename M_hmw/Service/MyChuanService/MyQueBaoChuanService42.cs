using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.MyChuanService
{
    /// <summary>
    /// 我的确报船
    /// </summary>
    public class MyQueBaoChuanService42 : BaseServiceBS
    {
        protected override string getSql()
        {
            //0为传入参数船代客户ID,1为系统时间
            //默认查询5条
            return String.Format(SQL.MyQueBaoChuanService42, this.Params[0], DateTime.Now.ToString(), this.Params.Length > 2 ? this.Params[2] : "5");
        }
    }
}