using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    /// <summary>
    /// 获取网上车源数据列表
    /// </summary>
    public class GetWangShangCheYuanDetailList169 : BaseService
    {
        protected override string getSql()
        {

            return String.Format(SQL.GetWangShangCheYuanDetailList169,  this.Params[0]);
        }
    }
}