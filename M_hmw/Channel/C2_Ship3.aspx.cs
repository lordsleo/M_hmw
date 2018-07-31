using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using ServiceInterface.Common;
using Leo;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Channel
{
    public partial class C2_Ship3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //锚地船
                IBaseService service = ServiceFactory.getService(35);
                var dt = service.executeForDataTable();
                var arrys = new Leo.Data.Table(dt).ToArray();

                Json = Request.QueryString.Get("callback") + "(" + JsonConvert.SerializeObject(arrys) + ")";
                //Json = JsonConvert.SerializeObject(arrys);
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(C2_Ship3), ex);
            }
        }
        protected string Json;
    }
}