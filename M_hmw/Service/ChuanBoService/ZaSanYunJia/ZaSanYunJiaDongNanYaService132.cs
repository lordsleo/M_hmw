﻿using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.ChuanBoService.ZaSanYunJia
{
    /// <summary>
    /// 杂散运价 东南亚
    /// </summary>
    public class ZaSanYunJiaDongNanYaService132 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.ZaSanYunJiaDongNanYaService132, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}