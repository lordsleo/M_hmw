//
//文件名：    GetWeighRecord.aspx.cs
//功能描述：  获取过磅记录
//创建时间：  2015/08/25
//作者：      
//修改时间：  暂无
//修改描述：  暂无
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Leo;
using Newtonsoft.Json;

namespace M_hmw.GetService.Scan
{
    public partial class GetWeighRecord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //号码
            var no = Request.Params["No"];
            //识别方式
            var recognizeMethod = Request.Params["RecognizeMethod"];

            Dictionary<string, string> info = new Dictionary<string, string>();
            try
            {
                if (no == null || recognizeMethod == null)
                {
                    info.Add("IsGet", "No");
                    info.Add("参数ExterNo，ScanType不能为null！举例", "http://218.92.115.55/M_Hmw/GetService/Scan/GetWeighRecord.aspx?No=904250&RecognizeMethod=QR");
                    Json = JsonConvert.SerializeObject(info);
                    return;
                }

                //号码字段名称
                string noFieldName = string.Empty;
                //SQL
                string sql = string.Empty;
                //车号
                string vehicle = string.Empty;
                //卡号
                string cardNo = string.Empty;

                switch (recognizeMethod)
                {
                    case "QR":
                        noFieldName = "EXTER_NO";
                        break;
                    case "NFC":
                        noFieldName = "PARK_CARD_NO";
                        break;
                    default:
                        throw new Exception("错误的对象索引");
                }

                //验证NO
                sql =
                    string.Format("select exter_no,vehicle,card_no from TRANSIT.CARD where {0}='{1}'", noFieldName, no);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    info.Add("IsGet", "No");
                    info.Add("Message", "此卡不存在，读取失败！");
                    Json = JsonConvert.SerializeObject(info);
                    return;
                }
                vehicle = Convert.ToString(dt.Rows[0]["vehicle"]);
                cardNo = Convert.ToString(dt.Rows[0]["card_no"]);

                string recordTime = string.Empty;
                string weightCargo = string.Empty;
                string record1 = string.Empty;
                string weight1 = string.Empty;
                string record2 = string.Empty;
                string weight2 = string.Empty;
                sql =
                    string.Format(@"select convert(varchar(100), RECORDTIME, 120) as RECORDTIME,WEIGHTCARGO,RECORD1,WEIGHT1,RECORD2,WEIGHT2 
                                    from BalanceCenter..V_MetageForComm 
                                    where CARDNO='{0}' and TRUCK='{1}' order by RECORDTIME desc", cardNo, vehicle);
                dt = new Leo.SqlServer.DataAccess(Leo.RegistryKey.KeyPathBc).ExecuteTable(sql);  
                if (dt.Rows.Count != 0)
                {
                    recordTime = Convert.ToString(dt.Rows[0]["RECORDTIME"]);
                    weightCargo = Convert.ToString(dt.Rows[0]["WEIGHTCARGO"]) == "" ? "" : string.Format("{0:N2}", Convert.ToDouble(dt.Rows[0]["WEIGHTCARGO"]) / 1000);
                    record1 = Convert.ToString(dt.Rows[0]["RECORD1"]);
                    weight1 = Convert.ToString(dt.Rows[0]["WEIGHT1"]) == "" ? "" : string.Format("{0:N2}", Convert.ToDouble(dt.Rows[0]["WEIGHT1"]) / 1000);
                    record2 = Convert.ToString(dt.Rows[0]["RECORD2"]);
                    weight2 = Convert.ToString(dt.Rows[0]["WEIGHT2"]) == "" ? "" : string.Format("{0:N2}", Convert.ToDouble(dt.Rows[0]["WEIGHT2"]) / 1000);
                }

                string[] nameArry = { "过磅时间", "货重", "过空时间", "空重", "过重时间", "毛重"};
                //排序字符串
                string order = string.Empty;
                order = nameArry[0] + "+" + nameArry[1] + "+" + nameArry[2] + "+" + nameArry[3] + "+" + nameArry[4] + "+" + nameArry[5];

                info.Add(nameArry[0], recordTime);
                info.Add(nameArry[1], weightCargo);
                info.Add(nameArry[2], record1);
                info.Add(nameArry[3], weight1);
                info.Add(nameArry[4], record2);
                info.Add(nameArry[5], weight2);
                info.Add("Order", order);

                Json = JsonConvert.SerializeObject(info);
            }
            catch (Exception ex)
            {
                info.Add("IsGet", "No");
                info.Add("Message", ex.Message);
                Json = JsonConvert.SerializeObject(info);
            }
        }
        protected string Json;
    }
}