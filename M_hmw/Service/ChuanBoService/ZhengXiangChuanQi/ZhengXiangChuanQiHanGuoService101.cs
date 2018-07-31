using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.ChuanBoService.ZhengXiangChuanQi
{
    /// <summary>
    /// 整箱船期 韩国
    /// </summary>
    public class ZhengXiangChuanQiHanGuoService101 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.ZhengXiangChuanQiHanGuoService101, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}