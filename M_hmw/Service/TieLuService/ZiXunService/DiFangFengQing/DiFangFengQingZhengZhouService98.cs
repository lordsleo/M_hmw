using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.ZiXunService.DiFangFengQing
{
    /// <summary>
    /// 地方风情 郑州
    /// </summary>
    public class DiFangFengQingZhengZhouService98 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.DiFangFengQingZhengZhouService98, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}