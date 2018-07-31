using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServiceInterface.Common;

namespace M_hmw.GetService
{
    public partial class DeviceBinding : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var deviceToken = Request.Params["devicetoken"];
                var deviceType = Request.Params["devicetype"];
                var isbinding = Request.Params["isbinding"] == "yes" ? "1" : "0";
                var usercode = Request.Params["usercode"];
                var message = Request.Params["message"];

                if (deviceToken == null || deviceType == null || usercode == null)
                {
                    Json = "参数deviceToken，deviceType，usercode不能为空！";
                    return;
                }

                var sql = string.Format("select * from tb_hmw_devid where usercode='{0}'", usercode);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    sql = string.Format("insert into tb_hmw_devid (usercode,devicetoken,devicetype,devicebinding) values ('{0}','{1}','{2}','{3}')", usercode, deviceToken, deviceType,isbinding);
                }
                else
                {
                    if (dt.Rows[0]["devicebinding"].ToString() == "1" && isbinding=="1")
                    {
                        Json = "当前用户已被绑定！";
                        return;
                    }
                    else
                    {
                        sql = string.Format("update tb_hmw_devid set devicetoken='{0}',devicetype='{1}',devicebinding='{2}' where usercode='{3}'", deviceToken, deviceType, isbinding, usercode);
                    }           
                }
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
                sql = string.Format("select devicetoken,devicebinding from tb_hmw_devid where usercode='{0}'", usercode);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    Json = "网络错误，请稍后再试！";
                    return;
                }
                if (dt.Rows[0]["devicetoken"].ToString() == deviceToken)
                {
                    if (dt.Rows[0]["devicebinding"].ToString() == isbinding)
                    {
                        switch (isbinding)
                        {
                            case "0": Json = "解绑成功！"; break;
                            case "1": Json = "绑定成功！"; break;
                            default: Json = "网络错误，请稍后再试！"; break;
                        }
                    }
                }

                if (message == "DATA") 
                {
                    Json = Json + "," + deviceToken + "," + deviceType + "," + isbinding + "," + usercode;
                }
                
            }
            catch(Exception ex)
            {
                LogTool.WriteLog(typeof(DeviceBinding), ex);
                Json = "Error";
            }
        }
        protected string Json;
    }
}