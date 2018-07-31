using M_hmw.Service.BaseUtil;
using Newtonsoft.Json;
using ServiceInterface.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M_hmw.Function
{
    public partial class jszx : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var userCode = Request.Params["userCode"];
                //userCode = "1190";

                //获取部门ID
                IBaseService service = ServiceFactory.getService(54);
                service.setParams(new String[] { userCode });
                var dt = service.executeForDataTable();
                var arryId = new Leo.Data.Table(dt).ToArray();
                var clientCode = arryId.GetValue(0) as string[];
                string Code_Department = clientCode[2];
                //我的个人信息
                service = ServiceFactory.getService(55);
                service.setParams(new String[] { userCode });
                dt = service.executeForDataTable();
                var arry0 = new Leo.Data.Table(dt).ToArray();
                //我的业务事项
                service = ServiceFactory.getService(57);
                service.setParams(new String[] { Code_Department });
                dt = service.executeForDataTable();
                var arry1 = new Leo.Data.Table(dt).ToArray();
                //我的已付账单
                service = ServiceFactory.getService(56);
                service.setParams(new String[] { Code_Department });
                dt = service.executeForDataTable();
                var arry2 = new Leo.Data.Table(dt).ToArray();
                ////我的费用结算
                //service = ServiceFactory.getService(58);
                //service.setParams(new String[] { Code_Department });
                //dt = service.executeForDataTable();
                //var arry3 = new Leo.Data.Table(dt).ToArray();
                ////我的当年每月支付额
                //service = ServiceFactory.getService(57);
                //service.setParams(new String[] { Code_Department });
                //dt = service.executeForDataTable();
                //var arry3 = new Leo.Data.Table(dt).ToArray();

                //我的港口结算余额
                string sql = string.Format(
                    "select sum(endtotal) from vw_client_endtotal where code_client='{0}'", Code_Department);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathFinaces).ExecuteTable(sql);
                var arry3 = new Leo.Data.Table(dt).ToArray();
                //我的本月支付
                int year = DateTime.Now.Year;
                int month = DateTime.Now.Month;
                DateTime firstDayOfThisMonth = new DateTime(year, month, 1);
                DateTime lastDayOfThisMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                sql = string.Format(
                    "select sum(total) as total from jszx_fp t  where t.code_client='{0}' and t.CREATETIME >= To_Date('{1}', 'yyyy-MM-dd HH24:MI:SS') and t.CREATETIME < To_Date('{2}', 'yyyy-MM-dd HH24:MI:SS')", Code_Department, firstDayOfThisMonth, lastDayOfThisMonth);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHmw).ExecuteTable(sql);
                var arry4 = new Leo.Data.Table(dt).ToArray();

                var arrys = new Array[5];
                arrys[0] = arry0;
                arrys[1] = arry1;
                arrys[2] = arry2;
                arrys[3] = arry3;
                arrys[4] = arry4;

                Json = Request.QueryString.Get("callback") + "(" + JsonConvert.SerializeObject(arrys) + ")";
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(jszx), ex);
            }
        }
        protected string Json;
    }
}