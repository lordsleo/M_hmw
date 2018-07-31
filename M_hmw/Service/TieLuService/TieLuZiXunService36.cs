using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.TieLuService
{
    /// <summary>
    /// 铁路资讯
    /// </summary>
    public class TieLuZiXunService36 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.TieLuZiXunService36, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}