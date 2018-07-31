using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    /// <summary>
    /// 获取散杂船期明细数据列表
    /// </summary>
    public class GetSanJiaChuanQiDetailList161 : BaseService
    {
        protected override string getSql()
        {
            return String.Format(SQL.GetSanJiaChuanQiDetailList161, this.Params[0]);
        }
    }
}