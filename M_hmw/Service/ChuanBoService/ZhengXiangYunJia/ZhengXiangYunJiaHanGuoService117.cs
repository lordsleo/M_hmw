using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.ChuanBoService.ZhengXiangYunJia
{
    /// <summary>
    /// 整箱运价 韩国
    /// </summary>
    public class ZhengXiangYunJiaHanGuoService117 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.ZhengXiangYunJiaHanGuoService117, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}