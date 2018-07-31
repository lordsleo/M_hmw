using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using ServiceInterface.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Detail
{
    public partial class More_Table_yifuzhuangdan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var message = Request.Params["message"];
                var str = message.ToString().Split(' ');
                var userCode = str[0];
                var minRow = str[1];
                var maxRow = str[2];
                //var minRow = "1";
                //var maxRow = "15";

                var sql =
                    string.Format(
                        "select CODE_CLIENT_HD,CODE_CLIENT_CD,CODE_DEPARTMENT from VW_IPT_USER where code_user ={0}", userCode);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    var arry = "Error";
                    Json = JsonConvert.SerializeObject(arry);
                    return;
                }
                var Code_D = dt.Rows[0]["CODE_DEPARTMENT"] as string;

                sql =
                    string.Format(
                        "select count(RBNO) as sum from jszx_fp t where CODE_CLIENT='{0}' ", Code_D);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHmw).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    var arry = 0;
                    Json = JsonConvert.SerializeObject(arry);
                    return;
                }

                sql =
                    string.Format(
                        "SELECT RBNO,to_char(CREATETIME,'yyyy-mm-dd') as CREATETIME,FYLX,RBDISPLAY,TOTAL from(select * from (select RBNO,CREATETIME,FYLX,RBDISPLAY,TOTAL from jszx_fp t where CODE_CLIENT='{0}'  order by CREATETIME asc) where rownum<='{1}' order by CREATETIME desc) where rownum<='{2}'",
                        Code_D, Convert.ToInt32(dt.Rows[0]["sum"]) - Convert.ToInt32(minRow) + 1, Convert.ToInt32(maxRow) - Convert.ToInt32(minRow) + 1);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHmw).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    var arry = 0;
                    Json = JsonConvert.SerializeObject(arry);
                    return;
                }
                var arrys = new Leo.Data.Table(dt).ToArray();
                Json = JsonConvert.SerializeObject(arrys);
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(More_Table_boweichuanbo), ex);
            }
        }
        protected string Json;
    }
}