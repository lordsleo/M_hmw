using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    /// <summary>
    /// 获取首页最新货源明细数据列表
    /// </summary>
    public class GetMainPageLatestGoodsSourceDetailList158 : BaseService
    {
        protected override string getSql()
        {
            return String.Format(SQL.GetMainPageLatestGoodsSourceDetailList158, this.Params[0]);
        }
    }
}