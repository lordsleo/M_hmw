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
    public partial class wdhyw2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var userCode = Request.Params["userCode"];
                //userCode = "227";
                //获取货代ID
                IBaseService service = ServiceFactory.getService(54);
                service.setParams(new String[] { userCode });
                var dt = service.executeForDataTable();
                var arryId = new Leo.Data.Table(dt).ToArray();
                var clientCode = arryId.GetValue(0) as string[];
                string Code_HD = clientCode[0];

                //我的货物结存
                service = ServiceFactory.getService(53);
                service.setParams(new String[] { Code_HD });
                dt = service.executeForDataTable();
                var arry0 = new Leo.Data.Table(dt).ToArray();

                var arrys = new Array[1];
                arrys[0] = arry0;
                Json = Request.QueryString.Get("callback") + "(" + JsonConvert.SerializeObject(arrys) + ")";
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(wdhyw2), ex);
            }
        }
        protected string Json;
    }
}