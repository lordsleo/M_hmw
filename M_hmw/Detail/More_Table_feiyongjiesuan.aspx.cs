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
    public partial class More_Table_feiyongjiesuan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var message = Request.Params["message"];
                var str = message.Split(' ');
                var sqlId = str[0];
                var userCode = str[1];
                //获取船代ID
                IBaseService service = ServiceFactory.getService(54);
                service.setParams(new String[] { userCode });
                var dt = service.executeForDataTable();
                var arryId = new Leo.Data.Table(dt).ToArray();
                var clientCode = arryId.GetValue(0) as string[];
                string Code_HD = clientCode[0];

                var rownum = "360";

                service = ServiceFactory.getService(Convert.ToInt16(sqlId));
                service.setParams(new String[] { Code_HD, rownum });
                dt = service.executeForDataTable();
                var arry = new Leo.Data.Table(dt).ToArray();

                Json = Request.QueryString.Get("callback") + "(" + JsonConvert.SerializeObject(arry) + ")";
                //Json = JsonConvert.SerializeObject(arry);  
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(More_Table_yewushixiang), ex);
            }

        }
        protected string Json;
    }
}