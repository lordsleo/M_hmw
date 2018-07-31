﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    /// <summary>
    /// 获取首页矿石专区明细数据列表
    /// </summary>
    public class GetMainPageOreZoneDetailList155 : BaseServiceWL
    {
        protected override string getSql()
        {
            return String.Format(SQL.GetMainPageOreZoneDetailList155, this.Params[0]);
        }
    }
}