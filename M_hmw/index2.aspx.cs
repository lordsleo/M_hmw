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

namespace M_hmw
{
    public partial class index2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //锚地船舶
                IBaseService service = ServiceFactory.getService(142);
                var dt = service.executeForDataTable();
                var arry0 = new Leo.Data.Table(dt).ToArray();
                //进出港计划
                service = ServiceFactory.getService(143);
                dt = service.executeForDataTable();
                var arry1 = new Leo.Data.Table(dt).ToArray();

                var arrys = new Array[10];
                arrys[0] = arry0;
                arrys[1] = arry1;


                //Json = Request.QueryString.Get("callback") + "(" + JsonConvert.SerializeObject(arrys) + ")";

                Json = JsonConvert.SerializeObject(arrys);

            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(index2), ex);
            }

        }
        protected string Json;
    }
}