using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Common
{
    /// <summary>
    /// 数据项工具
    /// </summary>
    public class TableItemTool
    {

        /// <summary>
        /// 获取指定编号的数据项集合
        /// </summary>
        /// <param name="tableIndex">从零开始的数据项集合索引</param>
        /// <returns></returns>
        public static string[] GetTableItemSet(int tableIndex)
        {
            switch (tableIndex)
            {
                //在港船舶
                case 0:
                    string[] zaigangchuanbo0 =        {    "船代",     "中文船名",    "泊位-号",     "泊位-位置",   "作业公司",     "状态",           "余吨",          "英文船名",      "国籍",        "内外贸",      "卸货名称",       "卸货数量",         "装货名称",      "装货数量"       };
                                                      //    NAME,      CHI_VESSEL,    BERTHNO,       BERTH_POSITION,DEPARTMENT,    SHIPSTATUS,        DQYD,            ENG_VESSEL,      NATION_CHA,    TRADE,         XHMC,              XHSL,              ZHMC,             ZHSL              
                    return zaigangchuanbo0;
                //锚地船舶
                case 1:
                    string[] maodichuanbo1 =          {   "船代",      "中文船名",     "进港吃水",   "出港吃水",    "贸别",          "抵锚时间",      "引水标识",        "英文船名",     "国籍",         "船长",        " 卸货名称",         "卸货数量",       "装货名称",     "装货数量"    };
				                                      //    NAME,      CHI_VESSEL,    THIS_DRAFT,    CHU_DRAFT,      TRADE,          YJDG,            YS,               eng_vessel,      NATION_CHA,       LOA,           XHMC,               XHSL,             ZHMC,           ZHSL
                    return maodichuanbo1;
                //进出港计划
                case 2:
                    string[] jingchugangjihua2 =      {   "船代",      "船名",         "泊位-号",      "泊位-位置", "属性",         "引水标识",      "国籍",          "船长",         "进港吃水",       "出港吃水",      "计划进港时间",      "备注"   };
				                                      //    CBDL,      CHI_VESSEL,     DQBW,           BWWZ,         PC,              PILOTAGE,        GJ,              CZ,            THIS_DRAFT,      CHU_DRAFT,       YJJG,               REMARK
                    return jingchugangjihua2;
                //最新船期
                case 3:  
                    string[] zuixinchuanqi3 =         {   "起始港",     "目的港",      "船名",        "载重吨",       "开航日期",      "航期",       "航线",         "联系人",        "联系电话",       "发布公司",      " 地址",         "电话",       "备注"        };
				                                      //  S_S_PORT,    S_E_PORT,      S_S_NAME,       S_CARRYING,     S_STAR_TIME,      S_VOYAGE,    S_SHIPLINE,      S_MAN,           S_PHONE,          DESCRIPT,       ADDRESS,        TELPHONE,      S_REMARK							
                    return zuixinchuanqi3;
                //最新货盘
                case 4:
                    string[] zuixinhuopan4 =          {   "装运港",    "目的港",      "F20",          "F40",          "所属船公司",     "航线",      "联系人",        "联系电话",     "发布公司",       "地址",         " 电话", };
                                                      //  S_S_PORT,    S_E_PORT,      F20,            F40,            S_DEP,            S_SHIPLINE,   S_MAN,         S_PHONE,          DESCRIPT,         ADDRESS,        TELPHONE
                    return zuixinhuopan4;
                //矿石专区
                case 5:
                  //string[] kuangshizhuanqu5 =       {  "供求类型",   "品名",        "规格",        "数量",          "产地",          "交货日期",   "品质证",         "质量",        "价格",          "交货地",        "备注"  };
                    string[] kuangshizhuanqu5 =       {   "供求类型",   "品名",        "规格",        "数量",          "产地",           "交货日期",                   "质量",         "价格",         "交货地",         "备注" };
                                                      // type_name,     CLASS,         SPEC,          TONS,            PLACE,            REG_DATE,      ,              quality,        price,          receive_place ,   remark
                   
                    return kuangshizhuanqu5;
                //煤炭专区
                case 6:
                  //string[] meitanzhuanqu6 =         {  "类型",       "产品名称",     "数量",        "产地",          "发布日期",       "意向价格",   "有效期",       "参数1(全水分%)","参数1(水分%)","参数1(干基灰分%)","参数1(干基挥发分%)","参数2(全硫%)","参数2(焦渣特征)","参数3(收到基低位发热量)","参数3(固态碳)",     "联系方式" ,     "详情" };
                    string[] meitanzhuanqu6 =         {  "类型",       "产品名称",     "数量",        "产地",          "发布日期",       "意向价格",    "有效期",      "参数1(全水分%)","参数1(水分%)","参数1(干基灰分%)","参数1(干基挥发分%)","参数2(全硫%)","参数2(焦渣特征)","参数3(收到基低位发热量)","参数3(固态碳)",      "联系方式" };
				                                      //  TYPE,        GOODSNAME,      NUM,           AREA,            CREATETIME,       price,         yxq,            MT,              MAD,           AD,                vd,                  ST_D,          crc,              QNET_AR,                  FCAD,                tel,             message							                   
					return meitanzhuanqu6;
                //最新运力
                case 7:
                // string[] zuixinyunli7 =            {   "车型",      "车牌号码",     "可出发地",   "到达地",       "联系方式",        "吨位(吨)",    "车长(米)",    "车况介绍",     "信息截止日期" };
				   string[] zuixinyunli7 =            {   "车型",      "车牌号码",     "可出发地",   "到达地",       "联系方式",        "吨位(吨)",    "车长(米)",                    "信息截止日期" };
				                                      //    CX,          CPHM,           KCFD,           DDD,            LXFS,             DW,           CC,            CKJS,            jzsj  
                    return zuixinyunli7;
                //最新货源
                case 8:
                    string[] zuixinhuoyuan8 =         {   "货物名称",    "数量",        "起运地",      "到达地",       "信息截止日期",    "车辆要求",  "联系方式"    }; 
				                                      //   HWMC,          SL,            QYD,           DDD,            JZSJ,             CLYQ,         LXFS     
                    return zuixinhuoyuan8;
                //集装箱船期
                case 9:
                    string[] jizhuangxiangchuanqi9 =  {   "装运港",     "目的港",      "船名",        "载重吨",        "开航日期",       "航期",        "航线",         "联系人",        "联系电话",       "发布公司",      " 地址",         "电话",        "备注"        };
                                                      //  S_S_PORT,    S_E_PORT,       S_S_NAME,       S_CARRYING,     S_STAR_TIME,      S_VOYAGE,     S_SHIPLINE,      S_MAN,           S_PHONE,          DESCRIPT,         ADDRESS,        TELPHONE,       S_REMARK							
                    return jizhuangxiangchuanqi9;
                //集装箱运价
                case 10:
                    string[] jizhuangxiangyunjia10 =  {   "起始港",    "目的港",       "F20",          "F40",          "所属船公司",     "航线",        "联系人",        "联系电话",      "发布公司",       "地址",         " 电话", };
                                                      //  S_S_PORT,    S_E_PORT,       F20,            F40,            S_DEP,            S_SHIPLINE,    S_MAN,           S_PHONE,          DESCRIPT,        ADDRESS,        TELPHONE
                    return jizhuangxiangyunjia10;
                //散杂船期
                case 11:
                    string[] sanjiachuanqi11 =        {   "装运港",     "目的港",      "开航日期",     "发布公司",      "航期",           "航线",       "载重吨",        "船名",          "联系人",        "联系电话",      " 地址",         "电话",         "备注"        };
                                                      //  S_S_PORT,    S_E_PORT,       S_STAR_TIME,     DESCRIPT,       S_VOYAGE,         S_SHIPLINE,   S_CARRYING,       s_s_name,        S_MAN,           S_PHONE,         ADDRESS,        TELPHONE,       S_REMARK								
                    return sanjiachuanqi11;
                //散杂运价
                case 12:
                    string[] sanjiayunjia12 =         {   "装运港",     "目的港",      "运价",         "发布公司",      "航线",           "所属船公司",  "联系人",        "联系电话",      " 地址",         "电话"  };
                                                      //  S_S_PORT,     S_E_PORT,       S_PRICE,        DESCRIPT,       S_SHIPLINE,         S_DEP,         S_MAN,          S_PHONE,         ADDRESS,       TELPHONE					
                    return sanjiayunjia12;
                //在船舶
                case 13:
                    string[] zaichuanbo13 =           {   "泊位-号",   "泊位-位置",   "中文船名",      "装货名称",     "装货数量" ,      "卸货名称",    "卸货数量",       "代理",           "作业公司",       "状态",         "余吨",          "英文船名",      "国籍",        "内外贸",                 };
                                                      //   BERTHNO,     BERTH_POSITION, CHI_VESSEL,      ZHMC,           ZHSL,            XHMC,         XHSL,            NAME,              DEPARTMENT,      SHIPSTATUS,      DQYD,           ENG_VESSEL,      NATION_CHA,     TRADE,                              			
                    return zaichuanbo13;
                //锚地船
                case 14:
                    string[] maodichuan14 =           {   "中文船名",  "装货名称",    "装货数量" ,     "卸货名称",     "卸货数量",         "代理",       "进港吃水",       "出港吃水",      "贸别",          "抵锚时间",      "引水标识",     "英文船名",      "国籍",         "船长",         };
                                                      //  CHI_VESSEL,   ZHMC,           ZHSL,           XHMC,          XHSL,               NAME,         THIS_DRAFT,       CHU_DRAFT,      TRADE,           YJDG,             YS,             eng_vessel,      NATION_CHA,       LOA,          
                    return maodichuan14; 
                //来港确报
                case 15:  
                    string[] laigangquebao15 =        {   "发站名",    "到站名",      "车辆总数" ,     "列车自重",      "发站号",          "到站号",     "编组车次",       "发报站号",      "经由站号",      "列车换长",     "运营载重",      "非运营载重",     "蓬布数",      "到发日期"          };
                                                      //  fzm,          dzm,           clzs,            ziz,             fzh,               dzh,          bzcc,            fbzh,            jyzh,             hc,             yyzaz,           fyyzaz,          pbs，           dfrq          
                    return laigangquebao15; 
                //码头卸车计划
                case 16:  
                    string[] matouxiechejihua16 =     {   "作业公司",   "白夜班",      "车皮数" ,      "计划日期",       "班组",          "道号",         "流程号",        "备注",     };
                                                     //code_department,  DAYNIGHT,     TRAINS,          PLANDATE,        teamtype,         trainroad,      WORKFLOWNO,     REMARK            
                    return matouxiechejihua16;
                //码头装车计划
                case 17:  
                    string[] matouzhuangchejihua17 =  {   "作业公司",   "委托人",      "计划日期",      "计划吨数",      "垛位",          "车数",          "货物",         "到站",         "道号",          "白夜班",         "船名" };
                                                     //code_department,  CLIENT,       TAKEDATE,        CONSIGN_TON,      FIELD,         TRAINNUMBER,      CARGO,          STATION ，      trainroad,       daynight,          VESSEL_CN,           
                    return matouzhuangchejihua17;
                //已放行车辆
                //case 17:  
                //    string[] matouzhuangchejihua17 =  {   "作业公司",   "委托人",      "计划日期",      "计划吨数",      "垛位",          "车数",          "货物",         "到站",        "道号",          "白夜班",         "船名" };
                //                                     //code_department,  CLIENT,       TAKEDATE,        CONSIGN_TON,      FIELD,         TRAINNUMBER,      CARGO,          STATION ，     trainroad,       daynight,          VESSEL_CN,           
                //    return matouzhuangchejihua17;



                //网上车源
                case 19: 
                    string[] wangshangcheyuan19 =      {   "可出发地",    "到达地",      "车型",         "吨位(吨)",       "车长(米)",    "截至日期",      "车牌号码",      "联系方式"  };
                                                      //    kcfd,         ddd,          cx,             dw,                cc,           jzsj,            CPHM,            LXFS ，              
                    return wangshangcheyuan19;
                //司机信息
                case 20: 
                    string[] sijixinxi20 =             {   "姓名",      "准驾车型",       "地址",        "有效期",         "驾驶证",      "电话",          "国籍",           "出生日期",     "有效起始日期", "发证机关"   };
                                                      //    name,       PERMIT_VEH_TYPE,   ADDRESS,      REG_TIME,          DRIVER_CARD,  PHONE,            NATIONALITY,     BIRTH_DATE ，    PERMIT_BEGIN,   GRANT_DEPT            
                    return sijixinxi20;
                //灌河国际确报船
                case 21: 
                    string[] guanheguojiquebaochuan21 ={   "中文船名",  "发布时间",       "单位名称",     "国籍",         "进口航次",     "出口航次",       "性质",          "来港",          "去港",        "预计到港时间", "载货信息-序号", "载货信息-作业名称","载货信息-货名","载货信息-数量","载货信息-卸货公司"    };
                                                               
                    return guanheguojiquebaochuan21;
				 //新云台确报船
                case 22: 
                    string[] xinyuntaiquebaochuan22 =  {   "中文船名",  "发布时间",       "单位名称",     "国籍",         "进口航次",     "出口航次",       "性质",          "来港",          "去港",        "预计到港时间", "载货信息-序号", "载货信息-作业名称","载货信息-货名","载货信息-数量","载货信息-卸货公司" };
                                                       //   nvessel,      declaretime,      client,          ,             i_voyage,      o_voyage,         vgstatus,        port_last ，    port_next,      arrivetime,                              zx,              cargo,         planweight                 
                    return xinyuntaiquebaochuan22;
                //灌河国际泊位船
                case 23: 
                    string[] guanheguojiboweichuan23 =  {   "中文船名",  "发布时间",       "单位名称",     "国籍",         "进口航次",     "出口航次",       "性质",          "来港",          "去港",        "预计到港时间", "载货信息-序号", "载货信息-作业名称","载货信息-货名","载货信息-数量","载货信息-卸货公司" };
                                                       //    nvessel,      declaretime,      client,          ,             i_voyage,      o_voyage,         vgstatus,        port_last ，    port_next,      arrivetime,                              zx,              cargo,         planweight                 
                    return guanheguojiboweichuan23;
                //新云台泊位船
                case 24: 
                    string[] xinyuntaiboweichuan24 =    {   "中文船名",    "发布时间",       "单位名称",     "国籍",         "进口航次",     "出口航次",       "性质",          "来港",        "去港",        "预计到港时间", "载货信息-序号", "载货信息-作业名称","载货信息-货名","载货信息-数量","载货信息-卸货公司" };
                                                        //   nvessel,      declaretime,      client,          ,             i_voyage,      o_voyage,         vgstatus,        port_last ，    port_next,      arrivetime,                              zx,              cargo,         planweight                 
                    return xinyuntaiboweichuan24;
                //煤焦供应
                case 25:            
                    string[] meijiaogongying25 =        {  "产品名称",     "数量",        "产地",          "发布日期",       "类型",        "意向价格",       "有效期",      "参数1(全水分%)","参数1(水分%)","参数1(干基灰分%)","参数1(干基挥发分%)","参数2(全硫%)","参数2(焦渣特征)","参数3(收到基低位发热量)","参数3(固态碳)",      "联系方式" };
				                                        //  GOODSNAME,      NUM,           AREA,            CREATETIME,       TYPE,           price,           yxq,            MT,              MAD,           AD,                vd,                  ST_D,          crc,              QNET_AR,                  FCAD,                tel,             message							
                   
					return meijiaogongying25;
                //煤焦求购
                case 26:
                    string[] meijiaoqiugou26 =          { "产品名称", "数量", "产地", "发布日期", "类型", "意向价格", "有效期", "参数1(全水分%)", "参数1(水分%)", "参数1(干基灰分%)", "参数1(干基挥发分%)", "参数2(全硫%)", "参数2(焦渣特征)", "参数3(收到基低位发热量)", "参数3(固态碳)", "联系方式" };
                                                        //  GOODSNAME,      NUM,           AREA,            CREATETIME,       TYPE,           price,           yxq,            MT,              MAD,           AD,                vd,                  ST_D,          crc,              QNET_AR,                  FCAD,                tel,             message							

                    return meijiaoqiugou26;
                //有色矿供求
				case 27:            
                    string[] yousekuanggongqiu27 =      {  "供求类型",     "品名",        "规格",           "数量",           "产地",        "交货日期",      "质量",       "价格",           "交货地",       "备注" };
				                                        //  TYPE_NAME,      class,         spec,            tons,             place,         receive_date,     quality,      price,          receive_place,    remark       
                   
					return yousekuanggongqiu27;
			    //铁矿砂供求
				case 28:            
                    string[] tiekuangshagongqiu28 =     {  "供求类型",     "品名",        "规格",           "数量",           "产地",        "交货日期",      "质量",       "价格",           "交货地",       "备注" };
				                                        //  TYPE_NAME,      class,         spec,            tons,             place,         receive_date,     quality,      price,          receive_place,    remark       
                   
					return tiekuangshagongqiu28;
			    //园区仓储
				case 29:            
                    string[] yuanqucangchu29 =          {  "标题",         "所在地",       "仓储属性",      "仓储类型",        "库房面积",    "库层层高",      "堆场面积",  "可用面积",       "办公面积",     "库房层数",   "库层层高",      "消防等级",  "仓库结构",         "货梯",          "最低租赁期限", "可用开始时段",  "可用结束时段",   "详细地址",     "雨篷",   "地面涂层防静电", "每平米承重(吨)",  "地面涂层防尘",   "装卸平台",   "装卸费"  , "仓储报价",   "集装箱拆装箱费",   "发布公司",   "联系电话" };
				                                        //  topic,         szd,            ddpsx,            lx,               kfmj,          kccg,            dcmj,         kymj,             bgmj,           kfcs ,        kccg,            xfdj,         ckjg,             ht,              zlqx,            kyks,            kyjs,             XXDZ,           yp,       fjd,              czd,               fc,               zxpt,         zxf ,        ccbj,            cxf,            descript,     tel    
                   
					return yuanqucangchu29;
				//待储货源
				case 30:            
                    string[] daichuhuoyuan30 =          {  "货物名称",     "联系人",       "联系方式",      "信息截止日期",     "数量",        "场地要求" };
				                                        //  HWMC,           LXR,            LXFS,            jzsj,               SL,            CDYQ,          
                   
					return daichuhuoyuan30;
				 //来港确报
                case 31:  
                    string[] laigangquebao31 =          {   "发站名",    "到站名",      "车辆总数" ,     "列车自重",      "发站号",          "到站号",     "编组车次",       "发报站号",      "经由站号",      "列车换长",     "运营载重",      "非运营载重",     "蓬布数",      "到发日期"          };
                                                       //    fzm,          dzm,           clzs,            ziz,             fzh,               dzh,          bzcc,            fbzh,            jyzh,             hc,             yyzaz,           fyyzaz,          pbs，           dfrq          
                    return laigangquebao31;
				//整箱船期
                case 32:
                    string[] zhengxiangchuanqi32 =     {   "装运港",     "目的港",      "船名",        "载重吨",        "开航日期",       "航期",        "航线",         "联系人",        "联系电话",       "发布公司",      " 地址",         "电话",        "备注"        };
                                                       //  S_S_PORT,    S_E_PORT,       S_S_NAME,       S_CARRYING,     S_STAR_TIME,      S_VOYAGE,     S_SHIPLINE,      S_MAN,           S_PHONE,          DESCRIPT,         ADDRESS,        TELPHONE,       S_REMARK							
                    return zhengxiangchuanqi32;
                //散杂船期
                case 33:
                    string[] sanjiachuanqi33 =         {   "装运港",     "目的港",      "发布公司",    "开航日期",       "航期",           "航线",       "载重吨",        "船名",          "联系人",        "联系电话",      " 地址",         "电话",         "备注"        };
                                                       //  S_S_PORT,    S_E_PORT,       DESCRIPT,     S_STAR_TIME,      S_VOYAGE,         S_SHIPLINE,   S_CARRYING,       s_s_name,        S_MAN,           S_PHONE,         ADDRESS,        TELPHONE,       S_REMARK								
                    return sanjiachuanqi33;
                //整箱运价
                case 34:    
                    string[] zhengxiangyunjia34 =      {   "起始港",     "目的港",       "F20",          "F40",          "所属船公司",      "航线",       "联系人",       "联系电话",       "发布公司",       "地址",          " 电话", };
                                                       //  S_S_PORT,    S_E_PORT,       F20,            F40,            S_DEP,            S_SHIPLINE,    S_MAN,           S_PHONE,          DESCRIPT,        ADDRESS,        TELPHONE
                    return zhengxiangyunjia34;
                //散杂运价
                case 35:
                    string[] sanjiayunjia35 =          { "装运港",      "目的港",       "运价",          "发布公司",     "航线",            "所属船公司",  "联系人",       "联系电话",       " 地址",          "电话" };
                                                       //  S_S_PORT,     S_E_PORT,       S_PRICE,        DESCRIPT,       S_SHIPLINE,         S_DEP,         S_MAN,          S_PHONE,         ADDRESS,         TELPHONE					
                    return sanjiayunjia35;
                //有船作业
                case 36:
                    string[] youchuanzuoye36 =         { "船名",         "航次",        "作业项目",    "起运港",        "到达港",           "交接地点",     "委托ID",       "审核标识",      "创建时间"};
                                                      //  chi_vessel,     voyage,         jobtype,      from_port,      to_port,             department,     bcno,          mark_audit,      createtime											
                    return youchuanzuoye36;
                //无船作业
                case 37:
                    string[] wuchuanzuoye37 =          {  "作业项目",     "起运港",       "到达港",     "委托ID",       "船名",             "航次",         "交接地点",      "审核标识",     "创建时间"};
				                                      //   department1,   qyg,            ddg,            id,            shipname,            shiphc,        zyxm,           constat1,        condate												  
                    return wuchuanzuoye37;
                //作业委托
                case 38:
                    string[] zuoyeweituo38 =           {  "作业名称"    ,"委托人",      "作业公司",     "计划体积",     "委托号",         "作业委托ID",     "委托人ID",      "船名",         "作业公司ID",        "计划件数",      "计划重量",       "创建时间",         "提单号",  "导入标识",    "导入",    "结束标识",     "结束",         "关联审核标识",   "关联审核",   "货运审核标识",  "货运审核"};
				                                      //   OPERATETYPE,  CLIENT,        DEPARTMENT,     PLANVOLUME,     TASKNO,             CGNO,          CODE_CLIENT,       shipname,      code_department,     PLANAMOUNT,      PLANWEIGHT,     CREATETIME,         BLNO,     sign_mark,   sign_mark,    complete_mark, complete_markT,     mark_dc,        mark_dc,     mark_hy,        mark_hy					
                    return zuoyeweituo38;
                //我的票货
                case 39:
                    string[] wodepiaohuo39 =           {   "货名",     "进出口",        "内外贸",        "件重",         "唛头",            "票货ID",          "包装",       "单位",          "船名",        "创建时间",      "导入标识",        "结束标识"};
				                                      //   Cargo,      Inout,            Trade,           PieceWeight,    Mark,              Gbno,             Pack,          Measure,        ShipName,       createtime,       sign_mark,        COMPLETE_MARK
                    return wodepiaohuo39;  
                //货物进港
                case 40:
                    string[] huowujingang40 =          {   "进港时间",   "作业公司",     "货种",        "重量",           "作业类型",        "进出口",       "件数",       "件重"};
				                                      //   signdate,     department,    cargo,           weight,          OperateType,       inout,          amount,      pieceweight                      
					return huowujingang40;
                //货物出港
                case 41:
                    string[] huowuchugang41 =          {   "进港时间",   "作业公司",     "货种",        "重量",           "作业类型",        "进出口",       "件数",       "件重"};
				                                      //   signdate,     department,    cargo,           weight,          OperateType,       inout,          amount,      pieceweight                      
					return huowuchugang41;
                //货物结存
                case 42:
                    string[] huowujiecun42 =          {    "货名",        "重量",       "作业公司",      "作业区域",      "进出口",          "内外贸",         "货场",      "件数",            "体积",      "进港时间"};
                                                       //   cargo,       weight,      department,       section,           inout,             trade,           storage         amount,          volume,      signdate 
					return huowujiecun42;
			    //费用结算
                case 43:
                    string[] feiyongjiesuan43 =       {    "记账日期",    "航次",       "作业公司",       "金额(元)",     "付款人",         "中文船名",         "描述",       "票据年份",       " 审核人",       "审核时间",       "核收人",       " 核收时间",};
                                                      //    signdate,     VOYAGE,        department,       total,         PAYER,             VESSEL_CN,         IVDISPLAY,     BILLYEAR,         AUDITOR,         AUDITTIME,       RECEIVER,         RECEIVETIME													
                    return feiyongjiesuan43;             
				//业务事项
                case 44:
                    string[] yewushixiang44 =        {    "作业时间",     "业务类型",    "船名",         "名称",          "时长",            "单位",         "作业公司",       "委托号",     };
                                                      //    SIGNDATE,     YWLX,         VGDISPLAY,        CARGO,          FACTWEIGHT,        YWLX_DW,         DEPARTMENT,      TASKNO												
                    return yewushixiang44;			 			
		        //已付账单
                case 45:    
                    string[] yifuzhangdan45 =        {    "时间",         "费用类型",     "船名",         "金额",          "作业公司"};
                                                      //   CREATETIME,     FYLX,         RBDISPLAY,        TOTAL,          company	
                    return yifuzhangdan45;	
	            //我的预报船
                case 46:    
                    string[] wodeyubaochuan46 =      {    "中文船名",     "预计到港时间",   "卸货货种",     "卸货数量",    "装货货种",      "装货数量",      "引水标识" ,     "船舶ID"       };
                                                      //   chi_vessel,      yjdg,            xhmc,          xhsl,           zhmc,            zhsl,            ys,              ship_id        	
                    return wodeyubaochuan46;	
	            //我的确报船
                case 47:           
                    string[] wodequebaochuan47 =      {    "中文船名",       "确报时间",       "卸货货种",     "卸货数量",     "装货货种",     "装货数量",      "引水标识",      "申报时间",       "船舶状态",       "船舶ID"           };
                                                      //    chi_vessel,         yjdg,            xhmc,            xhsl,            zhmc,          zhsl,          ys ,            S_declare,       shipstatus ,       ship_id           	  
                    return wodequebaochuan47;	
			    //我的引航费用
                case 48:           
                    string[] woyinhangfeiyong48 =      {    "发票号",          "中文船名",      "发票日期",       "费率代码",     "金额",    "是否付款",     "付款单位",      "付款登记号",      "载重吨"          };
                                                      //    invoicenum,         chi_vessel,      invoicedate,          fcode,       amount,       fk,            fkdw ,           fkdjh,         ton_net    												  
                    return woyinhangfeiyong48;
				//我的高频话费
                case 49:           
                    string[] wodegaopinhuafei49 =      {    "发票号",         "中文船名",       "持续时间",        "费用",       "付款单位" ,       "开始时间",        "结束时间",       "发送方",      "接受方",     "付款船代单位" };
                                                      //    INVOICENUM,       chi_vessel,        fzs,              money ,           fkdw ,          sta_time,         end_time,          fht,          sht,            name   												  
                    return wodegaopinhuafei49;   
				//我的泊位船舶
                case 50:           
                    string[] wodeboweichuanbo50 =      {   "船舶ID",         "中文船名",        "泊位",           "泊位位置",      "装货货种",      "装货数量",        "卸货货种",       "卸货数量",    "靠泊时间",    "余吨",   "船舶状态",   "船代",   "作业公司" };
                                                      //    ship_id,       chi_vessel,        BERTHNO,           BERTH_POSITION ,     zhmc ,         zhsl,             xhmc,              xhsl,          BerTH_time,    dqyd  ,   SHIPSTATUS,  namec,    DepartMent												  
					return wodeboweichuanbo50;   
				//我的船舶计划
                case 51:           
                    string[] wodebochuanbojihua51 =    {    "船代",          "中文船名",        "泊位",          "泊位位置",     "性质",    "引水" ,        "卸货名称",       "卸货重量",      "装货名称",       "装货重量",       "进港时间",    "离港时间",    "备注",     "计划时间" };
                                                      //    cbdl,            chi_vessel,          dqbw,            bwwz,          pc,        pilotage,      xhm ,             xhsl ,            zhm,              zhsl,              yjjg,          yjlg,        remark  ,    plandate								 
					return wodebochuanbojihua51;   
					
				default:
                    throw new Exception("错误的对象索引");
            }
        }
    }
}