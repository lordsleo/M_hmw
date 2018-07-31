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
    public partial class AnchorShip : System.Web.UI.Page
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
                        "select  count(ship_id) as sum  from view_sship_tan where ship_statu=1");
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathBases).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    var arry = 0;
                    Json = JsonConvert.SerializeObject(arry);
                    return;
                }

                sql =
                    string.Format(
                        "select ship_id,chi_vessel,nation_cha,loa,trade,THIS_DRAFT,CHU_DRAFT,zhmc,zhsl,xhmc,xhsl,to_char( yjdg,'yy-MM-dd'),name,ys from(select * from (select ship_id,chi_vessel,nation_cha,loa,trade,THIS_DRAFT,CHU_DRAFT,zhmc,zhsl,xhmc,xhsl,yjdg,name,ys from view_sship_tan where ship_statu=1  order by yjdg asc) where rownum<='{0}' order by yjdg desc) where rownum<='{1}'",
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
                LogTool.WriteLog(typeof(ForeShip), ex);
            }
        }
        protected string Json;
    }
}