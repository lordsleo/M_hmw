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
    public partial class Detail_News : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var message = Request.Params["message"];
                var str = message.Split('=');
                var str1 = str[1].Split(' ');
                var classId = str1[0];
                var newsId = str1[1];

                string sqlString = string.Format("select topic,source,to_char(POST_TIME,'yyyy-MM-dd HH24:mi:ss') as POST_TIME ,message from news_topic where  classid='{0}' and id = '{1}'", classId, newsId);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHmw).ExecuteTable(sqlString);
                var arry = new Leo.Data.Table(dt).ToArray();

                Json = Request.QueryString.Get("callback") + "(" + JsonConvert.SerializeObject(arry) + ")";
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(Detail_News), ex);
            }
        }
        protected string Json;
    }
}