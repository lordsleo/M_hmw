﻿using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.ChuanBoService.ZaSanChuanQi
{
    /// <summary>
    /// 杂散船期 欧洲
    /// </summary>
    public class ZaSanChuanQiOuZhouService113 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.ZaSanChuanQiOuZhouService113, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}