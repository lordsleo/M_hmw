using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.JieSuanZhongXinService
{
    /// <summary>
    /// 我的已付账单
    /// </summary>
    public class MyYiFuZhangDanService56 : BaseService
    {
        protected override string getSql()
        {
            //0为服务传入参数公司ID
            //默认查询5条
            return String.Format(SQL.MyYiFuZhangDanService56, this.Params[0], this.Params.Length > 1 ? this.Params[1] : "5");
        }
    }
}