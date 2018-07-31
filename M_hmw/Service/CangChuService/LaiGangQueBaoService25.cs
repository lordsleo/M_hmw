using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.CangChuService
{
    /// <summary>
    /// 来港确报
    /// </summary>
    public class LaiGangQueBaoService25 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.LaiGangQueBaoService25, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}