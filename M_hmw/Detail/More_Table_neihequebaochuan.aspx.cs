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
    public partial class More_Table_neihequebaochuan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //var sqlId = Request.Params["sqlId"];
                var rownum = "360";

                //灌河国际确报船
                IBaseService service = ServiceFactory.getService(5);
                service.setParams(new String[] { rownum });
                var dt = service.executeForDataTable();
                var arry0 = new Leo.Data.Table(dt).ToArray();
                //新云台确报船
                service = ServiceFactory.getService(6);
                service.setParams(new String[] { rownum });
                dt = service.executeForDataTable();
                var arry1 = new Leo.Data.Table(dt).ToArray();

                var arrys = new Array[2];
                arrys[0] = arry0;
                arrys[1] = arry1;

                Json = Request.QueryString.Get("callback") + "(" + JsonConvert.SerializeObject(arrys) + ")";
                //Json = JsonConvert.SerializeObject(arry);  
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(More_Table_neihequebaochuan), ex);
            }

        }
        protected string Json;
    }
}