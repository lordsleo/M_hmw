﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using M_hmw.Service.BaseUtil;
using Newtonsoft.Json;
using ServiceInterface.Common;

namespace M_hmw.Function
{
    public partial class wdcyw5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //var userCode = Request.Params["userCode"];
                //获取船代ID
                //IBaseService service = ServiceFactory.getService(54);
                //service.setParams(new String[] { userCode });
                //var dt = service.executeForDataTable();
                //var arryId = new Leo.Data.Table(dt).ToArray();
                //var clientCode = arryId.GetValue(0) as string[];
                //string Code_CD = clientCode[1];

                //我的船舶计划
                IBaseService service = ServiceFactory.getService(46);
                var dt = service.executeForDataTable();
                var arry0 = new Leo.Data.Table(dt).ToArray();

                var arrys = new Array[1];
                arrys[0] = arry0;

                Json = Request.QueryString.Get("callback") + "(" + JsonConvert.SerializeObject(arrys) + ")";
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(wdcyw), ex);
            }
        }
        protected string Json;
    }
}