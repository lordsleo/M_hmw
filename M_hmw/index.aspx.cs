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

namespace M_hmw.Home
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //要闻咨询
                IBaseService service = ServiceFactory.getService(140);
                var dt = service.executeForDataTable();
                var arry0 = new Leo.Data.Table(dt).ToArray();

                var arrys = new Array[1];
                arrys[0] = arry0;
                //Json = Request.QueryString.Get("callback") + "(" + JsonConvert.SerializeObject(arrys) + ")";

                Json = JsonConvert.SerializeObject(arrys);  

            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(index), ex);
            }

        }
        protected string Json;
    }
}