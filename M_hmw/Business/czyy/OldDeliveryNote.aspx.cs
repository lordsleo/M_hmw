using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M_hmw.Business.czyy
{
    public partial class OldDeliveryNote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var tel = Request["tel"];
            var sql = string.Format(
                         "select RID,taskno,RELATE_NO from V_CONSIGN_VEHICLE_QUICK where driverphone='{0}'  and rownum<=10", tel);
            var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
            var array = new Leo.Data.Table(dt).ToArray();
            Json = Leo.Json.JsonConvert.SerializeObject(array);
        }

        protected string Json;
    }
}