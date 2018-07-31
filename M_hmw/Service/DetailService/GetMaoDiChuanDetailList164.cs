using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    public class GetMaoDiChuanDetailList164 : BaseServiceBS
    {
        protected override string getSql()
        {
            //现在时间
            DateTime nowTime = DateTime.Now;

            //设置三个时间点,默认查询5条
            String[] values = new String[] { nowTime.AddDays(-6).ToString(), nowTime.AddDays(-3).ToString(), nowTime.AddDays(-1).ToString(), this.Params[0] };

            return String.Format(SQL.GetMaoDiChuanDetailList164, values);
        }
    }
}