﻿using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.ZiXunService.LianYunGang
{
    /// <summary>
    /// 经济发展 连云港
    /// </summary>
    public class JingJiFaZhanLianYunGangService134 : BaseService
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.JingJiFaZhanLianYunGangService134, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}