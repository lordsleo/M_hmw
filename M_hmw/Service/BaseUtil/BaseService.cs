using CWK.WebService.Util.UtilCollection.BaseConfig;
using CWK.WebService.Util.UtilCollection.DataBase;
using CWK.WebService.Util.UtilCollection.DataHandle;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace M_hmw.Service.BaseUtil
{
    /// <summary>
    /// 服务基类,身份账号为“hmw”
    /// </summary>
    public abstract class BaseService : IBaseService
    {
        #region 成员属性
        /// <summary>
        /// 服务传入参数集合
        /// </summary>
        private String[] param = new String[0];

        /// <summary>
        /// 服务传入参数集合
        /// </summary>
        protected String[] Params
        {
            get { return param; }
            private set { param = value; }
        }
        #endregion

        #region 实现接口

        #region 执行服务

        #region 获取XML结果
        public virtual String execute()
        {
            try
            {
                //读取配置
                DataBaseConfig config = Config.getConfig.dataBaseConfig[getDataBaseName()];
                Identity identity = config[getDataBaseIdentity()];

                //获取数据库控制器
                DataBaseClient db = DataBaseManger.getDataClient(identity);

                //语句
                String sql = getSql();

                //数据表
                DataTable table = db.QueryForTable(sql);

                //装配
                return getWebServiceDataXML(table);
            }
            catch (Exception e)
            {
                return getErrorWebServiceDataXML(e.Message);
            }
        }
        #endregion

        #region 获取DataTable结果
        public DataTable executeForDataTable()
        {
            try
            {
                //读取配置
                DataBaseConfig config = Config.getConfig.dataBaseConfig[getDataBaseName()];
                Identity identity = config[getDataBaseIdentity()];

                //获取数据库控制器
                DataBaseClient db = DataBaseManger.getDataClient(identity);

                //语句
                String sql = getSql();

                //返回数据表
                return db.QueryForTable(sql);
            }
            catch (Exception)
            {
                //异常返回null
                return null;
            }
        }
        #endregion

        #endregion

        #region 设置参数
        public void setParams(string[] param)
        {
            Params = param;
        }
        #endregion

        #endregion

        #region 数据装配
        #region 设置数据库名
        /// <summary>
        /// 获取数据库配置别名
        /// </summary>
        /// <returns>返回别名字符串</returns>
        protected virtual String getDataBaseName()
        {
            return "PORT";
        }

        /// <summary>
        /// 获取数据库身份别名
        /// </summary>
        /// <returns>返回别名字符串</returns>
        protected virtual String getDataBaseIdentity()
        {
            return "hmw";
        }
        #endregion

        #region 服务SQL语句
        /// <summary>
        /// 获取本服务的Sql语句，需要重写
        /// </summary>
        /// <returns>返回Sql语句</returns>
        protected abstract String getSql();
        #endregion

        #region 序列化对象
        /// <summary>
        /// 处理并装配数据，返回序列化字符串
        /// </summary>
        /// <param name="table">数据表</param>
        /// <returns>返回XML字符串</returns>
        protected virtual String getWebServiceDataXML(DataTable table)
        {
            //装配
            return new WebServiceDataXML
            {
                Success = true,
                Message = String.Empty,
                SourceType = DataSourceType.DataOnlyDataTable,
                ListName="Table",   
                RowName ="Row",
                DataDataTable = table

            }.ToXmlString();
        }

        /// <summary>
        /// 获取异常时的序列化字符串
        /// </summary>
        /// <returns>返回XML字符串</returns>
        protected virtual String getErrorWebServiceDataXML(String message)
        {
            //装配
            return new WebServiceDataXML
            {
                Success = false,
                Message = message,
                SourceType = DataSourceType.NoneData,
                ListName = "Table",
                RowName = "Row"

            }.ToXmlString();
        }
        #endregion
        #endregion

    }
}