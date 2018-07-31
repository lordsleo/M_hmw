using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServiceInterface.Common;
using YGSoft.IPort.Data;

namespace M_hmw.GetService
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var userId = Request.Params["userId"];
                var oldPassword = Request.Params["oldPassword"];
                var newPassword = Request.Params["newPassword"];

                if (userId == null || oldPassword == null || newPassword == null)
                {
                    Json = "参数userId，oldPassword，newPassword不能为空！";
                    return;
                }

                var sql = string.Format(
                         "select PASSWORD,CODE_USER from TB_SYS_USER t where t.CODE_USER ='{0}'", userId);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathIports).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    Json = "无此用户！";
                    return;
                }

                var row = dt.Rows[0];
                if (!Identity.VerifyText(oldPassword, row["PASSWORD"] as string))
                {
                    Json = "密码错误！";
                    return;
                }
                sql = string.Format(
                    "update TB_SYS_USER set DPBEGINTIME=null,DYNAMICPASSWORD=null,PASSWORD='{0}' where CODE_USER='{1}'",
                    Identity.EncodeText(newPassword), userId);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathIports).ExecuteTable(sql);

                Json = "修改成功！";
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ChangePassword), ex);
                Json = "Error";
            }
        }
        protected string Json;
    }
}