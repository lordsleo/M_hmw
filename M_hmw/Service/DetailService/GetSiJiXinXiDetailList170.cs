using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    /// <summary>
    /// 获取司机信息数据列表
    /// </summary>
    public class GetSiJiXinXiDetailList170 : BaseServiceT
    {
        protected override string getSql()
        {
            return String.Format(SQL.GetSiJiXinXiDetailList170, this.Params[0]);
        }
    }
}