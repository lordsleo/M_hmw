﻿using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.MyHuoService
{
    /// <summary>
    /// 我的业务委托有船作业
    /// </summary>
    public class MyYeWuWeiTuoYouChuanService47 : BaseServiceH
    {
        protected override string getSql()
        {
            //0为传入参数船代客户ID
            //默认查询5条
            return String.Format(SQL.MyYeWuWeiTuoYouChuanService47, this.Params[0], this.Params.Length > 1 ? this.Params[1] : "5");
        }
    }
}