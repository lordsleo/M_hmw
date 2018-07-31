using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.CangChuService
{
    /// <summary>
    /// 仓储资讯
    /// </summary>
    public class CangChuZiXunService22 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.CangChuZiXunService22, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}