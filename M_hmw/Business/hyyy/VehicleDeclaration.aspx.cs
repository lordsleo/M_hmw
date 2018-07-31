using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using ServiceInterface.Common;
using Leo;

namespace M_hmw.Business.hyyy
{
    public partial class VehicleDeclaration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var message = Request.Params["message"];
                var str = message.ToString().Split(' ');
                var userCode = str[0];
                var Vehicle = str[1];
                var minRow = str[2];
                var maxRow = str[3];

                var sql =
                    string.Format(
                        "select count(id) as sum from Transit.s_vehicle_web where VEHICLE like '%{0}%' and MARK_AUDIT=0", Vehicle);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathBases).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    var arry = 0; 
                    Json = JsonConvert.SerializeObject(arry);
                    return;
                }

                sql =
                    string.Format(
                        "select id,VEHICLE,VEH_CLASS_TYPE,VEH_TYPE,USE_KIND,BRAND_CODE,SELF_WEIGHT,LOAD_WEIGHT,VEH_CARD,OWNERNAME,TEL,INSPECT_PERIOD,to_char( DECLARETIME,'yy-MM-dd')  from(select * from (select id,VEHICLE,VEH_CLASS_TYPE,VEH_TYPE,USE_KIND,BRAND_CODE,SELF_WEIGHT,LOAD_WEIGHT,VEH_CARD,OWNERNAME,TEL,INSPECT_PERIOD,DECLARETIME  from Transit.s_vehicle_web where VEHICLE like '%{0}%' and MARK_AUDIT=0 order by DECLARETIME asc) where rownum<='{1}' order by DECLARETIME desc) where rownum<='{2}'",
                        Vehicle,Convert.ToInt16(dt.Rows[0]["sum"]) - Convert.ToInt16(minRow) + 1, Convert.ToInt16(maxRow) - Convert.ToInt16(minRow) + 1);
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
                LogTool.WriteLog(typeof(VehicleDeclaration), ex);
            }
        }
        protected string Json;
    }
}