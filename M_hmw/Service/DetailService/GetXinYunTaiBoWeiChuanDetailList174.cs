using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    public class GetXinYunTaiBoWeiChuanDetailList174 : BaseService
    {
        /// <summary>
        /// 获取新云台泊位船信息数据列表
        /// </summary>
        /// <returns></returns>
        protected override string getSql()
        {
            return String.Format(SQL.GetXinYunTaiBoWeiChuanDetailList174, this.Params[0]);
        }
    }
}