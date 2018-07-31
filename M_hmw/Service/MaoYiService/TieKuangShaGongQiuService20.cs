using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.MaoYiService
{
    /// <summary>
    /// 铁矿砂供求
    /// </summary>
    public class TieKuangShaGongQiuService20 : BaseServiceWL
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.TieKuangShaGongQiuService20, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}