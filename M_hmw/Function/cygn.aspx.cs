using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ServiceInterface.Model;
using ServiceInterface.Common;
using System.Data;
using Leo;
using Newtonsoft.Json;

namespace M_hmw.Function
{
    public partial class cygn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var userCode = Request.Params["userCode"];
            //userCode = "111";
            var sql = string.Format("select * from TB_HMW_CYGN where userid='{0}'", userCode);
            var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
            if (dt.Rows.Count == 0)
            {
                string[] arry = new string[1];
                arry[0] = "false";
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
                Json = Request.QueryString.Get("callback") + "(" + JsonConvert.SerializeObject(arry) + ")";
                return;
            }

            sql = string.Format("select cygn from TB_HMW_CYGN where userid='{0}'",userCode);
            dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
            var arrys = new Leo.Data.Table(dt).ToArray();
            if (dt.Rows.Count == 0)
            {
                sql = string.Format("insert into tb_hmw_cygn (userid) values ('{0}')", userCode);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
            }
            Json = Request.QueryString.Get("callback") + "(" + JsonConvert.SerializeObject(arrys) + ")";
        }
        protected string Json;
    }
}