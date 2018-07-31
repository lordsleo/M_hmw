using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.ZiXunService.ZhaoShangYinZi
{
    /// <summary>
    /// 招商引资 新疆
    /// </summary>
    public class ZhaoShangYinZiXinJiangService90 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.ZhaoShangYinZiXinJiangService90, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}