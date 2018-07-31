using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.ChuanBoService.ZhengXiangYunJia
{
    /// <summary>
    /// 整箱运价 非洲
    /// </summary>
    public class ZhengXiangYunJiaFeiZhouService123 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.ZhengXiangYunJiaFeiZhouService123, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}