﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.BaseUtil
{
    /// <summary>
    /// 服务基类，身份账号为“JZHDD”
    /// </summary>
    public abstract class BaseServiceJZ : BaseService
    {
        /// <summary>
        /// 重写数据库身份
        /// </summary>
        /// <returns>返回身份配置“JZHDD”</returns>
        protected override string getDataBaseIdentity()
        {
            return "JZHDD";
        }
    }
}