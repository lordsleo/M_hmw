using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    /// <summary>
    /// 获取我的高频话费明细数据列表
    /// </summary>
    public class GetWoDeGaoPinHuaFeiDetailList199 : BaseServiceMC
    {
        protected override string getSql()
        {
            return String.Format(SQL.GetWoDeGaoPinHuaFeiDetailList199, this.Params[0]);
        }
    }
}