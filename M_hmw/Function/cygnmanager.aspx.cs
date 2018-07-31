using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Leo;
using ServiceInterface.Common;

namespace M_hmw.Function
{
    public partial class cygnmanager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var userCode = Request.Params["userCode"];
            var cygnlist = Request.Params["cygnlist"];
            //userCode = "111";
            //cygnlist = "0101010101010";
            var sql = string.Format("update TB_HMW_CYGN set cygn='{0}' where userid='{1}'",cygnlist, userCode);
            var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
            Json = Request.QueryString.Get("callback") + "(true)";
        }
        protected string Json;
    }
}