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
    public partial class ShipBusinessConsign : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var message = Request.Params["message"];
                var str = message.ToString().Split(' ');
                var userCode = str[0];
                var mark_audit = str[1];
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
                        "select count(code_client) as sum from harbor.vw_bc_bconsign where  code_client='{0}' and mark_audit ='{1}'", clientCode, mark_audit);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    var arry = 0; 
                    Json = JsonConvert.SerializeObject(arry);
                    return;
                }


                sql =
                   string.Format(
                       "select bcno,chi_vessel,voyage,jobtype,department,from_port,to_port, mark_audit, to_char( createtime,'yy-MM-dd') from(select * from (select bcno,chi_vessel,voyage,jobtype,department,from_port,to_port,case mark_audit when '1' then '已审核' when '0' then '未审核' end as mark_audit, createtime  from harbor.vw_bc_bconsign where code_client='{0}'  and mark_audit = '{1}' order by createtime asc) where rownum<='{2}' order by createtime desc) where rownum<='{3}'",
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