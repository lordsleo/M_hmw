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
    public partial class CargoStock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var message = Request.Params["message"];
                var str = message.ToString().Split(' ');
                //var str1 = str[1].ToString().Split(' ');
                var companyCode = str[0];
                var userCode = str[1];
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
                        "select count(code_client) as sum from vw_stockdormant_web where code_department='{0}' and code_client='{1}' ", companyCode, clientCode);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    var arry = 0; 
                    Json = JsonConvert.SerializeObject(arry);
                    return;
                }

                sql =
                    string.Format(
                        "select id,department,section,inout,trade,storage,cargo,amount,weight,volume,to_char( signdate,'yy-MM-dd') from(select * from (select id,department,section,inout,trade,storage,cargo,amount,weight,volume,signdate from vw_stockdormant_web where code_department='{0}' and code_client='{1}'  order by signdate asc) where rownum<='{2}' order by signdate desc) where rownum<='{3}'",
                        companyCode, clientCode, Convert.ToInt16(dt.Rows[0]["sum"]) - Convert.ToInt16(minRow) + 1, Convert.ToInt16(maxRow) - Convert.ToInt16(minRow) + 1);
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