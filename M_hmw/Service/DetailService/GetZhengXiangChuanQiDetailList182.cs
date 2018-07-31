using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    public class GetZhengXiangChuanQiDetailList182 : BaseService
    {
        /// <summary>
        /// 获整箱船期明细数据列表
        /// </summary>
        /// <returns></returns>
        protected override string getSql()
        {
            return String.Format(SQL.GetZhengXiangChuanQiDetailList182, this.Params[0]);
        }
    }
}