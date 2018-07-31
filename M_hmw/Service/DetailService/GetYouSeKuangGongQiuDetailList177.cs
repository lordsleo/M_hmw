using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    /// <summary>
    /// 获取有色矿供求明细数据列表
    /// </summary>
    public class GetYouSeKuangGongQiuDetailList177 : BaseServiceWL
    {
        protected override string getSql()
        {
            return String.Format(SQL.GetYouSeKuangGongQiuDetailList177,  this.Params[0]);
        }
    }
}