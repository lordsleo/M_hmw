using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.NeiHeService
{
    /// <summary>
    /// 灌河国际泊位船
    /// </summary>
    public class GuanHeGuoJiBoWeiChuanService7 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.GuanHeGuoJiBoWeiChuanService7, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}