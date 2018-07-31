using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using M_hmw.Service.BaseUtil;
using Newtonsoft.Json;
using ServiceInterface.Common;

namespace M_hmw.Function
{
    public partial class wdcyw4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var userCode = Request.Params["userCode"];
                //userCode = "227";
                //获取船代ID
                IBaseService service = ServiceFactory.getService(54);
                service.setParams(new String[] { userCode });
                var dt = service.executeForDataTable();
                var arryId = new Leo.Data.Table(dt).ToArray();
                var clientCode = arryId.GetValue(0) as string[];
                string Code_CD = clientCode[1];

                //Code_CD = "10";

                //我的泊位船舶
                service = ServiceFactory.getService(45);
                service.setParams(new String[] { Code_CD });
                dt = service.executeForDataTable();
                var arry0 = new Leo.Data.Table(dt).ToArray();

                var arrys = new Array[1];
                arrys[0] = arry0;

                Json = Request.QueryString.Get("callback") + "(" + JsonConvert.SerializeObject(arrys) + ")";
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(wdcyw), ex);
            }
        }
        protected string Json;
    }
}