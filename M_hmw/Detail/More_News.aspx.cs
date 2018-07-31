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
    public partial class More_News : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var sqlId = Request.Params["sqlId"];
                //资讯
                var rownum = "360";
                IBaseService service = ServiceFactory.getService(Convert.ToInt16(sqlId));
                service.setParams(new String[] { rownum });
                var dt = service.executeForDataTable();
                var arry = new Leo.Data.Table(dt).ToArray();

                Json = Request.QueryString.Get("callback") + "(" + JsonConvert.SerializeObject(arry) + ")";
                //Json = JsonConvert.SerializeObject(arry);  
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(More_News), ex);
            }

        }
        protected string Json;
    }
}