using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using ServiceInterface.Common;
using System.Data;
using Leo;
using M_hmw.Service.BaseUtil;

namespace M_hmw.GetService
{
    public partial class HmwUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var deviceType = Request.Params["deviceType"];
                var version = Request.Params["version"];

                if (deviceType == null || version == null)
                {
                    Json = "参数deviceType，version不能为空！";
                    return;
                }

                var sql = string.Format("select version,url from tb_app_update where devicetype='{0}' and appname='HMW'", deviceType);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    Json = "";
                    return;
                }

                if (dt.Rows[0]["version"].ToString() != version)
                {
                    Json = dt.Rows[0]["version"].ToString();
                }
                else
                {
                    Json = "yes";
                }

            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(DeviceBinding), ex);
                Json = "Error";
            }
        }
        protected string Json;
    }
}