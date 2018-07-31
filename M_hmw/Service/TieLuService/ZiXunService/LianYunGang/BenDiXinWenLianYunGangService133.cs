using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.ZiXunService.LianYunGang
{
    /// <summary>
    /// 本地新闻 连云港
    /// </summary>
    public class BenDiXinWenLianYunGangService133 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.BenDiXinWenLianYunGangService133, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}