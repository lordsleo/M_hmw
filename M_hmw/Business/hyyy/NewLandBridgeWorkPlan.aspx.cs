using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using ServiceInterface.Common;
using Leo;

namespace M_hmw.Business.hyyy
{
    public partial class NewLandBridgeWorkPlan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var message = Request.Params["message"];
                var str = message.ToString().Split(' ');
                var userCode = str[0];
                var completeMark = str[1];
                var minRow = str[2];
                var maxRow = str[3];

                var sql =
                    string.Format(
                        "select CODE_CLIENT_HD from vw_ipt_user t where t.code_user={0}", userCode);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    var arry = "Error";
                    Json = JsonConvert.SerializeObject(arry);
                    return;
                }
                var clientCode = dt.Rows[0]["CODE_CLIENT_HD"] as string;

                sql =
                    string.Format(
                        "select count(CODE_AGENT) as sum from v_consign_plan_xlq where AUDIT_MARK='{0}' and CODE_AGENT='{1}'", completeMark, clientCode);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    var arry = 0; 
                    Json = JsonConvert.SerializeObject(arry);
                    return;
                }

                sql =
                    string.Format(
                        "select plan_no,taskno,GBNO,gbdisplay,plan_amount,PLAN_WEIGHT,PLAN_VEH_NUM,fact_VEH_NUM,balance1,balance2,CLIENT,username,auditorname,CODE_STORAGE,CODE_BOOTH,to_char( OPERTIME,'yy-MM-dd') from(select * from (select plan_no,taskno,GBNO,gbdisplay,plan_amount,PLAN_WEIGHT,PLAN_VEH_NUM,fact_VEH_NUM,balance1,balance2,CLIENT,username,auditorname,CODE_STORAGE,CODE_BOOTH,OPERTIME from v_consign_plan_xlq where AUDIT_MARK='{0}' and CODE_AGENT='{1}' order by OPERTIME asc) where rownum<='{2}' order by OPERTIME desc) where rownum<='{3}'",
                        completeMark, clientCode, Convert.ToInt16(dt.Rows[0]["sum"]) - Convert.ToInt16(minRow) + 1, Convert.ToInt16(maxRow) - Convert.ToInt16(minRow) + 1);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
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
                LogTool.WriteLog(typeof(NewLandBridgeWorkPlan), ex);
            }
        }
        protected string Json;
    }
}