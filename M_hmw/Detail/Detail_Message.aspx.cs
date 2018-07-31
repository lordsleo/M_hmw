using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using ServiceInterface.Common;
using Leo;

namespace M_hmw.Detail
{
    public partial class Detail_Message : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var messId = Request.Params["messId"];
                string sqlString = string.Format("select substr(message, 1,30),to_char(time,'YYYY-MM-dd HH24:mi:ss'),message from tb_hmw_messagepush where messId={0}", messId);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sqlString);
                var arry = new Leo.Data.Table(dt).ToArray();         
                //Json = Request.QueryString.Get("callback") + "(" + JsonConvert.SerializeObject(arry) + ")";
                Json = JsonConvert.SerializeObject(arry);
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(Detail_Message), ex);
            }
        }
        protected string Json;
    }
}