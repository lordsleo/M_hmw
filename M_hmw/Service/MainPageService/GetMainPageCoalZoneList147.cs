using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.MainPageService
{
    /// <summary>
    /// 获取首页煤炭专区数据列表
    /// </summary>
    public class GetMainPageCoalZoneList147 : BaseServiceLYG
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.GetMainPageCoalZoneList147, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}