using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.MainPageService
{
    /// <summary>
    /// 获取首页矿石专区数据列表
    /// </summary>
    public class GetMainPageOreZoneList146 : BaseServiceWL
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.GetMainPageOreZoneList146, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}