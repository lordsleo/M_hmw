using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.ZiXunService.ZhaoShangYinZi
{
    /// <summary>
    /// 招商引资 兰州
    /// </summary>
    public class ZhaoShangYinZiLanZhouService87 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.ZhaoShangYinZiLanZhouService87, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}