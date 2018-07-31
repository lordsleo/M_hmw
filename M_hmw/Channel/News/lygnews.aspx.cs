using Newtonsoft.Json;
using ServiceInterface.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Leo;
using M_hmw.Service.BaseUtil;

namespace M_hmw.News
{
    public partial class lygnews : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try 
            {
                var arry0 = new Leo.Data.Table(ServiceFactory.getService(133).executeForDataTable()).ToArray();  //本地新闻
                var arry1 = new Leo.Data.Table(ServiceFactory.getService(138).executeForDataTable()).ToArray();  //海运新闻
                var arry2 = new Leo.Data.Table(ServiceFactory.getService(134).executeForDataTable()).ToArray();  //经济发展
                var arry3 = new Leo.Data.Table(ServiceFactory.getService(137).executeForDataTable()).ToArray();  //招商引资
                var arry4 = new Leo.Data.Table(ServiceFactory.getService(135).executeForDataTable()).ToArray();  //物流贸易
                var arry5 = new Leo.Data.Table(ServiceFactory.getService(136).executeForDataTable()).ToArray();  //文化活动

                var arrys = new Array[6];
                arrys[0] = arry0;
                arrys[1] = arry1;
                arrys[2] = arry2;
                arrys[3] = arry3;
                arrys[4] = arry4;
                arrys[5] = arry5;

                Json = Request.QueryString.Get("callback") + "(" + JsonConvert.SerializeObject(arrys) + ")";
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(lygnews), ex);
            }

        }
        protected string Json;
    
    } 
}