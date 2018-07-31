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
    public partial class C3_Railway : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //铁路资讯
                IBaseService service = ServiceFactory.getService(36);
                var dt = service.executeForDataTable();
                var arry0 = new Leo.Data.Table(dt).ToArray(); 
                //来港火车确保
                service = ServiceFactory.getService(37);
                dt = service.executeForDataTable();
                var arry1 = new Leo.Data.Table(dt).ToArray();
                //码头卸车计划
                service = ServiceFactory.getService(38);
                dt = service.executeForDataTable();
                var arry2 = new Leo.Data.Table(dt).ToArray();
                //码头装车计划
                service = ServiceFactory.getService(39);
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
                LogTool.WriteLog(typeof(C3_Railway), ex);
            }

        }
        protected string Json;
    }
}