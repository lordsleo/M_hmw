using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using ServiceInterface.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Channel
{
    public partial class C4_Highway : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //公路资讯
                IBaseService service = ServiceFactory.getService(0);
                var dt = service.executeForDataTable();
                var arry0 = new Leo.Data.Table(dt).ToArray(); 
                //已放行车辆
                service = ServiceFactory.getService(1);
                dt = service.executeForDataTable();
                var arry1 = new Leo.Data.Table(dt).ToArray();
                //网上车源
                service = ServiceFactory.getService(2);
                dt = service.executeForDataTable();
                var arry2 = new Leo.Data.Table(dt).ToArray();
                //司机信息
                service = ServiceFactory.getService(3);
                dt = service.executeForDataTable();
                var arry3 = new Leo.Data.Table(dt).ToArray();

                var arrys = new Array[4];
                arrys[0] = arry0;
                arrys[1] = arry1;
                arrys[2] = arry2;
                arrys[3] = arry3;
                Json = Request.QueryString.Get("callback") + "(" + JsonConvert.SerializeObject(arrys) + ")";
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(C4_Highway), ex);
            }

        }
        protected string Json;
    }
}