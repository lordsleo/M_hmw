﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    /// <summary>
    /// 获取首页最新货盘明细数据列表
    /// </summary>
    public class GetMainPageLatestPalletDetailList154 : BaseService
    {
        protected override string getSql()
        {
            return String.Format(SQL.GetMainPageLatestPalletDetailList154, this.Params[0]);
        }
    }
}