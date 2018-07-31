//
//文件名：    GetTransportDeclare.aspx.cs
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
    public partial class GetTransportDeclare : System.Web.UI.Page
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
                    info.Add("参数ExtNoerNo，RecognizeMethod不能为null！举例", "http://218.92.115.55/M_Hmw/GetService/Scan/GetTransportDeclare.aspx?No=904250&RecognizeMethod=QR");
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
                no = Convert.ToString(dt.Rows[0]["exter_no"]);

                string ingateno = string.Empty;
                string submitTime = string.Empty;
                string submitComp = string.Empty;
                string submitAddr = string.Empty;
                string department = string.Empty;
                string taskno = string.Empty;
                string fullorempty = string.Empty;
                string cargo = string.Empty;
                string client = string.Empty;
                string vessel = string.Empty;
                string permitno = string.Empty;
                string weight = string.Empty;
                string storagebooth = string.Empty;
                string preptime = string.Empty;
                string audittime = string.Empty;
                string cgno = string.Empty;
                sql =
                    string.Format(@"select INGATENO,to_char(SUBMITTIME, 'yyyy-MM-dd HH24:mi :ss') as SUBMITTIME,SUBMIT_COMP,SUBMIT_ADDR,DEPARTMENT,PERMITNO,WEIGHT,STORAGE || BOOTH as STORAGEBOOTH,to_char(PREPTIME, 'yyyy-MM-dd HH24:mi :ss') as PREPTIME,to_char(AUDITTIME, 'yyyy-MM-dd HH24:mi :ss') as AUDITTIME,CGNO  
                                    from Harbor.V_CONSIGN_VEHICLE_ONLY_QUICK where EXTER_NO='{0}' and VEHICLE='{1}'", no, vehicle);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
                if (dt.Rows.Count != 0)
                {
                    ingateno = Convert.ToString(dt.Rows[0]["INGATENO"]);
                    submitTime = Convert.ToString(dt.Rows[0]["SUBMITTIME"]);
                    submitComp = Convert.ToString(dt.Rows[0]["SUBMIT_COMP"]);
                    submitAddr = Convert.ToString(dt.Rows[0]["SUBMIT_ADDR"]);
                    department = Convert.ToString(dt.Rows[0]["DEPARTMENT"]);
                    permitno = Convert.ToString(dt.Rows[0]["PERMITNO"]);
                    weight = Convert.ToString(dt.Rows[0]["WEIGHT"]);
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
                    client = Convert.ToString(dt.Rows[0]["CLIENT"]);
                    vessel = Convert.ToString(dt.Rows[0]["VESSEL"]);
                }

                string[] nameArry = { "申报编号", "申报时间", "申报单位", "申报入口", "码头公司", "任务号", "类别", "货名", "货代", 
                                      "船名", "小票号", "预装吨数", "场地货位", "到港时间", "放行时间"};
                //排序字符串
                string order = string.Empty;
                order = nameArry[0] + "+" + nameArry[1] + "+" + nameArry[2] + "+" + nameArry[3] + "+" + nameArry[4] + "+" + nameArry[5] + "+" +
                        nameArry[6] + "+" + nameArry[7] + "+" + nameArry[8] + "+" + nameArry[9] + "+" + nameArry[10] + "+" + nameArry[11] + "+" +
                        nameArry[12] + "+" + nameArry[13] + "+" + nameArry[14];

                info.Add(nameArry[0], ingateno);
                info.Add(nameArry[1], submitTime);
                info.Add(nameArry[2], submitComp);
                info.Add(nameArry[3], submitAddr);
                info.Add(nameArry[4], department);
                info.Add(nameArry[5], taskno);
                info.Add(nameArry[6], fullorempty);
                info.Add(nameArry[7], cargo);
                info.Add(nameArry[8], client);
                info.Add(nameArry[9], vessel);
                info.Add(nameArry[10], permitno);
                info.Add(nameArry[11], weight);
                info.Add(nameArry[12], storagebooth);
                info.Add(nameArry[13], preptime);
                info.Add(nameArry[14], audittime);
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