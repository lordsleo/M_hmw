﻿using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.TieLuService
{
    /// <summary>
    /// 码头装车计划
    /// </summary>
    public class MaTouZhuangCheJiHuaService39 : BaseServiceJZ
    {
        protected override string getSql()
        {
            //默认查询5条
            return String.Format(SQL.MaTouZhuangCheJiHuaService39, this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}