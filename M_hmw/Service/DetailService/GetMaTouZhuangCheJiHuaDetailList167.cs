using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    public class GetMaTouZhuangCheJiHuaDetailList167 : BaseServiceJZ
    {
        /// <summary>
        /// 获取码头装车计划数据列表
        /// </summary>
        /// <returns></returns>
        protected override string getSql()
        {
            return String.Format(SQL.GetMaTouZhuangCheJiHuaDetailList167,this.Params[0]);
        }
    }
}