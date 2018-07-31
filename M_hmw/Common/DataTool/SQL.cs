using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Common
{
    public class SQL
    {
        //0:
        /// <summary>
        /// 公路资讯
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String GongLuZiXunService0 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where  classid=103 order by POST_TIME desc) where rownum<={0}";
        //1:
        /// <summary>
        /// 已放行车辆
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String YiFangXingCheLiangService1 = "select * from (select id,VEHICLENET VEHICLE,DEPARTMENT,POSITION,to_char(AUDITTIME,'MM-dd hh24:mi') as  ATIME from harbor.v_consign_vehicle_only_quick t WHERE AUDITTIME>sysdate-1/4 and (AUDITTIME+(TIMEOUT_PARLOR+10)/(24*60)>sysdate or TIMEOUT_PARLOR is null) order by AUDITTIME desc) where rownum<={0}";
        //2:
        /// <summary>
        /// 网上车源
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String WangShangCheYuanService2 = "select a.id,a.kcfd,a.ddd,a.cx,a.dw,a.cc,to_char(a.jzsj,'yyyy-MM-dd') as jzsj from wscy a,user_group b where rownum<={0} and a.USERID=b.id and a.sh=1 order by a.jzsj desc";
        //3:
        /// <summary>
        /// 司机信息
        /// 身份账号为“transit”
        /// 0为返回行数
        /// </summary>
        public static String SiJiXinXiService3 = "select ID,substr(name, 1,1) || '先生'  as xs,PERMIT_VEH_TYPE,ADDRESS,to_char(REG_TIME,'yyyy-MM-dd') as REG_TIME from DRIVER where rownum<={0} order by REG_TIME";
        //4:
        /// <summary>
        /// 内河咨询
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String NeiHeZiXunService4 = "select classid,ID,TOPIC,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from (select * from news_topic where  classid=105  order by POST_TIME desc) where rownum<={0}";
        //5:
        /// <summary>
        /// 灌河国际确报船
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String GuanHeGuoJiQueBaoChuanService5 = "select nsno,nvessel,company,to_char(declaretime,'yyyy-MM-dd') as declaretime from BASERESOURCE.vw_br_nship_ywc where code_vgstatus=7 and code_company=2102 and rownum<={0}";
        //6:
        /// <summary>
        /// 新云台确报船
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String XinYunTaiQueBaoChuaService6 = "select * from (select nsno,nvessel,company, to_char(declaretime,'yyyy-MM-dd') as declaretime from BASERESOURCE.vw_br_nship_ywc where code_vgstatus=7 and code_company='693' order by  declaretime desc) where rownum<={0}";
        //7:
        /// <summary>
        /// 灌河国际泊位船
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String GuanHeGuoJiBoWeiChuanService7 = "select * from (select nsno,nvessel,to_char(declaretime,'yyyy-MM-dd') as declaretime from BASERESOURCE.vw_br_nship_ywc where code_vgstatus=2 and code_company='2102' order by  declaretime desc) where rownum<={0}";
        //8:
        /// <summary>
        /// 新云台泊位船
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String XinYunTaiBoWeiChuanService8 = "select * from (select nsno,nvessel,to_char(declaretime,'yyyy-MM-dd') as declaretime from BASERESOURCE.vw_br_nship_ywc where code_vgstatus=2 and code_company='693' order by  declaretime desc) where rownum<={0}";
        //9:
        /// <summary>
        /// 国际煤焦资讯
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String GuoJiMeiJiaoZiXunService9 = "select classid,ID,TOPIC,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from (select * from news_topic where  classid=115 order by POST_TIME desc) where rownum<={0}";
        //10:
        /// <summary>
        /// 国内煤焦资讯
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String GuoNeiMeiJiaoZiXunService10 = "select classid,ID,TOPIC,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from (select * from news_topic where  classid=114 order by POST_TIME desc) where rownum<={0}";
        //11:
        /// <summary>
        /// 港口煤焦资讯
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String GangKouMeiJiaoZiXunService11 = "select classid,ID,TOPIC,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from (select * from news_topic where  classid=96 order by POST_TIME desc) where rownum<={0}";
        //12:
        /// <summary>
        /// 煤焦供应
        /// 身份账号为“lygcoal”
        /// 0为返回行数
        /// </summary>
        public static String MeiJiaoGongYingService12 = "select ID,GOODSNAME,NUM,AREA,to_char(CREATETIME,'yyyy-MM-dd') as CREATETIME from(select * from vw_sup_dem where type='供货'  order by audittime desc) where rownum<={0}";
        //13:
        /// <summary>
        /// 煤焦求购
        /// 身份账号为“lygcoal”
        /// 0为返回行数
        /// </summary>
        public static String MeiJiaoQiuGouService13 = "select ID,GOODSNAME,NUM,AREA,to_char(CREATETIME,'yyyy-MM-dd') as CREATETIME from(select * from vw_sup_dem where type='需求' order by audittime desc) where rownum<={0}";
        //14:
        /// <summary>
        /// 煤炭行情
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String MeiJiaoHangQingService14 = "select * from(Select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME From NEWS_TOPIC Where classid=99 Order By POST_TIME DESC) where rownum<={0}";
        //15:
        /// <summary>
        /// 焦炭行情
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String JiaoTanHangQingService15 = "select * from(Select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME From NEWS_TOPIC Where classid=100 Order By POST_TIME DESC) where rownum<={0}";
        //16:
        /// <summary>
        /// 有色矿资讯
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String YouSeKuangZiXunService16 = "select classid,ID,TOPIC,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from (select * from news_topic where  classid=97 order by POST_TIME desc) where rownum<={0}";
        //17:
        /// <summary>
        /// 有色矿供求
        /// 身份账号为“wl”
        /// 0为返回行数
        /// </summary>
        public static String YouSeKuangGongQiuService17 = "select ID,TYPE_NAME,CLASS,SPEC,TONS,PLACE,to_char(REG_DATE,'yyyy-MM-dd') as REG_DATE from(select id,class,spec,place,tons,type_name,price,reg_date from view_trade_business where type=3 and isnew=1 order by reg_date desc) where rownum<={0}";
        //18:
        /// <summary>
        /// 有色矿行情
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String YouSeKuangHangQingService18 = "select * from(Select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME From NEWS_TOPIC Where classid=101 Order By POST_TIME DESC) where rownum<={0}";
        //19:
        /// <summary>
        /// 铁矿砂资讯
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String TieKuangShaZiXunService19 = "select classid,ID,TOPIC,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from (select * from news_topic where  classid=98  order by POST_TIME desc) where rownum<={0}";
        //20:
        /// <summary>
        /// 铁矿砂供求
        /// 身份账号为“wl”
        /// 0为返回行数
        /// </summary>
        public static String TieKuangShaGongQiuService20 = "select ID,TYPE_NAME,CLASS,SPEC,TONS,PLACE,to_char(REG_DATE,'yyyy-MM-dd') as REG_DATE from view_trade_business where isnew=1 AND TYPE=1 and above=1 and rownum<={0} and spec is not null order by reg_date desc";
        //21:
        /// <summary>
        /// 铁矿砂行情
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String TieKuangShaHangQingService21 = "select * from(Select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME From NEWS_TOPIC Where classid=102 Order By POST_TIME DESC) where rownum<={0}";
        //22:
        /// <summary>
        /// 仓储资讯
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String CangChuZiXunService22 = "select * from (select classid,ID,TOPIC,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where  classid=106  order by POST_TIME desc) where rownum<={0}";
        //23:
        /// <summary>
        /// 园区仓储
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String YuanQuCangChuService23 = "select ID,TOPIC,SZD from vw_ccps where rownum<={0} and sh=1 order by id desc";
        //24:
        /// <summary>
        /// 待储货源
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String DaiChuHuoYuanService24 = "select ID,HWMC,LXR,LXFS,to_char(JZSJ,'yyyy-MM-dd') as JZSJ from dchy where rownum<={0} and sh=1 order by JZSJ desc";
        //25:
        /// <summary>
        /// 来港确报
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String LaiGangQueBaoService25 = "select ID,FZM,DZM,CLZS,ZIZ from tmedi.TM_MORDDQB where rownum<={0} order by id";
        //26:
        /// <summary>
        /// 港口要闻
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String GangKouYaoWenService26 = "select * from (Select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME From NEWS_TOPIC Where NEWS_LEVEL=1  Order By POST_TIME DESC)Where  rownum<={0}";
        //27:
        /// <summary>
        /// 陆桥资讯
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String LuQiaoZiXunService27 = "select * from (Select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME From NEWS_TOPIC Where (classid=15 or classid=20 or classid=25 or classid=30 or classid=15 or classid=40 or classid=45 or classid=111)  order by POST_TIME desc)Where  rownum<={0}";
        //28:
        /// <summary>
        /// 行业观察
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String HangYeGuanChaService28 = "select * from (Select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME From NEWS_TOPIC Where classid=59   order by POST_TIME desc)Where  rownum<={0}";
        //29:
        /// <summary>
        /// 船舶资讯
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ChuanBoZiXunService29 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where  classid=0 order by POST_TIME desc) where rownum<={0}";
        //30:
        /// <summary>
        /// 集装箱船期
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String JiZhuangXiangChuanQiService30 = "select * from(select a.id,a.S_S_PORT,a.S_E_PORT,a.S_S_NAME,a.S_CARRYING,a.S_STAR_TIME from ship_entire_box_time a,user_group b where a.USERID=b.id and a.sh=1 order by a.S_STAR_TIME desc) where rownum<={0}";
        //31:
        /// <summary>
        /// 运价
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String YunJiaService31 = "select * from(select a.id,a.S_S_PORT,a.S_E_PORT,a.F20,a.F40,a.S_DEP from ship_entire_box_price a,user_group b where a.USERID=b.id and a.sh=1 order by a.s_date desc) where rownum<={0}";
        //32:
        /// <summary>
        /// 散杂船期
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZaSanChuanQiService32 = "select * from(select a.id,a.S_S_PORT,a.S_E_PORT,a.S_STAR_TIME,b.DESCRIPT from ship_disperses_goods_time a,user_group b where a.USERID=b.id and a.sh=1 order by a.S_STAR_TIME desc) where rownum<={0}";
        //33:
        /// <summary>
        /// 散杂运价
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZaSanYunJiaService33 = "select * from(select a.id,a.S_S_PORT,a.S_E_PORT,a.S_PRICE,b.DESCRIPT from ship_disperses_goods_price a,user_group b where a.USERID=b.id and a.sh=1 order by a.s_date desc) where rownum<={0}";
        //34:
        /// <summary>
        /// 在泊船
        /// 身份账号为“BASERESOURCE”
        /// 0为返回行数
        /// </summary>
        public static String ZaiBoChuanService34 = "select SHIP_ID,BERTHNO,BERTH_POSITION,CHI_VESSEL,ZHMC,ZHSL,XHMC,XHSL,NAME from view_sship_tan where (ship_statu=2 or ship_statu=4 or ship_statu=5 or ship_statu=3) and hgnh=0 and rownum<={0}";
        //15:
        /// <summary>
        /// 锚地船
        /// 身份账号为“BASERESOURCE”
        /// {0,1,2}分别为6天前，3天前，1天前
        /// 3为返回行数
        /// </summary>
        public static String MaoDiChuanService35 = "select SHIP_ID,CHI_VESSEL,ZHMC,ZHSL,XHMC,XHSL,NAME from view_sship_tan where ship_statu=1 and hgnh=0 and ((loa>260 and s_declare<=to_date('{0}','yyyy-MM-dd hh24:mi:ss')) or (loa<=260 and loa>180 and s_declare<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')) or (loa<=180 and s_declare<=to_date('{2}','yyyy-MM-dd hh24:mi:ss')) or (code_vesseltype='5') or (ship_id=318013) or (ship_id=318012) or (ship_id=318022) or (tsqk=1)) and rownum<={3}";
        //36:
        /// <summary>
        /// 铁路资讯
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String TieLuZiXunService36 = "select classid,ID,TOPIC,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from (select * from news_topic where  classid=104 order by POST_TIME desc) where rownum<={0}";
        //37:
        /// <summary>
        /// 来港确报
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String LaiGangHuoCheQueBaoService37 = "select * from (select ID,FZM,DZM,CLZS,ZIZ from tmedi.TM_MORDDQB order by createtime desc) where rownum<={0} ";
        //38:
        /// <summary>
        /// 码头卸车计划
        /// 身份账号为“JZHDD”
        /// 0为返回行数
        /// </summary>
        public static String MaTouXieCheJiHuaService38 = "select trainplanid, case CODE_DEPARTMENT when '010111' then '东联公司' when '010113' then '东源公司' when '010116' then '东泰公司' when '010118' then '新东润公司' end as DEPARTMENT,DAYNIGHT,TRAINS,to_char(PLANDATE,'yyyy-MM-dd') as PLANDATE from(select * from vw_ty_trainplan  order by PLANDATE desc) where rownum<={0}";
        //39:
        /// <summary>
        /// 码头装车计划
        /// 身份账号为“JZHDD”
        /// 0为返回行数
        /// </summary>
        public static String MaTouZhuangCheJiHuaService39 = "select consignid, case CODE_DEPARTMENT when '010111' then '东联公司' when '010113' then '东源公司' when '010116' then '东泰公司' when '010118' then '新东润公司' end as DEPARTMENT,CLIENT,to_char(TAKEDATE,'yyyy-MM-dd') as TAKEDATE from(select * from vw_ty_trainconsign  order by TAKEDATE desc) where rownum<={0}";
        //40:
        /// <summary>
        /// 今日头条
        /// 身份账号为“hmw”
        /// </summary>
        public static String JinRiTouTiaoService40 = "select * from (Select classid,ID,topic,POST_TIME,MESSAGE From NEWS_TOPIC Where NEWS_LEVEL=1  Order By POST_TIME DESC)Where  rownum<=1";
        //41:
        /// <summary>
        /// 我的预报船
        /// 身份账号为“BASERESOURCE”
        /// 0为传入参数船代客户ID,1为系统时间
        /// 2为返回行数
        /// </summary>
        public static String MyYuBaoChuanService41 = "select ship_id,chi_vessel,to_char(yjdg,'yyyy-mm-dd') as yjdg,zhmc,zhsl,xhmc,xhsl,ys from(select * from view_sship_tan where  agent='{0}' and ship_statu=0 order by yjdg desc)where rownum<={2}";
        //42:
        /// <summary>
        /// 我的确报船
        /// 身份账号为“BASERESOURCE”
        /// 0为传入参数船代客户ID,1为系统时间
        /// 2为返回行数
        /// </summary>
        public static String MyQueBaoChuanService42 = "select ship_id,chi_vessel,to_char(yjdg,'yyyy-mm-dd') as yjdg,zhmc,zhsl,xhmc,xhsl,ys from(select * from view_sship_tan where agent='{0}' and (SHIP_STATU = 7) order by S_declare desc)where rownum<={2}";
        //43:
        /// <summary>
        /// 我的引航费用
        /// 身份账号为“mobilecenter”
        /// 0为传入参数船代客户ID
        /// 1为返回行数
        /// </summary>
        public static String MyYinHangFeiService43 = "select invoicenum,CHI_VESSEL,to_char(invoicedate,'yyyy-mm-dd') as invoicedate,fcode,amount,fk,fkdjh,fkdw from(select viewworkbill.CHI_VESSEL,viewworkbill.FCODE,invoice.invoicenum,ton_net,amount,invoicedate,invoice.fkdw,invoice.sid,case when invoice.fk='0' then '否' when invoice.fk='1' then '是' end as fk,case when invoice.fkdjh='00000' then '    ' else invoice.fkdjh end as fkdjh  from inhual.viewworkbill,inhual.invoice  where viewworkbill.sid=invoice.sid and viewworkbill.agent = '{0}' order by invoicedate desc)where rownum<={1}";
        //44:
        /// <summary>
        /// 我的高频话费
        /// 身份账号为“mobilecenter”
        /// 0为传入参数船代客户ID
        /// 1为返回行数
        /// </summary>
        public static String MyGaoPingHuaFeiService44 = "select id,INVOICENUM,chi_vessel,fzs,money,fkdw from(select * from ddmis.viewtxgpinfo where agent = '{0}' order by sta_time desc)where rownum<={1}";
        //45:
        /// <summary>
        /// 我的泊位船舶
        /// 身份账号为“BASERESOURCE”
        /// 0为传入参数船代客户ID
        /// 1为返回行数
        /// </summary>
        public static String MyBoWeiChuanBoService45 = "select ship_id,name,chi_vessel,BERTHNO,BERTH_POSITION,DepartMent,SHIPSTATUS,dqyd,BerTH_time from (select * from view_sship_tan where agent = '{0}' and ship_statu=2 or ship_statu=4 or ship_statu=5 or ship_statu=3 or ship_statu=6  order by BerTH_time)where rownum<={1}";
        //46:
        /// <summary>
        /// 我的船舶计划
        /// 身份账号为“BASERESOURCE”
        /// 0为返回行数
        /// </summary>
        public static String MyChuanBoJiHuaService46 = "select p_id,cbdl,chi_vessel,dqbw,bwwz,pc,pilotage from(select * from ywcplan order by p_id desc)where rownum<={0}";
        //47:
        /// <summary>
        /// 我的业务委托有船作业
        /// 身份账号为“harbor”
        /// 0为服务传入参数船代ID
        /// 1为返回行数
        /// </summary>
        public static String MyYeWuWeiTuoYouChuanService47 = "select * from(select bcno,chi_vessel,voyage,jobtype,from_port,to_port from harbor.vw_bc_bconsign where code_client='{0}' order by createtime desc)where rownum<={1}";
        //48:
        /// <summary>
        /// 我的业务委托无船作业
        /// 身份账号为“harbor”
        /// 0为服务传入参数船代ID
        /// 1为返回行数
        /// </summary>
        public static String MyYeWuWeiTuoWuChuanService48 = "select * from(select id,zyxm, qyg, ddg from viewgoodscon where depid='{0}'  order by condate desc) where rownum<={1}";
        //49:
        /// <summary>
        /// 我的作业委托
        /// 身份账号为“harbor”
        /// 0为服务传入参数船代ID
        /// 1为返回行数
        /// </summary>
        public static String MyZuoYeWeiTuoService49 = "select CGNO,OPERATETYPE,CLIENT,DEPARTMENT,PLANVOLUME,TASKNO from (select * from vw_web_consign_query where code_client='{0}' order by createtime desc) where rownum<={1}";
        //50:
        /// <summary>
        /// 我的票货
        /// 身份账号为“harbor”
        /// 0为服务传入参数船代ID
        /// 1为返回行数
        /// </summary>
        public static String MyPiaoHuoService50 = "SELECT * from(select Gbno, Cargo, Inout  , Trade  , PieceWeight,  Mark  from vw_qyf_goodsbillnet where code_client='{0}' order by createtime desc) where rownum<={1}";
        //51:
        /// <summary>
        /// 我的货物进港
        /// 身份账号为“harbor”
        /// 0为服务传入参数船代ID
        /// 1为返回行数
        /// </summary>
        public static String MyHuoWuJinGangService51 = "select id,to_char(signdate,'yyyy-mm-dd') as signdate,department,cargo,weight from(select * from vw_stockin_web where code_client='{0}' order by signdate desc)where rownum<={1}";
        //52:
        /// <summary>
        /// 我的货物出港
        /// 身份账号为“harbor”
        /// 0为服务传入参数船代ID
        /// 1为返回行数
        /// </summary>
        public static String MyHuoWuChuGangService52 = "select id,to_char(out_date,'yyyy-mm-dd') as out_date,department,cargo,weight from(select * from vw_stockout_web where code_client='{0}' order by signdate desc)where rownum<={1}";
        //53:
        /// <summary>
        /// 我的货物结存
        /// 身份账号为“harbor”
        /// 0为服务传入参数船代ID
        /// 1为返回行数
        /// </summary>
        public static String MyHuoWuJieCunService53 = "select id,cargo,weight from(select * from vw_stockdormant_web where code_client='{0}'  order by signdate desc) where  rownum<={1}";
        //54:
        /// <summary>
        /// 我的船代货代
        /// 身份账号为“mobilecenter”
        /// 0为服务传入参数船代ID
        /// 1为返回行数
        /// </summary>
        public static String MyChuanDaiHuoDaiService54 = "select CODE_CLIENT_HD,CODE_CLIENT_CD,CODE_DEPARTMENT from VW_IPT_USER where code_user = '{0}'";
        //55:
        /// <summary>
        /// 我的个人信息
        /// 身份账号为“mobilecenter”
        /// 0为服务传入参数用户ID
        /// 1为返回行数
        /// </summary>
        public static String MyGeRenXinXiService55 = "select USERNAME,MOBILE,FULLNAME from VW_IPT_USER where code_user ='{0}'";
        //56:
        /// <summary>
        /// 我的已付账单
        /// 身份账号为“hmw”
        /// 0为服务传入参数用户ID
        /// 1为返回行数
        /// </summary>
        public static String MyYiFuZhangDanService56 = "select RBNO,to_char(CREATETIME,'yyyy-mm-dd') as CREATETIME,FYLX,RBDISPLAY,TOTAL from(select * from jszx_fp t where CODE_CLIENT='{0}' order by t.CREATETIME desc) where rownum<={1}";
        //57:
        /// <summary>
        /// 我的业务事项
        /// 身份账号为“hmw”
        /// 0为服务传入参数用户ID
        /// 1为返回行数
        /// </summary>
        public static String MyYeWuShiXiangService57 = "select TASKNO,to_char(SIGNDATE,'yyyy-mm-dd') as SIGNDATE,YWLX,VGDISPLAY,CARGO,FACTWEIGHT,YWLX_DW,DEPARTMENT from(select * from jszx_ywl t where CODE_CLIENT_IPORT='{0}' order by t.SIGNDATE desc) where rownum<={1}";
        //58:
        /// <summary>
        /// 我的费用结算
        /// 身份账号为“harbor”
        /// 0为服务传入参数船代ID
        /// 1为返回行数
        /// </summary>
        public static String MyFeiYongJieSuanService58 = "select * from (select IVNO,to_char(a.signdate,'yyyy-mm-dd') signdate,a.VOYAGE,replace(c.department,'分公司','') department,a.total from tb_pro_invoice a,tb_code_department c where a.code_client='{0}' and a.code_department=c.code_department order by a.signdate desc) where rownum<={1}";
        //59:
        /// <summary>
        /// 本地新闻 兰州
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String BenDiXinWenLanZhouService59 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=38 order by id desc) where rownum<={0}";
        //60:
        /// <summary>
        /// 本地新闻 山西
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String BenDiXinWenSanXiService60 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=23 order by id desc) where rownum<={0}";
        //61:
        /// <summary>
        /// 本地新闻 西安
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String BenDiXinWenXiAnService61 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=28 order by id desc) where rownum<={0}";
        //62:
        /// <summary>
        /// 本地新闻 新疆
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String BenDiXinWenXinJiangService62 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=43 order by id desc) where rownum<={0}";
        //63:
        /// <summary>
        /// 本地新闻 郑州
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String BenDiXinWenZhengZhouService63 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=18 order by id desc) where rownum<={0}";
        //64:
        /// <summary>
        /// 本地新闻 银川
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String BenDiXinWenYinChuanService64 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=33 order by id desc) where rownum<={0}";
        //65:
        /// <summary>
        /// 本地新闻 西宁
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String BenDiXinWenXiNingService65 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=109 order by id desc) where rownum<={0}";
        //66:
        /// <summary>
        /// 经济发展 兰州
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String JinJiFaZhanLanZhouService66 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=39 order by id desc) where rownum<={0}";
        //67:
        /// <summary>
        /// 经济发展 山西
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String JinJiFaZhanSanXiService67 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=24 order by id desc) where rownum<={0}";
        //68:
        /// <summary>
        /// 经济发展 西安
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String JinJiFaZhanXiAnService68 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=29 order by id desc) where rownum<={0}";
        //69:
        /// <summary>
        /// 经济发展 新疆
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String JinJiFaZhanXinJiangService69 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=44 order by id desc) where rownum<={0}";
        //70:
        /// <summary>
        /// 经济发展 郑州
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String JinJiFaZhanZhengZhouService70 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=19 order by id desc) where rownum<={0}";
        //71:
        /// <summary>
        /// 经济发展 银川
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String JinJiFaZhanYinChuanService71 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=34 order by id desc) where rownum<={0}";
        //72:
        /// <summary>
        /// 经济发展 西宁
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String JinJiFaZhanXiNingService72 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=110 order by id desc) where rownum<={0}";
        //73:
        /// <summary>
        /// 物流贸易 兰州
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String WuLiuMaoYiLanZhouService73 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=40 order by id desc) where rownum<={0}";
        //74:
        /// <summary>
        /// 物流贸易 山西
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String WuLiuMaoYiSanXiService74 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=25 order by id desc) where rownum<={0}";
        //75:
        /// <summary>
        /// 物流贸易 西安
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String WuLiuMaoYiXiAnService75 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=30 order by id desc) where rownum<={0}";
        //76:
        /// <summary>
        /// 物流贸易 新疆
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String WuLiuMaoYiXinJiangService76 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=45 order by id desc) where rownum<={0}";
        //77:
        /// <summary>
        /// 物流贸易 郑州
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String WuLiuMaoYiZhengZhouService77 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=20 order by id desc) where rownum<={0}";
        //78:
        /// <summary>
        /// 物流贸易 银川
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String WuLiuMaoYiYinChuanService78 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=15 order by id desc) where rownum<={0}";
        //79:
        /// <summary>
        /// 物流贸易 西宁
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String WuLiuMaoYiXiNingService79 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=111 order by id desc) where rownum<={0}";
        //80:
        /// <summary>
        /// 文化活动 兰州
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String WenHuaHuoDongLanZhouService80 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=41 order by id desc) where rownum<={0}";
        //81:
        /// <summary>
        /// 文化活动 山西
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String WenHuaHuoDongSanXiService81 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=26 order by id desc) where rownum<={0}";
        //82:
        /// <summary>
        /// 文化活动 西安
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String WenHuaHuoDongXiAnService82 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=31 order by id desc) where rownum<={0}";
        //83:
        /// <summary>
        /// 文化活动 新疆
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String WenHuaHuoDongXinJiangService83 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=46 order by id desc) where rownum<={0}";
        //84:
        /// <summary>
        /// 文化活动 郑州
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String WenHuaHuoDongZhengZhouService84 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=21 order by id desc) where rownum<={0}";
        //85:
        /// <summary>
        /// 文化活动 银川
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String WenHuaHuoDongYinChuanService85 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=36 order by id desc) where rownum<={0}";
        //86:
        /// <summary>
        /// 文化活动 西宁
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String WenHuaHuoDongXiNingService86 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=112 order by id desc) where rownum<={0}";
        //87:
        /// <summary>
        /// 招商引资 兰州
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZhaoShangYinZiLanZhouService87 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=42 order by id desc) where rownum<={0}";
        //88:
        /// <summary>
        /// 招商引资 山西
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZhaoShangYinZiSanXiService88 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=27 order by id desc) where rownum<={0}";
        //89:
        /// <summary>
        /// 招商引资 西安
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZhaoShangYinZiXiAnService89 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=32 order by id desc) where rownum<={0}";
        //90:
        /// <summary>
        /// 招商引资 新疆
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZhaoShangYinZiXinJiangService90 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=47 order by id desc) where rownum<={0}";
        //91:
        /// <summary>
        /// 招商引资 郑州
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZhaoShangYinZiZhengZhouService91 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=22 order by id desc) where rownum<={0}";
        //92:
        /// <summary>
        /// 招商引资 银川
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZhaoShangYinZiYinChuanService92 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=37 order by id desc) where rownum<={0}";
        //93:
        /// <summary>
        /// 招商引资 西宁
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZhaoShangYinZiXiNingService93 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=113 order by id desc) where rownum<={0}";
        //94:
        /// <summary>
        /// 地方风情 兰州
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String DiFangFengQingLanZhouService94 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=56 order by id desc) where rownum<={0}";
        //95:
        /// <summary>
        /// 地方风情 山西
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String DiFangFengQingSanXiService95 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=53 order by id desc) where rownum<={0}";
        //96:
        /// <summary>
        /// 地方风情 西安
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String DiFangFengQingXiAnService96 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=54 order by id desc) where rownum<={0}";
        //97:
        /// <summary>
        /// 地方风情 新疆
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String DiFangFengQingXinJiangService97 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=57 order by id desc) where rownum<={0}";
        //98:
        /// <summary>
        /// 地方风情 郑州
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String DiFangFengQingZhengZhouService98 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=52 order by id desc) where rownum<={0}";
        //99:
        /// <summary>
        /// 地方风情 银川
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String DiFangFengQingYinChuanService99 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=55 order by id desc) where rownum<={0}";
        //100:
        /// <summary>
        /// 地方风情 西宁
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String DiFangFengQingXiNingService100 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=108 order by id desc) where rownum<={0}";
        //101:
        /// <summary>
        /// 整箱船期 韩国
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZhengXiangChuanQiHanGuoService101 = "select * from (select ID,S_S_NAME,S_S_PORT,S_E_PORT,S_STAR_TIME,S_CARRYING,S_VOYAGE from ship_entire_box_time where S_SHIPLINEcode=0 and sh=1 order by S_STAR_TIME desc) where rownum<={0}";
        //102:
        /// <summary>
        /// 整箱船期 日本
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZhengXiangChuanQiRiBenService102 = "select * from (select ID,S_S_NAME,S_S_PORT,S_E_PORT,S_STAR_TIME,S_CARRYING,S_VOYAGE from ship_entire_box_time where S_SHIPLINEcode=1 and sh=1 order by S_STAR_TIME desc) where rownum<={0}";
        //103:
        /// <summary>
        /// 整箱船期 内支
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZhengXiangChuanQiNeiZhiService103 = "select * from (select ID,S_S_NAME,S_S_PORT,S_E_PORT,S_STAR_TIME,S_CARRYING,S_VOYAGE from ship_entire_box_time where S_SHIPLINEcode=2 and sh=1 order by S_STAR_TIME desc) where rownum<={0}";
        //104:
        /// <summary>
        /// 整箱船期 美洲
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZhengXiangChuanQiMeiZhouService104 = "select * from (select ID,S_S_NAME,S_S_PORT,S_E_PORT,S_STAR_TIME,S_CARRYING,S_VOYAGE from ship_entire_box_time where S_SHIPLINEcode=3 and sh=1 order by S_STAR_TIME desc) where rownum<={0}";
        //105:
        /// <summary>
        /// 整箱船期 欧洲
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZhengXiangChuanQiOuZhouService105 = "select * from (select ID,S_S_NAME,S_S_PORT,S_E_PORT,S_STAR_TIME,S_CARRYING,S_VOYAGE from ship_entire_box_time where S_SHIPLINEcode=4 and sh=1 order by S_STAR_TIME desc) where rownum<={0}";
        //106:
        /// <summary>
        /// 整箱船期 中东
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZhengXiangChuanQiZhongDongService106 = "select * from (select ID,S_S_NAME,S_S_PORT,S_E_PORT,S_STAR_TIME,S_CARRYING,S_VOYAGE from ship_entire_box_time where S_SHIPLINEcode=5 and sh=1 order by S_STAR_TIME desc) where rownum<={0}";
        //107:
        /// <summary>
        /// 整箱船期 非洲
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZhengXiangChuanQiFeiZhouService107 = "select * from (select ID,S_S_NAME,S_S_PORT,S_E_PORT,S_STAR_TIME,S_CARRYING,S_VOYAGE from ship_entire_box_time where S_SHIPLINEcode=6 and sh=1 order by S_STAR_TIME desc) where rownum<={0}";
        //108:
        /// <summary>
        /// 整箱船期 东南亚
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZhengXiangChuanQiDongNanYaService108 = "select * from (select ID,S_S_NAME,S_S_PORT,S_E_PORT,S_STAR_TIME,S_CARRYING,S_VOYAGE from ship_entire_box_time where S_SHIPLINEcode=7 and sh=1 order by S_STAR_TIME desc) where rownum<={0}";
        //109:
        /// <summary>
        /// 杂散船期 韩国
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZaSanChuanQiHanGuoService109 = "select * from (select ID,S_S_NAME,S_S_PORT,S_E_PORT,S_STAR_TIME,S_CARRYING,S_VOYAGE from ship_disperses_goods_time where S_SHIPLINEcode=0 and sh=1 order by S_STAR_TIME desc) where rownum<={0}";
        //110:
        /// <summary>
        /// 杂散船期 日本
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZaSanChuanQiRiBenService110 = "select * from (select ID,S_S_NAME,S_S_PORT,S_E_PORT,S_STAR_TIME,S_CARRYING,S_VOYAGE from ship_disperses_goods_time where S_SHIPLINEcode=1 and sh=1 order by S_STAR_TIME desc) where rownum<={0}";
        //111:
        /// <summary>
        /// 杂散船期 内支
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZaSanChuanQiNeiZhiService111 = "select * from (select ID,S_S_NAME,S_S_PORT,S_E_PORT,S_STAR_TIME,S_CARRYING,S_VOYAGE from ship_disperses_goods_time where S_SHIPLINEcode=2 and sh=1 order by S_STAR_TIME desc) where rownum<={0}";
        //112:
        /// <summary>
        /// 杂散船期 美洲
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZaSanChuanQiMeiZhouService112 = "select * from (select ID,S_S_NAME,S_S_PORT,S_E_PORT,S_STAR_TIME,S_CARRYING,S_VOYAGE from ship_disperses_goods_time where S_SHIPLINEcode=3 and sh=1 order by S_STAR_TIME desc) where rownum<={0}";
        //113:
        /// <summary>
        /// 杂散船期 欧洲
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZaSanChuanQiOuZhouService113 = "select * from (select ID,S_S_NAME,S_S_PORT,S_E_PORT,S_STAR_TIME,S_CARRYING,S_VOYAGE from ship_disperses_goods_time where S_SHIPLINEcode=4 and sh=1 order by S_STAR_TIME desc) where rownum<={0}";
        //114:
        /// <summary>
        /// 杂散船期 中东
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZaSanChuanQiZhongDongService114 = "select * from (select ID,S_S_NAME,S_S_PORT,S_E_PORT,S_STAR_TIME,S_CARRYING,S_VOYAGE from ship_disperses_goods_time where S_SHIPLINEcode=5 and sh=1 order by IDS_STAR_TIMEdesc) where rownum<={0}";
        //115:
        /// <summary>
        /// 杂散船期 非洲
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZaSanChuanQiFeiZhouService115 = "select * from (select ID,S_S_NAME,S_S_PORT,S_E_PORT,S_STAR_TIME,S_CARRYING,S_VOYAGE from ship_disperses_goods_time where S_SHIPLINEcode=6 and sh=1 order by S_STAR_TIME desc) where rownum<={0}";
        //116:
        /// <summary>
        /// 杂散船期 东南亚
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZaSanChuanQiDongNanYaService116 = "select * from (select ID,S_S_NAME,S_S_PORT,S_E_PORT,S_STAR_TIME,S_CARRYING,S_VOYAGE from ship_disperses_goods_time where S_SHIPLINEcode=7 and sh=1 order by S_STAR_TIME desc) where rownum<={0}";
        //117:
        /// <summary>
        /// 整箱运价 韩国
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZhengXiangYunJiaHanGuoService117 = "select * from (select ID,S_S_PORT,S_E_PORT,F20,F40,S_DEP,S_DATE from ship_entire_box_price where S_SHIPLINEcode=0 and sh=1 order by S_DATE desc) where rownum<={0}";
        //118:
        /// <summary>
        /// 整箱运价 日本
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZhengXiangYunJiaRiBenService118 = "select * from (select ID,S_S_PORT,S_E_PORT,F20,F40,S_DEP,S_DATE from ship_entire_box_price where S_SHIPLINEcode=1 and sh=1 order by S_DATE desc) where rownum<={0}";
        //119:
        /// <summary>
        /// 整箱运价 内支
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZhengXiangYunJiaNeiZhiService119 = "select * from (select ID,S_S_PORT,S_E_PORT,F20,F40,S_DEP,S_DATE from ship_entire_box_price where S_SHIPLINEcode=2 and sh=1 order by S_DATE desc) where rownum<={0}";
        //120:
        /// <summary>
        /// 整箱运价 美洲
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZhengXiangYunJiaMeiZhouService120 = "select * from (select ID,S_S_PORT,S_E_PORT,F20,F40,S_DEP,S_DATE from ship_entire_box_price where S_SHIPLINEcode=3 and sh=1 order by S_DATE desc) where rownum<={0}";
        //121:
        /// <summary>
        /// 整箱运价 欧洲
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZhengXiangYunJiaOuZhouService121 = "select * from (select ID,S_S_PORT,S_E_PORT,F20,F40,S_DEP,S_DATE from ship_entire_box_price where S_SHIPLINEcode=4 and sh=1 order by S_DATE desc) where rownum<={0}";
        //122:
        /// <summary>
        /// 整箱运价 中东
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZhengXiangYunJiaZhongDongService122 = "select * from (select ID,S_S_PORT,S_E_PORT,F20,F40,S_DEP,S_DATE from ship_entire_box_price where S_SHIPLINEcode=5 and sh=1 order by S_DATE desc) where rownum<={0}";
        //123:
        /// <summary>
        /// 整箱运价 非洲
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZhengXiangYunJiaFeiZhouService123 = "select * from (select ID,S_S_PORT,S_E_PORT,F20,F40,S_DEP,S_DATE from ship_entire_box_price where S_SHIPLINEcode=6 and sh=1 order by S_DATE desc) where rownum<={0}";
        //124:
        /// <summary>
        /// 整箱运价 东南亚
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZhengXiangYunJiaDongNanYaService124 = "select * from (select ID,S_S_PORT,S_E_PORT,F20,F40,S_DEP,S_DATE from ship_entire_box_price where S_SHIPLINEcode=7 and sh=1 order by S_DATE desc) where rownum<={0}";
        //125:
        /// <summary>
        /// 杂散运价 韩国
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZaSanYunJiaHanGuoService125 = "select * from (select ID,S_S_PORT,S_E_PORT,S_PRICE,S_DEP,S_DATE from ship_disperses_goods_price where S_SHIPLINEcode=0 and sh=1 order by S_DATE desc) where rownum<={0}";
        //126:
        /// <summary>
        /// 杂散运价 日本
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZaSanYunJiaRiBenService126 = "select * from (select ID,S_S_PORT,S_E_PORT,S_PRICE,S_DEP,S_DATE from ship_disperses_goods_price where S_SHIPLINEcode=1 and sh=1 order by S_DATE desc) where rownum<={0}";
        //127:
        /// <summary>
        /// 杂散运价 内支
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZaSanYunJiaNeiZhiService127 = "select * from (select ID,S_S_PORT,S_E_PORT,S_PRICE,S_DEP,S_DATE from ship_disperses_goods_price where S_SHIPLINEcode=2 and sh=1 order by S_DATE desc) where rownum<={0}";
        //128:
        /// <summary>
        /// 杂散运价 美洲
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZaSanYunJiaMeiZhouService128 = "select * from (select ID,S_S_PORT,S_E_PORT,S_PRICE,S_DEP,S_DATE from ship_disperses_goods_price where S_SHIPLINEcode=3 and sh=1 order by S_DATE desc) where rownum<={0}";
        //129:
        /// <summary>
        /// 杂散运价 欧洲
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZaSanYunJiaOuZhouService129 = "select * from (select ID,S_S_PORT,S_E_PORT,S_PRICE,S_DEP,S_DATE from ship_disperses_goods_price where S_SHIPLINEcode=4 and sh=1 order by S_DATE desc) where rownum<={0}";
        //130:
        /// <summary>
        /// 杂散运价 中东
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZaSanYunJiaZhongDongService130 = "select * from (select ID,S_S_PORT,S_E_PORT,S_PRICE,S_DEP,S_DATE from ship_disperses_goods_price where S_SHIPLINEcode=5 and sh=1 order by S_DATE desc) where rownum<={0}";
        //131:
        /// <summary>
        /// 杂散运价 非洲
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZaSanYunJiaFeiZhouService131 = "select * from (select ID,S_S_PORT,S_E_PORT,S_PRICE,S_DEP,S_DATE from ship_disperses_goods_price where S_SHIPLINEcode=6 and sh=1 order by S_DATE desc) where rownum<={0}";
        //132:
        /// <summary>
        /// 杂散运价 东南亚
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZaSanYunJiaDongNanYaService132 = "select * from (select ID,S_S_PORT,S_E_PORT,S_PRICE,S_DEP,S_DATE from ship_disperses_goods_price where S_SHIPLINEcode=7 and sh=1 order by S_DATE desc) where rownum<={0}";
        //133:
        /// <summary>
        /// 本地新闻 连云港
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String BenDiXinWenLianYunGangService133 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=13 order by id desc) where rownum<={0}";
        //134:
        /// <summary>
        /// 经济发展 连云港
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String JingJiFaZhanLianYunGangService134 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=14 order by id desc) where rownum<={0}";
        //135:
        /// <summary>
        /// 物流贸易 连云港
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String WuLiuMaoYiLianYunGangService135 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=15 order by id desc) where rownum<={0}";
        //136:
        /// <summary>
        /// 文化活动 连云港
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String WenHuaHuoDongLianYunGangService136 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=16 order by id desc) where rownum<={0}";
        //137:
        /// <summary>
        /// 招商引资 连云港
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String ZhaoShangYinZiLianYunGangService137 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=17 order by id desc) where rownum<={0}";
        //138:
        /// <summary>
        /// 海运新闻 连云港
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String HaiYunXinWenLianYunGangService138 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=0 order by id desc) where rownum<={0}";
        //139:
        /// <summary>
        /// 陆桥记事 连云港
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String LuQiaoJiShiLianYunGangService139 = "select * from (select classid,ID,topic,to_char(POST_TIME,'yyyy-MM-dd') as POST_TIME from news_topic where classid=6 order by id desc) where rownum<={0}";
        //140:
        /// <summary>
        /// 获取首页要闻资讯新闻列表
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String GetMainPageImptNewsList140 = "select ID,topic,to_char(post_time,'yyyy-mm-dd') as post_time from (select id,topic,post_time,message from news_topic where news_level=1 order by id desc) where rownum<={0}";
        //141:
        /// <summary>
        /// 获取首页在港船舶数据列表
        /// 身份账号为“BASERESOURCE”
        /// 0为返回行数
        /// </summary>
        public static String GetMainPagePortShipList141 = "select SHIP_ID,NAME,CHI_VESSEL,BERTHNO,BERTH_POSITION,DEPARTMENT,SHIPSTATUS,DQYD from view_sship_tan where (ship_statu=2 or ship_statu=4 or ship_statu=5 or ship_statu=3) and hgnh=0 and rownum<={0}";
        //142:
        /// <summary>
        /// 获取首页锚地船舶数据列表
        /// 身份账号为“BASERESOURCE”
        /// 0,1,2分别为6天前、3天前、1天前时间，3为返回行数
        /// </summary>
        public static String GetMainPageAnchorShipList142 = "select SHIP_ID,NAME,CHI_VESSEL,THIS_DRAFT,CHU_DRAFT,TRADE,to_char(YJDG,'yyyy-MM-dd'),YS from view_sship_tan where ship_statu=1 and hgnh=0 and ((loa>260 and s_declare<=to_date('{0}','yyyy-MM-dd hh24:mi:ss')) or (loa<=260 and loa>180 and s_declare<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')) or (loa<=180 and s_declare<=to_date('{2}','yyyy-MM-dd hh24:mi:ss')) or (code_vesseltype='5') or (ship_id=318013) or (ship_id=318012) or (ship_id=318022) or (tsqk=1)) and rownum<={3}";
        //143:
        /// <summary>
        /// 获取首页进出港计划数据列表
        /// 身份账号为“BASERESOURCE”
        /// 0为返回行数
        /// </summary>
        public static String GetMainPageInoutPortPlanList143 = "select P_ID,CBDL,CHI_VESSEL,DQBW,BWWZ,PC,PILOTAGE from ywcplan where hgnh=0 and rownum<={0}";
        //144:
        /// <summary>
        /// 获取首页最新船期数据列表
        /// 身份账号为“BASERESOURCE”
        /// 0为返回行数
        /// </summary>
        public static String GetMainPageLatestShipmentList144 = "select ID,S_S_PORT,S_E_PORT,S_S_NAME,S_CARRYING,S_STAR_TIME from(select a.S_S_PORT,a.S_E_PORT,a.S_STAR_TIME,b.DESCRIPT,a.id,a.S_SHIPLINE,a.S_CARRYING,a.S_S_NAME from hmw.ship_entire_box_time a,hmw.user_group b where a.USERID=b.id and a.sh=1 order by a.id desc) where rownum<={0}";
        //145:
        /// <summary>
        /// 获取首页最新货盘数据列表
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String GetMainPageLatestPalletList145 = "select ID,S_S_PORT,S_E_PORT,F20,F40,S_DEP from(select a.S_S_PORT,a.S_E_PORT,b.DESCRIPT,a.S_DATE,a.id,a.S_SHIPLINE,a.F20,a.F40,a.S_DEP from ship_entire_box_price a,user_group b where a.USERID=b.id and a.sh=1 order by a.id desc) where rownum<={0}";
        //146:
        /// <summary>
        /// 获取首页矿石专区数据列表
        /// 身份账号为“wl”
        /// 0为返回行数
        /// </summary>
        public static String GetMainPageOreZoneList146 = "select ID,case when type_name='供' then '供货' when type_name='求' then '需求' end as TYPE_NAME,CLASS,SPEC,TONS,PLACE,to_char(REG_DATE,'yyyy-MM-dd') from (select * from view_trade_business where type=3 and isnew=1 order by reg_date desc) where rownum<={0}";
        //147:
        /// <summary>
        /// 获取首页煤炭专区数据列表
        /// 身份账号为“lygcoal”
        /// 0为返回行数
        /// </summary>
        public static String GetMainPageCoalZoneList147 = "select ID,TYPE,GOODSNAME,NUM,AREA,to_char(CREATETIME,'yyyy-MM-dd') from (select id,goodsname,audittime,name,num,area,yxq,vd,createtime,type  from vw_sup_dem where  audit_mark='1' order by createtime desc) a where  rownum<={0}";
        //148:
        /// <summary>
        /// 获取首页最新运力数据列表
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String GetMainPageLatestTransCapList148 = "select ID,CX,CPHM,DW,CC,KCFD,DDD,LXFS from(select a.kcfd,a.ddd,a.id,a.cphm,b.DESCRIPT,a.jzsj,a.cx,a.cc,a.dw,a.lxfs from wscy a,user_group b  where a.USERID=b.id and a.sh=1 order by a.id desc) where rownum<={0}";
        //149:
        /// <summary>
        /// 获取首页最新货源数据列表
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String GetMainPageLatestGoodsSourceList149 = "select ID,HWMC,SL,QYD,DDD,to_char(JZSJ,'yyyy-MM-dd') from(select a.id,b.DESCRIPT,a.qyd,a.ddd,a.hwmc,a.jzsj,a.sl from wshy a,user_group b  where a.USERID=b.id and a.sh=1 order by a.id desc) where rownum<={0}";
       
        
        
        
        
        
        
        //150:
        /// <summary>
        /// 获取首页在港船舶明细数据列表
        /// 身份账号为“BASERESOURCE”
        /// 0为返回行数
        /// </summary>
        public static String GetMainPagePortShipDetailList150 = "select NAME,      CHI_VESSEL,    BERTHNO,       BERTH_POSITION,DEPARTMENT,    SHIPSTATUS,      DQYD,            ENG_VESSEL,      NATION_CHA,    TRADE,         XHMC,              XHSL,             ZHMC,             ZHSL  from view_sship_tan where (ship_statu=2 or ship_statu=4 or ship_statu=5 or ship_statu=3) and hgnh=0 and ship_id = {0}";
        //151:
        /// <summary>
        /// 获取首页锚地船舶明细数据列表
        /// 身份账号为“BASERESOURCE”
        /// 0,1,2分别为6天前、3天前、1天前时间，3为返回行数
        /// </summary>
        public static String GetMainPageAnchorShipDetailList151 = "select NAME,      CHI_VESSEL,    THIS_DRAFT,    CHU_DRAFT,    TRADE,          YJDG,           YS,              eng_vessel,    NATION_CHA,    LOA,           XHMC,           XHSL,          ZHMC,           ZHSL from view_sship_tan where ship_statu=1 and hgnh=0 and ((loa>260 and s_declare<=to_date('{0}','yyyy-MM-dd hh24:mi:ss')) or (loa<=260 and loa>180 and s_declare<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')) or (loa<=180 and s_declare<=to_date('{2}','yyyy-MM-dd hh24:mi:ss')) or (code_vesseltype='5') or (ship_id=318013) or (ship_id=318012) or (ship_id=318022) or (tsqk=1)) and ship_id={3}";
        //152:
        /// <summary>
        /// 获取首页进出港计划明细数据列表
        /// 身份账号为“BASERESOURCE”
        /// 0为返回行数
        /// </summary>
        public static String GetMainPageInoutPortPlanDetailList152 = "select CBDL,      CHI_VESSEL,    DQBW,           BWWZ,        PC,              PILOTAGE,       GJ,              CZ,            THIS_DRAFT,     CHU_DRAFT,        YJJG,               REMARK from ywcplan where hgnh=0 and p_id={0}";
        //153:
        /// <summary>
        /// 获取首页最新船期明细数据列表
        /// 身份账号为“BASERESOURCE”
        /// 0为返回行数
        /// </summary>
        public static String GetMainPageLatestShipmentDetailList153 = " select a.S_S_PORT,    a.S_E_PORT,      a.S_S_NAME,       a.S_CARRYING,     a.S_STAR_TIME,      a.S_VOYAGE,    a.S_SHIPLINE,       a.S_MAN,        a.S_PHONE,            b.DESCRIPT,            b.ADDRESS,        b.TELPHONE,      a.S_REMARK   from hmw.ship_entire_box_time a,hmw.user_group b where a.USERID=b.id and a.sh=1 and a.id={0}";
        //154:
        /// <summary>
        /// 获取首页最新货盘明细数据列表
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String GetMainPageLatestPalletDetailList154 = " select a.S_S_PORT,    a.S_E_PORT,      a.F20,            a.F40,            a.S_DEP,            a.S_SHIPLINE,   a.S_MAN,         a.S_PHONE,            b.DESCRIPT,      b.ADDRESS,        b.TELPHONE from ship_entire_box_price a,user_group b where a.USERID=b.id and a.sh=1 and a.id={0}";
        //155:
        /// <summary>
        /// 获取首页矿石专区明细数据列表
        /// 身份账号为“wl”
        /// 0为返回行数
        /// </summary>
        public static String GetMainPageOreZoneDetailList155 = "select case when a.type_name='供' then '供货' when a.type_name='求' then '需求' end as TYPE_NAME,a.CLASS,a.SPEC,a.TONS,a.PLACE,a.REG_DATE,       a.quality,a.price,b.receive_place,a.remark       from view_trade_business a,trade_business b where a.id=b.id and a.type=3 and a.isnew=1 and a.id={0}";
        //156:
        /// <summary>
        /// 获取首页煤炭专区明细数据列表
        /// 身份账号为“lygcoal”
        /// 0为返回行数
        /// </summary>
        public static String GetMainPageCoalZoneDetailList156 = "select a.type,a.goodsname,a.num,a.area,a.createtime,a.price,a.yxq,    b.MT,            b.MAD,           b.AD,               b.vd,                b.ST_D,         b.crc,              b.QNET_AR,                  b.FCAD,a.tel     from vw_sup_dem a,tb_coal_mark b,tb_sup_dem c where  a.id=b.sup_id and a.id=c.id and a.audit_mark='1' and a.id={0}";
        //157:
        /// <summary>
        /// 获取首页最新运力明细数据列表
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String GetMainPageLatestTransCapDetailList157 = "select  a.CX,          a.CPHM,          a.KCFD,          a.DDD,           a.LXFS,             a.DW,           a.CC,           a.jzsj           from wscy a where a.sh=1 and a.id={0}";
        //158:
        /// <summary>
        /// 获取首页最新货源明细数据列表
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String GetMainPageLatestGoodsSourceDetailList158 = "select a.HWMC,          a.SL,           a.QYD,          a.DDD,            a.JZSJ,             a.CLYQ,           a.LXFS    from wshy a,user_group b  where a.USERID=b.id and a.sh=1 and a.id={0}";


        //159:
        /// <summary>
        /// 集装箱船期明细数据列表
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String GetJiZhuangXiangChuanQiDetailList159 = "select a.S_S_PORT,    a.S_E_PORT,      a.S_S_NAME,       a.S_CARRYING,     a.S_STAR_TIME,      a.S_VOYAGE,    a.S_SHIPLINE,   a.S_MAN,        a.S_PHONE,            b.DESCRIPT,            b.ADDRESS,        b.TELPHONE,      a.S_REMARK   from hmw.ship_entire_box_time a,hmw.user_group b where a.USERID=b.id and a.sh=1 and a.id={0}";
        //160:
        /// <summary>
        /// 集装箱运价明细数据列表
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String GetJiZhuangXiangYunJiaDetailList160 = "select a.S_S_PORT,    a.S_E_PORT,      a.F20,            a.F40,            a.S_DEP,            a.S_SHIPLINE,   a.S_MAN,         a.S_PHONE,            b.DESCRIPT,      b.ADDRESS,        b.TELPHONE from ship_entire_box_price a,user_group b where a.USERID=b.id and a.sh=1 and a.id={0}";
        //161:
        /// <summary>
        /// 散杂船期明细数据列表
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String GetSanJiaChuanQiDetailList161 = "  select  a.S_S_PORT,    a.S_E_PORT,       a.S_STAR_TIME,     b.DESCRIPT,       a.S_VOYAGE,         a.S_SHIPLINE,  a.S_CARRYING,       a.s_s_name,        a.S_MAN,           a.S_PHONE,         b.ADDRESS,        b.TELPHONE,     a.S_REMARK from ship_disperses_goods_time a,user_group b where a.userid=b.id and a.sh=1 and a.id={0}";
        //162:
        /// <summary>
        /// 散杂运价明细数据列表
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String GetSanJiaYunJiaDetailList162 = "  select a.S_S_PORT,    a.S_E_PORT,       a.S_PRICE,        b.DESCRIPT,      a.S_SHIPLINE,         a.S_DEP,         a.S_MAN,           a.S_PHONE,         b.ADDRESS,        b.TELPHONE		 from ship_disperses_goods_price a,user_group b where a.USERID=b.id and a.sh=1 and a.id={0}";
        //163:
        /// <summary>
        /// 在船舶明细数据列表
        /// 身份账号为“BASERESOURCE”
        /// 0为返回行数
        /// </summary>
        public static String GetZaiChuanBoDetailList163 = " select BERTHNO,     BERTH_POSITION, CHI_VESSEL,      ZHMC,           ZHSL,            XHMC,         XHSL,            NAME,           DEPARTMENT,      SHIPSTATUS,      DQYD,           ENG_VESSEL,    NATION_CHA,    TRADE   from view_sship_tan where (ship_statu=2 or ship_statu=4 or ship_statu=5 or ship_statu=3) and hgnh=0 and ship_id = {0}";
        //164:
        /// <summary>
        /// 获取锚地船明细数据列表
        /// 身份账号为“BASERESOURCE”
        /// 0,1,2分别为6天前、3天前、1天前时间，3为返回行数
        /// </summary>
        public static String GetMaoDiChuanDetailList164 = "select CHI_VESSEL,   ZHMC,           ZHSL,          XHMC,          XHSL,               NAME,         THIS_DRAFT,       CHU_DRAFT,      TRADE,           YJDG,             YS,             eng_vessel,     NATION_CHA,       LOA      from view_sship_tan where ship_statu=1 and hgnh=0 and ((loa>260 and s_declare<=to_date('{0}','yyyy-MM-dd hh24:mi:ss')) or (loa<=260 and loa>180 and s_declare<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')) or (loa<=180 and s_declare<=to_date('{2}','yyyy-MM-dd hh24:mi:ss')) or (code_vesseltype='5') or (ship_id=318013) or (ship_id=318012) or (ship_id=318022) or (tsqk=1)) and ship_id={3}";
        //165:
        /// <summary>
        /// 获取来港确报数据列表
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String GetLaiGangQueBaoDetailList165 = "select  fzm,          dzm,           clzs,            ziz,             fzh,               dzh,          bzcc,            fbzh,            jyzh,             hc,             yyzaz,           fyyzaz,          pbs,  dfrq from tmedi.TM_MORDDQB where id={0}";
        //166:
        /// <summary>
        /// 获取码头卸车计划数据列表
        /// 身份账号为“JZHDD”
        /// 0为返回行数
        /// </summary>
        public static String GetMaTouXieCheJiHuaDetailList166 = "select case CODE_DEPARTMENT when '010111' then '东联公司' when '010113' then '东源公司' when '010116' then '东泰公司' when '010118' then '新东润公司' end as DEPARTMENT,DAYNIGHT,TRAINS,to_char(PLANDATE,'yyyy-MM-dd') as PLANDATE,teamtype,trainroad,WORKFLOWNO,REMARK  from(select * from JZHDD.vw_ty_trainplan  order by PLANDATE desc) where trainplanid={0}";            
        //167:
        /// <summary>
        /// 获取码头装车计划数据列表
        /// 身份账号为“JZHDD”
        /// 0为返回行数
        /// </summary>
        public static String GetMaTouZhuangCheJiHuaDetailList167 = "select case CODE_DEPARTMENT when '010111' then '东联公司' when '010113' then '东源公司' when '010116' then '东泰公司' when '010118' then '新东润公司' end as DEPARTMENT,CLIENT,to_char(TAKEDATE,'yyyy-MM-dd') as TAKEDATE,CONSIGN_TON,FIELD,TRAINNUMBER,CARGO,STATION,trainroad,daynight,VESSEL_CN   from(select * from JZHDD.vw_ty_trainconsign  order by TAKEDATE desc) where rownum<=5";            






        //169:
        /// <summary>
        /// 获取网上车源数据列表
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String GetWangShangCheYuanDetailList169 = "select a.kcfd,a.ddd,a.cx,a.dw,a.cc,to_char(a.jzsj,'yyyy-MM-dd') as jzsj,CPHM,LXFS from wscy a,user_group b where a.USERID=b.id and a.sh=1 and a.id={0}";
        //170:
        /// <summary>
        /// 获取司机信息数据列表
        /// 身份账号为“transit”
        /// 0为返回行数
        /// </summary>
        public static String GetSiJiXinXiDetailList170 = "select substr(name, 1,1) || '先生'  as xs, PERMIT_VEH_TYPE,ADDRESS,to_char(REG_TIME,'yyyy-MM-dd') as REG_TIME,          DRIVER_CARD,  PHONE,            NATIONALITY,     to_char(BIRTH_DATE,'yyyy-MM-dd') as BIRTH_DATE , to_char(PERMIT_BEGIN,'yyyy-MM-dd') as PERMIT_BEGIN,   GRANT_DEPT     from transit.DRIVER where id={0}";
		//171:
        /// <summary>
        /// 获取灌河国际确报船信息数据列表
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String GetGuanHeGuoJiQueBaoChuanDetailList171 = "select nvessel,      to_char(declaretime,'yyyy-MM-dd') as declaretime ,      client,  '中国'  as  nationality,             i_voyage,      o_voyage,         vgstatus,        port_last ,    port_next,     to_char(arrivetime,'yyyy-MM-dd') as arrivetime ,  '1'  as  num,                              zx,              cargo,         planweight, '新云台码头'  as  dischargecompany  from BASERESOURCE.vw_br_nship_ywc where nsno={0}";
		//172:
        /// <summary>
        /// 获取新云台确报船信息数据列表
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String GetXinYunTaiQueBaoChuanDetailList172 = "select nvessel,      to_char(declaretime,'yyyy-MM-dd') as declaretime ,      client,  '中国'  as  nationality,             i_voyage,      o_voyage,         vgstatus,        port_last ,    port_next,     to_char(arrivetime,'yyyy-MM-dd') as arrivetime ,  '1'  as  num,                              zx,              cargo,         planweight, '新云台码头'  as  dischargecompany  from BASERESOURCE.vw_br_nship_ywc where  nsno={0}";
		//173:
        /// <summary>
        /// 获取灌河国际泊位船信息数据列表
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String GetGuanHeGuoJiBoWeiChuanDetailList173 = "select nvessel,      to_char(declaretime,'yyyy-MM-dd') as declaretime ,      client,  '中国'  as  nationality,             i_voyage,      o_voyage,         vgstatus,        port_last ,    port_next,     to_char(arrivetime,'yyyy-MM-dd') as arrivetime ,  '1'  as  num,                              zx,              cargo,         planweight, '新云台码头'  as  dischargecompany  from BASERESOURCE.vw_br_nship_ywc where  nsno={0}";
		//174:
        /// <summary>
        /// 获取新云台泊位船信息数据列表
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String GetXinYunTaiBoWeiChuanDetailList174 = "select nvessel,      to_char(declaretime,'yyyy-MM-dd') as declaretime ,      client,  '中国'  as  nationality,             i_voyage,      o_voyage,         vgstatus,        port_last ,    port_next,     to_char(arrivetime,'yyyy-MM-dd') as arrivetime ,  '1'  as  num,                              zx,              cargo,         planweight, '新云台码头'  as  dischargecompany  from BASERESOURCE.vw_br_nship_ywc where nsno={0}";
        //175:
        /// <summary>
        /// 获取煤焦供应明细数据列表
        /// 身份账号为“lygcoal”
        /// 0为返回行数
        /// </summary>
        public static String GetMeiJiaoGongYingDetailList175 = "select a.goodsname,a.num,a.area,a.createtime,a.type,a.price,a.yxq,    b.MT,            b.MAD,           b.AD,               b.vd,                b.ST_D,         b.crc,              b.QNET_AR,                  b.FCAD,a.tel     from vw_sup_dem a,tb_coal_mark b,tb_sup_dem c where  a.id=b.sup_id and a.id=c.id and a.audit_mark='1' and a.id={0}";
        //176:
        /// <summary>
        /// 获取煤焦求购明细数据列表
        /// 身份账号为“lygcoal”
        /// 0为返回行数
        /// </summary>
        public static String GetMeiJiaoQiuGouDetailList176 = "select a.goodsname,a.num,a.area,a.createtime,a.type,a.price,a.yxq,    b.MT,            b.MAD,           b.AD,               b.vd,                b.ST_D,         b.crc,              b.QNET_AR,                  b.FCAD,a.tel     from vw_sup_dem a,tb_coal_mark b,tb_sup_dem c where  a.id=b.sup_id and a.id=c.id and a.audit_mark='1' and a.id={0}";
        //177:
        /// <summary>
        /// 获取有色矿供求明细数据列表
        /// 身份账号为“wl”
        /// 0为返回行数
        /// </summary>
        public static String GetYouSeKuangGongQiuDetailList177 = "select a.TYPE_NAME,      b.class,         b.spec,            b.tons,             b.place,         b.receive_date,     b.quality,      b.price,          b.receive_place,    b.remark  from view_trade_business a, trade_business b where a.id=b.id and a.id={0}";
         //178:
        /// <summary>
        /// 获取铁矿砂供求明细数据列表
        /// 身份账号为“wl”
        /// 0为返回行数
        /// </summary>
        public static String GetTieKuangShaGongQiuDetailList178 = "select a.TYPE_NAME,      b.class,         b.spec,            b.tons,             b.place,         b.receive_date,     b.quality,      b.price,          b.receive_place,    b.remark  from view_trade_business a, trade_business b where a.id=b.id and a.id={0}";              
	    //179:
        /// <summary>
        /// 获取园区仓储明细数据列表
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String GetYuanQuCangChuDetailList179 = "select topic,szd,case when sxid='0' then '全部' when sxid='1' then '自有仓储' when sxid='2' then '租用仓储' end as ccsx,lx,kfmj,          kccg,            dcmj,         kymj,             bgmj,           kfcs ,        kccg,            xfdj,         ckjg,             ht,              zlqx,            kyks,            kyjs,             XXDZ,           yp,       fjd,              czd,               fc,               zxpt,         zxf ,        ccbj,            cxf,            descript,     tel     from vw_ccps where id='{0}'";
        //180:
        /// <summary>
        /// 获取待储货源明细数据列表
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String GetDaiChuHuoYuanDetailList180 = "select HWMC,           LXR,            LXFS,            jzsj,               SL,            CDYQ from dchy where id='{0}'";
         //181:
        /// <summary>
        /// 获取来港确报明细数据列表
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String GetLaiGangQueBaoDetailList181 = "select  fzm,          dzm,           clzs,            ziz,             fzh,               dzh,          bzcc,            fbzh,            jyzh,             hc,             yyzaz,           fyyzaz,          pbs,  dfrq from tmedi.TM_MORDDQB where id={0}";
        //182:
        /// <summary>
        /// 获整箱船期明细数据列表
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String GetZhengXiangChuanQiDetailList182 = "select a.S_S_PORT,    a.S_E_PORT,      a.S_S_NAME,       a.S_CARRYING,     a.S_STAR_TIME,      a.S_VOYAGE,    a.S_SHIPLINE,   a.S_MAN,        a.S_PHONE,            b.DESCRIPT,            b.ADDRESS,        b.TELPHONE,      a.S_REMARK   from hmw.ship_entire_box_time a,hmw.user_group b where a.USERID=b.id and a.sh=1 and a.id={0}";
        //183:
        /// <summary>
        /// 获取散杂船期明细数据列表
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String GetSanJiaChuanQiDetailList183 = "  select  a.S_S_PORT,    a.S_E_PORT,       a.S_STAR_TIME,     b.DESCRIPT,       a.S_VOYAGE,         a.S_SHIPLINE,  a.S_CARRYING,       a.s_s_name,        a.S_MAN,           a.S_PHONE,         b.ADDRESS,        b.TELPHONE,     a.S_REMARK from ship_disperses_goods_time a,user_group b where a.userid=b.id and a.sh=1 and a.id={0}";
        //184:
        /// <summary>
        /// 获取整箱运价明细数据列表
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String GetZhengXiangYunJiaDetailList184 = "select a.S_S_PORT,    a.S_E_PORT,      a.F20,            a.F40,            a.S_DEP,            a.S_SHIPLINE,   a.S_MAN,         a.S_PHONE,            b.DESCRIPT,      b.ADDRESS,        b.TELPHONE from ship_entire_box_price a,user_group b where a.USERID=b.id and a.sh=1 and a.id={0}";
		//185:
        /// <summary>
        /// 获取散杂运价明细数据列表
        /// 身份账号为“hmw”
        /// 0为返回行数
        /// </summary>
        public static String GetSanJiaYunJiaDetailList185 = "  select a.S_S_PORT,    a.S_E_PORT,       a.S_PRICE,        b.DESCRIPT,      a.S_SHIPLINE,         a.S_DEP,         a.S_MAN,           a.S_PHONE,         b.ADDRESS,        b.TELPHONE		 from ship_disperses_goods_price a,user_group b where a.USERID=b.id and a.sh=1 and a.id={0}";	
		//186:
        /// <summary>
        /// 获取有船作业明细数据列表
        /// harbor
        /// 0为返回行数
        /// </summary>
        public static String GetYouChuanZuoYeDetailList186 = "  select  chi_vessel,     voyage,         jobtype,      from_port,     to_port,             department,          bcno,            case mark_audit when '1' then '已审核' when '0' then '未审核' end as mark_audit,      createtime  from harbor.vw_bc_bconsign where bcno='{0}'";
		//187:
        /// <summary>
        /// 获取无船作业明细数据列表
        /// harbor
        /// 0为返回行数
        /// </summary>
        public static String GetWuChuanZuoYeDetailList187 = "   select department1,   qyg,            ddg,            id,            shipname,            shiphc,        zyxm,          case constat1 when 0 then '未批准' when 1 then '已批准' end as constat,        condate	 from viewgoodscon where  id='{0}'";		
		//188:
        /// <summary>
        /// 获取作业委托明细数据列表
        /// harbor
        /// 0为返回行数
        /// </summary>
        public static String GetZuoYeWeiTuoDetailList188 = "select OPERATETYPE,CLIENT,DEPARTMENT,PLANVOLUME,TASKNO,CGNO,CODE_CLIENT,shipname,code_department,PLANAMOUNT,PLANWEIGHT,CREATETIME,BLNO,sign_mark, case sign_mark when '0' then '' when '1' then '√' end as sign_mark1,complete_mark,case complete_mark when '0' then '' when '1' then '√' end as complete_mark1,mark_dc,case mark_dc when '0' then '' when '1' then '√' end as mark_dc1,mark_hy,case mark_hy when '0' then '' when '1' then '√' end as mark_hy1  from  vw_web_consign_query where  CGNO='{0}'";
 		//189:
        /// <summary>
        /// 获取我的票货明细数据列表
        /// harbor
        /// 0为返回行数
        /// </summary>
        public static String GetWoDePiaoHuoDetailList189 = "select Cargo,      Inout,            Trade,           PieceWeight,   Mark,              Gbno,             Pack,          Measure,          ShipName,       createtime,       case sign_mark when '0' then '' when '1' then '√' end as sign,COMPLETE_MARK from vw_qyf_goodsbillnet where gbno='{0}'";
  		//190:
        /// <summary>
        /// 获取货物进港明细数据列表
        /// harbor
        /// 0为返回行数
        /// </summary>
        public static String GetHuoWuJingGangDetailList190 = "select signdate,     department,    cargo,           weight,          OperateType,            inout,          amount,      pieceweight    from vw_stockin_web where id='{0}'";			
		//191:
        /// <summary>
        /// 获取货物出港明细数据列表
        /// harbor
        /// 0为返回行数
        /// </summary>
        public static String GetHuoWuChuGangDetailList191 = "select signdate,     department,    cargo,           weight,          OperateType,            inout,          amount,      pieceweight    from vw_stockout_web where id='{0}'";		
		//192:
        /// <summary>
        /// 获取货物结存明细数据列表
        /// harbor
        /// 0为返回行数
        /// </summary>
        public static String GetHuoWuJieCunDetailList192 = "select cargo,       weight,      department,      section,         inout,          trade,            storage         amount,          volume,      signdate  from vw_stockdormant_web where id='{0}'";		
		//193:
        /// <summary>
        /// 获取费用结算明细数据列表
        /// 身份账号为“harbor”
        /// 0为返回行数
        /// </summary>
        public static String GetFeiYongJieSuanDetailList193 = "  select     a.signdate,     a.VOYAGE,        replace(b.department,'分公司','') department,       a.total,        a.PAYER,             a.VESSEL_CN,         a.IVDISPLAY,     a.BILLYEAR,         a.AUDITOR,        a.AUDITTIME,       a.RECEIVER,         a.RECEIVETIME from tb_pro_invoice a ,tb_code_department b where a.code_department=b.code_department and ivno='{0}'";
        //194:
        /// <summary>
        /// 获取我的业务事项明细数据列表
        /// 身份账号为“hmw”
        /// 0为服务传入参数用户ID
        /// 1为返回行数
        /// </summary>
        public static String GetWoDeYeWuShiXiangDetailList194 = "select SIGNDATE,     YWLX,         VGDISPLAY,        CARGO,          FACTWEIGHT,       YWLX_DW,         DEPARTMENT,      TASKNO from jszx_ywl t where TASKNO='{0}'";
        //195:
        /// <summary>
        /// 获取我的已付账单明细数据列表
        /// 身份账号为“hmw”
        /// 0为服务传入参数用户ID
        /// 1为返回行数
        /// </summary>
        public static String GetWoDeYiFuZhangDanDetailList195 = "select CREATETIME,     FYLX,         RBDISPLAY,        TOTAL,          company from jszx_fp where RBNO='{0}'";
		//196:
        /// <summary>
        /// 获取我的预报船明细数据列表
        /// 身份账号为“BASERESOURCE”
        /// 0为传入参数船代客户ID,1为系统时间
        /// 2为返回行数
        /// </summary>
        public static String GetWoDeYuBaoChuanDetailList196 = "select  chi_vessel,      yjdg,            xhmc,          xhsl,           zhmc,            zhsl,            ys,              ship_id  from view_sship_tan where ship_id='{0}'";	
		//197:
        /// <summary>
        /// 获取我的确报船明细数据列表
        /// 身份账号为“BASERESOURCE”
        /// 0为传入参数船代客户ID,1为系统时间
        /// 2为返回行数
        /// </summary>
        public static String GetWoDeQueBaoChuanDetailList197 = "select chi_vessel,         yjdg,            xhmc,          xhsl,            zhmc,          zhsl,          ys ,            S_declare,       shipstatus ,       ship_id   from view_sship_tan where ship_id='{0}'";								
        //198:
        /// <summary>
        /// 获取我的引航费用明细数据列表
        /// 身份账号为“mobilecenter”
        /// 0为传入参数船代客户ID
        /// 1为返回行数
        /// </summary>
        public static String GetWoDeYinHangFeiYongDetailList198 = " select  invoicenum,         chi_vessel,      invoicedate,          fcode,       amount,       fk,            fkdw ,           fkdjh,         ton_net   from  (select viewworkbill.CHI_VESSEL,viewworkbill.FCODE,invoice.invoicenum,ton_net,amount,invoicedate,invoice.fkdw,invoice.sid,case when invoice.fk='0' then '否' when invoice.fk='1' then '是' end as fk,case when invoice.fkdjh='00000' then  '  ' else invoice.fkdjh end as fkdjh from inhual.viewworkbill,inhual.invoice ) where invoicenum='{0}'";		
		//199:
        /// <summary>
        /// 获取我的高频话费明细数据列表
        /// 身份账号为“mobilecenter”
        /// 0为传入参数船代客户ID
        /// 1为返回行数
        /// </summary>
        public static String GetWoDeGaoPinHuaFeiDetailList199 = "select INVOICENUM,       chi_vessel,        fzs,              money ,           fkdw ,          sta_time,         end_time,          fht,          sht,            name  from ddmis.viewtxgpinfo where id='{0}'";	 
		//200:
        /// <summary>
        /// 获取我的泊位船舶明细数据列表
        /// 身份账号为“BASERESOURCE”
        /// 0为传入参数船代客户ID
        /// 1为返回行数
        /// </summary>
        public static String GetWoBoWeiChuanBoDetailList200 = "select ship_id,       chi_vessel,        BERTHNO,           BERTH_POSITION ,     zhmc ,         zhsl,             xhmc,              xhsl,          BerTH_time,    dqyd  ,   SHIPSTATUS,  name,    DepartMent	 from view_sship_tan where ship_id='{0}'";
        //201:
        /// <summary>
        /// 获取我的船舶计划明细数据列表
        /// 身份账号为“BASERESOURCE”
        /// 0为返回行数
        /// </summary>
        public static String GetWoDeChuanBoJiHuaDetailList201 = " select cbdl,            chi_vessel,          dqbw,            bwwz,          pc,        pilotage,      xhm ,             xhsl ,            zhm,              zhsl,              yjjg,          yjlg,        remark  ,    plandate	 from ywcplan where p_id='{0}'";
   
     

    }
}