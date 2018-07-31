using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    public class GetGuanHeGuoJiQueBaoChuanDetailList171 : BaseService
    {
        protected override string getSql()
        {
            return String.Format(SQL.GetGuanHeGuoJiQueBaoChuanDetailList171, this.Params[0]);
        }
    }
}