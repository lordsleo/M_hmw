using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServiceInterface.Model;
using ServiceInterface.Common;
using System.Data;
using Leo;
using Newtonsoft.Json;

namespace M_hmw.Function
{
    public partial class wyts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string conplainContent = Request.Params["message"];
                string sql = string.Format("insert into tb_hmw_complain (opinion) values('{0}')", conplainContent);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
                sql = string.Format("select * from tb_hmw_complain where opinion='{0}'", conplainContent);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    Json = "flase";
                }
                else
                {
                    Json = "true";
                }

            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(wyts), ex);
            }
        }
        protected string Json;
    }
}