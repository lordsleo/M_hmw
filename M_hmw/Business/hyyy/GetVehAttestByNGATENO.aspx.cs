using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M_hmw.Business.hyyy
{
    public partial class GetVehAttestByNGATENO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var inGateNo = Request.Params["inGateNo"];

                //inGateNo = "AAA8N3ABfAAAxqAAAD";
                //string inGateNo = "1501066333";
                string token = "ExterAttest";
                DataSet set;
                ConsignVehicleReport.ExterClient get = new ConsignVehicleReport.ExterClient();
                get.GetVehAttestByRID(inGateNo, token, out set);

                var dt = set.Tables[0];
                var DEPARTMENT = dt.Rows[0]["DEPARTMENT"] as string;
                var CLIENT = dt.Rows[0]["CLIENT"] as string;
                var TASKNO = dt.Rows[0]["TASKNO"] as string;
                var SUBMITTIME = dt.Rows[0]["SUBMITTIME"] as string;
                var CARGO = dt.Rows[0]["CARGO"] as string;
                var MARK = dt.Rows[0]["MARK"] as string;
                var VESSEL_VOYAGE = dt.Rows[0]["VESSEL"] as string+"/"+ dt.Rows[0]["VOYAGE"] as string;
                var BLNO = dt.Rows[0]["BLNO"] as string;
                var Weight1 = dt.Rows[0]["Weight1"] as string;
                var Weight2 = dt.Rows[0]["Weight2"] as string;
                var WeightCargo = dt.Rows[0]["WeightCargo"] as string;
                var VEHICLE = dt.Rows[0]["VEHICLE"] as string;
                var DRIVERNAME = dt.Rows[0]["DRIVERNAME"] as string;
                var WEIGHT = dt.Rows[0]["WEIGHT"] as string;
                var SeqName = dt.Rows[0]["SeqName"] as string;

                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("DEPARTMENT", DEPARTMENT);
                dic.Add("CLIENT", CLIENT);
                dic.Add("TASKNO", TASKNO);
                dic.Add("SUBMITTIME", SUBMITTIME);
                dic.Add("CARGO", CARGO);
                dic.Add("MARK", MARK);
                dic.Add("VESSEL_VOYAGE", VESSEL_VOYAGE);
                dic.Add("BLNO", BLNO);
                dic.Add("Weight1", Weight1);
                dic.Add("Weight2", Weight2);
                dic.Add("WeightCargo", WeightCargo);
                dic.Add("VEHICLE", VEHICLE);
                dic.Add("DRIVERNAME", DRIVERNAME);
                dic.Add("WEIGHT", WEIGHT);
                dic.Add("SeqName", SeqName);


                Json = Leo.Json.JsonConvert.SerializeObject(dic);
            }
            catch (Exception ex)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("error", ex.Message);
                Json = Leo.Json.JsonConvert.SerializeObject(dic);
            }
        }
        protected string Json;
    }
}