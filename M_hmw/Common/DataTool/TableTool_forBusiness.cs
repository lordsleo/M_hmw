using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Common.DataTool
{
    public class TableTool_forBusiness
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
                /******货代******/
                //票货查询
                case 0:
                    string[] GoodsBill = { "票货ID", "货名", "包装", "单位", "进出口", "内外贸", "船名", "件重", "创建时间", "唛头", "导入标识", "结束标识" };                                   
                    return GoodsBill;
                //业务大委托有船作业查询
                case 1:
                    string[] ShipBusinessConsign = { "委托ID", "船名", "航次", "作业项目", "交接地点", "起运港", "到达港", "审核标识", "创建时间"};
                    return ShipBusinessConsign;
                //业务大委托无船作业查询
                case 2:
                    string[] NoShipBusinessConsign = { "委托ID", "船名", "航次", "作业项目", "交接地点", "起运港", "到达港", "审核标识", "创建时间" };
                    return NoShipBusinessConsign;
                //作业委托查询
                case 3:
                    string[] JobConsign = { "作业委托ID", "作业名称", "委托人ID", "委托人", "船名", "作业公司ID", "作业公司", "计划件数", "计划重量", "计划体积", "创建时间", "委托号", "提单号", "导入标识", "导入", "结束标识", "结束", "关联审核标识", "关联审核", "货运审核标识", "货运审核" };
                    return JobConsign;
                //车辆运输查询
                case 4:
                    string[] VehicleTransport = { "委托ID", "类型", "货代ID", "货代", "作业公司ID", "作业公司",  "计划件数", "计划重量", "计划体积", "任务号", "提单号","创建时间", "结束标识", "结束", "导入标识", "导入"};
                    return VehicleTransport;
                //汽车衡量码单查询
                case 5:
                    string[] VehicleBalance = { "日期", "货代", "航次", "货名", "进出口", "内外贸", "委托号", "委托ID", "包装", "唛头", "作业公司" };
                    return VehicleBalance;
                //货物进港查询
                case 6:
                    string[] CargoIn = { "作业公司", "作业类型", "货名", "进出口", "件数", "重量", "件重", "进港时间" };
                    return CargoIn;
                //货物出港查询
                case 7:
                    string[] CargoOut = { "作业公司", "作业类型", "货名", "进出口", "件数", "重量", "件重", "进港时间" };
                    return CargoOut;
                //货物港内结存查询
                case 8:
                    string[] CargoStock = { "作业公司", "作业区域", "进出口", "内外贸", "货场", "货名", "件数", "重量", "体积", "进港时间" };
                    return CargoStock;

                /******船代******/
                //预报船舶查询
                case 9:
                    string[] ForeShip = { "船舶ID", "中文船名", "预计到港时间", "装货货种", "装货数量", "卸货货种", "卸货数量", "引水标识"};
                    return ForeShip;
                //确报船舶查询
                case 10:
                    string[] IndeedShip = { "船舶ID", "中文船名", "确报时间", "装货货种", "装货数量", "卸货货种", "卸货数量", "申报时间", "船舶状态", "引水标识" };
                    return IndeedShip;
                //锚地船舶查询
                case 11:
                    string[] AnchorShip = { "船舶ID", "中文船名", "国籍", "船长", "内外贸", "进港吃水", "离港吃水", "装货货种", "装货数量", "卸货货种", "卸货数量", "抵锚时间", "船代", "引水标识" };
                    return AnchorShip;
                //泊位船舶查询
                case 12:
                    string[] BerthShip = { "船舶ID", "中文船名", "泊位", "泊位位置", "装货货种", "装货数量", "卸货货种", "卸货数量", "靠泊时间", "余吨", "船舶状态", "船代", "作业公司" };
                    return BerthShip;
                //已做计划船舶信息列表
                case 13:
                    string[] PlanedShip = { "泊位", "泊位位置", "中文船名", "卸货名称", "卸货重量", "装货名称", "装货重量", "进港时间", "离港时间", "备注", "性质", "船代", "计划时间" };
                    return PlanedShip;
                //需要移泊船舶信息列表
                case 14:
                    string[] MoveShip = { "泊位", "泊位位置", "中文船名", "计划移泊时间", "计划移泊泊位", "计划移泊泊位位置", "计划离港时间", "备注", "性质" };          
                    return MoveShip;
                //查询引航费用信息
                case 15:
                    string[] PilotageFee = { "发票号", "中文船名", "载重吨", "费率代码", "金额", "发票日期", "是否付款", "付款登记号", "付款单位" };
                    return PilotageFee;
                //查询高频话费信息
                case 16:
                    string[] CommunicationFee = { "中文船名", "开始时间", "结束时间", "发送方", "接受方", "持续时间", "费用", "发票号", "付款船代单位", "付款单位" };
                    return CommunicationFee;

                /******货主******/
                //货物进港动态信息
                case 17:
                    string[] CargoIn2 = { "作业公司", "作业名称", "货物名称", "进出口", "件数", "重量", "件重", "进港时间" };
                    return CargoIn2;
                //货物出港动态信息
                case 18:
                    string[] CargoOut2 = { "作业公司", "作业名称", "货物名称", "进出口", "件数", "重量", "件重", "进港时间" };
                    return CargoOut2;
                //货物兑动态信息
                case 19:
                    string[] CargoDui = { "作业公司", "作业名称", "货物名称", "进出口", "船名航次", "件数", "重量", "件重", "进港时间" };
                    return CargoDui;
                //查询货物港内结存信息
                case 20:
                    string[] CargoBalance = { "作业公司", "作业区域", "进出口", "内外贸", "货场名", "货物名称", "件数", "重量", "体积", "进港时间" };
                    return CargoBalance;
                //case 21:
                //case 22:
                //case 23:
                //case 24:
                //case 25:

                /******货运******/
                //case 26:
                //网页申报未导入车队车辆
                case 27:
                    string[] VehicleDeclaration = { "车牌号码", "车辆类别", "车辆类型", "使用性质", "品牌", "自重", "载重", "档案号", "所有人", "手机", "年检有效期", "申报时间" };
                    return VehicleDeclaration;
                //已导入车队车辆
                case 28:
                    string[] VehicleRegistration = { "车牌号码", "车辆类别", "车辆类型", "使用性质", "品牌", "自重", "载重", "卡号", "是否有效", "卡性质", "档案号", "所有人", "手机", "审核状态", "导入时间" };           
                    return VehicleRegistration;
                //新路桥公司作业计划
                case 29:
                    string[] NewLandBridgeWorkPlan = { "编号", "任务号", "货票号", "货票描述", "计划件数", "计划重量", "计划车数", "提交车数", "空磅地点", "重磅地点", "货代", "申请人", "审核人", "库场", "货位", "计划时间" };
                    return NewLandBridgeWorkPlan;
                //已放行运输列表
                case 30:
                    string[] PassedTransportation = { "任务号", "货名", "车号", "司机", "联系手机", "船名", "预到时间", "申报人", "申报时间", "放行时间" };
                    return PassedTransportation;
                //已提交未放行运输列表
                case 31:
                    string[] NoPassedTransportation = { "任务号", "货名", "车号", "司机", "联系手机", "船名", "预到时间", "申报人", "申报时间", "放行时间" };
                    return NoPassedTransportation;
				//磅单查询
                case 32:
                    string[] WeightNote = { "委托号", "时间", "票号", "车号", "件数", "毛重", "皮重", "净重", "司磅员"};
                    return WeightNote;
                default:
                    throw new Exception("错误的对象索引");
            }
        }
    }
}