using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using ServiceInterface.Common;
using Leo;

namespace M_hmw.Business.cdyy
{
    public partial class CommunicationFee : System.Web.UI.Page
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

                var sql =
                    string.Format(
                        "select CODE_CLIENT_CD from vw_ipt_user t where t.code_user={0}", userCode);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    var arry = "Error";
                    Json = JsonConvert.SerializeObject(arry);
                    return;
                }
                var clientCode = dt.Rows[0]["CODE_CLIENT_CD"] as string;
                //clientCode = "9";
                sql =
                    string.Format(
                        "select count(agent) as sum from ddmis.viewtxgpinfo where agent = '{0}' ", clientCode);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    var arry = 0;
                    Json = JsonConvert.SerializeObject(arry);
                    return;
                }

               sql =
                    string.Format(
                        "select id,chi_vessel,to_char( sta_time,'yy-MM-dd'),end_time,fht,sht,fzs,money,INVOICENUM, name,fkdw from(select * from (select id,chi_vessel,sta_time,end_time,fht,sht,fzs,money,INVOICENUM, name,fkdw from ddmis.viewtxgpinfo where agent = '{0}'  order by sta_time asc) where rownum<='{1}' order by sta_time desc) where rownum<='{2}'",
                        clientCode, Convert.ToInt32(dt.Rows[0]["sum"]) - Convert.ToInt32(minRow) + 1, Convert.ToInt32(maxRow) - Convert.ToInt32(minRow) + 1);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
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
                LogTool.WriteLog(typeof(PilotageFee), ex);
            }
        }
        protected string Json;
    }
}