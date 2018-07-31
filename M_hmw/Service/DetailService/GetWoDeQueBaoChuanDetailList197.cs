using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    /// <summary>
    /// 获取我的确报船明细数据列表
    /// </summary>
    public class GetWoDeQueBaoChuanDetailList197 : BaseServiceBS
    {
        protected override string getSql()
        {
            return String.Format(SQL.GetWoDeQueBaoChuanDetailList197, this.Params[0]);
        }
    }
}