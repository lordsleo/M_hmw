//
//文件名：    GetBasicInfo.aspx.cs
//功能描述：  获取基本信息
//创建时间：  2015/08/26
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
    public partial class GetBasicInfo : System.Web.UI.Page
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
                    info.Add("参数ExterNo，ScanType不能为null！举例", "http://218.92.115.55/M_Hmw/GetService/Scan/GetBasicInfo.aspx?No=904250&RecognizeMethod=QR");
                    Json = JsonConvert.SerializeObject(info);
                    return;
                }

                //号码字段名称
                string noFieldName = string.Empty;
                //识别方式名称
                string recognizeName = string.Empty;
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
                        recognizeName = "二维码";
                        break;
                    case "NFC":
                        noFieldName = "PARK_CARD_NO";
                        recognizeName = "NFC";
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
                no = Convert.ToString(dt.Rows[0]["exter_no"]);

                //申报时间
                string submitTime = string.Empty;
                //码头公司
                string department = string.Empty;
                //任务号
                string taskno = string.Empty;
                //类别
                string fullorempty = string.Empty;
                //货名
                string cargo = string.Empty;
                //场地货位
                string storagebooth = string.Empty;
                //到港时间
                string preptime = string.Empty;
                //放行时间
                string audittime = string.Empty;
                //内部委托号
                string cgno = string.Empty;

                //办证时间
                string recordTime = string.Empty;
                //进港时间
                string gateInTime = string.Empty;
                //离港时间
                string gateOutTime = string.Empty;

                //过磅时间
                string recordTime1 = string.Empty;
                //货重
                string weightCargo = string.Empty;

                //运输申报
                sql =
                    string.Format(@"select INGATENO,to_char(SUBMITTIME, 'yyyy-MM-dd HH24:mi :ss') as SUBMITTIME,SUBMIT_COMP,SUBMIT_ADDR,DEPARTMENT,PERMITNO,WEIGHT,STORAGE || BOOTH as STORAGEBOOTH,to_char(PREPTIME, 'yyyy-MM-dd HH24:mi :ss') as PREPTIME,to_char(AUDITTIME, 'yyyy-MM-dd HH24:mi :ss') as AUDITTIME,CGNO  
                                    from Harbor.V_CONSIGN_VEHICLE_ONLY_QUICK where EXTER_NO='{0}' and VEHICLE='{1}'", no, vehicle);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
                if (dt.Rows.Count != 0)
                {
                    submitTime = Convert.ToString(dt.Rows[0]["SUBMITTIME"]);
                    department = Convert.ToString(dt.Rows[0]["DEPARTMENT"]);
                    storagebooth = Convert.ToString(dt.Rows[0]["STORAGEBOOTH"]);
                    preptime = Convert.ToString(dt.Rows[0]["PREPTIME"]);
                    audittime = Convert.ToString(dt.Rows[0]["AUDITTIME"]);
                    cgno = Convert.ToString(dt.Rows[0]["CGNO"]);
                }
                sql =
                    string.Format(@"select TASKNO,FULLOREMPTY,CARGO,CLIENT,VESSEL  
                                    from Harbor.V_CONSIGN_QUICK where CGNO='{0}'", cgno);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
                if (dt.Rows.Count != 0)
                {
                    taskno = Convert.ToString(dt.Rows[0]["TASKNO"]);
                    fullorempty = Convert.ToString(dt.Rows[0]["FULLOREMPTY"]);
                    cargo = Convert.ToString(dt.Rows[0]["CARGO"]);
                }

                //港区放行
                sql =
                    string.Format(@"select OPER,to_char(RECORD_TIME, 'yyyy-MM-dd HH24:mi :ss') as RECORD_TIME,FEE_NAME,ENDDATE 
                                    from Transit.V_PARLOR_RECORD_ALL 
                                    where CARD_NO='{0}' and VEHICLE='{1}' and MARK_VALID='1' order by RECORD_TIME desc", cardNo, vehicle);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
                if (dt.Rows.Count != 0)
                {
                    recordTime = Convert.ToString(dt.Rows[0]["RECORD_TIME"]);
                }
                sql =
                    string.Format(@"select GATE_IN_NAME,GATE_OUT_NAME,to_char(GATE_IN_TIME, 'yyyy-MM-dd HH24:mi :ss') as GATE_IN_TIME,to_char(GATE_OUT_TIME, 'yyyy-MM-dd HH24:mi :ss') as GATE_OUT_TIME
                                    from Transit.V_GATE_TIME 
                                    where CARD_NO='{0}' and VEHICLE='{1}' order by GATE_IN_TIME desc", cardNo, vehicle);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
                if (dt.Rows.Count != 0)
                {
                    gateInTime = Convert.ToString(dt.Rows[0]["GATE_IN_TIME"]);
                    gateOutTime = Convert.ToString(dt.Rows[0]["GATE_OUT_TIME"]);
                }
                 
                //过磅记录
                sql =
                    string.Format(@"select convert(varchar(100), RECORDTIME, 120) as RECORDTIME,WEIGHTCARGO,RECORD1,WEIGHT1,RECORD2,WEIGHT2 
                                    from BalanceCenter..V_MetageForComm 
                                    where CARDNO='{0}' and TRUCK='{1}' order by RECORDTIME desc", cardNo, vehicle);
                dt = new Leo.SqlServer.DataAccess(Leo.RegistryKey.KeyPathBc).ExecuteTable(sql);  
                if (dt.Rows.Count != 0)
                {
                    recordTime1 = Convert.ToString(dt.Rows[0]["RECORDTIME"]);
                    weightCargo = Convert.ToString(dt.Rows[0]["WEIGHTCARGO"]) == "" ? "" : string.Format("{0:N2}", Convert.ToDouble(dt.Rows[0]["WEIGHTCARGO"]) / 1000);
                }

                string[] nameArry = { "识别方式", "车牌号", "卡编号", "任务号", "码头公司", "类别", "货名", "场地货位", "申报时间", 
                                      "到港时间", "放行时间", "办证时间", "入港时间", "过磅时间", "货重", "离港时间"};
                //排序字符串
                string order = string.Empty;
                order = nameArry[0] + "+" + nameArry[1] + "+" + nameArry[2] + "+" + nameArry[3] + "+" + nameArry[4] + "+" + nameArry[5] + "+" +
                        nameArry[6] + "+" + nameArry[7] + "+" + nameArry[8] + "+" + nameArry[9] + "+" + nameArry[10] + "+" + nameArry[11] + "+" +
                        nameArry[12] + "+" + nameArry[13] + "+" + nameArry[14] + "+" + nameArry[15];

                info.Add(nameArry[0], recognizeName);
                info.Add(nameArry[1], vehicle);
                info.Add(nameArry[2], no);
                info.Add(nameArry[3], taskno);
                info.Add(nameArry[4], department);
                info.Add(nameArry[5], fullorempty);
                info.Add(nameArry[6], cargo);
                info.Add(nameArry[7], storagebooth);
                info.Add(nameArry[8], submitTime);
                info.Add(nameArry[9], preptime);
                info.Add(nameArry[10], audittime);
                info.Add(nameArry[11], recordTime);
                info.Add(nameArry[12], gateInTime);
                info.Add(nameArry[13], recordTime1);
                info.Add(nameArry[14], weightCargo);
                info.Add(nameArry[15], gateOutTime);
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