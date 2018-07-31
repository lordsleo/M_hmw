using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using ServiceInterface.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Detail
{
    public partial class More_Table_boweichuanbo : System.Web.UI.Page
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
                //var minRow = "1";
                //var maxRow = "15";

                var sql =
                    string.Format(
                        "select CODE_CLIENT_HD,CODE_CLIENT_CD,CODE_DEPARTMENT from VW_IPT_USER where code_user ={0}", userCode);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    var arry = "Error";
                    Json = JsonConvert.SerializeObject(arry);
                    return;
                }
                var Code_CD = dt.Rows[0]["CODE_CLIENT_CD"] as string;
                //Code_CD = "9";

                sql =
                    string.Format(
                        "select  count(ship_id) as sum from view_sship_tan where agent = '{0}' and ship_statu=2 or ship_statu=4 or ship_statu=5 or ship_statu=3 or ship_statu=6", Code_CD);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathBases).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    var arry = 0;
                    Json = JsonConvert.SerializeObject(arry);
                    return;
                }

                sql =
                    string.Format(
                        "select ship_id,name,chi_vessel,BERTHNO,BERTH_POSITION,DepartMent,SHIPSTATUS,dqyd,BerTH_time from(select * from (select ship_id,name,chi_vessel,BERTHNO,BERTH_POSITION,DepartMent,SHIPSTATUS,dqyd,BerTH_time from view_sship_tan where agent = '{0}' and ship_statu=2 or ship_statu=4 or ship_statu=5 or ship_statu=3 or ship_statu=6 order by BerTH_time asc) where rownum<='{1}' order by BerTH_time desc) where rownum<='{2}'",
                        Code_CD, Convert.ToInt32(dt.Rows[0]["sum"]) - Convert.ToInt32(minRow) + 1, Convert.ToInt32(maxRow) - Convert.ToInt32(minRow) + 1);
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
                LogTool.WriteLog(typeof(More_Table_boweichuanbo), ex);
            }
        }
        protected string Json;
    }
}