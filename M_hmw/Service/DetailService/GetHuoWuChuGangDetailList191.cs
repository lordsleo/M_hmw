﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    /// <summary>
    /// 获取货物出港明细数据列表
    /// </summary>
    public class GetHuoWuChuGangDetailList191 : BaseServiceH
    {
        protected override string getSql()
        {
            return String.Format(SQL.GetHuoWuChuGangDetailList191, this.Params[0]);
        }
    }
}