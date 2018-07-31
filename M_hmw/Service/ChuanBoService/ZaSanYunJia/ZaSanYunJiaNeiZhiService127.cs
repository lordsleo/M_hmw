using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.ChuanBoService.ZaSanYunJia
{
    /// <summary>
    /// 杂散运价 内支
    /// </summary>
    public class ZaSanYunJiaNeiZhiService127 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.ZaSanYunJiaNeiZhiService127, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}