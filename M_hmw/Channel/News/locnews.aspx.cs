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
    public partial class locnews : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var number = Request.Params["number"];
                int a = 0;
                int b = 0;
                int c = 0;
                int x = 0;
                int y = 0;
                int z = 0;
                switch (number)
                {
                    case "1": a = 59; b = 66; c = 73; x = 87; y = 80; z = 94; break; //兰州
                    case "2": a = 60; b = 67; c = 74; x = 88; y = 81; z = 95; break; //山西
                    case "3": a = 61; b = 68; c = 75; x = 89; y = 82; z = 96; break; //西安
                    case "4": a = 62; b = 69; c = 76; x = 90; y = 83; z = 97; break; //新疆
                    case "5": a = 63; b = 70; c = 77; x = 91; y = 84; z = 98; break; //郑州
                    case "6": a = 64; b = 71; c = 78; x = 92; y = 85; z = 99; break; //银川
                    case "7": a = 65; b = 72; c = 79; x = 93; y = 86; z = 100; break; //西宁
                }
                
                //国际煤焦资讯
                var arry0 = new Leo.Data.Table(ServiceFactory.getService(a).executeForDataTable()).ToArray();  //本地新闻
                var arry1 = new Leo.Data.Table(ServiceFactory.getService(b).executeForDataTable()).ToArray();  //经济发展
                var arry2 = new Leo.Data.Table(ServiceFactory.getService(c).executeForDataTable()).ToArray();  //物流贸易
                var arry3 = new Leo.Data.Table(ServiceFactory.getService(x).executeForDataTable()).ToArray();  //招商引资
                var arry4 = new Leo.Data.Table(ServiceFactory.getService(y).executeForDataTable()).ToArray();  //文化活动
                var arry5 = new Leo.Data.Table(ServiceFactory.getService(z).executeForDataTable()).ToArray();  //地方风情
                

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
                LogTool.WriteLog(typeof(locnews), ex);
            }

        }
        protected string Json;
    }
}