using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    public class GetYuanQuCangChuDetailList179 : BaseService
    {
        protected override string getSql()
        {
            return String.Format(SQL.GetYuanQuCangChuDetailList179, this.Params[0]);
        }
    }
}