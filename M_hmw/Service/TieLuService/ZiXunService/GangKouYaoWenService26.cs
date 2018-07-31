using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.ZiXunService
{
    /// <summary>
    /// 港口要闻
    /// </summary>
    public class GangKouYaoWenService26 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.GangKouYaoWenService26, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}