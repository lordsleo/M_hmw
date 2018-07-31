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
    public partial class NoPassedTransportation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var message = Request.Params["message"];
                var str = message.ToString().Split(' ');
                var userCode = str[0];
                var taskNo = str[1];
                var minRow = str[2];
                var maxRow = str[3];

                var sql =
                    string.Format(
                        "select CODE_COMPANY from vw_ipt_user t where t.code_user={0}", userCode);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    var arry = "Error";
                    Json = JsonConvert.SerializeObject(arry);
                    return;
                }
                var companyCode = dt.Rows[0]["CODE_COMPANY"] as string;

                sql =
                    string.Format(
                        "select count(id) as sum  from V_CONSIGN_VEHICLE_XLQ_PASS where CODE_DEPARTMENT = '{0}' and CGNO like '%{1}%' and SUBMIT_MARK='1' and AUDIT_MARK='0' ", companyCode, taskNo);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    var arry = 0; 
                    Json = JsonConvert.SerializeObject(arry);
                    return;
                }

                sql =
                    string.Format(
                        "select ID,TASKNO,CARGO,VEHICLE,DRIVERNAME,DRIVERPHONE,VESSEL,PREPTIME,SUBMITERNAME,SUBMITTIME,to_char(AUDITTIME,'yy-mm-dd') from(select * from (select  ID,TASKNO,CARGO,VEHICLE,DRIVERNAME,DRIVERPHONE,VESSEL,PREPTIME,SUBMITERNAME,SUBMITTIME,AUDITTIME  from V_CONSIGN_VEHICLE_XLQ_PASS where CODE_DEPARTMENT = '{0}' and CGNO like '%{1}%' and SUBMIT_MARK='1' and AUDIT_MARK='0' order by AUDITTIME asc) where rownum<='{2}' order by AUDITTIME desc) where rownum<='{3}'",
                        companyCode, taskNo, Convert.ToInt32(dt.Rows[0]["sum"]) - Convert.ToInt32(minRow) + 1, Convert.ToInt32(maxRow) - Convert.ToInt32(minRow) + 1);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
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