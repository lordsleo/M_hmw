using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    public class GetGuanHeGuoJiBoWeiChuanDetailList173 : BaseService
    {
        /// <summary>
        /// 获取灌河国际泊位船信息数据列表
        /// </summary>
        /// <returns></returns>
        protected override string getSql()
        {
            return String.Format(SQL.GetGuanHeGuoJiBoWeiChuanDetailList173, this.Params[0]);
        }
    }
}