using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using ServiceInterface.Common;
using Leo;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Channel
{
    public partial class C2_Ship : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //船舶资讯
                IBaseService service = ServiceFactory.getService(29);
                var dt = service.executeForDataTable();
                var arry0 = new Leo.Data.Table(dt).ToArray();
                //集装箱船期
                service = ServiceFactory.getService(30);
                dt = service.executeForDataTable();
                var arry1 = new Leo.Data.Table(dt).ToArray();
                //集装箱运价
                service = ServiceFactory.getService(31);
                dt = service.executeForDataTable();
                var arry2 = new Leo.Data.Table(dt).ToArray();

                var arrys = new Array[3];
                arrys[0] = arry0;
                arrys[1] = arry1;
                arrys[2] = arry2;

                Json = Request.QueryString.Get("callback") + "(" + JsonConvert.SerializeObject(arrys) + ")";
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(C2_Ship), ex);
            }
        }
        protected string Json;
    }
}