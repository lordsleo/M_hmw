using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.MaoYiService
{
    /// <summary>
    /// 有色矿供求
    /// </summary>
    public class YouSeKuangGongQiuService17 : BaseServiceWL
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.YouSeKuangGongQiuService17, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}