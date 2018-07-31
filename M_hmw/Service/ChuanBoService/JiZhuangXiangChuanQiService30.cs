using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.ChuanBoService
{
    /// <summary>
    /// 集装箱船期
    /// </summary>
    public class JiZhuangXiangChuanQiService30 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.JiZhuangXiangChuanQiService30, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}