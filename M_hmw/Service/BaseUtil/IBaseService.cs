using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace M_hmw.Service.BaseUtil
{
    /// <summary>
    /// 服务接口
    /// </summary>
    public interface IBaseService
    {
        /// <summary>
        /// 执行服务操作
        /// </summary>
        /// <returns>返回XML结果</returns>
        String execute();

        /// <summary>
        /// 执行服务操作
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        DataTable executeForDataTable();

        /// <summary>
        /// 设置服务传入参数
        /// </summary>
        /// <param name="param">参数数组</param>
        void setParams(String[] param);
    }
}