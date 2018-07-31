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
    public partial class C6_Trade4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //有色矿行情
                IBaseService service = ServiceFactory.getService(18);
                var dt = service.executeForDataTable();
                var arry0 = new Leo.Data.Table(dt).ToArray();
                //铁矿砂资讯
                service = ServiceFactory.getService(19);
                dt = service.executeForDataTable();
                var arry1 = new Leo.Data.Table(dt).ToArray();
                //铁矿砂供求
                service = ServiceFactory.getService(20);
                dt = service.executeForDataTable();
                var arry2 = new Leo.Data.Table(dt).ToArray();
                //铁矿砂行情
                service = ServiceFactory.getService(21);
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
                LogTool.WriteLog(typeof(C6_Trade4), ex);
            }

        }
        protected string Json;
    }
}