﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    public class GetWoDeYuBaoChuanDetailList196 : BaseServiceBS
    {
        protected override string getSql()
        {
            return String.Format(SQL.GetWoDeYuBaoChuanDetailList196, this.Params[0]);
        }
    }
}