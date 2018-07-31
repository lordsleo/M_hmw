﻿using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.ZiXunService.DiFangFengQing
{
    /// <summary>
    /// 地方风情 银川
    /// </summary>
    public class DiFangFengQingYinChuanService99 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.DiFangFengQingYinChuanService99, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}