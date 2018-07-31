using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    /// <summary>
    /// / 获取集装箱船期明细数据列表
    /// </summary>
    public class GetJiZhuangXiangChuanQiDetailList159 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.GetJiZhuangXiangChuanQiDetailList159, this.Params[0]);
        }
    }
}