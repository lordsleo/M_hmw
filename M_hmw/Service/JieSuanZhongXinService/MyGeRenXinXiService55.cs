using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.JieSuanZhongXinService
{
    /// <summary>
    /// 我的个人信息
    /// </summary>
    public class MyGeRenXinXiService55 : BaseServiceMC
    {
        protected override string getSql()
        {
            //0为服务传入参数用户ID
            //默认查询5条
            return String.Format(SQL.MyGeRenXinXiService55, this.Params[0],this.Params.Length > 1 ? this.Params[1] : "5");
        }
    }
}