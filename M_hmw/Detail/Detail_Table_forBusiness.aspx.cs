using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using ServiceInterface.Common;
using System.Data;
using M_hmw.Common.DataTool;


namespace M_hmw.Detail
{
    public partial class Detail_Table_forBusiness : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var message = Request.Params["message"];
                var str = message.Split(' ');
                var businessItem = str[0];
                var id = str[1];
                var dataId = str[2];

                string dataTable = "";
                switch(businessItem){
                    case "GoodsBill": 
                    case "ShipBusinessConsign": 
                    case "NoShipBusinessConsign": 
                    case "JobConsign": 
                    case "VehicleTransport": 
                    case "VehicleBalance": 
                    case "CargoIn": 
                    case "CargoOut": 
                    case "CargoStock":
                    case "CargoDui":
                    case "NewLandBridgeWorkPlan": 
                    case "PassedTransportation": 
                    case "NoPassedTransportation": 
                    case "CargoBalance": dataTable = Leo.RegistryKey.KeyPathHarbors; break;
                    case "ForeShip": 
                    case "IndeedShip": 
                    case "AnchorShip": 
                    case "BerthShip": 
                    case "PlanedShip":
                    case "VehicleDeclaration": 
                    case "VehicleRegistration": 
                    case "MoveShip": dataTable = Leo.RegistryKey.KeyPathBases; break;
                    case "PilotageFee": 
                    case "CommunicationFee": dataTable = Leo.RegistryKey.KeyPathMas; break;
                    case "WeightNote": dataTable = Leo.RegistryKey.KeyPathBc; break;

                    default:
                        throw new Exception("错误的对象索引");
                }

                //数据项
                var arry0 = TableTool_forBusiness.GetTableItemSet(Convert.ToInt16(id));
                //sql
                var sql = SQLTool_forBusiness.GetSQL(Convert.ToInt16(id));
                sql = string.Format(sql, dataId);
                var dt = new DataTable();
                if (businessItem == "WeightNote")
                {
                    dt = new Leo.SqlServer.DataAccess(Leo.RegistryKey.KeyPathBc).ExecuteTable(sql); 
                }
                else
                {
                    dt = new Leo.Oracle.DataAccess(dataTable).ExecuteTable(sql);
                }  
                var arry1 = new Leo.Data.Table(dt).ToArray();

                var arrys = new Array[2];
                arrys[0] = arry0;
                arrys[1] = arry1;

                //Json = Request.QueryString.Get("callback") + "(" + JsonConvert.SerializeObject(arrys) + ")";
                Json = JsonConvert.SerializeObject(arrys);

            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(Detail_Table_forBusiness), ex);
            }

        }
        protected string Json;
    }
}