using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using ServiceInterface.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Detail
{
    public partial class More_Table_meijiaozixun : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //var sqlId = Request.Params["sqlId"];
                var rownum = "360";

                //国际
                IBaseService service = ServiceFactory.getService(9);
                service.setParams(new String[] { rownum });
                var dt = service.executeForDataTable();
                var arry0 = new Leo.Data.Table(dt).ToArray();
                //国内
                service = ServiceFactory.getService(10);
                service.setParams(new String[] { rownum });
                dt = service.executeForDataTable();
                var arry1 = new Leo.Data.Table(dt).ToArray();
                //港口
                service = ServiceFactory.getService(11);
                service.setParams(new String[] { rownum });
                dt = service.executeForDataTable();
                var arry2 = new Leo.Data.Table(dt).ToArray();

                var arrys = new Array[3];
                arrys[0] = arry0;
                arrys[1] = arry1;
                arrys[2] = arry2;

                Json = Request.QueryString.Get("callback") + "(" + JsonConvert.SerializeObject(arrys) + ")";
                //Json = JsonConvert.SerializeObject(arry);  
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(More_Table_meijiaozixun), ex);
            }

        }
        protected string Json;
    }
}