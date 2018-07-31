using Newtonsoft.Json;
using ServiceInterface.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Leo;

namespace M_hmw.Detail
{
    public partial class Detail_News2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var newsId = Request.Params["newsId"];
                string sqlString = string.Format("Select TOPIC,SOURCE,POST_TIME,MESSAGE MESSAGE From hmw.NEWS_TOPIC Where id= {0}",newsId);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sqlString);
                var arry = new Leo.Data.Table(dt).ToArray();

                //Json = Request.QueryString.Get("callback") + "(" + JsonConvert.SerializeObject(arry) + ")";
                Json = JsonConvert.SerializeObject(arry);  
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(Detail_News2), ex);
            }
        }
        protected string Json;
    }
}