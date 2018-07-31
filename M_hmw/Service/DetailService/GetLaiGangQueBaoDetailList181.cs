using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    /// <summary>
    /// 获取来港确报明细数据列表
    /// </summary>
    public class GetLaiGangQueBaoDetailList181 : BaseService
    {
        protected override string getSql()
        {
            return String.Format(SQL.GetLaiGangQueBaoDetailList181, this.Params[0]);
        }
    }
}