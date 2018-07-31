using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using ServiceInterface.Common;
using Leo;

namespace M_hmw.Business.hdyy
{
    public partial class NoShipBusinessConsign : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var message = Request.Params["message"];
                var str = message.ToString().Split(' ');
                var userCode = str[0];
                var auditMark = str[1];
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

                var mark_audit="";
                switch (auditMark)
                {
                    case "0": mark_audit = "N"; break;
                    case "1": mark_audit = "Y"; break;
                }

                sql =
                    string.Format(
                        "select count(depid) as sum from viewgoodscon where  depid='{0}' and constat='{1}' ", clientCode, mark_audit);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    var arry = 0; 
                    Json = JsonConvert.SerializeObject(arry);
                    return;
                }

                sql =
                    string.Format(
                        "select id, shipname,shiphc,zyxm, department1,qyg, ddg,constat,to_char( condate,'yy-MM-dd') from(select * from (select id, shipname,shiphc,zyxm, department1,qyg, ddg, case constat1 when 0 then '未批准' when 1 then '已批准' end as constat,condate from viewgoodscon where  depid='{0}' and constat='{1}' order by condate asc) where rownum<='{2}' order by condate desc) where rownum<='{3}'",
                            clientCode, mark_audit, Convert.ToInt16(dt.Rows[0]["sum"]) - Convert.ToInt16(minRow) + 1, Convert.ToInt16(maxRow) - Convert.ToInt16(minRow) + 1);
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
                LogTool.WriteLog(typeof(GoodsBill), ex);
            }
        }
        protected string Json;
    }
}