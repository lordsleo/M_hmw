using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    public class GetXinYunTaiQueBaoChuanDetailList172 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.GetXinYunTaiQueBaoChuanDetailList172, this.Params[0]);
        }
    }
}