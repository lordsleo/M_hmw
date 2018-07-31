using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    /// <summary>
    ///  获取码头卸车计划数据列表
    /// </summary>
    public class GetMaTouXieCheJiHuaDetailList166 : BaseServiceJZ
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.GetMaTouXieCheJiHuaDetailList166, this.Params[0]);
        }
    }
}