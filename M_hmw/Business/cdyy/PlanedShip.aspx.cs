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
    public partial class PlanedShip : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var message = Request.Params["message"];
                var str = message.ToString().Split(' ');
                var minRow = str[0];
                var maxRow = str[1];

                var sql =
                    string.Format(
                        "select count(ship_id) as sum from ywcplan");
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathBases).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    var arry = "None";
                    Json = JsonConvert.SerializeObject(arry);
                    return;
                }

                sql =
                    string.Format(
                        "select p_id,dqbw,bwwz,chi_vessel,xhm,xhsl,zhm,zhsl,yjjg,yjlg,remark,pc,cbdl,to_char( plandate,'yy-MM-dd') from(select * from (select p_id,dqbw,bwwz,chi_vessel,xhm,xhsl,zhm,zhsl,yjjg,yjlg,remark,pc,cbdl,plandate from ywcplan order by plandate asc) where rownum<='{0}' order by plandate desc) where rownum<='{1}'",
                        Convert.ToInt16(dt.Rows[0]["sum"]) - Convert.ToInt16(minRow) + 1, Convert.ToInt16(maxRow) - Convert.ToInt16(minRow) + 1);
               
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
                LogTool.WriteLog(typeof(PlanedShip), ex);
            }
        }
        protected string Json;
    }

}