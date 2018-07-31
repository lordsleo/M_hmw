using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    /// <summary>
    /// 获取首页最新运力明细数据列表
    /// </summary>
    public class GetMainPageLatestTransCapDetailList157 : BaseService
    {
        protected override string getSql()
        {
            return String.Format(SQL.GetMainPageLatestTransCapDetailList157, this.Params[0]);
        }
    }
}