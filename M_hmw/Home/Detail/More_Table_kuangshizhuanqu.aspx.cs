﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using ServiceInterface.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Home.Detail
{
    public partial class More_Table_kuangshizhuanqu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var message = Request.Params["message"];
                var str = message.ToString().Split(' ');
                var minRow = str[0];
                var maxRow = str[1];
                //var minRow = "1";
                //var maxRow = "15";

                var sql =
                    string.Format(
                        "select count(id) as sum from view_trade_business where type=3 and isnew=1 ");
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathWl).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    var arry = 0;
                    Json = JsonConvert.SerializeObject(arry);
                    return;
                }

                sql =
                    string.Format(
                        "select ID,case when type_name='供' then '供货' when type_name='求' then '需求' end as TYPE_NAME,CLASS,SPEC,TONS,PLACE,to_char(REG_DATE,'yyyy-MM-dd') from(select * from (select ID,TYPE_NAME,CLASS,SPEC,TONS,PLACE,REG_DATE from view_trade_business where type=3 and isnew=1 order by reg_date asc) where rownum<='{0}' order by reg_date desc) where rownum<='{1}'",
                        Convert.ToInt16(dt.Rows[0]["sum"]) - Convert.ToInt16(minRow) + 1, Convert.ToInt16(maxRow) - Convert.ToInt16(minRow) + 1);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathWl).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    var arry = 0;
                    Json = JsonConvert.SerializeObject(arry);
                    return;
                }
                var arrys = new Leo.Data.Table(dt).ToArray();
                Json = JsonConvert.SerializeObject(arrys);
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(More_Table_kuangshizhuanqu), ex);
            }
        }
        protected string Json;
    }
}