using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.GongLuService
{
    /// <summary>
    /// 已放行车辆
    /// </summary>
    public class YiFangXingCheLiangService1 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.YiFangXingCheLiangService1, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}