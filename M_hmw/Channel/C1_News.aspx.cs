using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using ServiceInterface.Common;
using System.Data;
using Leo;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Channel
{
    public partial class C1_News : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try 
            {                          
                //今日头条
                IBaseService service = ServiceFactory.getService(40);
                var dt = service.executeForDataTable();
                var arry0 = new Leo.Data.Table(dt).ToArray();
                //港口要闻
                 service = ServiceFactory.getService(26);
                dt = service.executeForDataTable();
                var arry1 = new Leo.Data.Table(dt).ToArray();
                //路桥资讯
                service = ServiceFactory.getService(27);
                dt = service.executeForDataTable();
                var arry2 = new Leo.Data.Table(dt).ToArray();
                //行业观察
                service = ServiceFactory.getService(28);
                dt = service.executeForDataTable();
                var arry3 = new Leo.Data.Table(dt).ToArray();

                var arrys = new Array[4];
                arrys[0] = arry0;
                arrys[1] = arry1;
                arrys[2] = arry2;
                arrys[3] = arry3;

                Json = Request.QueryString.Get("callback") + "(" + JsonConvert.SerializeObject(arrys)+")";

                //Json = JsonConvert.SerializeObject(arrys);  

            }
            catch(Exception ex)
            {
                LogTool.WriteLog(typeof(C1_News),ex);
            }

        }
        protected string Json;
    }
}