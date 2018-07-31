using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using ServiceInterface.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Home.Detail
{
    public partial class More_Table_zuixinchuanqi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var message = Request.Params["message"];
                var str = message.ToString().Split(' ');
                var minRow = str[0];
                var maxRow = str[1];
                //var minRow = "1";
                //var maxRow = "15";

                var sql =
                    string.Format(
                        "select count(a.id) as sum from hmw.ship_entire_box_time a,hmw.user_group b where a.USERID=b.id and a.sh=1 ");
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathBases).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    var arry = 0;
                    Json = JsonConvert.SerializeObject(arry);
                    return;
                }

                sql =
                    string.Format(
                        "select ID,S_S_PORT,S_E_PORT,S_S_NAME,S_CARRYING,S_STAR_TIME from(select * from (select a.S_S_PORT,a.S_E_PORT,a.S_STAR_TIME,b.DESCRIPT,a.id,a.S_SHIPLINE,a.S_CARRYING,a.S_S_NAME from hmw.ship_entire_box_time a,hmw.user_group b where a.USERID=b.id and a.sh=1 order by S_STAR_TIME asc) where rownum<='{0}' order by S_STAR_TIME desc) where rownum<='{1}'",
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
                LogTool.WriteLog(typeof(More_Table_zuixinchuanqi), ex);
            }
        }
        protected string Json;
    }
}