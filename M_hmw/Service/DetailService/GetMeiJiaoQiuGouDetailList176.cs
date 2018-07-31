using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    public class GetMeiJiaoQiuGouDetailList176 : BaseServiceLYG
    {
        /// <summary>
        /// 获取煤焦求购明细数据列表
        /// </summary>
        /// <returns></returns>
        protected override string getSql()
        {
            return String.Format(SQL.GetMeiJiaoQiuGouDetailList176, this.Params[0]);
        }
    }
}