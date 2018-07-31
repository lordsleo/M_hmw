using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ServiceInterface.Model;
using ServiceInterface.Common;
using System.Data;
using Leo;
using M_hmw.Service.BaseUtil;
using YGSoft.IPort.Data;
using System.Net;
using System.Diagnostics;
using System.IO;
using AndroidPush;

namespace M_hmw
{

    /// <summary>
    /// ServiceHMW 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class ServiceHMW : System.Web.Services.WebService
    {
        #region 例子
        [WebMethod]
        public DataTable HelloWorld()
        {
            var sql = string.Format("select a.kcfd,a.ddd,a.id,a.cphm,b.DESCRIPT,a.jzsj,a.cx,a.cc,a.dw,a.lxfs from hmw.wscy a,hmw.user_group b  where a.USERID=b.id and a.sh=1 order by a.id desc");
            var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathBases).ExecuteTable(sql);
            dt.TableName = "table";
            return dt;
        }
        #endregion

        #region 首页

        #region 系统功能

        #region 用户登录
        /// <summary> 用户登录。 </summary>
        /// <param name="token">身份验证码</param>
        /// <param name="loginname">登录名</param>
        /// <param name="password">密码</param>
        /// <returns>登录信息</returns>
        [WebMethod]
        public string GetLogin(string token, string logogram, string password, string deviceID,string DeviceType,string snsToken)
        {
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new Leo.Xml.Message("Token未通过校验。").FalseXml();
                }

                string sql = string.Format("select a.code_user, a.code_department, a.code_company from TB_SYS_USER a where a.logogram='{0}' and a.password is not null", logogram);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathIports).ExecuteTable(sql);
                var xml = new Leo.Xml.ToXml("").GetData(dt);
                if (dt.Rows.Count == 0)
                {
                    return new Leo.Xml.Message("用户名错误！").FalseXml();
                }

                sql = string.Format("select code_user,password from TB_SYS_USER t where t.logogram='{0}' and t.password is not null", logogram);
                var dt1 = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathIports).ExecuteTable(sql);

                if (!Identity.VerifyText(password, dt1.Rows[0]["PASSWORD"] as string))
                {
                    return new Leo.Xml.Message("密码错误！").FalseXml();
                }

                sql = string.Format("select devicetoken,DEVICEBINDING from tb_hmw_devid where usercode='{0}'", dt1.Rows[0]["CODE_USER"] as string);
                var dt2 = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);

                sql = string.Format("select a.code_user, a.code_department, a.code_company, '0' as DEVICEBINDING from TB_SYS_USER a where a.logogram='{0}' and a.password is not null", logogram);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathIports).ExecuteTable(sql);
                xml = new Leo.Xml.ToXml("").GetData(dt);

                if (dt2.Rows.Count == 0)
                {
                    sql = string.Format("insert into tb_hmw_devid (usercode,devicetoken,devicetype) values ('{0}','{1}','{2}')", dt1.Rows[0]["CODE_USER"] as string, deviceID, DeviceType);
                    dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
                    sql = string.Format("select devicetoken from tb_hmw_devid where usercode='{0}'", dt1.Rows[0]["CODE_USER"] as string);
                    dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
                    if (dt.Rows[0]["devicetoken"].ToString() != deviceID)
                    {
                        return new Leo.Xml.Message("登录失败，未注册成功！").FalseXml();
                    }

                    return new Leo.Xml.Message("登陆成功，设备未绑定！").TrueXml(xml);
                }
               
                sql = string.Format("select a.code_user, a.code_department, a.code_company,b.DEVICEBINDING from TB_SYS_USER a,mobilecenter.tb_hmw_devid b where a.logogram='{0}' and a.code_user = b.usercode and a.password is not null", logogram);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathIports).ExecuteTable(sql);
                xml = new Leo.Xml.ToXml("").GetData(dt);

                if ("0" == Convert.ToString(dt2.Rows[0]["devicebinding"]))
                {
                    return new Leo.Xml.Message("登陆成功，设备未绑定！").TrueXml(xml);
                }

                if (deviceID != dt2.Rows[0]["DEVICETOKEN"] as string)
                {
                    return new Leo.Xml.Message("登录失败，已绑定到其他设备！").FalseXml();
                }

                return new Leo.Xml.Message("登陆成功！").TrueXml(xml);
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return new Leo.Xml.Message(string.Format("获取数据异常。{0}", ex.Message)).FalseXml();
            }
        }

        #endregion

        #region 修改密码
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="token">身份验证码</param>
        /// <param name="userId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        [WebMethod]
        public string ChangePassword(string token, string userId, string oldPassword, string newPassword)
        {
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new Leo.Xml.Message("Token未通过校验。").FalseXml();
                }

                string sql = string.Format("select PASSWORD,CODE_USER from TB_SYS_USER t where t.CODE_USER ='{0}'", userId);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathIports).ExecuteTable(sql);

                if (dt.Rows.Count == 0)
                {
                    return new Leo.Xml.Message("无此用户！").FalseXml();
                }

                var row = dt.Rows[0];

                if (!Identity.VerifyText(oldPassword, row["PASSWORD"] as string))
                {
                    return new Leo.Xml.Message("密码错误！").FalseXml();
                }

                sql = string.Format(
                    "update TB_SYS_USER set DPBEGINTIME=null,DYNAMICPASSWORD=null,PASSWORD='{0}' where CODE_USER='{1}'",
                    Identity.EncodeText(newPassword), userId);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathIports).ExecuteTable(sql);

                return new Leo.Xml.Message("修改成功！").TrueXml();

            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return new Leo.Xml.Message(string.Format("获取数据异常。{0}", ex.Message)).FalseXml();
            }
        }

        #endregion

        #region 获取个人信息
        /// <summary>
        /// 获取个人信息
        /// </summary>
        /// <param name="token">身份验证码</param>
        /// <param name="userId">ID</param>
        /// <param name="tel">固定电话</param>
        /// <param name="phone">移动电话</param>
        /// <param name="email">电子邮箱</param>
        /// <returns>列表</returns>
        [WebMethod]
        public string GetPersonDetailData(string token, string userId)
        {

            //固定电话
            const string phone = "PHONE";

            //移动电话
            const string mobile = "MOBILE";

            //电子邮箱
            const string email = "EMAIL";

            //SQL串参数
            string[] values = new string[] { phone, mobile, email, userId };
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new Leo.Xml.Message("Token未通过校验。").FalseXml();
                }

                string sql = string.Format("select {0},{1},{2} from tb_sys_userinfo t where t.CODE_USER='{3}'", values);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathIports).ExecuteTable(sql);
                var xml = new Leo.Xml.ToXml("").GetData(dt);
                return new Leo.Xml.Message("获取信息成功！").TrueXml(xml);
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return new Leo.Xml.Message(string.Format("获取数据异常。{0}", ex.Message)).FalseXml();
            }
        }

        #endregion

        #endregion

        #region 启动
        /// <summary>
        /// 获取应用启动图片列表。
        /// </summary>
        /// <returns>列表</returns>
        [WebMethod]
        public string GetLoadingPictureList()
        {
            const string sql = "select t.name,t.url,t.vercode from TB_BR_WEBMENU t where t.parent_id=60 and t.mark_audit='1' order by t.sort";
            //var dt = new DataAccess(Leo.RegistryKey.KeyPathMa).ExecuteTable(sql);
            var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
            var list = (from DataRow row in dt.Rows.AsParallel() 
                        select new HmwLoadingPicture
                        {
                            Name = row["NAME"] as string,
                            Url = row["URL"] as string,
                            VersionCode = Convert.ToInt32(row["VERCODE"])
                        }).ToArray();
            return new HmwLoadingPictureList { Message = null, Success = true, Value = list }
                .ToXmlString();
        }

        /// <summary>
        /// 获取应用帮助图片列表。
        /// </summary>
        /// <returns>列表</returns>
        [WebMethod]
        public string GetHelpPictureList()
        {
            const string sql = "select t.name,t.url,t.vercode from TB_BR_WEBMENU t where t.parent_id=64 and t.mark_audit='1' order by t.sort";
            //var dt = new DataAccess(Leo.RegistryKey.KeyPathMa).ExecuteTable(sql);
            var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
            var list = (from DataRow row in dt.Rows.AsParallel()
                        select new HmwHelpPicture
                        {
                            Name = row["NAME"] as string,
                            Url = row["URL"] as string,
                            VersionCode = Convert.ToInt32(row["VERCODE"])
                        }).ToArray();
            return new HmwHelpPictureList { Message = null, Success = true, Value = list }
                .ToXmlString();
        }
        #endregion

        #region 首页
        /// <summary>
        /// 获取首页通用频道导航菜单项列表。
        /// </summary>
        /// <returns>列表</returns>
        //[WebMethod]
        //public string GetMainPageNavMenuList()
        //{
        //    //TODO:

        //    return
        //        new HmwMainPageNavItemList { Message = null, Success = true, Value = new HmwMainPageNavItem[1] }
        //            .ToXmlString();
        //}
        /// <summary>
        /// 获取首页滚动图片URL列表。
        /// </summary>
        /// <returns>列表</returns>
        //[WebMethod]
        //public string GetMainPageRollingPictureList()
        //{
        //    //TODO:

        //    return
        //        new HmwMainPageRollingPictureList
        //        {
        //            Message = null,
        //            Success = true,
        //            Value = new HmwMainPageRollingPicture[1]
        //        }
        //            .ToXmlString();
        //}
        /// <summary>
        /// 获取首页常用功能导航菜单项列表。
        /// </summary>
        /// <returns>列表</returns>
        //[WebMethod]
        //public string GetMainPageCommonFuncList()
        //{
        //    //TODO:

        //    return
        //        new HmwMainPageCommonFuncList
        //        {
        //            Message = null,
        //            Success = true,
        //            Value = new HmwMainPageCommonFunc[1]
        //        }
        //            .ToXmlString();
        //}
        /// <summary>
        /// 获取首页要闻资讯新闻列表。
        /// </summary>
        /// <returns>列表</returns>
        [WebMethod]
        public string GetMainPageImptNewsList()
        {
            const string sql =
                "select to_char(id) pkid,topic,to_char(post_time,'yyyy-mm-dd') post_time, message from (select id,topic,post_time,message from hmw.news_topic where news_level=1 order by id desc) where rownum<6";
            //var dt = new DataAccess(Leo.RegistryKey.KeyPathMa).ExecuteTable(sql);
            var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);

            var list = (from DataRow row in dt.Rows.AsParallel()
                        select new HmwMainPageImptNews
                        {
                            NewsId = row["PKID"] as string,
                            PostTime = row["POST_TIME"] as string,
                            Topic = row["TOPIC"] as string
                        }).ToArray();

            return
                new HmwMainPageImptNewsList
                {
                    Message = null,
                    Success = true,
                    Value = list
                }
                    .ToXmlString();
        }
        /// <summary>
        /// 获取首页要闻咨询新闻详细内容。
        /// </summary>
        /// <param name="newsId">新闻ID</param>
        /// <returns>详细内容</returns>
        [WebMethod]
        public string GetMainPageImptNewsDetail(string newsId)
        {
            var sql = string.Format("Select MESSAGE From hmw.NEWS_TOPIC Where id= {0} Order By ID DESC", newsId);
            //var dt = new DataAccess(Leo.RegistryKey.KeyPathMa).ExecuteTable(sql);
            var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);

            if (dt.Rows.Count == 0) return new HmwMainPageImptNews { Content = null }.ToXmlString();

            return new HmwMainPageImptNews { Content = HttpUtility.UrlDecode(dt.Rows[0]["MESSAGE"].ToString()) }.ToXmlString();
        }
        /// <summary>
        /// 获取首页在港船舶数据列表。
        /// </summary>
        /// <returns>列表</returns>
        [WebMethod]
        public string GetMainPagePortShipList()
        {

            //船舶ID
            const string ship_id = "SHIP_ID";

            //船代
            const string name = "NAME";

            //船名
            const string chi_vessel = "CHI_VESSEL";

            //泊位号
            const string berthno = "BERTHNO";

            //泊位方位
            const string berth_position = "BERTH_POSITION";

            //作业公司
            const string department = "DEPARTMENT";

            //状态
            const string shipstatus = "SHIPSTATUS";

            //余吨
            const string dqyd = "DQYD";

            //显示的记录数
            const int count = 5;

            //SQL串参数
            string[] values = new string[] { ship_id, name, chi_vessel, berthno, berth_position, department, shipstatus, dqyd, count.ToString() };

            string sql = string.Format("select {0},{1},{2},{3},{4},{5},{6},{7} from view_sship_tan where (ship_statu=2 or ship_statu=4 or ship_statu=5 or ship_statu=3) and hgnh=0 and rownum<={8}", values);

            try
            {

                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathBase).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathBases).ExecuteTable(sql);

                var list = from DataRow row in dt.Rows.AsParallel()
                           select new HmwMainPagePortShip
                           {
                               ShipId = Convert.ToString(row[ship_id]),
                               AgentName = Convert.ToString(row[name]),
                               ShipNameCn = Convert.ToString(row[chi_vessel]),
                               BerthDesc = Convert.ToString(row[berthno]) + " " + Convert.ToString(row[berth_position]),
                               JobCompany = Convert.ToString(row[department]),
                               State = Convert.ToString(row[shipstatus]),
                               RestTon = Convert.ToString(row[dqyd])
                           };

                return
                    new HmwMainPagePortShipList
                    {
                        Message = null,
                        Success = true,
                        Value = list.ToArray()
                    }
                        .ToXmlString();

            }
            catch (Exception ex)
            {

                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new HmwMainPagePortShipList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        /// <summary>
        /// 获取首页锚地船舶数据列表。
        /// </summary>
        /// <returns>列表</returns>
        [WebMethod]
        public string GetMainPageAnchorShipList()
        {
            //船舶ID
            const string ship_id = "SHIP_ID";

            //船代
            const string name = "NAME";

            //船名
            const string chi_vessel = "CHI_VESSEL";

            //this吃水
            const string this_draft = "THIS_DRAFT";

            //chu吃水
            const string chu_draft = "CHU_DRAFT";

            //内外贸
            const string trade = "TRADE";

            //抛锚时间
            const string yjdg = "YJDG";

            //引水
            const string ys = "YS";

            //显示的记录数
            const int count = 5;

            //当前时间
            DateTime nowTime = System.DateTime.Now;

            //SQL串参数
            string[] values = new string[] { ship_id, name, chi_vessel, this_draft, chu_draft, trade, yjdg, ys, nowTime.AddDays(-6).ToString(), nowTime.AddDays(-3).ToString(), nowTime.AddDays(-1).ToString(), count.ToString() };

            string sql = string.Format("select {0},{1},{2},{3},{4},{5},{6},{7} from view_sship_tan where ship_statu=1 and hgnh=0 and ((loa>260 and s_declare<=to_date('{8}','yyyy-MM-dd hh24:mi:ss')) or (loa<=260 and loa>180 and s_declare<=to_date('{9}','yyyy-MM-dd hh24:mi:ss')) or (loa<=180 and s_declare<=to_date('{10}','yyyy-MM-dd hh24:mi:ss')) or (code_vesseltype='5') or (ship_id=318013) or (ship_id=318012) or (ship_id=318022) or (tsqk=1)) and rownum<={11}", values);

            try
            {

                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathBase).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathBases).ExecuteTable(sql);

                var list = from DataRow row in dt.Rows.AsParallel()
                           select new HmwMainPageAnchorShip
                           {

                               ShipId = Convert.ToString(row[ship_id]),
                               AgentName = Convert.ToString(row[name]),
                               ShipNameCn = Convert.ToString(row[chi_vessel]),
                               Draft = Convert.ToString(row[this_draft]) + " " + Convert.ToString(row[chu_draft]),
                               Trade = Convert.ToString(row[trade]),
                               ArrivalTime = Convert.ToString(row[yjdg]),
                               Pilot = Convert.ToString(row[ys])

                           };

                return
                    new HmwMainPageAnchorShipList
                    {
                        Message = null,
                        Success = true,
                        Value = list.ToArray()
                    }
                        .ToXmlString();

            }
            catch (Exception ex)
            {

                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new HmwMainPageAnchorShipList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        /// <summary>
        /// 获取首页进出港计划数据列表。
        /// </summary>
        /// <returns>列表</returns>
        [WebMethod]
        public string GetMainPageInoutPortPlanList()
        {
            //ID
            const string p_id = "P_ID";

            //船代
            const string cbdl = "CBDL";

            //船名
            const string chi_vessel = "CHI_VESSEL";

            //泊位号
            const string dqbw = "DQBW";

            //泊位位置
            const string bwwz = "BWWZ";

            //性质
            const string trade = "PC";

            //引水
            const string pilotage = "PILOTAGE";

            //显示的记录数
            const int count = 5;

            //SQL串参数
            string[] values = new string[] { p_id, cbdl, chi_vessel, dqbw, bwwz, trade, pilotage, count.ToString() };

            string sql = string.Format("select {0},{1},{2},{3},{4},{5},{6} from ywcplan where hgnh=0 and rownum<={7}", values);

            try
            {
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathBase).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathBases).ExecuteTable(sql);

                var list = from DataRow row in dt.Rows.AsParallel()
                           select new HmwMainPageInoutPortPlan
                           {
                               ShipId = Convert.ToString(row[p_id]),
                               AgentName = Convert.ToString(row[cbdl]),
                               ShipNameCn = Convert.ToString(row[chi_vessel]),
                               BerthDesc = Convert.ToString(row[dqbw]) + " " + Convert.ToString(row[bwwz]),
                               Nature = Convert.ToString(row[trade]),
                               Pilot = Convert.ToString(row[pilotage])
                           };

                return
                    new HmwMainPageInoutPortPlanList
                    {
                        Message = null,
                        Success = true,
                        Value = list.ToArray()
                    }
                        .ToXmlString();
            }
            catch (Exception ex)
            {

                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new HmwMainPageInoutPortPlanList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        /// <summary>
        /// 获取首页最新船期数据列表。
        /// </summary>
        /// <returns>列表</returns>
        [WebMethod]
        public string GetMainPageLatestShipmentList()
        {
            //主键
            const string p_id = "ID";

            //起始港
            const string s_s_port = "S_S_PORT";

            //目的港
            const string s_e_port = "S_E_PORT";

            //货名
            const string s_s_name = "S_S_NAME";

            //载重
            const string s_carrying = "S_CARRYING";

            //开航日期
            const string s_start_time = "S_STAR_TIME";

            //显示的记录数
            const int count = 5;

            //SQL串参数
            string[] values = new string[] { p_id, s_s_port, s_e_port, s_s_name, s_carrying, s_start_time, count.ToString() };

            string sql = string.Format("select {0},{1},{2},{3},{4},{5} from(select a.S_S_PORT,a.S_E_PORT,a.S_STAR_TIME,b.DESCRIPT,a.id,a.S_SHIPLINE,a.S_CARRYING,a.S_S_NAME from hmw.ship_entire_box_time a,hmw.user_group b where a.USERID=b.id and a.sh=1 order by a.id desc) where rownum<={6}", values);

            try
            {
                //hmw/Je2HKbUY8iye@port_168.100.1.100
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathBase).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHmw).ExecuteTable(sql);

                var list = from DataRow row in dt.Rows.AsParallel()
                           select new HmwMainPageLatestShipment
                           {
                               Pkid = Convert.ToString(row[p_id]),
                               FromPort = Convert.ToString(row[s_s_port]),
                               ToPort = Convert.ToString(row[s_e_port]),
                               CargoName = Convert.ToString(row[s_s_name]),
                               Weight = Convert.ToString(row[s_carrying]),
                               SailingDate = Convert.ToString(row[s_start_time])
                           };


                return
                    new HmwMainPageLatestShipmentList
                    {
                        Message = null,
                        Success = true,
                        Value = list.ToArray()
                    }
                        .ToXmlString();

            }
            catch (Exception ex)
            {

                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new HmwMainPageLatestShipmentList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        /// <summary>
        /// 获取首页最新货盘数据列表。
        /// </summary>
        /// <returns>列表</returns>
        [WebMethod]
        public string GetMainPageLatestPalletList()
        {
            //主键
            const string p_id = "ID";

            //起始港
            const string s_s_port = "S_S_PORT";

            //目的港
            const string s_e_port = "S_E_PORT";

            //F20
            const string f20 = "F20";

            //F40
            const string f40 = "F40";

            //物流公司
            const string s_dep = "S_DEP";

            //显示的记录数
            const int count = 5;

            //SQL串参数
            string[] values = new string[] { p_id, s_s_port, s_e_port, f20, f40, s_dep, count.ToString() };

            string sql = string.Format("select {0},{1},{2},{3},{4},{5} from(select a.S_S_PORT,a.S_E_PORT,b.DESCRIPT,a.S_DATE,a.id,a.S_SHIPLINE,a.F20,a.F40,a.S_DEP from hmw.ship_entire_box_price a,hmw.user_group b where a.USERID=b.id and a.sh=1 order by a.id desc) where rownum<={6}", values);

            try
            {
                //hmw/Je2HKbUY8iye@port_168.100.1.100
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathBase).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHmw).ExecuteTable(sql);

                var list = from DataRow row in dt.Rows.AsParallel()
                           select new HmwMainPageLatestPallet
                           {
                               Pkid = Convert.ToString(row[p_id]),
                               FromPort = Convert.ToString(row[s_s_port]),
                               ToPort = Convert.ToString(row[s_e_port]),
                               F20 = Convert.ToString(row[f20]),
                               F40 = Convert.ToString(row[f40]),
                               LogisticsCompany = Convert.ToString(row[s_dep])
                           };

                return
                    new HmwMainPageLatestPalletList
                    {
                        Message = null,
                        Success = true,
                        Value = list.ToArray()
                    }
                        .ToXmlString();

            }
            catch (Exception ex)
            {

                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new HmwMainPageLatestPalletList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        /// <summary>
        /// 获取首页矿石专区数据列表。
        /// </summary>
        /// <returns>列表</returns>
        [WebMethod]
        public string GetMainPageOreZoneList()
        {
            //主键
            const string p_id = "ID";

            //供需
            const string type_name = "TYPE_NAME";

            //货名
            const string cargo_name = "CLASS";

            //规格
            const string spec = "SPEC";

            //产地
            const string place = "PLACE";

            //交货日期
            const string reg_date = "REG_DATE";

            //重量
            const string weight = "TONS";

            //显示的记录数
            const int count = 5;

            //SQL串参数
            string[] values = new string[] { p_id, cargo_name, spec, place, reg_date, weight, type_name, count.ToString() };

            string sql = string.Format("select {0},{1},{2},{3},{4},{5},case when type_name='供' then '供货' when type_name='求' then '需求' end as {6} from wl.view_trade_business where type=3 and isnew=1 and rownum<={7} order by reg_date desc", values);

            try
            {
                //wl/wlgs001@port_168.100.1.100
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathBase).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathWl).ExecuteTable(sql);

                var list = from DataRow row in dt.Rows.AsParallel()
                           select new HmwMainPageOreZone
                           {
                               Pkid = Convert.ToString(row[p_id]),
                               SupplyDemand = Convert.ToString(row[type_name]),
                               CargoName = Convert.ToString(row[cargo_name]),
                               Spec = Convert.ToString(row[spec]),
                               Weight = Convert.ToString(row[weight]),
                               Product = Convert.ToString(row[place]),
                               DeliveryDate = Convert.ToString(row[reg_date])
                           };

                return
                    new HmwMainPageOreZoneList
                    {
                        Message = null,
                        Success = true,
                        Value = list.ToArray()
                    }
                        .ToXmlString();

            }
            catch (Exception ex)
            {

                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new HmwMainPageOreZoneList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        /// <summary>
        /// 获取首页煤炭专区数据列表。
        /// </summary>
        /// <returns>列表</returns>
        [WebMethod]
        public string GetMainPageCoalZoneList()
        {
            //主键
            const string p_id = "ID";

            //供需
            const string type_name = "TYPE";

            //煤种
            const string cargo_name = "GOODSNAME";

            //地区
            const string place = "AREA";

            //发布日期
            const string reg_date = "CREATETIME";

            //重量
            const string weight = "NUM";

            //显示的记录数
            const int count = 5;

            //SQL串参数
            string[] values = new string[] { p_id, cargo_name, place, reg_date, weight, type_name, count.ToString() };

            string sql = string.Format("select {0},{1},{2},{3},{4},{5} from (select id,goodsname,audittime,name,num,area,yxq,vd,createtime,type  from lygcoal.vw_sup_dem where  audit_mark='1' order by createtime desc) a where  rownum<={6}", values);

            try
            {
                //lygcoal/lyg!@#coal@port_168.100.1.100
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathBase).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathLyg).ExecuteTable(sql);

                var list = from DataRow row in dt.Rows.AsParallel()
                           select new HmwMainPageCoalZone
                           {
                               Pkid = Convert.ToString(row[p_id]),
                               SupplyDemand = Convert.ToString(row[type_name]),
                               CargoName = Convert.ToString(row[cargo_name]),
                               Weight = Convert.ToString(row[weight]),
                               Product = Convert.ToString(row[place]),
                               DeliveryDate = Convert.ToString(row[reg_date])
                           };


                return
                    new HmwMainPageCoalZoneList
                    {
                        Message = null,
                        Success = true,
                        Value = list.ToArray()
                    }
                        .ToXmlString();
            }
            catch (Exception ex)
            {

                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new HmwMainPageCoalZoneList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        /// <summary>
        /// 获取首页最新运力数据列表。
        /// </summary>
        /// <returns>列表</returns>
        [WebMethod]
        public string GetMainPageLatestTransCapList()
        {
            //主键
            const string p_id = "ID";

            //车型
            const string vehicle_type = "CX";

            //车号
            const string vehicle_no = "CPHM";

            //车重
            const string weight = "DW";

            //车长
            const string length = "CC";

            //起始
            const string run_from = "KCFD";

            //到止
            const string run_to = "DDD";

            //联系方式
            const string tel = "LXFS";

            //显示的记录数
            const int count = 5;

            //SQL串参数
            string[] values = new string[] { p_id, vehicle_type, vehicle_no, weight, length, run_from, run_to, tel, count.ToString() };

            //string sql = string.Format("select {0},{1},{2},{3},{4},{5},{6},{7} from(select a.kcfd,a.ddd,a.id,a.cphm,b.DESCRIPT,a.jzsj,a.cx,a.cc,a.dw,a.lxfs from hmw.wscy a,hmw.user_group b  where a.USERID=b.id and a.sh=1 order by a.id desc) where rownum<={8}", values);

            string sql = string.Format("       select ID,CX,CPHM,DW,CC,KCFD,DDD,LXFS from(select a.kcfd,a.ddd,a.id,a.cphm,b.DESCRIPT,a.jzsj,a.cx,a.cc,a.dw,a.lxfs from hmw.wscy a,hmw.user_group b  where a.USERID=b.id and a.sh=1 order by a.id desc) where rownum<=5");

            try
            {
                //hmw/Je2HKbUY8iye@port_168.100.1.100
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathBase).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHmw).ExecuteTable(sql);

                var list = from DataRow row in dt.Rows.AsParallel()
                           select new HmwMainPageLatestTransCap
                           {
                               Pkid = Convert.ToString(row[p_id]),
                               VehicleType = Convert.ToString(row[vehicle_type]),
                               VehicleNo = Convert.ToString(row[vehicle_no]),
                               Weight = Convert.ToString(row[weight]),
                               Length = Convert.ToString(row[length]),
                               From = Convert.ToString(row[run_from]),
                               To = Convert.ToString(row[run_to]),
                               Tel = Convert.ToString(row[tel])

                           };

                return
                    new HmwMainPageLatestTransCapList
                    {
                        Message = null,
                        Success = true,
                        Value = list.ToArray()
                    }
                        .ToXmlString();
            }
            catch (Exception ex)
            {

                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new HmwMainPageLatestTransCapList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        /// <summary>
        /// 获取首页最新货源数据列表。
        /// </summary>
        /// <returns>列表</returns>
        [WebMethod]
        public string GetMainPageLatestGoodsSourceList()
        {
            //主键
            const string p_id = "ID";

            //货名
            const string cargo_name = "HWMC";

            //重量
            const string weight = "SL";

            //起始
            const string run_from = "QYD";

            //到止
            const string run_to = "DDD";

            //截止日期
            const string end_date = "JZSJ";

            //显示的记录数
            const int count = 5;

            //SQL串参数
            string[] values = new string[] { p_id, cargo_name, weight, run_from, run_to, end_date, count.ToString() };

            string sql = string.Format("select {0},{1},{2},{3},{4},{5} from(select a.id,b.DESCRIPT,a.qyd,a.ddd,a.hwmc,a.jzsj,a.sl from hmw.wshy a,hmw.user_group b  where a.USERID=b.id and a.sh=1 order by a.id desc) where rownum<={6}", values);

            try
            {
                //hmw/Je2HKbUY8iye@port_168.100.1.100
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathBase).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHmw).ExecuteTable(sql);

                var list = from DataRow row in dt.Rows.AsParallel()
                           select new HmwMainPageLatestGoodsSource
                           {
                               Pkid = Convert.ToString(row[p_id]),
                               CargoName = Convert.ToString(row[cargo_name]),
                               Weight = Convert.ToString(row[weight]),
                               From = Convert.ToString(row[run_from]),
                               To = Convert.ToString(row[run_to]),
                               EndDate = Convert.ToString(row[end_date])
                           };

                return
                    new HmwMainPageLatestGoodsSourceList
                    {
                        Message = null,
                        Success = true,
                        Value = list.ToArray()
                    }
                        .ToXmlString();
            }
            catch (Exception ex)
            {

                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new HmwMainPageLatestGoodsSourceList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #region 消息
        /// <summary>
        /// 获取未读消息列表。
        /// </summary>
        /// <param name="token">身份验证码</param>
        /// <param name="userId">用户ID</param>
        /// <returns>列表</returns>
        [WebMethod]
        public string GetMessageList(string token, string userId)
        {
            if (!TokenTool.VerifyToken(token))
            {
                return new HmwMessageList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
            }

            var sql =
                string.Format(
                    "select t.pkid,t.content from TB_BR_MSG t where (t.userid is null or t.userid ='{0}') and t.mark_audit='1' order by t.createtime desc",
                    userId);
            //var dt = new DataAccess(Leo.RegistryKey.KeyPathMa).ExecuteTable(sql);
            var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
            var list = (from DataRow row in dt.Rows.AsParallel()
                        select new HmwMessage
                        {
                            MsgId = Convert.ToInt32(row["PKID"]),
                            Content = row["CONTENT"] as string
                        }).ToArray();

            return
                new HmwMessageList
                {
                    Message = null,
                    Success = true,
                    Value = list
                }
                    .ToXmlString();
        }
        /// <summary>
        /// 更新消息状态为已读。
        /// </summary>
        /// <param name="token">身份验证码</param>
        /// <param name="userId">用户ID</param>
        /// <param name="msgId">消息ID</param>
        /// <returns>操作结果</returns>
        //[WebMethod]
        //public string UpdateMessageState(string token, string userId, string msgId)
        //{
        //    if (!TokenTool.VerifyToken(token))
        //    {
        //        return new OperInfo { Success = false, Message = "Token未通过校验。" }.ToXmlString();
        //    }

        //    //TODO:

        //    return new OperInfo { Message = null, Success = true }.ToXmlString();
        //}
        #endregion

        #region 客服
        /// <summary>
        /// 获取客服类别数据列表。
        /// </summary>
        /// <returns>列表</returns>
        [WebMethod]
        public string GetCustomerServiceTypeList()
        {
            const string sql =
                "select t.pkid,t.name from TB_BR_CSTYPE t where t.mark_audit='1' order by t.createtime desc";
            //var dt = new DataAccess(Leo.RegistryKey.KeyPathMa).ExecuteTable(sql);
            var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
            var list = (from DataRow row in dt.Rows.AsParallel()
                        select new HmwCustomerServiceType
                        {
                            Pkid = Convert.ToInt32(row["PKID"]),
                            Name = row["NAME"] as string
                        }).ToArray();
            return
                new HmwCustomerServiceTypeList
                {
                    Message = null,
                    Success = true,
                    Value = list
                }
                    .ToXmlString();
        }
        /// <summary>
        /// 获取客服联系方式数据列表。
        /// </summary>
        /// <returns>列表</returns>
        [WebMethod]
        public string GetCustomerServiceList()
        {
            const string sql =
                "select t.name,t.contact,t.typeid from TB_BR_CSCONTACT t where t.mark_audit='1' order by t.typeid,t.createtime desc";
            //var dt = new DataAccess(Leo.RegistryKey.KeyPathMa).ExecuteTable(sql);
            var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);

            var list = (from DataRow row in dt.Rows.AsParallel()
                        select new HmwCustomerService
                        {
                            ServiceTypeId = Convert.ToInt32(row["TYPEID"]),
                            ServerName = row["NAME"] as string,
                            ServerContact = row["CONTACT"] as string
                        }).ToArray();

            return
                new HmwCustomerServiceList
                {
                    Message = null,
                    Success = true,
                    Value = list
                }
                    .ToXmlString();
        }
        #endregion

        #region 设置
        /// <summary>
        /// 更新个人信息。
        /// </summary>
        /// <param name="token">身份验证码</param>
        /// <param name="userId">用户ID</param>
        /// <param name="tel">固定电话</param>
        /// <param name="phone">移动电话</param>
        /// <param name="email">电子邮箱</param>
        /// <returns>操作结果</returns>
        [WebMethod]
        public string UpdatePersonDetailData(string token, string userId, string phone, string mobile, string email)
        {
            if (!TokenTool.VerifyToken(token))
            {
                return new OperInfo { Success = false, Message = "Token未通过校验。" }.ToXmlString();
            }

            var sql =
                string.Format("update tb_sys_userinfo set phone='{1}',mobile='{2}',email='{3}' where code_user='{0}'",
                    userId, phone, mobile, email, userId);
            //new DataAccess(Leo.RegistryKey.KeyPathMa).ExecuteNonQuery(sql);
            var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathIports).ExecuteTable(sql);

            return new OperInfo { Message = null, Success = true }.ToXmlString();
        }
        #endregion

        #endregion

        #region 二级页面

        #region 频道

        #region 资讯

        #region 今日头条
        [WebMethod]
        public String GetChannelJinRiTouTiaoList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(40);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 港口要闻
        [WebMethod]
        public String GetChannelGangKouYaoWenList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(26);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 陆桥资讯
        [WebMethod]
        public String GetChannelLuQiaoZiXunList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(27);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 行业观察
        [WebMethod]
        public String GetChannelHangYeGuanChaList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(28);
            //返回结果
            return service.execute();
        }
        #endregion

        #endregion

        #region 船舶

        #region 船舶资讯
        [WebMethod]
        public String GetChannelChuanBoZiXunList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(29);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 集装箱船期
        [WebMethod]
        public String GetChannelJiZhuangXiangChuanQiList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(30);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 运价
        [WebMethod]
        public String GetChannelYunJiaList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(31);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 散杂船期
        [WebMethod]
        public String GetChannelZaSanChuanQiList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(32);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 散杂运价
        [WebMethod]
        public String GetChannelZaSanYunJiaList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(33);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 在泊船
        [WebMethod]
        public String GetChannelZaiBoChuanList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(34);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 锚地船
        [WebMethod]
        public String GetChannelMaoDiChuanList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(35);
            //返回结果
            return service.execute();
        }
        #endregion

        #endregion

        #region 铁路

        #region 铁路资讯
        [WebMethod]
        public String GetChannelTieLuZiXunList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(36);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 来港火车确报
        [WebMethod]
        public String GetChannelLaiGangHuoCheQueBaoList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(37);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 码头卸车计划
        [WebMethod]
        public String GetChannelMaTouXieCheJiHuaList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(38);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 码头装车计划
        [WebMethod]
        public String GetChannelMaTouZhuangCheJiHuaList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(39);
            //返回结果
            return service.execute();
        }
        #endregion

        #endregion

        #region 公路

        #region 公路资讯

        [WebMethod]
        public String GetChannelGongLuZiXunList()
        {

            //获取服务
            IBaseService service = ServiceFactory.getService(0);

            //返回结果
            return service.execute();
        }
        #endregion

        #region 已放行车辆
        [WebMethod]
        public String GetChannelYiFangXingCheLiangList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(1);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 网上车源
        [WebMethod]
        public String GetChannelWangShangCheYuanList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(2);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 司机信息
        [WebMethod]
        public String GetChannelSiJiXinXiList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(3);
            //返回结果
            return service.execute();
        }
        #endregion

        #endregion

        #region 内河

        #region 内河资讯
        [WebMethod]
        public String GetChannelNeiHeZiXunList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(4);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 灌河国际确报船
        [WebMethod]
        public String GetChannelGuanHeGuoJiQueBaoChuanList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(5);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 新云台确报船
        [WebMethod]
        public String GetChannelXinYunTaiQueBaoChuanList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(6);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 灌河国际泊位船
        [WebMethod]
        public String GetChannelGuanHeGuoJiBoWeiChuanList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(7);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 新云台泊位船
        [WebMethod]
        public String GetChannelXinYunTaiBoWeiChuanList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(8);
            //返回结果
            return service.execute();
        }
        #endregion

        #endregion

        #region 贸易

        #region 国际煤焦资讯
        [WebMethod]
        public String GetChannelGuoJiMeiJiaoZiXunList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(9);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 国内煤焦资讯
        [WebMethod]
        public String GetChannelGuoNeiMeiJiaoZiXunList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(10);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 港口煤焦资讯
        [WebMethod]
        public String GetChannelGangKouMeiJiaoZiXunList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(11);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 煤焦供应
        [WebMethod]
        public String GetChannelMeiJiaoGongYingList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(12);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 煤焦求购
        [WebMethod]
        public String GetChannelMeiJiaoQiuGouList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(13);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 煤炭行情
        [WebMethod]
        public String GetChannelMeiJiaoHangQingList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(14);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 焦炭行情
        [WebMethod]
        public String GetChannelJiaoTanHangQingList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(15);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 有色矿资讯
        [WebMethod]
        public String GetChannelYouSeKuangZiXunList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(16);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 有色矿供求
        [WebMethod]
        public String GetChannelYouSeKuangGongQiuList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(17);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 有色矿行情
        [WebMethod]
        public String GetChannelYouSeKuangHangQingList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(18);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 铁矿砂资讯
        [WebMethod]
        public String GetChannelTieKuangShaZiXunList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(19);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 铁矿砂供求
        [WebMethod]
        public String GetChannelTieKuangShaGongQiuList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(20);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 铁矿砂行情
        [WebMethod]
        public String GetChannelTieKuangShaHangQingList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(21);
            //返回结果
            return service.execute();
        }
        #endregion

        #endregion

        #region 仓储

        #region 仓储资讯
        [WebMethod]
        public String GetChannelCangChuZiXunList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(22);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 园区仓储
        [WebMethod]
        public String GetChannelYuanQuCangChuList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(23);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 待储货源
        [WebMethod]
        public String GetChannelDaiChuHuoYuanList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(24);
            //返回结果
            return service.execute();
        }
        #endregion

        #region 来港确报
        [WebMethod]
        public String GetChannelLaiGangQueBaoList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(25);
            //返回结果
            return service.execute();
        }
        #endregion

        #endregion

        #endregion

        #region 功能

        #region 我的船业务

        #region 我的预报船
        /// <summary>
        /// 我的预报船
        /// </summary>
        /// <param name="clientCode">船代客户ID</param>
        /// <returns>XML序列串</returns>
        [WebMethod]
        public String GetChannelMyYuBaoChuanList(String clientCode)
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(41);

            //设置参数
            service.setParams(new String[] { clientCode });

            //返回结果
            return service.execute();
        }
        #endregion

        #region 我的确报船
        /// <summary>
        /// 我的确报船
        /// </summary>
        /// <param name="clientCode">船代客户ID</param>
        /// <returns>XML序列串</returns>
        [WebMethod]
        public String GetChannelMyQueBaoChuanList(String clientCode)
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(42);

            //设置参数
            service.setParams(new String[] { clientCode });

            //返回结果
            return service.execute();
        }
        #endregion

        #region 我的引航费用
        /// <summary>
        /// 我的引航费用
        /// </summary>
        /// <param name="clientCode">船代客户ID</param>
        /// <returns>XML序列串</returns>
        [WebMethod]
        public String GetChannelMyYinHangFeiList(String clientCode)
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(43);

            //设置参数
            service.setParams(new String[] { clientCode });

            //返回结果
            return service.execute();
        }
        #endregion

        #region 我的高频话费
        /// <summary>
        /// 我的高频话费
        /// </summary>
        /// <param name="clientCode">船代客户ID</param>
        /// <returns>XML序列串</returns>
        [WebMethod]
        public String GetChannelMyGaoPingHuaFeiList(String clientCode)
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(44);

            //设置参数
            service.setParams(new String[] { clientCode });

            //返回结果
            return service.execute();
        }
        #endregion

        #region 我的泊位船舶
        /// <summary>
        /// 我的泊位船舶
        /// </summary>
        /// <param name="clientCode">船代客户ID</param>
        /// <returns>XML序列串</returns>
        [WebMethod]
        public String GetChannelMyBoWeiChuanBoList(String clientCode)
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(45);

            //设置参数
            service.setParams(new String[] { clientCode });

            //返回结果
            return service.execute();
        }
        #endregion

        #region 我的船舶计划
        /// <summary>
        /// 我的船舶计划
        /// </summary>
        /// <returns>XML序列串</returns>
        [WebMethod]
        public String GetChannelMyChuanBoJiHuaList()
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(46);

            //返回结果
            return service.execute();
        }
        #endregion

        #endregion

        #region 我的货业务

        #region 我的业务委托

        #region 有船作业
        /// <summary>
        /// 我的业务委托有船作业
        /// </summary>
        /// <param name="clientCode">船代客户ID</param>
        /// <returns>XML序列串</returns>
        [WebMethod]
        public String GetChannelMyYeWuWeiTuoYouChuanList(String clientCode)
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(47);

            //设置参数
            service.setParams(new String[] { clientCode });

            //返回结果
            return service.execute();
        }
        #endregion

        #region 无船作业
        /// <summary>
        /// 我的业务委托无船作业
        /// </summary>
        /// <param name="clientCode">船代客户ID</param>
        /// <returns>XML序列串</returns>
        [WebMethod]
        public String GetChannelMyYeWuWeiTuoWuChuanList(String clientCode)
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(48);

            //设置参数
            service.setParams(new String[] { clientCode });

            //返回结果
            return service.execute();
        }
        #endregion

        #endregion

        #region 我的作业委托
        /// <summary>
        /// 我的作业委托
        /// </summary>
        /// <param name="clientCode">船代客户ID</param>
        /// <returns>XML序列串</returns>
        [WebMethod]
        public String GetChannelMyZuoYeWeiTuoList(String clientCode)
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(49);

            //设置参数
            service.setParams(new String[] { clientCode });

            //返回结果
            return service.execute();
        }
        #endregion

        #region 我的票货
        /// <summary>
        /// 我的票货
        /// </summary>
        /// <param name="clientCode">船代客户ID</param>
        /// <returns>XML序列串</returns>
        [WebMethod]
        public String GetChannelMyPiaoHuoList(String clientCode)
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(50);

            //设置参数
            service.setParams(new String[] { clientCode });

            //返回结果
            return service.execute();
        }
        #endregion

        #region 我的货物进出港

        #region 进港
        /// <summary>
        /// 我的货物进港
        /// </summary>
        /// <param name="clientCode">船代客户ID</param>
        /// <returns>XML序列串</returns>
        [WebMethod]
        public String GetChannelMyHuoWuJinGangList(String clientCode)
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(51);

            //设置参数
            service.setParams(new String[] { clientCode });

            //返回结果
            return service.execute();
        }
        #endregion

        #region 出港
        /// <summary>
        /// 我的货物出港
        /// </summary>
        /// <param name="clientCode">船代客户ID</param>
        /// <returns>XML序列串</returns>
        [WebMethod]
        public String GetChannelMyHuoWuChuGangList(String clientCode)
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(52);

            //设置参数
            service.setParams(new String[] { clientCode });

            //返回结果
            return service.execute();
        }
        #endregion

        #endregion

        #region 我的货物结存
        /// <summary>
        /// 我的货物结存
        /// </summary>
        /// <param name="clientCode">船代客户ID</param>
        /// <returns>XML序列串</returns>
        [WebMethod]
        public String GetChannelMyHuoWuJieCunList(String clientCode)
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(53);

            //设置参数
            service.setParams(new String[] { clientCode });

            //返回结果
            return service.execute();
        }
        #endregion

        #region 我的费用结算
        /// <summary>
        /// 我的费用结算
        /// </summary>
        /// <param name="clientCode">船代客户ID</param>
        /// <returns>XML序列串</returns>
        [WebMethod]
        public String GetChannelMyFeiYongJieSuanList(String clientCode)
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(58);

            //设置参数
            service.setParams(new String[] { clientCode });

            //返回结果
            return service.execute();
        }
        #endregion

        #endregion

        #region 结算中心

        #region 我的业务事项
        /// <summary>
        /// 我的业务事项
        /// </summary>
        /// <param name="departmentCode">公司ID</param>
        /// <returns>XML序列串</returns>
        [WebMethod]
        public String GetChannelMyYeWuShiXiangList(String departmentCode)
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(57);

            //设置参数
            service.setParams(new String[] { departmentCode });

            //返回结果
            return service.execute();
        }
        #endregion

        #region 我的已付账单
        /// <summary>
        /// 我的已付账单
        /// </summary>
        /// <param name="departmentCode">公司ID</param>
        /// <returns>XML序列串</returns>
        [WebMethod]
        public String GetChannelMyYiFuZhangDanList(String departmentCode)
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(56);

            //设置参数
            service.setParams(new String[] { departmentCode });

            //返回结果
            return service.execute();
        }
        #endregion

        #region 我的个人信息
        /// <summary>
        /// 我的个人信息
        /// </summary>
        /// <param name="userCode">用户ID</param>
        /// <returns>XML序列串</returns>
        [WebMethod]
        public String GetChannelMyGeRenXinXiList(String userCode)
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(55);

            //设置参数
            service.setParams(new String[] { userCode });

            //返回结果
            return service.execute();
        }
        #endregion

        #region 我的当年每月支付额
        #endregion

        #endregion

        #region 我的船代货代
        /// <summary>
        /// 我的船代货代
        /// </summary>
        /// <param name="userCode">用户ID</param>
        /// <returns>XML序列串</returns>
        [WebMethod]
        public String GetChannelMyChuanDaiHuoDaiList(String userCode)
        {
            //获取服务
            IBaseService service = ServiceFactory.getService(54);

            //设置参数
            service.setParams(new String[] { userCode });

            //返回结果
            return service.execute();
        }
        #endregion

        #endregion

        #endregion

        #region 推送

        #region 设备注册
        [WebMethod]
        public String DeviceResign(string token, string UserId, string DeviceToken, string DeviceType)
        {
            if (!TokenTool.VerifyToken(token))
            {
                return new Leo.Xml.Message("Token未通过校验。").FalseXml();
            }
            var sql = string.Format("select * from tb_hmw_devid where usercode='{0}'", UserId);
            var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
            if (dt.Rows.Count == 0)
            {
                sql = string.Format("insert into tb_hmw_devid (usercode,devicetoken,devicetype) values ('{0}','{1}','{2}')", UserId, DeviceToken, DeviceType);
            }
            else
            {
                sql = string.Format("update tb_hmw_devid set devicetoken='{0}' ,devicetype='{1}' where usercode='{2}'", DeviceToken, DeviceType, UserId);
            }
            dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
            sql = string.Format("select devicetoken from tb_hmw_devid where usercode='{0}'", UserId);
            dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
            if (dt.Rows[0]["devicetoken"].ToString() == DeviceToken)
            {
                return "True";
            }
            else
            {
                return "False";
            }
        }
        #endregion

        #region 消息推送
        [WebMethod]
        public String Push(string token, string UserId, string Message)
        {
            if (!TokenTool.VerifyToken(token))
            {
                return new Leo.Xml.Message("Token未通过校验。").FalseXml();
            }
            var sql = string.Format("select * from tb_hmw_devid where usercode='{0}'", UserId);
            var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
            if (dt.Rows.Count == 0)
            {
                return "ERROR";
            }
            var devicetoken = dt.Rows[0]["devicetoken"].ToString();
            var devicetype = dt.Rows[0]["devicetype"].ToString();
            switch (devicetype)
            {
                case "iOS":
                    {
                        //IOS测试devicetoken  7a6f8056710252ff8561ed1611b1b82bc398eab45b8882c12fdb143a7fb15e46
                        string URL = "http://168.100.1.218/test.php";
                        System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection(); //参数类
                        Message = HttpUtility.UrlEncode(Message); //中文UTF8编码转化
                        PostVars.Add("devicetoken", devicetoken);//参数USERid  
                        PostVars.Add("message", Message);//参数msg
                        System.Net.WebClient wb = new System.Net.WebClient();
                        byte[] byRemoteInfo = wb.UploadValues(URL, "POST", PostVars); //HTTP地址，POST请求，参数类
                        string sRemoteInfo = System.Text.Encoding.Default.GetString(byRemoteInfo);//获取返回值
                        if (sRemoteInfo == "Connected to APNS\r\nMessage successfully delivered\r\n")
                        {
                            return "True";
                        }
                        else
                        {
                            return string.Format("False");
                        }
                    }

                case "Android":
                    {
                        // 推送服务器地址
                        const String url = "http://127.0.0.1:8080/androidpn/notification.do?action=send";

                        // androidpn推送工具
                        AndroidPn androidPush = new AndroidPn(url);

                        try
                        {
                            // 单用户推送
                            androidPush.PushToSingle(devicetoken, null, Message);

                            return "True";
                        }
                        catch (Exception e)
                        {
                            // 失败原因
                            return string.Format("False");
                        }
                    }
                default:
                    return "ERROR";
            }

            try
            {
                sql = string.Format("insert into tb_hmw_messagepush (userid,message) values ('{0}','{1}')", UserId, Message);
                new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
                return "True";
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return new Leo.Xml.Message(string.Format("获取数据异常。{0}", ex.Message)).FalseXml();
            }
        }
        #endregion

        #region 消息摘要查询
        /// <summary>
        /// 消息摘要查询
        /// </summary>
        /// <param name="UserId">用户ID</param>
        /// <returns>XML序列串</returns>
        [WebMethod]
        public String SelectMessageAbstract(string token, string UserId, string minRow,string maxRow)
        {
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new Leo.Xml.Message("Token未通过校验。").FalseXml();
                }
                var sql = string.Format("select count(*) as sum from tb_hmw_messagepush where userid='{0}'", UserId);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    return new Leo.Xml.Message("无更多数据！").FalseXml();
                }
                sql = string.Format("select messid,isread,to_char(time,'MM-dd HH24:mi') as time   ,senderid,substr(message, 1,50) from (select messid,time,isread,senderid,message from (select messid,time,isread,senderid,message from tb_hmw_messagepush where userid='{0}' order by time asc ) where rownum<='{1}' order by time desc) where rownum<='{2}'", UserId, Convert.ToInt16(dt.Rows[0]["sum"]) - Convert.ToInt16(minRow) + 1, Convert.ToInt16(maxRow) - Convert.ToInt16(minRow) + 1);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    return new Leo.Xml.Message("无更多数据！").FalseXml();
                }
                var xml = new Leo.Xml.ToXml("").GetData(dt);
                xml = xml.Replace("SUBSTR(MESSAGE,1,50)", "MESSAGE");

                return new Leo.Xml.Message("查询成功！").TrueXml(xml);
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return new Leo.Xml.Message(string.Format("获取数据异常。{0}", ex.Message)).FalseXml();
            }
        }
        #endregion

        #region 消息内容查询
        /// <summary>
        /// 消息内容查询
        /// </summary>
        /// <param name="MessId">消息ID</param>
        /// <returns>XML序列串</returns>
        [WebMethod]
        public String SelectMessageContent(string token, string MessId)
        {
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new Leo.Xml.Message("Token未通过校验。").FalseXml();
                }
                var sql = string.Format("select message from tb_hmw_messagepush where messid='{0}'", MessId);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    return new Leo.Xml.Message("查询失败！").FalseXml();
                }

                sql = string.Format("update tb_hmw_messagepush set isread='1' where messid='{0}'", MessId);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);

                sql = string.Format(
                    "select * from tb_hmw_messagepush where messid='{0}' and isread='{1}'", MessId, 1);
                dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
                if (0 == dt.Rows.Count)
                {
                    return new Leo.Xml.Message("状态更新失败！").FalseXml();
                }

                var xml = new Leo.Xml.ToXml("").GetData(dt);
                return new Leo.Xml.Message("查询并更新状态成功！").TrueXml(xml);
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return new Leo.Xml.Message(string.Format("获取数据异常。{0}", ex.Message)).FalseXml();
            }
        }
        #endregion

        #endregion

        #region 业务查询

        #region 货代应用

        #region 票货管理
        /// <summary> 获取货代的票货列表。</summary>
        /// <param name="token">身份认证码</param>
        /// <param name="companyCode">作业公司ID</param>
        /// <param name="clientCode">货代客户ID</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="completeMark">票货结束标识</param>
        /// <param name="createtime">排序关键字</param>
        /// <returns>货代的票货列表</returns>
        [WebMethod]
        public string GetCcGoodsBillList(string token, string companyCode, string clientCode, string beginDate, string endDate, string completeMark, string createtime)
        {

            #region sql

            /*
             * select code_department,code_client,gbno,cargo,pack,CODE_MEASURE,inout,trade,shipname,MEASURE,PIECEWEIGHT,CODE_INOUT,createtime,mark,case sign_mark when '0' then '' when '1' then '√' end as sign,case complete_mark when '0' then '' when '1' then '√' end as complete,apartoperate from vw_qyf_goodsbillnet
             */
            /*
             * strSql = "select gbno,cargo,pack,CODE_MEASURE,inout,trade,shipname,MEASURE,PIECEWEIGHT,CODE_INOUT,createtime,mark,case sign_mark when '0' then '' when '1' then '√' end as sign,case complete_mark when '0' then '' when '1' then '√' end as complete,apartoperate from vw_qyf_goodsbillnet"

        strSql = strSql + " where code_department='" & Session("CodeCompany") & "' and code_client='" & Session("CodeClient") & "'"
        If Me.drlInout.SelectedValue <> "" Then
            strSql = strSql + " and code_inout='" & Me.drlInout.SelectedValue & "'"
        End If
        If Me.drlTrade.SelectedValue <> "" Then
            strSql = strSql + " and code_trade='" & Me.drlTrade.SelectedValue & "'"
        End If

        If Me.drlCargo.SelectedValue <> "" Then
            strSql = strSql + " and code_cargo='" & Me.drlCargo.SelectedValue & "'"

        End If

        If Me.drlPack.SelectedValue <> "" Then
            strSql = strSql + " and code_pack='" & Me.drlPack.SelectedValue & "'"
        End If
        If Me.drlComplete.SelectedValue <> "3" Then
            strSql = strSql + " and complete_mark='" & Me.drlComplete.SelectedValue & "'"

        End If
        If Me.txtStartTime.Text <> "" Then
            strSql = strSql + " and createtime>=to_date('" & Me.txtStartTime.Text & "','yyyy-mm-dd hh:mi:ss')"

        End If
        If Me.txtEndTime.Text <> "" Then
            strSql = strSql + " and createtime<=to_date('" & Me.txtEndTime.Text & "','yyyy-mm-dd')"

        End If
        If Me.txtPiCi.Text <> "" Then
            strSql = strSql + " and pici='" & Me.txtPiCi.Text.Trim() & "'"
        End If

        strSql = strSql + " order by mark asc"
             */

            #endregion

            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new CcGoodsBillList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }

                string bd = Convert.ToDateTime(beginDate).ToString("yyyy-MM-dd");
                string ed = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd");
                string mt;
                if (createtime.Length == 0)
                {
                    mt = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    mt = Convert.ToDateTime(createtime).ToString("yyyy-MM-dd HH:mm:ss");
                }
                var sql =
                    string.Format(
                        "SELECT * from(select Gbno, Cargo, Pack , Measure, Inout  , Trade  , ShipName , PieceWeight, CreateTime, Mark , COMPLETE_MARK,case sign_mark when '0' then '' when '1' then '√' end as sign  from vw_qyf_goodsbillnet where code_department='{0}' and code_client='{1}' and createtime >= to_date( '{2}','yyyy-MM-dd') and createtime <= to_date( '{3}','yyyy-MM-dd') and complete_mark ='{4}' order by createtime desc) where createtime < to_date('{5}','yyyy-mm-dd hh24:mi:ss') and rownum <= 10",
                        companyCode, clientCode, bd, ed, completeMark, mt);
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbor).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new CcGoodsBill
                            {
                                Gbno = row["gbno"] as string,
                                CargoName = row["cargo"] as string,
                                PackName = row["pack"] as string,
                                MeasureName = row["measure"] as string,
                                InoutName = row["inout"] as string,
                                TradeName = row["trade"] as string,
                                ShipName = row["shipname"] as string,
                                PieceWeight = Convert.ToDouble(row["PieceWeight"]),
                                CreateTime = Convert.ToDateTime(row["createtime"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss"),
                                Mark = row["mark"] as string,
                                SignMark = row["sign"] as string,
                                CompleteMark = row["complete_mark"] as string
                            });
                return
                    new CcGoodsBillList
                    {
                        Success = true,
                        Message = string.Empty,
                        CcGoodsBills = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new CcGoodsBillList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #region 业务大委托管理

        #region 有船作业
        /// <summary> 获取货代的有船作业申请列表。 </summary>
        /// <param name="token">身份认证码</param>
        /// <param name="clientCode">货代客户ID</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="auditMark">审核标识</param>
        /// <param name="bcno">排序关键字</param>
        /// <returns>货代的有船作业申请列表</returns>
        [WebMethod]
        public string GetCcShipBusinessConsignList(string token, string clientCode, string beginDate, string endDate, string auditMark, string bcno)
        {
            #region sql
            /*
             * select BCNO,chi_vessel,voyage,jobtype,department,from_port,to_port,case mark_audit when '1' then '已审核' when '0' then '未审核' end as mark_audit  from harbor.vw_bc_bconsign
             */
            /*
             * GID = OraDB_HB.GetRST("select code_client from tb_code_clientaccount where code_clientaccount=" & Me.LabUser.Text & "")
        '有船作业申请
        sqlstr = "select BCNO,chi_vessel,voyage,jobtype,department,from_port,to_port,case mark_audit when '1' then '已审核' when '0' then '未审核' end as mark_audit  from vw_bc_bconsign where code_client='" & GID & "'  "

        sqlstr = sqlstr + "  and Mark_audit= '" & Me.ddlMark.SelectedValue & "' "

        If Me.STA_Date.Text <> "" Then
            sqlstr = sqlstr + " and to_char(modifytime,'J')-to_char(to_date('" & Me.STA_Date.Text & "','YYYY-MM-DD'),'J') >=0  "
        End If
        If Me.End_date.Text <> "" Then
            sqlstr = sqlstr + " and to_char(modifytime,'J')-to_char(to_date('" & Me.End_date.Text & "','YYYY-MM-DD'),'J') <=0  "
        End If
        sqlstr = sqlstr + "   order by BCNO desc"
        dv = OraDB_HB.Filldata(sqlstr)
        Me.DGwcht.DataSource = dv
        Me.DGwcht.DataBind()

        
             */
            #endregion
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new CcBusinessConsignList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                string bd = Convert.ToDateTime(beginDate).ToString("yyyy-MM-dd");
                string ed = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd");
                var sql = "";
                if (bcno.Length == 0)
                {
                    sql =
                   string.Format(
                       "select * from(select bcno,chi_vessel,voyage,jobtype,department,from_port,to_port,case mark_audit when '1' then '已审核' when '0' then '未审核' end as mark_audit  from harbor.vw_bc_bconsign where code_client='{0}' and createtime >= to_date( '{1}','yyyy-MM-dd') and createtime <= to_date( '{2}','yyyy-MM-dd') and mark_audit = '{3}' order by BCNO desc)where rownum <= 10",
                        clientCode, bd, ed, auditMark);
                }
                else
                {
                    sql =
                        string.Format(
                            "select * from(select bcno,chi_vessel,voyage,jobtype,department,from_port,to_port,case mark_audit when '1' then '已审核' when '0' then '未审核' end as mark_audit  from harbor.vw_bc_bconsign where code_client='{0}' and createtime >= to_date( '{1}','yyyy-MM-dd') and createtime <= to_date( '{2}','yyyy-MM-dd') and mark_audit = '{3}' order by BCNO desc)where bcno < '{4}' and rownum <= 10",
                             clientCode, bd, ed, auditMark, bcno);
                }
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbor).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new CcBusinessConsign
                            {
                                Bcno = Convert.ToInt32(row["bcno"]),
                                ChiVessel = row["chi_vessel"] as string,
                                Voyage = row["voyage"] as string,
                                JobType = row["jobtype"] as string,
                                CompanyName = row["department"] as string,
                                FromPort = row["from_port"] as string,
                                ToPort = row["to_port"] as string,
                                AuditMark = row["mark_audit"] as string
                            });
                return
                    new CcBusinessConsignList
                    {
                        Success = true,
                        Message = string.Empty,
                        CcBusinessConsigns = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new CcBusinessConsignList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #region 无船作业
        /// <summary> 获取货代的无船作业申请列表。 </summary>
        /// <param name="token">身份认证码</param>
        /// <param name="clientCode">货代客户ID</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="auditMark">审核标识</param>
        /// <param name="bcno">排序关键字</param>
        /// <returns>货代的无船作业申请列表</returns>
        [WebMethod]
        public string GetCcNoShipBusinessConsignList(string token, string clientCode, string beginDate, string endDate, int auditMark, string bcno)
        {
            #region sql
            /*
             * select id, depid, shipname,shiphc,hwwtname, hwwtadd, zyxm,   hwjsd, hwjfd, qyg, ddg, remark,department1, case constat1 when 0 then '未批准' when 1 then '已批准' end as constat, condate from viewgoodscon
             */
            /*
             * '无船作业申请
        sqlstr = "select id, depid, shipname,shiphc,hwwtname, hwwtadd, zyxm,   hwjsd, hwjfd, qyg, ddg, remark,department1, case constat1 when 0 then '未批准' when 1 then '已批准' end as constat, condate from viewgoodscon where depid='" & GID & "'  "

        sqlstr = sqlstr + "  and constat1=" & Me.ddlMark.SelectedValue & "  and shipid=0"

        If Me.STA_Date.Text <> "" Then
            sqlstr = sqlstr + " and to_char(condate,'J')-to_char(to_date('" & Me.STA_Date.Text & "','YYYY-MM-DD'),'J') >=0  "
        End If
        If Me.End_date.Text <> "" Then
            sqlstr = sqlstr + " and to_char(condate,'J')-to_char(to_date('" & Me.End_date.Text & "','YYYY-MM-DD'),'J') <=0  "
        End If
        sqlstr = sqlstr + "   order by id desc"
        dv = OraDB_HB.Filldata(sqlstr)
             */
            #endregion

            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new CcBusinessConsignList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                string bd = Convert.ToDateTime(beginDate).ToString("yyyy-MM-dd");
                string ed = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd");
                char mark_audit = (char)auditMark;
                switch (auditMark)
                {
                    case 0: mark_audit = 'N'; break;
                    case 1: mark_audit = 'Y'; break;
                }
                var sql = "";
                if (bcno.Length == 0)
                {
                    sql =
                                            string.Format(
                                                "select * from(select id, depid, shipname,shiphc,hwwtname, hwwtadd, zyxm,hwjsd, hwjfd, qyg, ddg, remark,department1, case constat1 when 0 then '未批准' when 1 then '已批准' end as constat, condate from viewgoodscon where depid='{0}' and condate >= to_date( '{1}','yyyy-MM-dd') and condate <= to_date( '{2}','yyyy-MM-dd') and constat = '{3}' order by id desc) where rownum <= 10",
                                                 clientCode, bd, ed, mark_audit);
                }
                else
                {
                    sql =
                        string.Format(
                            "select * from(select id, depid, shipname,shiphc,hwwtname, hwwtadd, zyxm,hwjsd, hwjfd, qyg, ddg, remark,department1, case constat1 when 0 then '未批准' when 1 then '已批准' end as constat, condate from viewgoodscon where depid='{0}' and condate >= to_date( '{1}','yyyy-MM-dd') and condate <= to_date( '{2}','yyyy-MM-dd') and constat = '{3}' order by id desc)where id < '{4}' and rownum <= 10",
                             clientCode, bd, ed, mark_audit, bcno);
                }
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbor).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new CcBusinessConsign
                            {
                                Bcno = Convert.ToInt32(row["id"]),
                                ChiVessel = row["shipname"] as string,
                                Voyage = row["shiphc"] as string,
                                JobType = row["zyxm"] as string,
                                CompanyName = row["department1"] as string,
                                FromPort = row["qyg"] as string,
                                ToPort = row["ddg"] as string,
                                AuditMark = row["constat"] as string
                            });
                return
                    new CcBusinessConsignList
                    {
                        Success = true,
                        Message = string.Empty,
                        CcBusinessConsigns = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new CcBusinessConsignList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #endregion

        #region 作业委托管理
        /// <summary> 获取货代的作业委托列表。 </summary>
        /// <param name="token">身份认证码</param>
        /// <param name="companyCode">作业公司ID</param>
        /// <param name="clientCode">货代客户ID</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="createtime">排序关键字</param>
        /// <param name="completeMark">作业委托结束标识</param>
        /// <returns>货代的作业委托列表</returns>
        [WebMethod]
        public string GetCcJobConsignList(string token, string companyCode, string clientCode, string beginDate, string endDate, string completeMark, string createtime)
        {
            #region sql
            /*
             * select bino,OPERATETYPE,CLIENT,complete_mark,shipname,DEPARTMENT,PLANAMOUNT,PLANWEIGHT,PLANVOLUME,CREATETIME,TASKNO,CODE_DEPARTMENT,CODE_CLIENT,BLNO,CGNO,sign_mark, case sign_mark when '0' then '' when '1' then '√' end as sign,case complete_mark when '0' then '' when '1' then '√' end as sign1,case mark_dc when '0' then '' when '1' then '√' end as mark_dc,case mark_hy when '0' then '' when '1' then '√' end as mark_hy1  from vw_web_consign_query
             */
            /*
             * strSql = "select bino,OPERATETYPE,CLIENT,complete_mark,shipname,DEPARTMENT,PLANAMOUNT,PLANWEIGHT,PLANVOLUME,CREATETIME,TASKNO,CODE_DEPARTMENT,CODE_CLIENT,BLNO,CGNO,sign_mark, case sign_mark when '0' then '' when '1' then '√' end as sign,case complete_mark when '0' then '' when '1' then '√' end as sign1,case mark_dc when '0' then '' when '1' then '√' end as mark_dc,case mark_hy when '0' then '' when '1' then '√' end as mark_hy1"


        strSql = strSql + " from vw_web_consign_query where code_department='" & Session("CodeCompany") & "' and code_client='" & Session("CodeClient") & "' "

        If Me.drlComplete.SelectedValue <> "3" Then
            strSql = strSql + " and complete_mark='" & Me.drlComplete.SelectedValue & "'"

        End If
        If Me.CheBoxIn.Checked = True Then
            strSql = strSql + " and sign_mark='1'"
        Else
            strSql = strSql + " and sign_mark='0'"

        End If
        If Me.txtStartTime.Text <> "" Then
            strSql = strSql + " and createtime>=to_date('" & Me.txtStartTime.Text & "','yyyy-mm-dd hh:mi:ss')"

        End If
        If Me.txtEndTime.Text <> "" Then
            strSql = strSql + " and createtime<=to_date('" & Me.txtEndTime.Text & "','yyyy-mm-dd')"

        End If
        If Me.ddlaudit.SelectedValue = "0" Then
            strSql = strSql + " and bino is null"

        Else
            strSql = strSql + " and bino is not null"
        End If

        strSql = strSql + " order by createtime desc"


        dv = OraDB_HB.Filldata(strSql)
             */
            #endregion
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new CcJobConsignList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                string bd = Convert.ToDateTime(beginDate).ToString("yyyy-MM-dd");
                string ed = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd");
                string mt;
                if (createtime.Length == 0)
                {
                    mt = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    mt = Convert.ToDateTime(createtime).ToString("yyyy-MM-dd HH:mm:ss");
                }
                var sql =
                    string.Format(
                        "select bino,OPERATETYPE,CLIENT,complete_mark,shipname,DEPARTMENT,PLANAMOUNT,PLANWEIGHT,PLANVOLUME,CREATETIME,TASKNO,CODE_DEPARTMENT,CODE_CLIENT,BLNO,CGNO,sign_mark,complete_mark,mark_dc,mark_hy, case sign_mark when '0' then '' when '1' then '√' end as sign_mark1,case complete_mark when '0' then '' when '1' then '√' end as complete_mark1,case mark_dc when '0' then '' when '1' then '√' end as mark_dc1,case mark_hy when '0' then '' when '1' then '√' end as mark_hy1  from (select * from vw_web_consign_query where code_department='{0}' and code_client='{1}' and createtime >= to_date( '{2}','yyyy-MM-dd') and createtime <= to_date( '{3}','yyyy-MM-dd') and complete_mark = '{4}' order by createtime desc) where createtime <to_date('{5}','yyyy-mm-dd hh24:mi:ss') and rownum <=10",
                         companyCode, clientCode, bd, ed, completeMark, mt);
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbor).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new CcJobConsign
                            {
                                Cgno = row["cgno"] as string,
                                Bino = row["bino"] as string,
                                OperateType = row["OperateType"] as string,
                                ClientCode = row["code_client"] as string,
                                ClientName = row["client"] as string,
                                ShipName = row["ShipName"] as string,
                                CompanyCode = row["code_department"] as string,
                                CompanyName = row["department"] as string,
                                PlanAmount = Convert.ToInt16(row["PlanAmount"]),
                                PlanWeight = Convert.ToDouble(row["PlanWeight"]),
                                PlanVolume = Convert.ToDouble(row["PlanVolume"]),
                                CreateTime = Convert.ToDateTime(row["createtime"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss"),
                                TaskNo = row["TaskNo"] as string,
                                Blno = row["Blno"] as string,
                                SignMark = row["sign_mark"] as string,
                                SignDesc = row["sign_mark1"] as string,
                                CompleteMark = row["complete_mark"] as string,
                                CompleteDesc = row["complete_mark1"] as string,
                                DcMark = row["mark_dc"] as string,
                                DcDesc = row["mark_dc1"] as string,
                                HyMark = row["mark_hy"] as string,
                                HyDesc = row["mark_hy1"] as string
                            });
                return
                 new CcJobConsignList
                 {
                     Success = true,
                     Message = string.Empty,
                     CcJobConsigns = list.ToArray()
                 }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new CcJobConsignList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #region 查询车辆运输
        /// <summary> 获取货代的车辆运输列表。 </summary>
        /// <param name="token">身份认证码</param>
        /// <param name="companyCode">作业公司ID</param>
        /// <param name="clientCode">货代客户ID</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="createtime">排序关键字</param>
        /// <returns>货代的车辆运输列表</returns>
        [WebMethod]
        public string GetCcVehicleTransportList(string token, string companyCode, string clientCode, string beginDate, string endDate, string createtime)
        {
            #region sql
            /*
             * select OPERATETYPE,CLIENT,complete_mark,DEPARTMENT,PLANAMOUNT,PLANWEIGHT,PLANVOLUME,CREATETIME,TASKNO,CODE_DEPARTMENT,CODE_CLIENT,BLNO,CGNO, case net_mark when '0' then '' when '1' then '√' end as net from vw_qyf_consign  where (operatetype like '%汽%' or operatetype like '%外%') and code_client='' and complete_mark='0'
             * select OPERATETYPE,CLIENT,complete_mark,DEPARTMENT,PLANAMOUNT,PLANWEIGHT,PLANVOLUME,CREATETIME,TASKNO,CODE_DEPARTMENT,CODE_CLIENT,BLNO,CGNO, case sign_mark when '0' then '' when '1' then '√' end as net from vw_web_consign_query  where (operatetype like '%汽%' or operatetype like '%外%') and code_client='' and complete_mark='0'
             */
            /*
             * strSql = "select * from ((select OPERATETYPE,CLIENT,complete_mark,DEPARTMENT,PLANAMOUNT,PLANWEIGHT,PLANVOLUME,CREATETIME,TASKNO,CODE_DEPARTMENT,CODE_CLIENT,BLNO,CGNO, case net_mark when '0' then '' when '1' then '√' end as net from vw_qyf_consign  where (operatetype like '%汽%' or operatetype like '%外%') and code_client='" & Session("CodeClient") & "' and complete_mark='0' ) "
        strSql = strSql + "union (select OPERATETYPE,CLIENT,complete_mark,DEPARTMENT,PLANAMOUNT,PLANWEIGHT,PLANVOLUME,CREATETIME,TASKNO,CODE_DEPARTMENT,CODE_CLIENT,BLNO,CGNO, case sign_mark when '0' then '' when '1' then '√' end as net from vw_web_consign_query  where (operatetype like '%汽%' or operatetype like '%外%') and code_client='" & Session("CodeClient") & "' and complete_mark='0' ))"

        strSql = strSql + " where 2>1  "

        If Me.txtStartTime.Text <> "" Then
            strSql = strSql + " and createtime>=to_date('" & Me.txtStartTime.Text & "','yyyy-mm-dd')"

        End If
        If Me.txtEndTime.Text <> "" Then
            strSql = strSql + " and createtime<=to_date('" & Me.txtEndTime.Text & "','yyyy-mm-dd')"

        End If

        If Me.txtTask.Text <> "" Then
            strSql = strSql + " and taskno like '%" & Me.txtTask.Text.Trim & "%' "

        End If

        If Me.ddlcompany.SelectedValue <> "00" Then
            strSql = strSql + " and CODE_DEPARTMENT='" & Me.ddlcompany.SelectedValue & "' "
        End If

        strSql = strSql + " order by createtime desc"

        dv = OraDB_HB.Filldata(strSql)
             */
            #endregion
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new CcVehicleTransportList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                string bd = Convert.ToDateTime(beginDate).ToString("yyyy-MM-dd");
                string ed = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd");
                string mt;
                if (createtime.Length == 0)
                {
                    mt = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    mt = Convert.ToDateTime(createtime).ToString("yyyy-MM-dd HH:mm:ss");
                }
                var sql =
                    string.Format(
                        "select * from (select * from((select OPERATETYPE,CLIENT,complete_mark,DEPARTMENT,PLANAMOUNT,PLANWEIGHT,PLANVOLUME,CREATETIME,TASKNO,CODE_DEPARTMENT,CODE_CLIENT,BLNO,CGNO,sign_mark, case sign_mark when '0' then '' when '1' then '√' end as net,case complete_mark when '0' then '' when '1' then '√' end as complete_mark1 from vw_web_consign_query)union all(select OPERATETYPE,CLIENT,complete_mark,DEPARTMENT,PLANAMOUNT,PLANWEIGHT,PLANVOLUME,CREATETIME,TASKNO,CODE_DEPARTMENT,CODE_CLIENT,BLNO,CGNO,net_mark, case net_mark when '0' then '' when '1' then '√' end as net,case complete_mark when '0' then '' when '1' then '√' end as complete_mark1 from vw_qyf_consign)) where code_department='{0}' and code_client='{1}' and createtime >= to_date( '{2}','yyyy-MM-dd') and createtime <= to_date( '{3}','yyyy-MM-dd') order by createtime desc) where createtime <to_date('{4}','yyyy-mm-dd hh24:mi:ss') and rownum <=10",
                         companyCode, clientCode, bd, ed, mt);
                var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new CcVehicleTransport
                            {
                                Cgno = row["cgno"] as string,
                                OperateType = row["OperateType"] as string,
                                ClientCode = row["code_client"] as string,
                                ClientName = row["client"] as string,
                                CompanyCode = row["code_department"] as string,
                                CompanyName = row["department"] as string,
                                PlanAmount = Convert.ToInt16(row["PlanAmount"]),
                                PlanWeight = Convert.ToDouble(row["PlanWeight"]),
                                PlanVolume = Convert.ToDouble(row["PlanVolume"]),
                                TaskNo = row["TaskNo"] as string,
                                Blno = row["Blno"] as string,
                                CreateTime = Convert.ToDateTime(row["createtime"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss"),
                                CompleteMark = row["complete_mark"] as string,
                                CompleteDesc = row["complete_mark1"] as string,
                                NetMark = row["sign_mark"] as string,
                                NetDesc = row["net"] as string
                            });
                return
                    new CcVehicleTransportList
                    {
                        Success = true,
                        Message = string.Empty,
                        CcVehicleTransports = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new CcVehicleTransportList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #region 查询汽车衡重码单
        /// <summary> 获取货代的车辆衡重码单列表。</summary>
        /// <param name="token">身份认证码</param>
        /// <param name="companyCode">作业公司ID</param>
        /// <param name="clientCode">货代客户ID</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="signdate">排序关键字</param>
        /// <returns>货代的车辆衡重码单列表</returns>
        [WebMethod]
        public string GetCcVehicleBalanceList(string token, string companyCode, string clientCode, string beginDate, string endDate, string signdate)
        {
            #region sql
            /*
             * strSql = "select signdate,client,vgdisplay,cargo,inout,trade,taskno,cgno,pack,mark,lygcoal_mark,department from vw_gjp_consign where code_department='" & Me.ddpcom.SelectedValue & "' and code_operatetype='t07' and code_client='" & client & "' "
        If Me.STA_Date.Text <> "" Then
            strSql = strSql + " and signdate>=to_date('" & Me.STA_Date.Text & "','yyyy-mm-dd')"
        End If
        If Me.End_date.Text <> "" Then
            strSql = strSql + " and signdate<=to_date('" & Me.End_date.Text & "','yyyy-mm-dd')"
        End If
        If Me.txt_wth.Text <> "" Then
            strSql = strSql + " and taskno like '%" & txt_wth.Text.Trim & "%'"
        End If
        If Me.txt_hm.Text <> "" Then
            strSql = strSql + " and cargo like '%" & txt_hm.Text.Trim & "%'"
        End If
        strSql = strSql + " order by signdate desc"


        dv = OraDB_HB.Filldata(strSql)
             */
            #endregion
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new CcVehicleBalanceList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                string bd = Convert.ToDateTime(beginDate).ToString("yyyy-MM-dd");
                string ed = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd");
                string mt;
                if (signdate.Length == 0)
                {
                    mt = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    mt = Convert.ToDateTime(signdate).ToString("yyyy-MM-dd HH:mm:ss");
                }
                var sql =
                    string.Format(
                        "select * from (select signdate,client,vgdisplay,cargo,inout,trade,taskno,cgno,pack,mark,lygcoal_mark,department from vw_gjp_consign where code_department='{0}' and code_client='{1}' and signdate >= to_date( '{2}','yyyy-MM-dd') and signdate <= to_date( '{3}','yyyy-MM-dd') order by signdate desc)where signdate <to_date('{4}','yyyy-mm-dd hh24:mi:ss') and rownum <=10",
                         companyCode, clientCode, bd, ed, mt);
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbor).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new CcVehicleBalance
                            {
                                SignDate = Convert.ToDateTime(row["signdate"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss"),
                                ClientName = row["client"] as string,
                                VgDisplay = row["VgDisplay"] as string,
                                CargoName = row["cargo"] as string,
                                InoutName = row["inout"] as string,
                                TradeName = row["trade"] as string,
                                TaskNo = row["TaskNo"] as string,
                                Cgno = row["cgno"] as string,
                                PackName = row["pack"] as string,
                                Mark = row["mark"] as string,
                                CompanyName = row["department"] as string
                            });
                return
                    new CcVehicleBalanceList
                    {
                        Success = true,
                        Message = string.Empty,
                        CcVehicleBalances = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new CcVehicleBalanceList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #region 查询货物进出港信息

        #region 进港信息
        /// <summary>获取货代的货物进场列表。 </summary>
        /// <param name="token">身份认证码</param>
        /// <param name="companyCode">作业公司ID</param>
        /// <param name="clientCode">货代客户ID</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="signdate">排序关键字</param>
        /// <returns>货代的货物进场列表</returns>
        [WebMethod]
        public string GetCcCargoInList(string token, string companyCode, string clientCode, string beginDate, string endDate, string signdate)
        {
            #region sql
            /*
             * 
             * 
            select t.department,t.operatetype,t.cargo,t.inout,t.amount,t.weight,t.pieceweight,t.signdate from harbor.vw_stockin_web  t

             * 
             * sql = "select * from vw_stockin_web where code_client='" & client & "'"
        If Me.STA_Date.Text <> "" Then
            sql = sql + " and to_char(signdate,'J')-to_char(to_date('" & Me.STA_Date.Text & "','YYYY-MM-DD'),'J') >=0  "
        End If
        If Me.End_date.Text <> "" Then
            sql = sql + " and to_char(signdate,'J')-to_char(to_date('" & Me.End_date.Text & "','YYYY-MM-DD'),'J') <=0  "
        End If
        If Me.txt_hm.Text <> "" Then
            sql = sql + " and cargo like '%" & Me.txt_hm.Text & "%'"
        End If
        sql = sql + " and code_department='" & Me.ddpcom.SelectedValue & "' order by signdate desc"
        dv = OraDB_HB.Filldata(sql)
             */
            #endregion
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new CcCargoInOutList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                string bd = Convert.ToDateTime(beginDate).ToString("yyyy-MM-dd");
                string ed = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd");
                string mt;
                if (signdate.Length == 0)
                {
                    mt = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    mt = Convert.ToDateTime(signdate).ToString("yyyy-MM-dd HH:mm:ss");
                }
                var sql =
                    string.Format(
                        "select * from(select * from vw_stockin_web where code_department='{0}' and code_client='{1}' and signdate >= to_date( '{2}','yyyy-MM-dd') and signdate <= to_date( '{3}','yyyy-MM-dd') order by signdate desc)where signdate <to_date('{4}','yyyy-mm-dd hh24:mi:ss') and rownum <=10",
                         companyCode, clientCode, bd, ed, mt);
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbor).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new CcCargoInOut
                            {
                                CompanyName = row["department"] as string,
                                OperateType = row["OperateType"] as string,
                                CargoName = row["cargo"] as string,
                                InoutName = row["inout"] as string,
                                Amount = Convert.ToInt32(row["amount"]),
                                Weight = Convert.ToDouble(row["weight"]),
                                PieceWeight = Convert.ToDouble(row["pieceweight"]),
                                SignDate = Convert.ToDateTime(row["signdate"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss")
                            });
                return
                    new CcCargoInOutList
                    {
                        Success = true,
                        Message = string.Empty,
                        CcCargoInOuts = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new CcCargoInOutList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #region 出港信息
        /// <summary> 获取货代的货物出场列表。</summary>
        /// <param name="token">身份认证码</param>
        /// <param name="companyCode">作业公司ID</param>
        /// <param name="clientCode">货代客户ID</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="signdate">排序关键字</param>
        /// <returns>货代的货物出场列表</returns>
        [WebMethod]
        public string GetCcCargoOutList(string token, string companyCode, string clientCode, string beginDate, string endDate, string signdate)
        {
            #region sql
            /*
             *
             * select t.department,t.operatetype,t.cargo,t.inout,t.amount,t.weight,t.pieceweight,t.signdate from harbor.vw_stockout_web  t
             * 
             * sql = "select * from vw_stockout_web where code_client='" & client & "'"
        If Me.STA_Date.Text <> "" Then
            sql = sql + " and to_char(signdate,'J')-to_char(to_date('" & Me.STA_Date.Text & "','YYYY-MM-DD'),'J') >=0  "
        End If
        If Me.End_date.Text <> "" Then
            sql = sql + " and to_char(signdate,'J')-to_char(to_date('" & Me.End_date.Text & "','YYYY-MM-DD'),'J') <=0  "
        End If
        If Me.txt_hm.Text <> "" Then
            sql = sql + " and cargo like '%" & Me.txt_hm.Text & "%'"
        End If
        sql = sql + " and code_department='" & Me.ddpcom.SelectedValue & "' order by signdate desc"
        dv = OraDB_HB.Filldata(sql)
             */
            #endregion
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new CcCargoInOutList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                string bd = Convert.ToDateTime(beginDate).ToString("yyyy-MM-dd");
                string ed = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd");
                string mt;
                if (signdate.Length == 0)
                {
                    mt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    mt = Convert.ToDateTime(signdate).ToString("yyyy-MM-dd HH:mm:ss");
                }
                var sql =
                    string.Format(
                        "select * from(select * from vw_stockout_web where code_department='{0}' and code_client='{1}' and signdate >= to_date( '{2}','yyyy-MM-dd') and signdate <= to_date( '{3}','yyyy-MM-dd') order by signdate desc)where signdate <to_date('{4}','yyyy-mm-dd hh24:mi:ss') and rownum <=10",
                         companyCode, clientCode, bd, ed, mt);
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbor).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new CcCargoInOut
                            {
                                CompanyName = row["department"] as string,
                                OperateType = row["OperateType"] as string,
                                CargoName = row["cargo"] as string,
                                InoutName = row["inout"] as string,
                                Amount = Convert.ToInt32(row["amount"]),
                                Weight = Convert.ToDouble(row["weight"]),
                                PieceWeight = Convert.ToDouble(row["pieceweight"]),
                                SignDate = Convert.ToDateTime(row["signdate"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss")
                            });
                return
                    new CcCargoInOutList
                    {
                        Success = true,
                        Message = string.Empty,
                        CcCargoInOuts = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new CcCargoInOutList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #endregion

        #region 查询货物港内结存
        /// <summary>获取货代的货物港存列表。 </summary>
        /// <param name="token">身份认证码</param>
        /// <param name="companyCode">作业公司ID</param>
        /// <param name="clientCode">货代客户ID</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="signdate">排序关键字</param>
        /// <returns>货代的货物港存列表</returns>
        [WebMethod]
        public string GetCcCargoStockList(string token, string companyCode, string clientCode, string beginDate, string endDate, string signdate)
        {
            #region sql
            /*
             * sql = "select * from vw_stockdormant_web where code_client='" & client & "'"
        If Me.STA_Date.Text <> "" Then
            sql = sql + " and to_char(signdate,'J')-to_char(to_date('" & Me.STA_Date.Text & "','YYYY-MM-DD'),'J') >=0  "
        End If
        If Me.End_date.Text <> "" Then
            sql = sql + " and to_char(signdate,'J')-to_char(to_date('" & Me.End_date.Text & "','YYYY-MM-DD'),'J') <=0  "
        End If
        If Me.txt_hm.Text <> "" Then
            sql = sql + " and cargo like '%" & Me.txt_hm.Text & "%'"
        End If
        If Me.txt_kcm.Text <> "" Then
            sql = sql + " and STORAGE like '%" & Me.txt_kcm.Text & "%'"
        End If
        sql = sql + " and code_department='" & Me.ddpcom.SelectedValue & "' order by signdate desc"
        dv = OraDB_HB.Filldata(sql)
             */
            #endregion
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new CcCargoStockList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                string bd = Convert.ToDateTime(beginDate).ToString("yyyy-MM-dd");
                string ed = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd");
                string mt;
                if (signdate.Length == 0)
                {
                    mt = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    mt = Convert.ToDateTime(signdate).ToString("yyyy-MM-dd HH:mm:ss");
                }
                var sql =
                    string.Format(
                        "select * from(select * from vw_stockdormant_web where code_department='{0}' and code_client='{1}' and signdate >= to_date( '{2}','yyyy-MM-dd') and signdate <= to_date( '{3}','yyyy-MM-dd') order by signdate desc) where signdate <to_date('{4}','yyyy-mm-dd hh24:mi:ss') and rownum <=10",
                         companyCode, clientCode, bd, ed, mt);
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbor).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new CcCargoStock
                            {
                                CompanyName = row["department"] as string,
                                SectionName = row["section"] as string,
                                InoutName = row["inout"] as string,
                                TradeName = row["trade"] as string,
                                StorageName = row["storage"] as string,
                                CargoName = row["cargo"] as string,
                                Amount = Convert.ToDouble(row["amount"]),
                                Weight = Convert.ToDouble(row["weight"]),
                                Volume = Convert.ToString(row["volume"]),
                                SignDate = Convert.ToDateTime(row["signdate"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss")
                            });
                return
                    new CcCargoStockList
                    {
                        Success = true,
                        Message = string.Empty,
                        CcCargoStocks = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new CcCargoStockList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #endregion

        #region 船代应用

        #region 查询船舶预报信息
        /// <summary>获取船代的预报船舶列表。</summary>
        /// <param name="token">身份认证码</param>
        /// <param name="clientCode">船代客户ID</param>
        /// <param name="ForeArriveTime">排序关键字</param>
        /// <returns>船代的预报船舶列表</returns>
        [WebMethod]
        public string GetScForeShipList(string token, string clientCode, string ForeArriveTime)
        {
            #region sql
            /*
             * sqlstr = "select * from view_sship_tan where Agent=" & GID & " and SHIP_STATU = 0"
        dv = OraDB_BR.Filldata(sqlstr)
             */
            #endregion
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new ScForeShipList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                string mt;
                if (ForeArriveTime.Length == 0)
                {
                    mt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    mt = Convert.ToDateTime(ForeArriveTime).ToString("yyyy-MM-dd HH:mm:ss");
                }
                var sql =
                    string.Format(
                        "select * from(select * from view_sship_tan where  agent='{0}' order by yjdg desc)where yjdg <to_date('{1}','yyyy-mm-dd hh24:mi:ss') and rownum <=10",
                          clientCode, mt);
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathBase).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathBases).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new ScForeShip
                            {
                                ShipId = row["ship_id"] as string,
                                ShipNameCn = row["chi_vessel"] as string,
                                ForeArriveTime = Convert.ToDateTime(row["yjdg"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss"),
                                UnloadCargoName = row["zhmc"] as string,
                                UnloadWeight = Convert.ToString((row["zhsl"])),
                                LoadCargoName = row["xhmc"] as string,
                                LoadWeight = Convert.ToString((row["xhsl"])),
                                DivMark = row["ys"] as string
                            });
                return
                    new ScForeShipList
                    {
                        Success = true,
                        Message = string.Empty,
                        ScForeShips = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new ScForeShipList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #region 查询船舶确报信息
        /// <summary>获取船代的确报船舶列表。 </summary>
        /// <param name="token">身份认证码</param>
        /// <param name="clientCode">船代客户ID</param>
        /// <param name="DeclareTime">排序关键字</param>
        /// <returns>船代的确报船舶列表</returns>
        [WebMethod]
        public string GetScIndeedShipList(string token, string clientCode, string DeclareTime)
        {
            #region sql
            /*
             * Dim sqlstr As String
        Dim dv As DataView
        Dim a4, a2, a1 As Date
        a4 = DateAdd("d", -4, Now)
        a2 = DateAdd("d", -2, Now)
        a1 = DateAdd("d", -1, Now)
        sqlstr = "select * from view_sship_tan where Agent=" & GID & " and (SHIP_STATU = 7 or SHIP_STATU = 1)"
        'sqlstr = "select * from view_sship_tan where Agent=" & GID & " and SHIP_STATU = 7 and ((loa>260 and s_declare<=to_date('" & a4 & "','yyyy-MM-dd hh24:mi:ss')) or (loa<=260 and loa>180 and s_declare<=to_date('" & a2 & "','yyyy-MM-dd hh24:mi:ss')) or (loa<=180) or (code_vesseltype='5')) union select * from view_sship_tan where Agent=" & GID & " and ship_statu=1 and ((loa>260 and s_declare<=to_date('" & a2 & "','yyyy-MM-dd hh24:mi:ss')) or (loa<=260 and s_declare<=to_date('" & a1 & "','yyyy-MM-dd hh24:mi:ss')) or (code_vesseltype='5') )"
        dv = OraDB_BR.Filldata(sqlstr)
             */
            #endregion
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new ScIndeedShipList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                string mt;
                if (DeclareTime.Length == 0)
                {
                    mt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    mt = Convert.ToDateTime(DeclareTime).ToString("yyyy-MM-dd HH:mm:ss");
                }
                var sql =
                    string.Format(
                        "select * from(select * from view_sship_tan where  (SHIP_STATU = 7 or SHIP_STATU = 1) order by S_declare desc)where S_declare <to_date('{0}','yyyy-mm-dd hh24:mi:ss') and rownum <=10",
                           mt);
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathBase).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathBases).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new ScIndeedShip
                            {
                                ShipId = row["ship_id"] as string,
                                ShipNameCn = row["chi_vessel"] as string,
                                IndeedTime = Convert.ToDateTime(row["yjdg"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss"),
                                UnloadCargoName = row["zhmc"] as string,
                                UnloadWeight = Convert.ToString((row["zhsl"])),
                                LoadCargoName = row["xhmc"] as string,
                                LoadWeight = Convert.ToString((row["xhsl"])),
                                DeclareTime = Convert.ToDateTime(row["S_declare"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss"),
                                ShipStatus = row["shipstatus"] as string,
                                DivMark = row["ys"] as string
                            });
                return
                    new ScIndeedShipList
                    {
                        Success = true,
                        Message = string.Empty,
                        ScIndeedShips = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new ScIndeedShipList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #region 查询锚地船舶信息
        /// <summary>获取船代的锚地船舶列表。 </summary>
        /// <param name="token">身份认证码</param>
        /// <param name="clientCode">船代客户ID</param>
        /// <param name="ArriveTime">排序关键字</param>
        /// <returns>船代的锚地船舶列表</returns>
        [WebMethod]
        public string GetScAnchorShipList(string token, string clientCode, string ArriveTime)
        {
            #region sql
            /*
             * Dim sqlstr As String
        Dim dv As DataView
        Dim a4, a2, a1, a6, a3 As Date
        a4 = DateAdd("d", -4, Now)
        a2 = DateAdd("d", -2, Now)
        a1 = DateAdd("d", -1, Now)
        a3 = DateAdd("d", -3, Now)
        a6 = DateAdd("d", -6, Now)
        'sqlstr = "select * from view_sship_tan where ship_statu=1 and ((loa>260 and s_declare<=to_date('" & a2 & "','yyyy-MM-dd hh24:mi:ss')) or (loa<=260 and s_declare<=to_date('" & a1 & "','yyyy-MM-dd hh24:mi:ss')) or (code_vesseltype='5') )"
        sqlstr = "select * from view_sship_tan where ship_statu=1 and ((loa>260 and s_declare<=to_date('" & a6 & "','yyyy-MM-dd hh24:mi:ss')) or (loa<=260 and loa>180 and s_declare<=to_date('" & a3 & "','yyyy-MM-dd hh24:mi:ss')) or (loa<=180 and s_declare<=to_date('" & a1 & "','yyyy-MM-dd hh24:mi:ss')) or (code_vesseltype='5') or (ship_id=318013) or (ship_id=318012)or (ship_id=318022) or (tsqk=1))"
        dv = OraDB_BR.Filldata(sqlstr)
             */
            #endregion
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new ScAnchorShipList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                string mt = "";
                if (ArriveTime.Length == 0)
                {
                    mt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    mt = Convert.ToDateTime(ArriveTime).ToString("yyyy-MM-dd HH:mm:ss");
                }
                var sql =
                    string.Format(
                        "select * from(select * from view_sship_tan where ship_statu=1  order by s_declare desc)where s_declare <to_date('{0}','yyyy-mm-dd hh24:mi:ss') and rownum <=10",
                           mt);
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathBase).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathBases).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new ScAnchorShip
                            {
                                ShipId = row["ship_id"] as string,
                                ShipNameCn = row["chi_vessel"] as string,
                                Nation = row["nation_cha"] as string,
                                ShipLength = Convert.ToDouble(row["loa"]),
                                TradeName = row["trade"] as string,
                                InDraft = Convert.ToDouble(row["THIS_DRAFT"]),
                                OutDraft = Convert.ToDouble(row["CHU_DRAFT"]),
                                UnloadCargoName = row["zhmc"] as string,
                                UnloadWeight = Convert.ToString((row["zhsl"])),
                                LoadCargoName = row["xhmc"] as string,
                                LoadWeight = Convert.ToString((row["xhsl"])),
                                ArriveTime = Convert.ToDateTime(row["S_declare"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss"),
                                ClientName = row["name"] as string,
                                DivMark = row["ys"] as string
                            });
                return
                    new ScAnchorShipList
                    {
                        Success = true,
                        Message = string.Empty,
                        ScAnchorShips = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new ScAnchorShipList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #region 查询泊位船舶信息
        /// <summary> 获取船代的泊位船舶列表。</summary>
        /// <param name="token">身份认证码</param>
        /// <param name="clientCode">船代客户ID</param>
        /// <param name="berthno">排序关键字</param>
        /// <returns>船代的泊位船舶列表</returns>
        [WebMethod]
        public string GetScBerthShipList(string token, string clientCode, string berthno)
        {
            #region sql
            /*
             * Dim sqlstr As String
        Dim dv As DataView
        sqlstr = "select * from view_sship_tan where ship_statu=2 or ship_statu=4 or ship_statu=5 or ship_statu=3 order by berthno "
        dv = OraDB_BR.Filldata(sqlstr)
             */
            #endregion
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new ScBerthShipList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                var sql = "";
                if (berthno.Length == 0)
                {
                    sql =
                       string.Format(
                           "select * from (select * from view_sship_tan where (ship_statu=2 or ship_statu=4 or ship_statu=5 or ship_statu=3)   order by berthno)where rownum<=10"
                           );
                }
                else
                {
                    sql =
                        string.Format(
                            "select * from (select * from view_sship_tan where agent = '{0}' and (ship_statu=2 or ship_statu=4 or ship_statu=5 or ship_statu=3)   order by berthno)where berthno > '{1}' and rownum<=10",
                            clientCode, berthno);
                }
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathBase).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathBases).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new ScBerthShip
                            {
                                ShipId = row["ship_id"] as string,
                                ShipNameCn = row["chi_vessel"] as string,
                                BerthNo = row["BERTHNO"] as string,
                                BerthPos = row["BERTH_POSITION"] as string,
                                UnloadCargoName = row["zhmc"] as string,
                                UnloadWeight = Convert.ToString((row["zhsl"])),
                                LoadCargoName = row["xhmc"] as string,
                                LoadWeight = Convert.ToString((row["xhsl"])),
                                ArriveTime = Convert.ToDateTime(row["BerTH_time"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss"),
                                RestWeight = Convert.ToString((row["dqyd"])),
                                ShipStatus = row["SHIPSTATUS"] as string,
                                ClientName = row["name"] as string,
                                CompanyName = row["DepartMent"] as string
                            });
                return
                    new ScBerthShipList
                    {
                        Success = true,
                        Message = string.Empty,
                        ScBerthShips = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new ScBerthShipList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #region 查询船舶进出港计划

        #region 已做计划船舶信息列表
        /// <summary>获取已做计划船舶信息列表。</summary>
        /// <param name="token">身份认证码</param>
        /// <param name="PlanId">排序关键字</param>
        /// <returns>已做计划船舶信息列表</returns>
        [WebMethod]
        public string GetScPlanedShipList(string token, string PlanId)
        {
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new ScPlanedShipList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                string sql = "";
                if (PlanId.Length == 0)
                {
                    sql =
                    string.Format(
                        "select * from(select * from ywcplan order by p_id desc)where rownum <=10");
                }
                else
                {
                    sql =
                    string.Format(
                        "select * from(select * from ywcplan order by p_id desc)where p_id <{0} and rownum <=10", PlanId);
                }

                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathBase).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathBases).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new ScPlanedShip
                            {
                                Berth = string.Format("{0}/{1}", Convert.ToString(row["dqbw"]), Convert.ToString(row["bwwz"])),
                                ShipNameCn = row["chi_vessel"] as string,
                                PlanId = Convert.ToInt32(row["p_id"]),
                                UnloadCargoName = row["xhm"] as string,
                                UnloadWeight = Convert.ToString(row["xhsl"]),
                                LoadCargoName = row["zhm"] as string,
                                LoadWeight = Convert.ToString(row["zhsl"]),
                                InPortTime = Convert.ToString((row["yjjg"]) as Nullable<DateTime>),
                                OutPortTime = Convert.ToString((row["yjlg"]) as Nullable<DateTime>),
                                Remark = row["remark"] as string,
                                Pc = row["pc"] as string,
                                Client = row["cbdl"] as string
                            });
                return
                    new ScPlanedShipList
                    {
                        Success = true,
                        Message = string.Empty,
                        ScPlanedShips = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new ScPlanedShipList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #region 需要移泊船舶信息列表
        /// <summary> 获取需要移泊船舶信息列表。</summary>
        /// <param name="token">身份认证码</param>
        /// <param name="PlanTime">排序关键字</param>
        /// <returns>需要移泊船舶信息列表</returns>
        [WebMethod]
        public string GetScMoveShipList(string token, string PlanTime)
        {
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new ScMoveShipList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                string mt = "";
                if (PlanTime.Length == 0)
                {
                    mt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    mt = Convert.ToDateTime(PlanTime).ToString("yyyy-MM-dd HH:mm:ss");
                }
                var sql =
                    string.Format(
                        "select * from(select * from  YwcPlanYB order by yjyb desc)where yjyb <to_date('{0}','yyyy-mm-dd hh24:mi:ss') and rownum <=10", mt);
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathBase).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathBases).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new ScMoveShip
                            {
                                Berth = string.Format("{0}/{1},", Convert.ToString(row["dqbw"]), Convert.ToString(row["bwwz"])),
                                ShipNameCn = row["chi_vessel"] as string,
                                PlanTime = Convert.ToDateTime(row["yjyb"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss"),
                                PlanBerth = string.Format("{0}/{1},", Convert.ToString(row["ybbw"]), Convert.ToString(row["ybbwwz"])),
                                PlanOutPort = row["yblg"] as string,
                                Remark = row["remark"] as string,
                                PC = row["pc"] as string
                            });
                return
                    new ScMoveShipList
                    {
                        Success = true,
                        Message = string.Empty,
                        ScMoveShips = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new ScMoveShipList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #endregion

        #region 查询引航费用信息
        /// <summary>获取引航费用信息。 </summary>
        /// <param name="token">身份认证码</param>
        /// <param name="clientCode">船代公司ID</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="Invoicedate">排序关键字</param>
        /// <returns>引航费用信息</returns>
        [WebMethod]
        public string GetScPilotageFeeList(string token, string clientCode, string beginDate,
            string endDate, string Invoicedate)
        {
            try
            {
                string bd = Convert.ToDateTime(beginDate).ToString("yyyy-MM-dd");
                string ed = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd");
                string mt = "";
                if (Invoicedate.Length == 0)
                {
                    mt = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    mt = Convert.ToDateTime(Invoicedate).ToString("yyyy-MM-dd HH:mm:ss");
                }
                if (!TokenTool.VerifyToken(token))
                {
                    return new ScPilotageFeeList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                var sql =
                    string.Format(
                        "select * from(select viewworkbill.CHI_VESSEL,viewworkbill.FCODE,invoice.invoicenum,ton_net,amount,invoicedate,invoice.fkdw,invoice.sid,case when invoice.fk='0' then '否' when invoice.fk='1' then '是' end as fk,case when invoice.fkdjh='00000' then '    ' else invoice.fkdjh end as fkdjh  from inhual.viewworkbill,inhual.invoice  where viewworkbill.sid=invoice.sid and viewworkbill.agent = '{0}' and invoicedate >=to_date( '{1}','yyyy-MM-dd') and invoicedate <= to_date( '{2}','yyyy-MM-dd') order by invoicedate desc)where invoicedate <to_date('{3}','yyyy-mm-dd hh24:mi:ss') and rownum <=10",
                          clientCode, bd, ed, mt);
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathMa).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new ScPilotageFee
                            {
                                Invoicenum = row["invoicenum"] as string,
                                ShipNameCn = row["chi_vessel"] as string,
                                Tons = Convert.ToDouble(row["ton_net"]),
                                Fcode = row["fcode"] as string,
                                Amount = Convert.ToDouble(row["amount"]),
                                Invoicedate = Convert.ToDateTime(row["invoicedate"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss"),
                                Fk = string.Format("{0}{1}", Convert.ToString(row["fk"]), Convert.ToString(row["fkdjh"])),
                                Fkdw = row["fkdw"] as string
                            });
                return
                    new ScPilotageFeeList
                    {
                        Success = true,
                        Message = string.Empty,
                        ScPilotageFees = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new ScPilotageFeeList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #region 查询高频话费信息
        /// <summary> 获取船代的泊位船舶列表。</summary>
        /// <param name="token">身份认证码</param>
        /// <param name="clientCode">船代客户ID</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="StartTime">排序关键字</param>
        /// <returns>船代的泊位船舶列表</returns>
        [WebMethod]
        public string GetScCommunicationFeeList(string token, string clientCode, string beginDate,
            string endDate, string StartTime)
        {
            try
            {
                string bd = Convert.ToDateTime(beginDate).ToString("yyyy-MM-dd");
                string ed = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd");
                string mt = "";
                if (StartTime.Length == 0)
                {
                    mt = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    mt = Convert.ToDateTime(StartTime).ToString("yyyy-MM-dd HH:mm:ss");
                }
                if (!TokenTool.VerifyToken(token))
                {
                    return new ScCommunicationFeeList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                var sql =
                    string.Format(
                        "select * from(select * from ddmis.viewtxgpinfo where agent = '{0}' and sta_time >=to_date( '{1}','yyyy-MM-dd') and sta_time <= to_date( '{2}','yyyy-MM-dd') order by sta_time desc)where sta_time <to_date('{3}','yyyy-mm-dd hh24:mi:ss') and rownum <=10",
                          clientCode, bd, ed, mt);
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathMa).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new ScCommunicationFee
                            {
                                ShipNameCn = row["chi_vessel"] as string,
                                StartTime = Convert.ToDateTime(row["sta_time"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss"),
                                EndTime = Convert.ToDateTime(row["end_time"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss"),
                                Sender = row["fht"] as string,
                                Receiver = row["sht"] as string,
                                Time = Convert.ToInt32(row["fzs"]),
                                Cost = Convert.ToDouble(row["money"]),
                                InvoiceNum = row["INVOICENUM"] as string,
                                Client = row["name"] as string,
                                Company = row["fkdw"] as string
                            });
                return
                    new ScCommunicationFeeList
                    {
                        Success = true,
                        Message = string.Empty,
                        ScCommunicationFees = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new ScCommunicationFeeList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #endregion

        #region 货主应用

        #region 货源管理
        /// <summary>获取货主的货源列表。</summary>
        /// <param name="token">身份认证码</param>
        /// <param name="clientCode">货主ID</param>
        /// <param name="DeclarationTime">排序关键字</param>
        /// <returns>货主的货源列表</returns>
        [WebMethod]
        public string GetCoCargoSupplyList(string token, string clientCode, string DeclarationTime)
        {
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new CoCargoSupplyList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                string mt = "";
                if (DeclarationTime.Length == 0)
                {
                    mt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    mt = Convert.ToDateTime(DeclarationTime).ToString("yyyy-MM-dd HH:mm:ss");
                }
                var sql =
                    string.Format(
                        "select * from(select * from ddmis.view_hy where id='{0}' order by sbrq desc)where sbrq <to_date('{1}','yyyy-mm-dd hh24:mi:ss') and rownum <=10",
                          clientCode, mt);
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathMa).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new CoCargoSupply
                            {
                                SupplyCompany = row["hydw"] as string,
                                MainObject = row["hyzy"] as string,
                                Contacts = row["hylxr"] as string,
                                Supplier = row["TRUENAME"] as string,
                                Tel = row["tel"] as string,
                                DeclarationTime = Convert.ToDateTime(row["sbrq"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss"),
                                Company = row["department"] as string
                            });
                return
                    new CoCargoSupplyList
                    {
                        Success = true,
                        Message = string.Empty,
                        CoCargoSupplys = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new CoCargoSupplyList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #region 查询货物进出港信息

        #region 货物进港动态信息
        /// <summary>获取货物进港列表。</summary>
        /// <param name="token">身份认证码</param>
        /// <param name="clientCode">货主ID</param>
        /// <param name="signdate">排序关键字</param>
        /// <returns>货物进港列表</returns>
        [WebMethod]
        public string GetCoCargoInList(string token, string clientCode, string signdate)
        {
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new CoCargoInList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                string mt = "";
                if (signdate.Length == 0)
                {
                    mt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    mt = Convert.ToDateTime(signdate).ToString("yyyy-MM-dd HH:mm:ss");
                }
                var sql =
                    string.Format(
                        "select * from(select * from vw_stockin_web where code_client='{0}' order by signdate desc)where signdate <to_date('{1}','yyyy-mm-dd hh24:mi:ss') and rownum <=10",
                          clientCode, mt);
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbor).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new CoCargoIn
                            {
                                Company = row["department"] as string,
                                OperateType = row["operatetype"] as string,
                                Cargo = row["Cargo"] as string,
                                InOut = row["inout"] as string,
                                Amount = Convert.ToInt32(row["amount"]),
                                Weight = Convert.ToString(Convert.ToDouble(row["weight"])),
                                PieceWeight = Convert.ToString(Convert.ToDouble(row["pieceweight"])),
                                SignDate = Convert.ToDateTime(row["signdate"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss")
                            });
                return
                    new CoCargoInList
                    {
                        Success = true,
                        Message = string.Empty,
                        CoCargoIns = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new CoCargoInList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }

        #endregion

        #region 货物出港动态信息
        /// <summary>获取货物出港列表。</summary>
        /// <param name="token">身份认证码</param>
        /// <param name="clientCode">货主ID</param>
        /// <param name="berthno">排序关键字</param>
        /// <returns>货物出港列表</returns>
        [WebMethod]
        public string GetCoCargoOutList(string token, string clientCode, string SignDate)
        {
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new CoCargoOutList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                string mt = "";
                if (SignDate.Length == 0)
                {
                    mt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    mt = Convert.ToDateTime(SignDate).ToString("yyyy-MM-dd HH:mm:ss");
                }
                var sql =
                    string.Format(
                        "select * from(select * from vw_stockout_web where code_client='{0}' order by signdate desc)where signdate <to_date('{1}','yyyy-mm-dd hh24:mi:ss') and rownum <=10",
                          clientCode, mt);
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbor).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new CoCargoOut
                            {
                                Company = row["department"] as string,
                                OperateType = row["operatetype"] as string,
                                Cargo = row["Cargo"] as string,
                                InOut = row["inout"] as string,
                                Amount = Convert.ToInt32(row["amount"]),
                                Weight = Convert.ToString(Convert.ToDouble(row["weight"])),
                                PieceWeight = Convert.ToString(Convert.ToDouble(row["pieceweight"])),
                                SignDate = Convert.ToDateTime(row["signdate"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss")
                            });
                return
                    new CoCargoOutList
                    {
                        Success = true,
                        Message = string.Empty,
                        CoCargoOuts = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new CoCargoOutList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }

        #endregion

        #region 货物兑动态信息
        /// <summary>获取货物兑动态信息。</summary>
        /// <param name="token">身份认证码</param>
        /// <param name="clientCode">货主ID</param>
        /// <param name="SignDate">排序关键字</param>
        /// <returns>货物兑动态信息</returns>
        [WebMethod]
        public string GetCoCargoDuiList(string token, string clientCode, string SignDate)
        {
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new CoCargoDuiList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                string mt = "";
                if (SignDate.Length == 0)
                {
                    mt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    mt = Convert.ToDateTime(SignDate).ToString("yyyy-MM-dd HH:mm:ss");
                }
                var sql =
                    string.Format(
                        "select * from(select * from vw_stockdui_web where code_client='{0}' order by signdate desc)where SignDate <to_date('{1}','yyyy-mm-dd hh24:mi:ss') and rownum <=10",
                          clientCode, mt);
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbor).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new CoCargoDui
                            {
                                Company = row["department"] as string,
                                OperateType = row["operatetype"] as string,
                                Cargo = row["Cargo"] as string,
                                InOut = row["inout"] as string,
                                Voyage = row["vgdisplay"] as string,
                                Amount = Convert.ToInt32(row["amount"]),
                                Weight = Convert.ToString(Convert.ToDouble(row["weight"])),
                                PieceWeight = Convert.ToString(Convert.ToDouble(row["pieceweight"])),
                                SignDate = Convert.ToDateTime(row["SignDate"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss"),
                            });
                return
                    new CoCargoDuiList
                    {
                        Success = true,
                        Message = string.Empty,
                        CoCargoDuis = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new CoCargoDuiList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }

        #endregion

        #endregion

        #region 查询货物港内结存信息
        /// <summary>货物港内结存信息。</summary>
        /// <param name="token">身份认证码</param>
        /// <param name="clientCode">货主ID</param>
        /// <param name="signdate">排序关键字</param>
        /// <returns>货物港内结存信息</returns>
        [WebMethod]
        public string GetCoCargoBalanceList(string token, string clientCode, string signdate)
        {
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new CoCargoBalanceList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                string mt = "";
                if (signdate.Length == 0)
                {
                    mt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    mt = Convert.ToDateTime(signdate).ToString("yyyy-MM-dd HH:mm:ss");
                }
                var sql =
                    string.Format(
                        "select * from(select * from vw_stockdormant_web where code_client='{0}' order by signdate desc)where signdate <to_date('{1}','yyyy-mm-dd hh24:mi:ss') and rownum <=10",
                          clientCode, mt);
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbor).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new CoCargoBalance
                            {
                                Company = row["department"] as string,
                                Section = row["Section"] as string,
                                InOutAndTrade = string.Format("{0}/{1}", Convert.ToString(row["inout"]), Convert.ToString(row["trade"])),
                                StorageName = row["storage"] as string,
                                Cargo = row["Cargo"] as string,
                                Amount = Convert.ToInt32(row["amount"]),
                                Weight = Convert.ToString(Convert.ToDouble(row["weight"])),
                                Volume = row["volume"] as string,
                                SignDate = Convert.ToDateTime(row["signdate"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss")
                            });
                return
                    new CoCargoBalanceList
                    {
                        Success = true,
                        Message = string.Empty,
                        CoCargoBalances = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new CoCargoBalanceList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }

        #endregion

        #region 查询船舶确报信息
        /// <summary>获取船代的确报船舶列表。 </summary>
        /// <param name="token">身份认证码</param>
        /// <param name="clientCode">船代客户ID</param>
        /// <param name="DeclareTime">排序关键字</param>
        /// <returns>船代的确报船舶列表</returns>
        [WebMethod]
        public string GetCoIndeedShipList(string token, string clientCode, string DeclareTime)
        {
            #region sql
            /*
             * Dim sqlstr As String
        Dim dv As DataView
        Dim a4, a2, a1 As Date
        a4 = DateAdd("d", -4, Now)
        a2 = DateAdd("d", -2, Now)
        a1 = DateAdd("d", -1, Now)
        sqlstr = "select * from view_sship_tan where Agent=" & GID & " and (SHIP_STATU = 7 or SHIP_STATU = 1)"
        'sqlstr = "select * from view_sship_tan where Agent=" & GID & " and SHIP_STATU = 7 and ((loa>260 and s_declare<=to_date('" & a4 & "','yyyy-MM-dd hh24:mi:ss')) or (loa<=260 and loa>180 and s_declare<=to_date('" & a2 & "','yyyy-MM-dd hh24:mi:ss')) or (loa<=180) or (code_vesseltype='5')) union select * from view_sship_tan where Agent=" & GID & " and ship_statu=1 and ((loa>260 and s_declare<=to_date('" & a2 & "','yyyy-MM-dd hh24:mi:ss')) or (loa<=260 and s_declare<=to_date('" & a1 & "','yyyy-MM-dd hh24:mi:ss')) or (code_vesseltype='5') )"
        dv = OraDB_BR.Filldata(sqlstr)
             */
            #endregion
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new CoIndeedShipList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                string mt = "";
                if (DeclareTime.Length == 0)
                {
                    mt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    mt = Convert.ToDateTime(DeclareTime).ToString("yyyy-MM-dd HH:mm:ss");
                }
                var sql =
                    string.Format(
                        "select * from(select * from view_sship_tan where   (SHIP_STATU = 7 or SHIP_STATU = 1) order by S_declare desc)where S_declare <to_date('{0}','yyyy-mm-dd hh24:mi:ss') and rownum <=10",
                           mt);
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathBase).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new CoIndeedShip
                            {
                                ShipId = Convert.ToString((row["ship_id"])),
                                ShipNameCn = row["chi_vessel"] as string,
                                IndeedTime = row["yjdg"] as string,
                                UnloadCargoName = row["zhmc"] as string,
                                UnloadWeight = Convert.ToString((row["zhsl"])),
                                LoadCargoName = row["xhmc"] as string,
                                LoadWeight = Convert.ToString((row["xhsl"])),
                                DeclareTime = Convert.ToDateTime(row["S_declare"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss"),
                                ShipStatus = row["shipstatus"] as string,
                                DivMark = row["ys"] as string
                            });
                return
                    new CoIndeedShipList
                    {
                        Success = true,
                        Message = string.Empty,
                        CoIndeedShips = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new CoIndeedShipList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #region 查询锚地船舶信息
        /// <summary>获取船代的锚地船舶列表。 </summary>
        /// <param name="token">身份认证码</param>
        /// <param name="clientCode">船代客户ID</param>
        /// <param name="ArriveTime">排序关键字</param>
        /// <returns>船代的锚地船舶列表</returns>
        [WebMethod]
        public string GetCoAnchorShipList(string token, string clientCode, string ArriveTime)
        {
            #region sql
            /*
             * Dim sqlstr As String
        Dim dv As DataView
        Dim a4, a2, a1, a6, a3 As Date
        a4 = DateAdd("d", -4, Now)
        a2 = DateAdd("d", -2, Now)
        a1 = DateAdd("d", -1, Now)
        a3 = DateAdd("d", -3, Now)
        a6 = DateAdd("d", -6, Now)
        'sqlstr = "select * from view_sship_tan where ship_statu=1 and ((loa>260 and s_declare<=to_date('" & a2 & "','yyyy-MM-dd hh24:mi:ss')) or (loa<=260 and s_declare<=to_date('" & a1 & "','yyyy-MM-dd hh24:mi:ss')) or (code_vesseltype='5') )"
        sqlstr = "select * from view_sship_tan where ship_statu=1 and ((loa>260 and s_declare<=to_date('" & a6 & "','yyyy-MM-dd hh24:mi:ss')) or (loa<=260 and loa>180 and s_declare<=to_date('" & a3 & "','yyyy-MM-dd hh24:mi:ss')) or (loa<=180 and s_declare<=to_date('" & a1 & "','yyyy-MM-dd hh24:mi:ss')) or (code_vesseltype='5') or (ship_id=318013) or (ship_id=318012)or (ship_id=318022) or (tsqk=1))"
        dv = OraDB_BR.Filldata(sqlstr)
             */
            #endregion
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new CoAnchorShipList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                string mt = "";
                if (ArriveTime.Length == 0)
                {
                    mt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    mt = Convert.ToDateTime(ArriveTime).ToString("yyyy-MM-dd HH:mm:ss");
                }
                var sql =
                    string.Format(
                        "select * from(select * from view_sship_tan where ship_statu=1 order by s_declare desc)where  s_declare <to_date('{0}','yyyy-mm-dd hh24:mi:ss') and rownum <=10",
                           mt);
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathBase).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathBases).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new CoAnchorShip
                            {
                                ShipId = row["ship_id"] as string,
                                ShipNameCn = row["chi_vessel"] as string,
                                Nation = row["nation_cha"] as string,
                                ShipLength = Convert.ToDouble(row["loa"]),
                                TradeName = row["trade"] as string,
                                InDraft = Convert.ToDouble(row["THIS_DRAFT"]),
                                OutDraft = Convert.ToDouble(row["CHU_DRAFT"]),
                                UnloadCargoName = row["zhmc"] as string,
                                UnloadWeight = Convert.ToString((row["zhsl"])),
                                LoadCargoName = row["xhmc"] as string,
                                LoadWeight = Convert.ToString((row["xhsl"])),
                                ArriveTime = Convert.ToDateTime(row["S_declare"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss"),
                                ClientName = row["name"] as string,
                                DivMark = row["ys"] as string
                            });
                return
                    new CoAnchorShipList
                    {
                        Success = true,
                        Message = string.Empty,
                        CoAnchorShips = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new CoAnchorShipList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #region 查询泊位船舶信息
        /// <summary> 获取船代的泊位船舶列表。</summary>
        /// <param name="token">身份认证码</param>
        /// <param name="clientCode">船代客户ID</param>
        /// <param name="berthno">排序关键字</param>
        /// <returns>船代的泊位船舶列表</returns>
        [WebMethod]
        public string GetCoBerthShipList(string token, string clientCode, string berthno)
        {
            #region sql
            /*
             * Dim sqlstr As String
        Dim dv As DataView
        sqlstr = "select * from view_sship_tan where ship_statu=2 or ship_statu=4 or ship_statu=5 or ship_statu=3 order by berthno "
        dv = OraDB_BR.Filldata(sqlstr)
             */
            #endregion
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new CoBerthShipList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                var sql = "";
                if (berthno.Length == 0)
                {
                    sql =
                       string.Format(
                           "select * from (select * from view_sship_tan where  (ship_statu=2 or ship_statu=4 or ship_statu=5 or ship_statu=3)   order by berthno)where rownum<=10"
                             );
                }
                else
                {
                    sql =
                                          string.Format(
                                              "select * from (select * from view_sship_tan where agent = '{0}' and (ship_statu=2 or ship_statu=4 or ship_statu=5 or ship_statu=3)   order by berthno)where berthno > '{1}' and rownum<=10",
                                                clientCode, berthno);
                }
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathBase).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathBases).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new CoBerthShip
                            {
                                ShipId = row["ship_id"] as string,
                                ShipNameCn = row["chi_vessel"] as string,
                                BerthNo = row["BERTHNO"] as string,
                                BerthPos = row["BERTH_POSITION"] as string,
                                UnloadCargoName = row["zhmc"] as string,
                                UnloadWeight = Convert.ToString((row["zhsl"])),
                                LoadCargoName = row["xhmc"] as string,
                                LoadWeight = Convert.ToString((row["xhsl"])),
                                ArriveTime = Convert.ToDateTime(row["BerTH_time"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss"),
                                RestWeight = Convert.ToString((row["dqyd"])),
                                ShipStatus = row["SHIPSTATUS"] as string,
                                ClientName = row["name"] as string,
                                CompanyName = row["DepartMent"] as string
                            });
                return
                    new CoBerthShipList
                    {
                        Success = true,
                        Message = string.Empty,
                        CoBerthShips = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new CoBerthShipList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #region 查询船舶进出港计划

        #region 已做计划船舶信息列表
        /// <summary>获取已做计划船舶信息列表。</summary>
        /// <param name="token">身份认证码</param>
        /// <param name="PlanId">排序关键字</param>
        /// <returns>已做计划船舶信息列表</returns>
        [WebMethod]
        public string GetCoPlanedShipList(string token, string PlanId)
        {
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new CoPlanedShipList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                string sql = "";
                if (PlanId.Length == 0)
                {
                    sql =
                    string.Format(
                        "select * from(select * from ywcplan order by p_id desc)where rownum <=10");
                }
                else
                {
                    sql =
                    string.Format(
                        "select * from(select * from ywcplan order by p_id desc)where p_id <{0} and rownum <=10", PlanId);
                }

                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathBase).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathBases).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new CoPlanedShip
                            {
                                Berth = string.Format("{0}/{1}", Convert.ToString(row["dqbw"]), Convert.ToString(row["bwwz"])),
                                ShipNameCn = row["chi_vessel"] as string,
                                PlanId = Convert.ToInt32(row["p_id"]),
                                UnloadCargoName = row["xhm"] as string,
                                UnloadWeight = Convert.ToString(row["xhsl"]),
                                LoadCargoName = row["zhm"] as string,
                                LoadWeight = Convert.ToString(row["zhsl"]),
                                InPortTime = Convert.ToString((row["yjjg"]) as Nullable<DateTime>),
                                OutPortTime = Convert.ToString((row["yjlg"]) as Nullable<DateTime>),
                                Remark = row["remark"] as string,
                                Pc = row["pc"] as string,
                                Client = row["cbdl"] as string
                            });
                return
                    new CoPlanedShipList
                    {
                        Success = true,
                        Message = string.Empty,
                        CoPlanedShips = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new CoPlanedShipList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #region 需要移泊船舶信息列表
        /// <summary> 获取需要移泊船舶信息列表。</summary>
        /// <param name="token">身份认证码</param>
        /// <param name="PlanTime">排序关键字</param>
        /// <returns>需要移泊船舶信息列表</returns>
        [WebMethod]
        public string GetCoMoveShipList(string token, string PlanTime)
        {
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new CoMoveShipList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                string mt = "";
                if (PlanTime.Length == 0)
                {
                    mt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    mt = Convert.ToDateTime(PlanTime).ToString("yyyy-MM-dd HH:mm:ss");
                }
                var sql =
                    string.Format(
                        "select * from(select * from  YwcPlanYB order by yjyb desc)where yjyb <to_date('{0}','yyyy-mm-dd hh24:mi:ss') and rownum <=10", mt);
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathBase).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathBases).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new CoMoveShip
                            {
                                Berth = string.Format("{0}/{1},", Convert.ToString(row["dqbw"]), Convert.ToString(row["bwwz"])),
                                ShipNameCn = row["chi_vessel"] as string,
                                PlanTime = Convert.ToDateTime(row["yjyb"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss"),
                                PlanBerth = string.Format("{0}/{1},", Convert.ToString(row["ybbw"]), Convert.ToString(row["ybbwwz"])),
                                PlanOutPort = row["yblg"] as string,
                                Remark = row["remark"] as string,
                                PC = row["pc"] as string
                            });
                return
                    new CoMoveShipList
                    {
                        Success = true,
                        Message = string.Empty,
                        CoMoveShips = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new CoMoveShipList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #endregion

        #endregion

        #region 货运应用

        #region 查询汽车衡重码单
        /// <summary> 获取货代的车辆衡重码单列表。</summary>
        /// <param name="token">身份认证码</param>
        /// <param name="companyCode">作业公司ID</param>
        /// <param name="clientCode">货代客户ID</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="signdate">排序关键字</param>
        /// <returns>货代的车辆衡重码单列表</returns>
        [WebMethod]
        public string GetCtVehicleBalanceList(string token, string companyCode, string clientCode, string beginDate, string endDate, string signdate)
        {
            #region sql
            /*
             * strSql = "select signdate,client,vgdisplay,cargo,inout,trade,taskno,cgno,pack,mark,lygcoal_mark,department from vw_gjp_consign where code_department='" & Me.ddpcom.SelectedValue & "' and code_operatetype='t07' and code_client='" & client & "' "
        If Me.STA_Date.Text <> "" Then
            strSql = strSql + " and signdate>=to_date('" & Me.STA_Date.Text & "','yyyy-mm-dd')"
        End If
        If Me.End_date.Text <> "" Then
            strSql = strSql + " and signdate<=to_date('" & Me.End_date.Text & "','yyyy-mm-dd')"
        End If
        If Me.txt_wth.Text <> "" Then
            strSql = strSql + " and taskno like '%" & txt_wth.Text.Trim & "%'"
        End If
        If Me.txt_hm.Text <> "" Then
            strSql = strSql + " and cargo like '%" & txt_hm.Text.Trim & "%'"
        End If
        strSql = strSql + " order by signdate desc"


        dv = OraDB_HB.Filldata(strSql)
             */
            #endregion
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new CtVehicleBalanceList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                string bd = Convert.ToDateTime(beginDate).ToString("yyyy-MM-dd");
                string ed = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd");
                string mt;
                if (signdate.Length == 0)
                {
                    mt = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    mt = Convert.ToDateTime(signdate).ToString("yyyy-MM-dd HH:mm:ss");
                }
                var sql =
                    string.Format(
                        "select * from (select signdate,client,vgdisplay,cargo,inout,trade,taskno,cgno,pack,mark,lygcoal_mark,department from vw_gjp_consign where code_department='{0}' and code_client='{1}' and signdate >= to_date( '{2}','yyyy-MM-dd') and signdate <= to_date( '{3}','yyyy-MM-dd') order by signdate desc)where signdate <to_date('{4}','yyyy-mm-dd hh24:mi:ss') and rownum <=10",
                         companyCode, clientCode, bd, ed, mt);
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbor).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new CtVehicleBalance
                            {
                                SignDate = Convert.ToDateTime(row["signdate"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss"),
                                ClientName = row["client"] as string,
                                VgDisplay = row["VgDisplay"] as string,
                                CargoName = row["cargo"] as string,
                                InoutName = row["inout"] as string,
                                TradeName = row["trade"] as string,
                                TaskNo = row["TaskNo"] as string,
                                Cgno = row["cgno"] as string,
                                PackName = row["pack"] as string,
                                Mark = row["mark"] as string,
                                CompanyName = row["department"] as string
                            });
                return
                    new CtVehicleBalanceList
                    {
                        Success = true,
                        Message = string.Empty,
                        CtVehicleBalances = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new CtVehicleBalanceList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #region 查询注册车辆信息

        #region 网上申报未导入车队车辆
        /// <summary> 网上申报未导入车队车辆。</summary>
        /// <param name="token">身份认证码</param>
        /// <param name="Vehicle">车牌号</param>
        /// <param name="DECLARETIME">排序关键字</param>
        /// <returns>网上申报未导入车队车辆</returns>
        [WebMethod]
        public string GetCtVehicleDeclarationList(string token, string Vehicle, string DECLARETIME)
        {
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new CtVehicleDeclarationList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                string mt = "";
                if (DECLARETIME.Length == 0)
                {
                    mt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    mt = Convert.ToDateTime(DECLARETIME).ToString("yyyy-MM-dd HH:mm:ss");
                }
                var sql =
                    string.Format(
                        "select * from(select *  from Transit.s_vehicle_web where VEHICLE like '{0}%' and MARK_AUDIT=0 order by DECLARETIME desc)where DECLARETIME <to_date('{1}','yyyy-mm-dd hh24:mi:ss') and rownum <=10",
                         Vehicle, mt);
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathMa).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new CtVehicleDeclaration
                            {
                                Vehicle = row["VEHICLE"] as string,
                                VehicleClassType = row["VEH_CLASS_TYPE"] as string,
                                VehicleType = row["VEH_TYPE"] as string,
                                UseKind = row["USE_KIND"] as string,
                                BrandCode = row["BRAND_CODE"] as string,
                                SelfWeight = Convert.ToString((row["SELF_WEIGHT"])),
                                LoadWeight = Convert.ToString((row["LOAD_WEIGHT"])),
                                VehicleCard = row["VEH_CARD"] as string,
                                OwnerName = row["OWNERNAME"] as string,
                                OwnerPhone = row["TEL"] as string,
                                InspectPeriod = row["INSPECT_PERIOD"] as string,
                                DECLARETIME = Convert.ToDateTime(row["DECLARETIME"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss")
                            });
                return
                    new CtVehicleDeclarationList
                    {
                        Success = true,
                        Message = string.Empty,
                        CtVehicleDeclarations = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new CtVehicleDeclarationList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #region 已导入车队车辆
        /// <summary> 已导入车队车辆。</summary>
        /// <param name="token">身份认证码</param>
        /// <param name="Vehicle">车牌号</param>
        /// <param name="Inputtime">排序关键字</param>
        /// <returns>已导入车队车辆</returns>
        [WebMethod]
        public string GetCtVehicleRegistrationList(string token, string Vehicle, string Inputtime)
        {
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new CtVehicleRegistrationList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                string mt = "";
                if (Inputtime.Length == 0)
                {
                    mt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    mt = Convert.ToDateTime(Inputtime).ToString("yyyy-MM-dd HH:mm:ss");
                }
                var sql =
                    string.Format(
                        "select * from(select *  from Transit.s_vehicle_br where VEHICLE like '{0}%' order by INPUT_TIME desc)where INPUT_TIME <to_date('{1}','yyyy-mm-dd hh24:mi:ss') and rownum <=10",
                         Vehicle, mt);
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathMa).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new CtVehicleRegistration
                            {
                                Vehicle = row["VEHICLE"] as string,
                                VehicleClassType = row["VEH_CLASS_TYPE"] as string,
                                VehicleType = row["VEH_TYPE"] as string,
                                UseKind = row["USE_KIND"] as string,
                                BrandCode = row["BRAND_CODE"] as string,
                                SelfWeight = Convert.ToString((row["SELF_WEIGHT"])),
                                LoadWeight = Convert.ToString((row["LOAD_WEIGHT"])),
                                CardNo = row["card_no"] as string,
                                ValidMark = row["mark_validname"] as string,
                                TypeName = row["TypeName"] as string,
                                VehicleCard = row["VEH_CARD"] as string,
                                OwnerName = row["OWNERNAME"] as string,
                                OwnerPhone = row["OWNER_PHONE"] as string,
                                ExamineMark = row["emark"] as string,
                                InspectPeriod = Convert.ToDateTime(row["INSPECT_PERIOD"] as Nullable<DateTime>).ToString("yyyy-MM-dd"),
                                InPutTime = Convert.ToDateTime(row["INPUT_TIME"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss")
                            });
                return
                    new CtVehicleRegistrationList
                    {
                        Success = true,
                        Message = string.Empty,
                        CtVehicleRegistrations = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new CtVehicleRegistrationList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #endregion

        #region 查询新陆桥公司运输申报信息

        #region 已放行运输申报
        /// <summary> 已放行运输申报</summary>
        /// <param name="token">身份认证码</param>
        /// <param name="taskNo">任务号</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <summary> 已放行运输申报。</summary>
        [WebMethod]
        public string GetCtPassedTransportationList(string token, string taskNo, string beginDate, string endDate)
        {
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new CcGoodsBillList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }

                string bd = Convert.ToDateTime(beginDate).ToString("yyyy-MM-dd");
                string ed = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd");


                string strErr;
                CIService.CIServiceSoapClient ci = new CIService.CIServiceSoapClient();
                DataTable dt = ci.GetAttestFinish("CIXLQ", "9876xlq", "191", "C2428", taskNo, bd, ed, out strErr);
                var list = (from DataRow row in dt.Rows
                            select new CtPassedTransportation
                            {
                                ARRIVAL_TIME = row["ARRIVAL_TIME"] as string,
                                AUDITTIME = row["AUDITTIME"] as string,
                                BALANCE1 = row["BALANCE1"] as string,
                                BALANCE2 = row["BALANCE2"] as string,
                                CARGO = row["CARGO"] as string,
                                DEPARTMENT = row["DEPARTMENT"] as string,
                                DRIVERNAME = row["DRIVERNAME"] as string,
                                HEAVY_DESC = row["HEAVY_DESC"] as string,
                                PRE_PASS_TIME = row["PRE_PASS_TIME"] as string,
                                STORAGE = row["STORAGE"] as string,
                                TASKNO = row["TASKNO"] as string,
                                CLIENT = row["CLIENT"] as string,
                                DIRECT_DESC = row["DIRECT_DESC"] as string,
                                DRIVERPHONE = row["DRIVERPHONE"] as string,
                                PREPTIME = row["PREPTIME"] as string,
                                VEHICLE = row["VEHICLE"] as string,
                                VESSEL = row["VESSEL"] as string,
                                WEIGHT = row["WEIGHT"] as string
                            });
                return
                    new CtPassedTransportationList
                    {
                        Success = true,
                        Message = string.Empty,
                        CtPassedTransportations = list.ToArray()
                    }.ToXmlString();
            }

            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new CtPassedTransportationList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #region 已提交未放行
        /// <summary> 已提交未放行</summary>
        /// <param name="token">身份认证码</param>
        /// <summary> 已提交未放行。</summary>
        [WebMethod]
        public string GetCtNoPassedTransportationList(string token)
        {
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new CcGoodsBillList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }



                string strErr;
                CIService.CIServiceSoapClient ci = new CIService.CIServiceSoapClient();
                DataTable dt = ci.GetAttestNoPass("CIXLQ", "9876xlq", "191", "C2428", "", out strErr);

                var list = (from DataRow row in dt.Rows
                            select new CtPassedTransportation
                            {
                                ARRIVAL_TIME = row["ARRIVAL_TIME"] as string,
                                BALANCE1 = row["BALANCE1"] as string,
                                BALANCE2 = row["BALANCE2"] as string,
                                CARGO = row["CARGO"] as string,
                                DEPARTMENT = row["DEPARTMENT"] as string,
                                DRIVERNAME = row["DRIVERNAME"] as string,
                                HEAVY_DESC = row["HEAVY_DESC"] as string,
                                PRE_PASS_TIME = row["PRE_PASS_TIME"] as string,
                                TASKNO = row["TASKNO"] as string,
                                CLIENT = row["CLIENT"] as string,
                                DIRECT_DESC = row["DIRECT_DESC"] as string,
                                DRIVERPHONE = row["DRIVERPHONE"] as string,
                                PREPTIME = row["PREPTIME"] as string,
                                VEHICLE = row["VEHICLE"] as string,
                                VESSEL = row["VESSEL"] as string,
                                WEIGHT = row["WEIGHT"] as string
                            });
                return
                    new CtPassedTransportationList
                    {
                        Success = true,
                        Message = string.Empty,
                        CtPassedTransportations = list.ToArray()
                    }.ToXmlString();
            }

            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new CtPassedTransportationList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #endregion

        #region 查询新陆桥公司作业计划信息
        /// <summary> 查询新陆桥公司作业计划信息。</summary>
        /// <param name="token">身份认证码</param>
        /// <param name="audit">审核标记</param>
        /// <param name="agent">货代客户ID</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="OPERTIME">排序关键字</param>
        /// <returns>查询新陆桥公司作业计划信息</returns>
        [WebMethod]
        public string GetCtNewLandBridgeWorkPlanList(string token, string audit, string agent, string beginDate, string endDate, string OPERTIME)
        {
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new CtNewLandBridgeWorkPlanList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                string bd = Convert.ToDateTime(beginDate).ToString("yyyy-MM-dd");
                string ed = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd");
                string mt = "";
                if (OPERTIME.Length == 0)
                {
                    mt = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    mt = Convert.ToDateTime(OPERTIME).ToString("yyyy-MM-dd HH:mm:ss");
                }
                var sql =
                    string.Format(
                        "select * from(select * from v_consign_plan_xlq where AUDIT_MARK='{0}' and CODE_AGENT='{1}' and OPERTIME >= to_date( '{2}','yyyy-MM-dd') and OPERTIME <= to_date( '{3}','yyyy-MM-dd') order by OPERTIME desc)where OPERTIME <to_date('{4}','yyyy-mm-dd hh24:mi:ss') and rownum <=10",
                         audit, agent, bd, ed, mt);
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbor).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathHarbors).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new CtNewLandBridgeWorkPlan
                            {
                                PlanNo = row["plan_no"] as string,
                                TaskNo = row["taskno"] as string,
                                GoodsBillNo = row["GBNO"] as string,
                                GoodsBillDisplay = row["gbdisplay"] as string,
                                PlanAmount = Convert.ToString((row["plan_amount"])),
                                PlanWeight = Convert.ToString((row["PLAN_WEIGHT"])),
                                PlanVehicleNumber = Convert.ToString((row["PLAN_VEH_NUM"])),
                                FactVehicleNumber = Convert.ToString((row["fact_VEH_NUM"] as Nullable<Int32>)),
                                EmptyBalance = row["balance1"] as string,
                                HeavyBalance = row["balance2"] as string,
                                Client = row["CLIENT"] as string,
                                ApplicantName = row["username"] as string,
                                AuditorName = row["auditorname"] as string,
                                Storage = row["CODE_STORAGE"] as string,
                                Booth = row["CODE_BOOTH"] as string,
                                OPERTIME = Convert.ToDateTime(row["OPERTIME"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss")
                            });
                return
                    new CtNewLandBridgeWorkPlanList
                    {
                        Success = true,
                        Message = string.Empty,
                        CtNewLandBridgeWorkPlans = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new CtNewLandBridgeWorkPlanList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #endregion

        #region 车主应用

        #region 查询注册车辆信息

        #region 网上申报未导入车队车辆
        /// <summary> 网上申报未导入车队车辆。</summary>
        /// <param name="token">身份认证码</param>
        /// <param name="Vehicle">车牌号</param>
        /// <param name="DECLARETIME">排序关键字</param>
        /// <returns>网上申报未导入车队车辆</returns>
        [WebMethod]
        public string GetToVehicleDeclarationList(string token, string Vehicle, string DECLARETIME)
        {
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new ToVehicleDeclarationList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                string mt = "";
                if (DECLARETIME.Length == 0)
                {
                    mt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    mt = Convert.ToDateTime(DECLARETIME).ToString("yyyy-MM-dd HH:mm:ss");
                }
                var sql =
                    string.Format(
                        "select * from(select *  from Transit.s_vehicle_web where VEHICLE like '{0}%' and MARK_AUDIT=0 order by DECLARETIME desc)where DECLARETIME <to_date('{1}','yyyy-mm-dd hh24:mi:ss') and rownum <=10",
                         Vehicle, mt);
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathMa).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new ToVehicleDeclaration
                            {
                                Vehicle = row["VEHICLE"] as string,
                                VehicleClassType = row["VEH_CLASS_TYPE"] as string,
                                VehicleType = row["VEH_TYPE"] as string,
                                UseKind = row["USE_KIND"] as string,
                                BrandCode = row["BRAND_CODE"] as string,
                                SelfWeight = Convert.ToString((row["SELF_WEIGHT"])),
                                LoadWeight = row["LOAD_WEIGHT"] as string,
                                VehicleCard = row["VEH_CARD"] as string,
                                OwnerName = row["OWNERNAME"] as string,
                                OwnerPhone = row["TEL"] as string,
                                InspectPeriod = row["INSPECT_PERIOD"] as string,
                                DECLARETIME = Convert.ToDateTime(row["DECLARETIME"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss")
                            });
                return
                    new ToVehicleDeclarationList
                    {
                        Success = true,
                        Message = string.Empty,
                        ToVehicleDeclarations = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new ToVehicleDeclarationList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #region 已导入车队车辆
        /// <summary> 已导入车队车辆。</summary>
        /// <param name="token">身份认证码</param>
        /// <param name="Vehicle">车牌号</param>
        /// <param name="Inputtime">排序关键字</param>
        /// <returns>已导入车队车辆</returns>
        [WebMethod]
        public string GetToVehicleRegistrationList(string token, string Vehicle, string Inputtime)
        {
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new ToVehicleRegistrationList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                string mt = "";
                if (Inputtime.Length == 0)
                {
                    mt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    mt = Convert.ToDateTime(Inputtime).ToString("yyyy-MM-dd HH:mm:ss");
                }
                var sql =
                    string.Format(
                        "select * from(select *  from Transit.s_vehicle_br where VEHICLE like '{0}%' order by INPUT_TIME desc)where INPUT_TIME <to_date('{1}','yyyy-mm-dd hh24:mi:ss') and rownum <=10",
                         Vehicle, mt);
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathMa).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathMas).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new ToVehicleRegistration
                            {
                                Vehicle = row["VEHICLE"] as string,
                                VehicleClassType = row["VEH_CLASS_TYPE"] as string,
                                VehicleType = row["VEH_TYPE"] as string,
                                UseKind = row["USE_KIND"] as string,
                                BrandCode = row["BRAND_CODE"] as string,
                                SelfWeight = Convert.ToString((row["SELF_WEIGHT"])),
                                LoadWeight = Convert.ToString((row["LOAD_WEIGHT"])),
                                CardNo = row["card_no"] as string,
                                ValidMark = row["mark_validname"] as string,
                                TypeName = row["TypeName"] as string,
                                VehicleCard = row["VEH_CARD"] as string,
                                OwnerName = row["OWNERNAME"] as string,
                                OwnerPhone = row["OWNER_PHONE"] as string,
                                ExamineMark = row["emark"] as string,
                                InspectPeriod = Convert.ToDateTime(row["INSPECT_PERIOD"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss"),
                                InPutTime = Convert.ToDateTime(row["INPUT_TIME"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss")
                            });
                return
                    new ToVehicleRegistrationList
                    {
                        Success = true,
                        Message = string.Empty,
                        ToVehicleRegistrations = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new ToVehicleRegistrationList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #endregion

        #region 查询船舶确报信息
        /// <summary>获取船代的确报船舶列表。 </summary>
        /// <param name="token">身份认证码</param>
        /// <param name="clientCode">船代客户ID</param>
        /// <param name="DeclareTime">排序关键字</param>
        /// <returns>船代的确报船舶列表</returns>
        [WebMethod]
        public string GetToIndeedShipList(string token, string clientCode, string DeclareTime)
        {
            #region sql
            /*
             * Dim sqlstr As String
        Dim dv As DataView
        Dim a4, a2, a1 As Date
        a4 = DateAdd("d", -4, Now)
        a2 = DateAdd("d", -2, Now)
        a1 = DateAdd("d", -1, Now)
        sqlstr = "select * from view_sship_tan where Agent=" & GID & " and (SHIP_STATU = 7 or SHIP_STATU = 1)"
        'sqlstr = "select * from view_sship_tan where Agent=" & GID & " and SHIP_STATU = 7 and ((loa>260 and s_declare<=to_date('" & a4 & "','yyyy-MM-dd hh24:mi:ss')) or (loa<=260 and loa>180 and s_declare<=to_date('" & a2 & "','yyyy-MM-dd hh24:mi:ss')) or (loa<=180) or (code_vesseltype='5')) union select * from view_sship_tan where Agent=" & GID & " and ship_statu=1 and ((loa>260 and s_declare<=to_date('" & a2 & "','yyyy-MM-dd hh24:mi:ss')) or (loa<=260 and s_declare<=to_date('" & a1 & "','yyyy-MM-dd hh24:mi:ss')) or (code_vesseltype='5') )"
        dv = OraDB_BR.Filldata(sqlstr)
             */
            #endregion
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new ToIndeedShipList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                string mt;
                if (DeclareTime.Length == 0)
                {
                    mt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    mt = Convert.ToDateTime(DeclareTime).ToString("yyyy-MM-dd HH:mm:ss");
                }
                var sql =
                    string.Format(
                        "select * from(select * from view_sship_tan where  (SHIP_STATU = 7 or SHIP_STATU = 1) order by S_declare desc)where S_declare <to_date('{0}','yyyy-mm-dd hh24:mi:ss') and rownum <=10",
                           mt);
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathBase).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathBases).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new ToIndeedShip
                            {
                                ShipId = row["ship_id"] as string,
                                ShipNameCn = row["chi_vessel"] as string,
                                IndeedTime = Convert.ToDateTime(row["yjdg"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss"),
                                UnloadCargoName = row["zhmc"] as string,
                                UnloadWeight = Convert.ToString(row["zhsl"]),
                                LoadCargoName = row["xhmc"] as string,
                                LoadWeight = Convert.ToString(row["xhsl"]),
                                DeclareTime = Convert.ToDateTime(row["S_declare"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss"),
                                ShipStatus = row["shipstatus"] as string,
                                DivMark = row["ys"] as string
                            });
                return
                    new ToIndeedShipList
                    {
                        Success = true,
                        Message = string.Empty,
                        ToIndeedShips = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new ToIndeedShipList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #region 查询锚地船舶信息
        /// <summary>获取船代的锚地船舶列表。 </summary>
        /// <param name="token">身份认证码</param>
        /// <param name="clientCode">船代客户ID</param>
        /// <param name="ArriveTime">排序关键字</param>
        /// <returns>船代的锚地船舶列表</returns>
        [WebMethod]
        public string GetToAnchorShipList(string token, string clientCode, string ArriveTime)
        {
            #region sql
            /*
             * Dim sqlstr As String
        Dim dv As DataView
        Dim a4, a2, a1, a6, a3 As Date
        a4 = DateAdd("d", -4, Now)
        a2 = DateAdd("d", -2, Now)
        a1 = DateAdd("d", -1, Now)
        a3 = DateAdd("d", -3, Now)
        a6 = DateAdd("d", -6, Now)
        'sqlstr = "select * from view_sship_tan where ship_statu=1 and ((loa>260 and s_declare<=to_date('" & a2 & "','yyyy-MM-dd hh24:mi:ss')) or (loa<=260 and s_declare<=to_date('" & a1 & "','yyyy-MM-dd hh24:mi:ss')) or (code_vesseltype='5') )"
        sqlstr = "select * from view_sship_tan where ship_statu=1 and ((loa>260 and s_declare<=to_date('" & a6 & "','yyyy-MM-dd hh24:mi:ss')) or (loa<=260 and loa>180 and s_declare<=to_date('" & a3 & "','yyyy-MM-dd hh24:mi:ss')) or (loa<=180 and s_declare<=to_date('" & a1 & "','yyyy-MM-dd hh24:mi:ss')) or (code_vesseltype='5') or (ship_id=318013) or (ship_id=318012)or (ship_id=318022) or (tsqk=1))"
        dv = OraDB_BR.Filldata(sqlstr)
             */
            #endregion
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new ToAnchorShipList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                string mt = "";
                if (ArriveTime.Length == 0)
                {
                    mt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    mt = Convert.ToDateTime(ArriveTime).ToString("yyyy-MM-dd HH:mm:ss");
                }
                var sql =
                    string.Format(
                        "select * from(select * from view_sship_tan where ship_statu=1  order by s_declare desc)where s_declare <to_date('{0}','yyyy-mm-dd hh24:mi:ss') and rownum <=10",
                           mt);
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathBase).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathBases).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new ToAnchorShip
                            {
                                ShipId = row["ship_id"] as string,
                                ShipNameCn = row["chi_vessel"] as string,
                                Nation = row["nation_cha"] as string,
                                ShipLength = Convert.ToDouble(row["loa"]),
                                TradeName = row["trade"] as string,
                                InDraft = Convert.ToDouble(row["THIS_DRAFT"]),
                                OutDraft = Convert.ToDouble(row["CHU_DRAFT"]),
                                UnloadCargoName = row["zhmc"] as string,
                                UnloadWeight = row["zhsl"] as string,
                                LoadCargoName = row["xhmc"] as string,
                                LoadWeight = Convert.ToString(row["xhsl"]),
                                ArriveTime = Convert.ToDateTime(row["S_declare"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss"),
                                ClientName = row["name"] as string,
                                DivMark = row["ys"] as string
                            });
                return
                    new ToAnchorShipList
                    {
                        Success = true,
                        Message = string.Empty,
                        ToAnchorShips = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new ToAnchorShipList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #region 查询泊位船舶信息
        /// <summary> 获取船代的泊位船舶列表。</summary>
        /// <param name="token">身份认证码</param>
        /// <param name="clientCode">船代客户ID</param>
        /// <param name="berthno">排序关键字</param>
        /// <returns>船代的泊位船舶列表</returns>
        [WebMethod]
        public string GetToBerthShipList(string token, string clientCode, string berthno)
        {
            #region sql
            /*
             * Dim sqlstr As String
        Dim dv As DataView
        sqlstr = "select * from view_sship_tan where ship_statu=2 or ship_statu=4 or ship_statu=5 or ship_statu=3 order by berthno "
        dv = OraDB_BR.Filldata(sqlstr)
             */
            #endregion
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new ToBerthShipList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                var sql = "";
                if (berthno.Length == 0)
                {
                    sql =
                       string.Format(
                           "select * from (select * from view_sship_tan where  (ship_statu=2 or ship_statu=4 or ship_statu=5 or ship_statu=3)   order by berthno)where rownum<=10"
                             );
                }
                else
                {
                    sql =
                                          string.Format(
                                              "select * from (select * from view_sship_tan where agent = '{0}' and (ship_statu=2 or ship_statu=4 or ship_statu=5 or ship_statu=3)   order by berthno)where berthno > '{1}' and rownum<=10",
                                                clientCode, berthno);
                }
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathBase).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathBases).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new ToBerthShip
                            {
                                ShipId = row["ship_id"] as string,
                                ShipNameCn = row["chi_vessel"] as string,
                                BerthNo = row["BERTHNO"] as string,
                                BerthPos = row["BERTH_POSITION"] as string,
                                UnloadCargoName = row["zhmc"] as string,
                                UnloadWeight = row["zhsl"] as string,
                                LoadCargoName = row["xhmc"] as string,
                                LoadWeight = Convert.ToString(row["xhsl"]),
                                ArriveTime = Convert.ToDateTime(row["BerTH_time"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss"),
                                RestWeight = row["dqyd"] as string,
                                ShipStatus = row["SHIPSTATUS"] as string,
                                ClientName = row["name"] as string,
                                CompanyName = row["DepartMent"] as string
                            });
                return
                    new ToBerthShipList
                    {
                        Success = true,
                        Message = string.Empty,
                        ToBerthShips = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new ToBerthShipList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #region 查询船舶进出港计划

        #region 已做计划船舶信息列表
        /// <summary>获取已做计划船舶信息列表。</summary>
        /// <param name="token">身份认证码</param>
        /// <param name="PlanId">排序关键字</param>
        /// <returns>已做计划船舶信息列表</returns>
        [WebMethod]
        public string GetToPlanedShipList(string token, string PlanId)
        {
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new ToPlanedShipList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                string sql = "";
                if (PlanId.Length == 0)
                {
                    sql =
                    string.Format(
                        "select * from(select * from ywcplan order by p_id desc)where rownum <=10");
                }
                else
                {
                    sql =
                    string.Format(
                        "select * from(select * from ywcplan order by p_id desc)where p_id <{0} and rownum <=10", PlanId);
                }

                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathBase).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathBases).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new ToPlanedShip
                            {
                                Berth = string.Format("{0}/{1}", Convert.ToString(row["dqbw"]), Convert.ToString(row["bwwz"])),
                                ShipNameCn = row["chi_vessel"] as string,
                                PlanId = Convert.ToInt32(row["p_id"]),
                                UnloadCargoName = row["xhm"] as string,
                                UnloadWeight = Convert.ToString(row["xhsl"]),
                                LoadCargoName = row["zhm"] as string,
                                LoadWeight = Convert.ToString(row["zhsl"]),
                                InPortTime = Convert.ToString((row["yjjg"]) as Nullable<DateTime>),
                                OutPortTime = Convert.ToString((row["yjlg"]) as Nullable<DateTime>),
                                Remark = row["remark"] as string,
                                Pc = row["pc"] as string,
                                Client = row["cbdl"] as string
                            });
                return
                    new ToPlanedShipList
                    {
                        Success = true,
                        Message = string.Empty,
                        ToPlanedShips = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new ToPlanedShipList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #region 需要移泊船舶信息列表
        /// <summary> 获取需要移泊船舶信息列表。</summary>
        /// <param name="token">身份认证码</param>
        /// <param name="PlanTime">排序关键字</param>
        /// <returns>需要移泊船舶信息列表</returns>
        [WebMethod]
        public string GetToMoveShipList(string token, string PlanTime)
        {
            try
            {
                if (!TokenTool.VerifyToken(token))
                {
                    return new ToMoveShipList { Success = false, Message = "Token未通过校验。" }.ToXmlString();
                }
                string mt = "";
                if (PlanTime.Length == 0)
                {
                    mt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    mt = Convert.ToDateTime(PlanTime).ToString("yyyy-MM-dd HH:mm:ss");
                }
                var sql =
                    string.Format(
                        "select * from(select * from  YwcPlanYB order by yjyb desc)where yjyb <to_date('{0}','yyyy-mm-dd hh24:mi:ss') and rownum <=10", mt);
                //var dt = new YGSoft.IPort.Data.Core.Oracle.DataAccess(Leo.RegistryKey.KeyPathBase).ExecuteTable(sql);
                var dt = new Leo.Oracle.DataAccess(Leo.RegistryKey.KeyPathBases).ExecuteTable(sql);
                var list = (from DataRow row in dt.Rows
                            select new ToMoveShip
                            {
                                Berth = string.Format("{0}/{1},", Convert.ToString(row["dqbw"]), Convert.ToString(row["bwwz"])),
                                ShipNameCn = row["chi_vessel"] as string,
                                PlanTime = Convert.ToDateTime(row["yjyb"] as Nullable<DateTime>).ToString("yyyy-MM-dd HH:mm:ss"),
                                PlanBerth = string.Format("{0}/{1},", Convert.ToString(row["ybbw"]), Convert.ToString(row["ybbwwz"])),
                                PlanOutPort = row["yblg"] as string,
                                Remark = row["remark"] as string,
                                PC = row["pc"] as string
                            });
                return
                    new ToMoveShipList
                    {
                        Success = true,
                        Message = string.Empty,
                        ToMoveShips = list.ToArray()
                    }.ToXmlString();
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(ServiceHMW), ex);
                return
                    new ToMoveShipList { Success = false, Message = string.Format("获取数据异常。{0}", ex.Message) }
                        .ToXmlString();
            }
        }
        #endregion

        #endregion


        #endregion

        #endregion
    }
}
