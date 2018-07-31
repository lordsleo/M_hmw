using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    /// <summary>
    /// 获取我的泊位船舶明细数据列表
    /// </summary>
    public class GetWoBoWeiChuanBoDetailList200 : BaseServiceBS
    {
        protected override string getSql()
        {
            return String.Format(SQL.GetWoBoWeiChuanBoDetailList200, this.Params[0]);
        }
    }
}