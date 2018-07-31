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

namespace M_hmw.ships
{
    public partial class ships : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var number = Request.Params["number"];
                //number = "1";
                int a = 0;
                int b = 0;
                int c = 0;
                int d = 0;
                switch (number)
                {
                    case "0": a = 101; b = 109; c = 117; d = 125; break; //韩国航线
                    case "1": a = 102; b = 110; c = 118; d = 126; break; //日本航线
                    case "2": a = 103; b = 111; c = 119; d = 127; break; //内支航线
                    case "3": a = 104; b = 112; c = 120; d = 128; break; //美洲航线
                    case "4": a = 105; b = 113; c = 121; d = 129; break; //欧洲航线
                    case "5": a = 106; b = 114; c = 122; d = 130; break; //中东航线
                    case "6": a = 107; b = 115; c = 123; d = 131; break; //非洲航线
                    case "7": a = 108; b = 116; c = 124; d = 132; break; //东南亚航线
                }

                var arry0 = new Leo.Data.Table(ServiceFactory.getService(a).executeForDataTable()).ToArray();  //整箱船期
                var arry1 = new Leo.Data.Table(ServiceFactory.getService(b).executeForDataTable()).ToArray();  //杂散船期
                var arry2 = new Leo.Data.Table(ServiceFactory.getService(c).executeForDataTable()).ToArray();  //整箱运价
                var arry3 = new Leo.Data.Table(ServiceFactory.getService(d).executeForDataTable()).ToArray();  //杂散运价


                
                var arrys = new Array[4];
                arrys[0] = arry0;
                arrys[1] = arry1;
                arrys[2] = arry2;
                arrys[3] = arry3;


                //Json = JsonConvert.SerializeObject(arrys);
                Json = Request.QueryString.Get("callback") + "(" + JsonConvert.SerializeObject(arrys) + ")";
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ships), ex);
            }
        }
        protected string Json;
    }
}