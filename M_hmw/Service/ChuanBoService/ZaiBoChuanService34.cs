using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.ChuanBoService
{
    /// <summary>
    /// 在泊船
    /// </summary>
    public class ZaiBoChuanService34 : BaseServiceBS
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.ZaiBoChuanService34, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}