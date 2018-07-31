using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.ZiXunService
{
    /// <summary>
    /// 今日头条
    /// </summary>
    public class JinRiTouTiaoService40 : BaseService
    {
        protected override string getSql()
        {
            //默认查询1条
            return String.Format(SQL.JinRiTouTiaoService40);
        }
    }
}