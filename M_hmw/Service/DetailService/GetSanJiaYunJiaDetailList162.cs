using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    /// <summary>
    /// 散杂运价明细数据列表
    /// </summary>
    public class GetSanJiaYunJiaDetailList162 : BaseService
    {
        protected override string getSql()
        {
            return String.Format(SQL.GetSanJiaYunJiaDetailList162, this.Params[0]);
        }
    }
}