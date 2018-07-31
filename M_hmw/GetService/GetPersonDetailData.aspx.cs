using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServiceInterface.Common;

namespace M_hmw.GetService
{
    public partial class GetPersonDetailData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var userId = Request.Params["userId"];

                if (userId == null)
                {
                    Json = " 为空！";
                    return;
                }

                var sql = string.Format("select PHONE,MOBILE,EMAIL from tb_sys_userinfo t where t.CODE_USER='{0}'", userId);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathIports).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    Json = 0 + "," + 0 + "," + 0 ;
                    return;
                }
                else
                {
                    Json = dt.Rows[0]["PHONE"].ToString() + "," + dt.Rows[0]["MOBILE"].ToString() + "," + dt.Rows[0]["EMAIL"].ToString();
                }
                
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(GetPersonDetailData), ex);
                Json = "Error";
            }
        }
        protected string Json;
    }
}