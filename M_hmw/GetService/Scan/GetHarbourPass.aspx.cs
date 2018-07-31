//
//文件名：    GetHarbourPass.aspx.cs
//功能描述：  获取运输申报数据
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
    public partial class GetHarbourPass : System.Web.UI.Page
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
                    info.Add("参数ExterNo，ScanType不能为null！举例", "http://218.92.115.55/M_Hmw/GetService/Scan/GetHarbourPass.aspx?No=904250&RecognizeMethod=QR");
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

                string oper = string.Empty;
                string recordTime = string.Empty;
                string feeName = string.Empty;
                string endDate = string.Empty;
                sql =
                    string.Format(@"select OPER,to_char(RECORD_TIME, 'yyyy-MM-dd HH24:mi :ss') as RECORD_TIME,FEE_NAME,to_char(ENDDATE, 'yyyy-MM-dd HH24:mi :ss') ENDDATE 
                                    from Transit.V_PARLOR_RECORD_ALL 
                                    where CARD_NO='{0}' and VEHICLE='{1}' and MARK_VALID='1' order by RECORD_TIME desc", cardNo, vehicle);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
                if (dt.Rows.Count != 0)
                {
                    oper = Convert.ToString(dt.Rows[0]["OPER"]);
                    recordTime = Convert.ToString(dt.Rows[0]["RECORD_TIME"]);
                    feeName = Convert.ToString(dt.Rows[0]["FEE_NAME"]);
                    endDate = Convert.ToString(dt.Rows[0]["ENDDATE"]);
                }

                string gateInName = string.Empty;
                string gateOutName = string.Empty;
                string gateInTime = string.Empty;
                string gateOutTime = string.Empty;
                sql =
                    string.Format(@"select GATE_IN_NAME,GATE_OUT_NAME,to_char(GATE_IN_TIME, 'yyyy-MM-dd HH24:mi :ss') as GATE_IN_TIME,to_char(GATE_OUT_TIME, 'yyyy-MM-dd HH24:mi :ss') as GATE_OUT_TIME
                                    from Transit.V_GATE_TIME 
                                    where CARD_NO='{0}' and VEHICLE='{1}' order by GATE_IN_TIME desc", cardNo, vehicle);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
                if (dt.Rows.Count != 0)
                {
                    gateInName = Convert.ToString(dt.Rows[0]["GATE_IN_NAME"]);
                    gateOutName = Convert.ToString(dt.Rows[0]["GATE_OUT_NAME"]);
                    gateInTime = Convert.ToString(dt.Rows[0]["GATE_IN_TIME"]);
                    gateOutTime = Convert.ToString(dt.Rows[0]["GATE_OUT_TIME"]);
                }

                string cgateInName = string.Empty;
                string cgateOutName = string.Empty;
                string cgateInTime = string.Empty;
                string cgateOutTime = string.Empty;
                sql =
                    string.Format(@"select CGATE_IN_NAME,CGATE_OUT_NAME,to_char(CGATE_IN_TIME, 'yyyy-MM-dd HH24:mi :ss') as CGATE_IN_TIME,to_char(CGATE_OUT_TIME, 'yyyy-MM-dd HH24:mi :ss') as CGATE_OUT_TIME  
                                    from CGate.V_CGATE_TIME 
                                    where CARD_NO='{0}' and VEHICLE='{1}' order by CGATE_IN_TIME desc", cardNo, vehicle);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
                if (dt.Rows.Count != 0)
                {
                    cgateInName = Convert.ToString(dt.Rows[0]["CGATE_IN_NAME"]);
                    cgateOutName = Convert.ToString(dt.Rows[0]["CGATE_OUT_NAME"]);
                    cgateInTime = Convert.ToString(dt.Rows[0]["CGATE_IN_TIME"]);
                    cgateOutTime = Convert.ToString(dt.Rows[0]["CGATE_OUT_TIME"]);
                }

                string[] nameArry = { "业务类别", "办证时间", "证件类型", "有效期", "入港岗亭", "入港时间", "进门岗亭", "进门时间", 
                                      "出门岗亭", "出门时间", "离港岗亭", "离港时间"};
                //排序字符串
                string order = string.Empty;
                order = nameArry[0] + "+" + nameArry[1] + "+" + nameArry[2] + "+" + nameArry[3] + "+" + nameArry[4] + "+" + nameArry[5] + "+" +
                        nameArry[6] + "+" + nameArry[7] + "+" + nameArry[8] + "+" + nameArry[9] + "+" + nameArry[10] + "+" + nameArry[11]; 

                info.Add(nameArry[0], oper);
                info.Add(nameArry[1], recordTime);
                info.Add(nameArry[2], feeName);
                info.Add(nameArry[3], endDate);
                info.Add(nameArry[4], gateInName);
                info.Add(nameArry[5], gateInTime);
                info.Add(nameArry[6], cgateInName);
                info.Add(nameArry[7], cgateInTime);
                info.Add(nameArry[8], cgateOutName);
                info.Add(nameArry[9], cgateOutTime);
                info.Add(nameArry[10], gateOutName);        
                info.Add(nameArry[11], gateOutTime);       

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