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
    public partial class C5_River : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //内河资讯
                IBaseService service = ServiceFactory.getService(4);
                var dt = service.executeForDataTable();
                var arry0 = new Leo.Data.Table(dt).ToArray(); 
                //灌河国际确报船
                service = ServiceFactory.getService(5);
                dt = service.executeForDataTable();
                var arry1 = new Leo.Data.Table(dt).ToArray();
                //新云台确报船
                service = ServiceFactory.getService(6);
                dt = service.executeForDataTable();
                var arry2 = new Leo.Data.Table(dt).ToArray();
                //灌河国际泊位船
                service = ServiceFactory.getService(7);
                dt = service.executeForDataTable();
                var arry3 = new Leo.Data.Table(dt).ToArray();
                //新云台泊位船
                service = ServiceFactory.getService(8);
                dt = service.executeForDataTable();
                var arry4 = new Leo.Data.Table(dt).ToArray();

                var arrys = new Array[5];
                arrys[0] = arry0;
                arrys[1] = arry1;
                arrys[2] = arry2;
                arrys[3] = arry3;
                arrys[4] = arry4;;

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