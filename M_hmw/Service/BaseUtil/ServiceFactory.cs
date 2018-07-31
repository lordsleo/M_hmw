using M_hmw.Service.CangChuService;
using M_hmw.Service.ChuanBoService;
using M_hmw.Service.ChuanBoService.ZaSanChuanQi;
using M_hmw.Service.ChuanBoService.ZaSanYunJia;
using M_hmw.Service.ChuanBoService.ZhengXiangChuanQi;
using M_hmw.Service.ChuanBoService.ZhengXiangYunJia;
using M_hmw.Service.GongLuService;
using M_hmw.Service.JieSuanZhongXinService;
using M_hmw.Service.MainPageService;
using M_hmw.Service.MaoYiService;
using M_hmw.Service.MyChuanService;
using M_hmw.Service.MyHuoService;
using M_hmw.Service.MyInfoService;
using M_hmw.Service.NeiHeService;
using M_hmw.Service.TieLuService;
using M_hmw.Service.ZiXunService;
using M_hmw.Service.ZiXunService.BenDiXinWen;
using M_hmw.Service.ZiXunService.DiFangFengQing;
using M_hmw.Service.ZiXunService.JinJiFaZhan;
using M_hmw.Service.ZiXunService.LianYunGang;
using M_hmw.Service.ZiXunService.WenHuaHuoDong;
using M_hmw.Service.ZiXunService.WuLiuMaoYi;
using M_hmw.Service.ZiXunService.ZhaoShangYinZi;
using M_hmw.Service.DetailService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.BaseUtil
{
    /// <summary>
    /// 服务工厂类，用于装配指定的服务
    /// </summary>
    public class ServiceFactory
    {
        /// <summary>
        /// 获取指定编号的服务
        /// </summary>
        /// <param name="serviceIndex">从零开始的服务索引</param>
        public static IBaseService getService(int serviceIndex)
        {
            switch (serviceIndex)
            {
                case 0:
                    //公路咨询
                    return new GongLuZiXunService0();
                case 1:
                    //已放行车辆
                    return new YiFangXingCheLiangService1();
                case 2:
                    //网上车源
                    return new WangShangCheYuanService2();
                case 3:
                    //司机信息
                    return new SiJiXinXiService3();
                case 4:
                    //内河咨询
                    return new NeiHeZiXunService4();
                case 5:
                    //灌河国际确报船
                    return new GuanHeGuoJiQueBaoChuanService5();
                case 6:
                    //新云台确报船
                    return new XinYunTaiQueBaoChuaService6();
                case 7:
                    //灌河国际泊位船
                    return new GuanHeGuoJiBoWeiChuanService7();
                case 8:
                    //新云台泊位船
                    return new XinYunTaiBoWeiChuanService8();
                case 9:
                    //国际煤焦资讯
                    return new GuoJiMeiJiaoZiXunService9();
                case 10:
                    //国内煤焦资讯
                    return new GuoNeiMeiJiaoZiXunService10();
                case 11:
                    //港口煤焦资讯
                    return new GangKouMeiJiaoZiXunService11();
                case 12:
                    //煤焦供应
                    return new MeiJiaoGongYingService12();
                case 13:
                    //煤焦求购
                    return new MeiJiaoQiuGouService13();
                case 14:
                    //煤炭行情
                    return new MeiJiaoHangQingService14();
                case 15:
                    //焦炭行情
                    return new JiaoTanHangQingService15();
                case 16:
                    //有色矿资讯
                    return new YouSeKuangZiXunService16();
                case 17:
                    //有色矿供求
                    return new YouSeKuangGongQiuService17();
                case 18:
                    //有色矿行情
                    return new YouSeKuangHangQingService18();
                case 19:
                    //铁矿砂资讯
                    return new TieKuangShaZiXunService19();
                case 20:
                    //铁矿砂供求
                    return new TieKuangShaGongQiuService20();
                case 21:
                    //铁矿砂行情
                    return new TieKuangShaHangQingService21();
                case 22:
                    //仓储资讯
                    return new CangChuZiXunService22();
                case 23:
                    //园区仓储
                    return new YuanQuCangChuService23();
                case 24:
                    //待储货源
                    return new DaiChuHuoYuanService24();
                case 25:
                    //来港确报
                    return new LaiGangQueBaoService25();
                case 26:
                    //港口要闻
                    return new GangKouYaoWenService26();
                case 27:
                    //陆桥资讯
                    return new LuQiaoZiXunService27();
                case 28:
                    //行业观察
                    return new HangYeGuanChaService28();
                case 29:
                    //船舶资讯
                    return new ChuanBoZiXunService29();
                case 30:
                    //集装箱船期
                    return new JiZhuangXiangChuanQiService30();
                case 31:
                    //运价
                    return new YunJiaService31();
                case 32:
                    //散杂船期
                    return new ZaSanChuanQiService32();
                case 33:
                    //散杂运价
                    return new ZaSanYunJiaService33();
                case 34:
                    //在泊船
                    return new ZaiBoChuanService34();
                case 35:
                    //锚地船
                    return new MaoDiChuanService35();
                case 36:
                    //铁路资讯
                    return new TieLuZiXunService36();
                case 37:
                    //来港确报
                    return new LaiGangHuoCheQueBaoService37();
                case 38:
                    //码头卸车计划
                    return new MaTouXieCheJiHuaService38();
                case 39:
                    //码头装车计划
                    return new MaTouZhuangCheJiHuaService39();
                case 40:
                    //今日头条
                    return new JinRiTouTiaoService40();
                case 41:
                    //我的预报船
                    return new MyYuBaoChuanService41();
                case 42:
                    //我的确报船
                    return new MyQueBaoChuanService42();
                case 43:
                    //我的引航费用
                    return new MyYinHangFeiService43();
                case 44:
                    //我的高频话费
                    return new MyGaoPingHuaFeiService44();
                case 45:
                    //我的泊位船舶
                    return new MyBoWeiChuanBoService45();
                case 46:
                    //我的船舶计划
                    return new MyChuanBoJiHuaService46();
                case 47:
                    //我的业务委托有船作业
                    return new MyYeWuWeiTuoYouChuanService47();
                case 48:
                    //我的业务委托无船作业
                    return new MyYeWuWeiTuoWuChuanService48();
                case 49:
                    //我的作业委托
                    return new MyZuoYeWeiTuoService49();
                case 50:
                    //我的票货
                    return new MyPiaoHuoService50();
                case 51:
                    //我的货物进港
                    return new MyHuoWuJinGangService51();
                case 52:
                    //我的货物出港
                    return new MyHuoWuChuGangService52();
                case 53:
                    //我的货物结存
                    return new MyHuoWuJieCunService53();
                case 54:
                    //我的船代货代
                    return new MyChuanDaiHuoDaiService54();
                case 55:
                    //我的个人信息
                    return new MyGeRenXinXiService55();
                case 56:
                    //我的已付账单
                    return new MyYiFuZhangDanService56();
                case 57:
                    //我的业务事项
                    return new MyYeWuShiXiangService57();
                case 58:
                    //我的费用结算
                    return new MyFeiYongJieSuanService58();
                case 59:
                    //本地新闻 兰州
                    return new BenDiXinWenLanZhouService59();
                case 60:
                    //本地新闻 山西
                    return new BenDiXinWenSanXiService60();
                case 61:
                    //本地新闻 西安
                    return new BenDiXinWenXiAnService61();
                case 62:
                    //本地新闻 新疆
                    return new BenDiXinWenXinJiangService62();
                case 63:
                    //本地新闻 郑州
                    return new BenDiXinWenZhengZhouService63();
                case 64:
                    //本地新闻 银川
                    return new BenDiXinWenYinChuanService64();
                case 65:
                    //本地新闻 西宁
                    return new BenDiXinWenXiNingService65();
                case 66:
                    //经济发展 兰州
                    return new JinJiFaZhanLanZhouService66();
                case 67:
                    //经济发展 山西
                    return new JinJiFaZhanSanXiService67();
                case 68:
                    //经济发展 西安
                    return new JinJiFaZhanXiAnService68();
                case 69:
                    //经济发展 新疆
                    return new JinJiFaZhanXinJiangService69();
                case 70:
                    //经济发展 郑州
                    return new JinJiFaZhanZhengZhouService70();
                case 71:
                    //经济发展 银川
                    return new JinJiFaZhanYinChuanService71();
                case 72:
                    //经济发展 西宁
                    return new JinJiFaZhanXiNingService72();
                case 73:
                    //物流贸易 兰州
                    return new WuLiuMaoYiLanZhouService73();
                case 74:
                    //物流贸易 山西
                    return new WuLiuMaoYiSanXiService74();
                case 75:
                    //物流贸易 西安
                    return new WuLiuMaoYiXiAnService75();
                case 76:
                    //物流贸易 新疆
                    return new WuLiuMaoYiXinJiangService76();
                case 77:
                    //物流贸易 郑州
                    return new WuLiuMaoYiZhengZhouService77();
                case 78:
                    //物流贸易 银川
                    return new WuLiuMaoYiYinChuanService78();
                case 79:
                    //物流贸易 西宁
                    return new WuLiuMaoYiXiNingService79();
                case 80:
                    //文化活动 兰州
                    return new WenHuaHuoDongLanZhouService80();
                case 81:
                    //文化活动 山西
                    return new WenHuaHuoDongSanXiService81();
                case 82:
                    //文化活动 西安
                    return new WenHuaHuoDongXiAnService82();
                case 83:
                    //文化活动 新疆
                    return new WenHuaHuoDongXinJiangService83();
                case 84:
                    //文化活动 郑州
                    return new WenHuaHuoDongZhengZhouService84();
                case 85:
                    //文化活动 银川
                    return new WenHuaHuoDongYinChuanService85();
                case 86:
                    //文化活动 西宁
                    return new WenHuaHuoDongXiNingService86();
                case 87:
                    //招商引资 兰州
                    return new ZhaoShangYinZiLanZhouService87();
                case 88:
                    //招商引资 山西
                    return new ZhaoShangYinZiSanXiService88();
                case 89:
                    //招商引资 西安
                    return new ZhaoShangYinZiXiAnService89();
                case 90:
                    //招商引资 新疆
                    return new ZhaoShangYinZiXinJiangService90();
                case 91:
                    //招商引资 郑州
                    return new ZhaoShangYinZiZhengZhouService91();
                case 92:
                    //招商引资 银川
                    return new ZhaoShangYinZiYinChuanService92();
                case 93:
                    //招商引资 西宁
                    return new ZhaoShangYinZiXiNingService93();
                case 94:
                    //地方风情 兰州
                    return new DiFangFengQingLanZhouService94();
                case 95:
                    //地方风情 山西
                    return new DiFangFengQingSanXiService95();
                case 96:
                    //地方风情 西安
                    return new DiFangFengQingXiAnService96();
                case 97:
                    //地方风情 新疆
                    return new DiFangFengQingXinJiangService97();
                case 98:
                    //地方风情 郑州
                    return new DiFangFengQingZhengZhouService98();
                case 99:
                    //地方风情 银川
                    return new DiFangFengQingYinChuanService99();
                case 100:
                    //地方风情 西宁
                    return new DiFangFengQingXiNingService100();
                case 101:
                    //整箱船期 韩国
                    return new ZhengXiangChuanQiHanGuoService101();
                case 102:
                    //整箱船期 日本
                    return new ZhengXiangChuanQiRiBenService102();
                case 103:
                    //整箱船期 内支
                    return new ZhengXiangChuanQiNeiZhiService103();
                case 104:
                    //整箱船期 美洲
                    return new ZhengXiangChuanQiMeiZhouService104();
                case 105:
                    //整箱船期 欧洲
                    return new ZhengXiangChuanQiOuZhouService105();
                case 106:
                    //整箱船期 中东
                    return new ZhengXiangChuanQiZhongDongService106();
                case 107:
                    //整箱船期 非洲
                    return new ZhengXiangChuanQiFeiZhouService107();
                case 108:
                    //整箱船期 东南亚
                    return new ZhengXiangChuanQiDongNanYaService108();
                case 109:
                    //杂散船期 韩国
                    return new ZaSanChuanQiHanGuoService109();
                case 110:
                    //杂散船期 日本
                    return new ZaSanChuanQiRiBenService110();
                case 111:
                    //杂散船期 内支
                    return new ZaSanChuanQiNeiZhiService111();
                case 112:
                    //杂散船期 美洲
                    return new ZaSanChuanQiMeiZhouService112();
                case 113:
                    //杂散船期 欧洲
                    return new ZaSanChuanQiOuZhouService113();
                case 114:
                    //杂散船期 中东
                    return new ZaSanChuanQiZhongDongService114();
                case 115:
                    //杂散船期 非洲
                    return new ZaSanChuanQiFeiZhouService115();
                case 116:
                    //杂散船期 东南亚
                    return new ZaSanChuanQiDongNanYaService116();
                case 117:
                    //整箱运价 韩国
                    return new ZhengXiangYunJiaHanGuoService117();
                case 118:
                    //整箱运价 日本
                    return new ZhengXiangYunJiaRiBenService118();
                case 119:
                    //整箱运价 内支
                    return new ZhengXiangYunJiaNeiZhiService119();
                case 120:
                    //整箱运价 美洲
                    return new ZhengXiangYunJiaMeiZhouService120();
                case 121:
                    //整箱运价 欧洲
                    return new ZhengXiangYunJiaOuZhouService121();
                case 122:
                    //整箱运价 中东
                    return new ZhengXiangYunJiaZhongDongService122();
                case 123:
                    //整箱运价 非洲
                    return new ZhengXiangYunJiaFeiZhouService123();
                case 124:
                    //整箱运价 东南亚
                    return new ZhengXiangYunJiaDongNanYaService124();
                case 125:
                    //杂散运价 韩国
                    return new ZaSanYunJiaHanGuoService125();
                case 126:
                    //杂散运价 日本
                    return new ZaSanYunJiaRiBenService126();
                case 127:
                    //杂散运价 内支
                    return new ZaSanYunJiaNeiZhiService127();
                case 128:
                    //杂散运价 美洲
                    return new ZaSanYunJiaMeiZhouService128();
                case 129:
                    //杂散运价 欧洲
                    return new ZaSanYunJiaOuZhouService129();
                case 130:
                    //杂散运价 中东
                    return new ZaSanYunJiaZhongDongService130();
                case 131:
                    //杂散运价 非洲
                    return new ZaSanYunJiaFeiZhouService131();
                case 132:
                    //杂散运价 东南亚
                    return new ZaSanYunJiaDongNanYaService132();
                case 133:
                    //本地新闻 连云港
                    return new BenDiXinWenLianYunGangService133();
                case 134:
                    //经济发展 连云港
                    return new JingJiFaZhanLianYunGangService134();
                case 135:
                    //物流贸易 连云港
                    return new WuLiuMaoYiLianYunGangService135();
                case 136:
                    //文化活动 连云港
                    return new WenHuaHuoDongLianYunGangService136();
                case 137:
                    //招商引资 连云港
                    return new ZhaoShangYinZiLianYunGangService137();
                case 138:
                    //海运新闻 连云港
                    return new HaiYunXinWenLianYunGangService138();
                case 139:
                    //陆桥记事 连云港
                    return new LuQiaoJiShiLianYunGangService139();
                case 140:
                    //获取首页要闻资讯新闻列表
                    return new GetMainPageImptNewsList140();
                case 141:
                    //获取首页在港船舶数据列表
                    return new GetMainPagePortShipList141();
                case 142:
                    //获取首页锚地船舶数据列表
                    return new GetMainPageAnchorShipList142();
                case 143:
                    //获取首页进出港计划数据列表
                    return new GetMainPageInoutPortPlanList143();
                case 144:
                    //获取首页最新船期数据列表
                    return new GetMainPageLatestShipmentList144();
                case 145:
                    //获取首页最新货盘数据列表
                    return new GetMainPageLatestPalletList145();
                case 146:
                    //获取首页矿石专区数据列表
                    return new GetMainPageOreZoneList146();
                case 147:
                    //获取首页煤炭专区数据列表
                    return new GetMainPageCoalZoneList147();
                case 148:
                    //获取首页最新运力数据列表
                    return new GetMainPageLatestTransCapList148();
                case 149:
                    //获取首页最新货源数据列表
                    return new GetMainPageLatestGoodsSourceList149();

                case 150:
                    //获取首页在港船舶明细数据列表
                    return new GetMainPagePortShipDetailList150();
                case 151:
                    // 获取首页锚地船舶明细数据列表
                    return new GetMainPageAnchorShipDetailList151();
                case 152:
                    // 获取首页进出港计划明细数据列表
                    return new GetMainPageInoutPortPlanDetailList152();
                case 153:
                    // 获取首页最新船期明细数据列表
                    return new GetMainPageLatestShipmentDetailList153();
                case 154:
                    // 获取首页最新货盘明细数据列表
                    return new GetMainPageLatestPalletDetailList154();
                case 155:
                    // 获取首页矿石专区明细数据列表
                    return new GetMainPageOreZoneDetailList155();
                case 156:
                    // 获取首页煤炭专区明细数据列表
                    return new GetMainPageCoalZoneDetailList156();
                case 157:
                    // 获取首页最新运力明细数据列表
                    return new GetMainPageLatestTransCapDetailList157();
                case 158:
                    // 获取首页最新货源明细数据列表
                    return new GetMainPageLatestGoodsSourceDetailList158();
                case 159:
                    // 获取集装箱船期明细数据列表
                    return new GetJiZhuangXiangChuanQiDetailList159();
                case 160:
                    // 获取集装箱运价明细数据列表
                    return new GetJiZhuangXiangYunJiaDetailList160();
                case 161:
                    // 获取散杂船期明细数据列表
                    return new GetSanJiaChuanQiDetailList161();
                case 162:
                    // 获取散杂运价明细数据列表
                    return new GetSanJiaYunJiaDetailList162();
                case 163:
                    // 获取在船舶明细数据列表
                    return new GetZaiChuanBoDetailList163();
                case 164:
                    // 获取锚地船明细数据列表
                    return new GetMaoDiChuanDetailList164();
                case 165:
                    // 获取来港确报明细数据列表
                    return new GetLaiGangQueBaoDetailList165();
                case 166:
                    // 获取码头卸车计划数据列表
                    return new GetMaTouXieCheJiHuaDetailList166();
                case 167:
                    // 获取码头装车计划数据列表
                    return new GetMaTouZhuangCheJiHuaDetailList167();



                case 169:
                    // 获取网上车源数据列表
                    return new GetWangShangCheYuanDetailList169();
                case 170:
                    // 获取司机信息数据列表
                    return new GetSiJiXinXiDetailList170();
                case 171:
                    // 获取灌河国际确报船信息数据列表
                    return new GetGuanHeGuoJiQueBaoChuanDetailList171();
                case 172:
                    // 获取新云台确报船信息数据列表
                    return new GetXinYunTaiQueBaoChuanDetailList172();

                case 173:
                    // 获取灌河国际泊位船信息数据列表
                    return new GetGuanHeGuoJiBoWeiChuanDetailList173();
                case 174:
                    // 获取新云台泊位船信息数据列表
                    return new GetXinYunTaiBoWeiChuanDetailList174();
                case 175:
                    // 获取煤焦供应明细数据列表
                    return new GetMeiJiaoGongYingDetailList175();
                case 176:
                    // 获取煤焦求购明细数据列表
                    return new GetMeiJiaoQiuGouDetailList176();
                case 177:
                    // 获取有色矿供求明细数据列表
                    return new GetYouSeKuangGongQiuDetailList177();
                case 178:
                    // 获取铁矿砂供求明细数据列表
                    return new GetTieKuangShaGongQiuDetailList178();
                case 179:
                    // 获取园区仓储明细数据列表
                    return new GetYuanQuCangChuDetailList179();
                case 180:
                    // 获取待储货源明细数据列表
                    return new GetDaiChuHuoYuanDetailList180();
                case 181:
                    // 获取来港确报明细数据列表
                    return new GetLaiGangQueBaoDetailList181();
                case 182:
                    // 获整箱船期明细数据列表
                    return new GetZhengXiangChuanQiDetailList182();
                case 183:
                    // 获散杂船期明细数据列表
                    return new GetSanJiaChuanQiDetailList183();
                case 184:
                    // 获整箱运价明细数据列表
                    return new GetZhengXiangYunJiaDetailList184();
                case 185:
                    // 获散杂运价明细数据列表
                    return new GetSanJiaYunJiaDetailList185();

                case 186:
                    // 获取有船作业明细数据列表
                    return new GetYouChuanZuoYeDetailList186();
                case 187:
                    // 获取无船作业明细数据列表
                    return new GetWuChuanZuoYeDetailList187();
                case 188:
                    // 获取作业委托明细数据列表
                    return new GetZuoYeWeiTuoDetailList188();
                case 189:
                    // 获取我的票货明细数据列表
                    return new GetWoDePiaoHuoDetailList189();
                case 190:
                    // 获取货物进港明细数据列表
                    return new GetHuoWuJingGangDetailList190();
                case 191:
                    // 获取货物出港明细数据列表
                    return new GetHuoWuChuGangDetailList191();
                case 192:
                    // 获取货物结存明细数据列表
                    return new GetHuoWuJieCunDetailList192();
                case 193:
                    // 获取费用结算明细数据列表
                    return new GetFeiYongJieSuanDetailList193();
                case 194:
                    // 获取我的业务事项明细数据列表
                    return new GetWoDeYeWuShiXiangDetailList194();
                case 195:
                    // 获取我的已付账单明细数据列表
                    return new GetWoDeYiFuZhangDanDetailList195();
                case 196:
                    // 获取我的预报船明细数据列表
                    return new GetWoDeYuBaoChuanDetailList196();
                case 197:
                    // 获取我的确报船明细数据列表
                    return new GetWoDeQueBaoChuanDetailList197();
                case 198:
                    // 获取我的引航费用明细数据列表
                    return new GetWoDeYinHangFeiYongDetailList198();
                case 199:
                    // 获取我的高频话费明细数据列表
                    return new GetWoDeGaoPinHuaFeiDetailList199();
                case 200:
                    // 获取我的泊位船舶明细数据列表
                    return new GetWoBoWeiChuanBoDetailList200();
                case 201:
                    // 获取我的船舶计划明细数据列表
                    return new GetWoDeChuanBoJiHuaDetailList201();
                default:
                    throw new Exception("错误的对象索引");
            }
        }
    }
}