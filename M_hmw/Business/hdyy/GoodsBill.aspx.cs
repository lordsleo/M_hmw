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
    public partial class GoodsBill : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var message = Request.Params["message"];
                var str = message.ToString().Split(' ');
                var companyCode = str[0];
                var userCode = str[1];
                var completeMark = str[2];
                var minRow = str[3];
                var maxRow = str[4];

                var sql =
                    string.Format(
                        "select CODE_CLIENT_HD from vw_ipt_user t where t.code_user={0}",userCode);
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
                        "select count(code_client) as sum from vw_qyf_goodsbillnet where code_department='{0}' and code_client='{1}' and complete_mark ='{2}'", companyCode, clientCode, completeMark);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    var arry = 0; 
                    Json = JsonConvert.SerializeObject(arry);
                    return;
                }
 
                sql =
                    string.Format(
                        "select Gbno, Cargo, Pack , Measure, Inout  , Trade  , ShipName , PieceWeight, to_char( createtime,'yy-MM-dd') , Mark , sign,COMPLETE_MARK from(select * from (select Gbno, Cargo, Pack , Measure, Inout  , Trade  , ShipName , PieceWeight, createtime , Mark , case sign_mark when '0' then '' when '1' then '√' end as sign,COMPLETE_MARK from vw_qyf_goodsbillnet where code_department='{0}' and code_client='{1}' and complete_mark ='{2}' order by createtime asc) where rownum<='{3}' order by createtime desc) where rownum<='{4}'",
                        companyCode, clientCode, completeMark, Convert.ToInt16(dt.Rows[0]["sum"]) - Convert.ToInt16(minRow) + 1, Convert.ToInt16(maxRow) - Convert.ToInt16(minRow) + 1);           
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    var arry  = 0;
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