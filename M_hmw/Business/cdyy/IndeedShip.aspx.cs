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
    public partial class IndeedShip : System.Web.UI.Page
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
                        "select count(ship_id) as sum  from view_sship_tan where agent='{0}' and SHIP_STATU = 7", clientCode);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathBases).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    var arry = 0;
                    Json = JsonConvert.SerializeObject(arry);
                    return;
                }

                sql =
                    string.Format(
                        "select ship_id,chi_vessel,to_char( yjdg,'yy-MM-dd'),zhmc,zhsl,xhmc,xhsl,S_declare,shipstatus,ys from(select * from (select ship_id,chi_vessel,yjdg,zhmc,zhsl,xhmc,xhsl,S_declare,shipstatus,ys from view_sship_tan where  agent='{0}' and (SHIP_STATU = 7) order by S_declare asc) where rownum<='{1}' order by S_declare desc) where rownum<='{2}'",
                        clientCode, Convert.ToInt16(dt.Rows[0]["sum"]) - Convert.ToInt16(minRow) + 1, Convert.ToInt16(maxRow) - Convert.ToInt16(minRow) + 1);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathBases).ExecuteTable(sql);
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
                LogTool.WriteLog(typeof(ForeShip), ex);
            }
        }
        protected string Json;
    }
}