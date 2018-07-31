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
    public partial class More_Table_daichuhuoyuan : System.Web.UI.Page
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
                        "select count(id) as sum from dchy ");
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHmw).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    var arry = 0;
                    Json = JsonConvert.SerializeObject(arry);
                    return;
                }

                sql =
                    string.Format(
                        "select ID,HWMC,LXR,LXFS,to_char(JZSJ,'yyyy-MM-dd') as JZSJ from(select * from (select ID,HWMC,LXR,LXFS,JZSJ from dchy where sh=1 order by JZSJ asc) where rownum<='{0}' order by JZSJ desc) where rownum<='{1}'",
                        Convert.ToInt32(dt.Rows[0]["sum"]) - Convert.ToInt32(minRow) + 1, Convert.ToInt32(maxRow) - Convert.ToInt32(minRow) + 1);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHmw).ExecuteTable(sql);
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
                LogTool.WriteLog(typeof(More_Table_daichuhuoyuan), ex);
            }
        }
        protected string Json;
    }
}