﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    /// <summary>
    /// 获取首页进出港计划明细数据列表
    /// </summary>
    public class GetMainPageInoutPortPlanDetailList152 : BaseServiceBS
    {
        protected override string getSql()
        {
            return String.Format(SQL.GetMainPageInoutPortPlanDetailList152, this.Params[0]);
        }
    }
}