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
    public partial class More_Table_matouxieche : System.Web.UI.Page
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
                        "select count(trainplanid) as sum from JZHDD.vw_ty_trainplan  ");
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathJzhdd).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    var arry = 0;
                    Json = JsonConvert.SerializeObject(arry);
                    return;
                }

                sql =
                    string.Format(
                        "select trainplanid, case CODE_DEPARTMENT when '010111' then '东联公司' when '010113' then '东源公司' when '010116' then '东泰公司' when '010118' then '新东润公司' end as DEPARTMENT,DAYNIGHT,TRAINS,to_char(PLANDATE,'yyyy-MM-dd') as PLANDATE from(select * from (select * from vw_ty_trainplan  order by PLANDATE asc) where rownum<='{0}' order by PLANDATE desc) where rownum<='{1}'",
                        Convert.ToInt32(dt.Rows[0]["sum"]) - Convert.ToInt32(minRow) + 1, Convert.ToInt32(maxRow) - Convert.ToInt32(minRow) + 1);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathJzhdd).ExecuteTable(sql);
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
                LogTool.WriteLog(typeof(More_Table_matouxieche), ex);
            }
        }
        protected string Json;
    }
}