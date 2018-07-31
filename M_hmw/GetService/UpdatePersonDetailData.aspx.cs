using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServiceInterface.Common;

namespace M_hmw.GetService
{
    public partial class UpdatePersonDetailData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var userId = Request.Params["userId"];
                var phone = Request.Params["phone"];
                var mobile = Request.Params["mobile"];
                var email = Request.Params["email"];

                if (userId == null)
                {
                    Json = "参数userId不能为空！";
                    return;
                }

                var sql = string.Format("update tb_sys_userinfo set phone='{1}',mobile='{2}',email='{3}' where code_user='{0}'", userId, phone, mobile, email, userId);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathIports).ExecuteTable(sql);

                sql = string.Format("select phone,mobile,email from tb_sys_userinfo where code_user='{0}'", userId);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathIports).ExecuteTable(sql);

                var aa =  Convert.ToString(dt.Rows[0]["phone"]);
                var bb = Convert.ToString(dt.Rows[0]["mobile"]);
                var cc = Convert.ToString(dt.Rows[0]["email"]);
                string str = "" + aa + bb + cc;
                string str1 = "" + phone + mobile + email;
                if (dt.Rows.Count == 0 || !str.Equals(str1))
                {
                    Json = "网络错误，请稍后再试";
                    return;
                }
   
                Json = "更新成功！";
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(UpdatePersonDetailData), ex);
                Json = "Error";
            }
        }
        protected string Json;
    }
}