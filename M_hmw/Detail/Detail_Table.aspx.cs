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
using M_hmw.Common;

namespace M_hmw.Detail
{
    public partial class Detail_Table : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var message = Request.Params["message"];
                var str = message.Split(' ');
                var tableId = str[0];
                var dataId = str[1];

                //数据项
                var arry0 = TableItemTool.GetTableItemSet(Convert.ToInt16(tableId));
                //数据
                IBaseService service = ServiceFactory.getService(Convert.ToInt16(tableId)+150);
                service.setParams(new String[] { dataId });
                var dt = service.executeForDataTable();
                var arry1 = new Leo.Data.Table(dt).ToArray();

                var arrys = new Array[2];
                arrys[0] = arry0;
                arrys[1] = arry1;

                //Json = Request.QueryString.Get("callback") + "(" + JsonConvert.SerializeObject(arrys) + ")";
                Json = JsonConvert.SerializeObject(arrys);

            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(Detail_Table), ex);
            }

        }
        protected string Json;
    }
}