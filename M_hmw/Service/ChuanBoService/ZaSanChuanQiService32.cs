using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.ChuanBoService
{
    /// <summary>
    /// 散杂船期
    /// </summary>
    public class ZaSanChuanQiService32 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.ZaSanChuanQiService32, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}