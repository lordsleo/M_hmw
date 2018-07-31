using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    /// <summary>
    /// 获取集装箱运价明细数据列表
    /// </summary>
    public class GetJiZhuangXiangYunJiaDetailList160 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.GetJiZhuangXiangYunJiaDetailList160, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}