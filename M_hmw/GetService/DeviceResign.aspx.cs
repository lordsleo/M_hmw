using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServiceInterface.Common;
using Leo;

namespace M_hmw.GetService
{
    public partial class DeviceResign : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var usercode = Request.Params["usercode"];
            var DeviceToken = Request.Params["deviceToken"];
            var DeviceType = Request.Params["DeviceType"];

            var sql = string.Format("select * from tb_hmw_devid where usercode='{0}'", usercode);
            var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
            if (dt.Rows.Count == 0)
            {
                sql = string.Format("insert into tb_hmw_devid (usercode,devicetoken,devicetype) values ('{0}','{1}','{2}')", usercode, DeviceToken, DeviceType);
            }
            else
            {
                sql = string.Format("update tb_hmw_devid set devicetoken='{0}' ,devicetype='{1}' where usercode='{2}'", DeviceToken, DeviceType, usercode);
            }
            dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
            sql = string.Format("select devicetoken from tb_hmw_devid where usercode='{0}'", usercode);
            dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
            if (dt.Rows[0]["devicetoken"].ToString() == DeviceToken)
            {
                Json = "True";
            }
            else
            {
                Json = "False";
            }
        }
        protected string Json;
    }
}