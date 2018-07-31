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
    public partial class C6_Trade : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //国际煤焦资讯
                IBaseService service = ServiceFactory.getService(9);
                var dt = service.executeForDataTable();
                var arry0 = new Leo.Data.Table(dt).ToArray(); 
                //国内煤焦资讯
                service = ServiceFactory.getService(10);
                dt = service.executeForDataTable();
                var arry1 = new Leo.Data.Table(dt).ToArray();
                //港口煤焦资讯
                service = ServiceFactory.getService(11);
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
                LogTool.WriteLog(typeof(C6_Trade), ex);
            }

        }

        protected string Json;
    }
}