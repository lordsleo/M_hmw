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
    public partial class PilotageFee : System.Web.UI.Page
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
                        "select count(viewworkbill.agent) as sum from inhual.viewworkbill,inhual.invoice  where viewworkbill.sid=invoice.sid and viewworkbill.agent = '{0}'", clientCode);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    var arry = 0; ;
                    Json = JsonConvert.SerializeObject(arry);
                    return;
                }

                sql =
                    string.Format(
                        "select  invoicenum,chi_vessel,ton_net,fcode,amount,to_char( invoicedate,'yy-MM-dd'),fk,fkdjh,fkdw from(select * from (select viewworkbill.CHI_VESSEL,viewworkbill.FCODE,invoice.invoicenum,ton_net,amount,invoicedate,invoice.fkdw,invoice.sid,case when invoice.fk='0' then '否' when invoice.fk='1' then '是' end as fk,case when invoice.fkdjh='00000' then  '  ' else invoice.fkdjh end as fkdjh from inhual.viewworkbill,inhual.invoice  where viewworkbill.sid=invoice.sid and viewworkbill.agent = '{0}' order by invoicedate ASC ) where rownum<='{1}' order by invoicedate desc) where rownum<='{2}'",
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