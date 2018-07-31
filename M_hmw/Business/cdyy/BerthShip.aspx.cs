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
    public partial class BerthShip : System.Web.UI.Page
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
                            "select count(ship_id) as sum from view_sship_tan where ship_statu=2 or ship_statu=3 or ship_statu=4 or ship_statu=5 or ship_statu=6");
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathBases).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    var arry = 0;
                    Json = JsonConvert.SerializeObject(arry);
                    return;
                }

                sql =
                    string.Format(
                        "select ship_id,chi_vessel,BERTHNO,BERTH_POSITION,zhmc,zhsl,xhmc,xhsl,to_char( BerTH_time,'yy-MM-dd'),dqyd,SHIPSTATUS,name,DepartMent from(select * from (select ship_id,chi_vessel,BERTHNO,BERTH_POSITION,zhmc,zhsl,xhmc,xhsl,BerTH_time,dqyd,SHIPSTATUS,name,DepartMent from view_sship_tan where ship_statu=2 or ship_statu=3 or ship_statu=4 or ship_statu=5 or ship_statu=6   order by BerTH_time) where rownum<='{0}' order by BerTH_time desc) where rownum<='{1}'",
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
                LogTool.WriteLog(typeof(BerthShip), ex);
            }
        }
        protected string Json;
    }
}