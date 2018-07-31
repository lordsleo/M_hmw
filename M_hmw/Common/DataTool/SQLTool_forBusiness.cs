using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Common.DataTool
{
    public class SQLTool_forBusiness
    {
        /// <summary>
        /// 获取业务查询模块指定编号的SQL语句,
        /// </summary>
        /// <param name="sqlIndex">从零开始的数据项集合索引</param>
        /// <returns></returns>
        public static string GetSQL(int sqlIndex)
        {
            switch (sqlIndex)
            {
                /******货代******/
                /// <summary>
                /// 获取票货查询明细数据列表
                /// 身份账号为“harbor”
                case 0:
                    string GoodsBill = "select Gbno, Cargo, Pack , Measure, Inout  , Trade  , ShipName , PieceWeight, createtime , Mark , case sign_mark when '0' then '' when '1' then '√' end as sign,COMPLETE_MARK from vw_qyf_goodsbillnet where gbno='{0}'";
                    return GoodsBill;
                /// <summary>
                /// 获取业务大委托有船作业查询明细数据列表
                /// 身份账号为“harbor“
                case 1:
                    string ShipBusinessConsign = "select bcno,chi_vessel,voyage,jobtype,department,from_port,to_port,case mark_audit when '1' then '已审核' when '0' then '未审核' end as mark_audit, createtime from harbor.vw_bc_bconsign where bcno='{0}'";
                    return ShipBusinessConsign;
                /// <summary>
                /// 获取业务大委托无船作业查询明细数据列表
                /// 身份账号为“harbor“
                case 2:
                    string NoShipBusinessConsign = "select id, shipname,shiphc,zyxm, department1,qyg, ddg, case constat1 when 0 then '未批准' when 1 then '已批准' end as constat,condate from viewgoodscon where  id='{0}'";
                    return NoShipBusinessConsign;
                /// <summary>
                /// 获取作业委托查询查询明细数据列表
                /// 身份账号为“harbor“
                case 3:
                    string JobConsign = "select CGNO,OPERATETYPE,CODE_CLIENT,CLIENT,shipname,code_department,DEPARTMENT,PLANAMOUNT,PLANWEIGHT,PLANVOLUME,CREATETIME,TASKNO,BLNO,sign_mark, case sign_mark when '0' then '' when '1' then '√' end as sign_mark1,complete_mark,case complete_mark when '0' then '' when '1' then '√' end as complete_mark1,mark_dc,case mark_dc when '0' then '' when '1' then '√' end as mark_dc1,mark_hy,case mark_hy when '0' then '' when '1' then '√' end as mark_hy1  from  vw_web_consign_query where  CGNO='{0}'";
                    return JobConsign;
                /// <summary>
                /// 获取车辆运输查询明细数据列表
                /// 身份账号为“harbor“
                case 4:
                    string VehicleTransport = "select cgno,OperateType,code_client,client,code_department,department,PlanAmount,PlanWeight,PlanVolume,TaskNo,Blno,createtime,complete_mark,complete_mark1,sign_mark,net from((select OPERATETYPE,CLIENT,complete_mark,DEPARTMENT,PLANAMOUNT,PLANWEIGHT,PLANVOLUME,CREATETIME,TASKNO,CODE_DEPARTMENT,CODE_CLIENT,BLNO,CGNO,sign_mark, case sign_mark when '0' then '' when '1' then '√' end as net,case complete_mark when '0' then '' when '1' then '√' end as complete_mark1 from vw_web_consign_query)union all(select OPERATETYPE,CLIENT,complete_mark,DEPARTMENT,PLANAMOUNT,PLANWEIGHT,PLANVOLUME,CREATETIME,TASKNO,CODE_DEPARTMENT,CODE_CLIENT,BLNO,CGNO,net_mark, case net_mark when '0' then '' when '1' then '√' end as net,case complete_mark when '0' then '' when '1' then '√' end as complete_mark1 from vw_qyf_consign)) where cgno = '{0}'";
                    return VehicleTransport;
                /// <summary>
                /// 获取汽车衡量码单查询明细数据列表
                /// 身份账号为“harbor
                case 5:
                    string VehicleBalance = "select signdate,client,vgdisplay,cargo,inout,trade,taskno,cgno,pack,mark,department from vw_gjp_consign where id ='{0}'";
                    return VehicleBalance;
                /// <summary>
                /// 获取货物进港查询明细数据列表
                /// 身份账号为“harbor“
                case 6:
                    string CargoIn = "select department,OperateType,cargo,inout,amount,weight,pieceweight,signdate from vw_stockin_web where id='{0}'";
                    return CargoIn;
                /// <summary>
                /// 获取货物出港查询明细数据列表
                /// 身份账号为“harbor“
                case 7:
                    string CargoOut = "select department,OperateType,cargo,inout,amount,weight,pieceweight,signdate from vw_stockout_web where id='{0}'";
                    return CargoOut;
                /// <summary>
                /// 获取货物港内结存查询明细数据列表
                /// 身份账号为“harbor“
                case 8:
                    string CargoStock = "select department,section,inout,trade,storage,cargo,amount,weight,volume,signdate from vw_stockdormant_web where id='{0}'";
                    return CargoStock;

                /******船代******/
                /// <summary>
                /// 获取预报船舶查询明细数据列表
                /// 身份账号为“harbor“
                case 9:
                    string ForeShip = "select ship_id,chi_vessel,yjdg,zhmc,zhsl,xhmc,xhsl,ys from view_sship_tan where ship_id='{0}'";
                    return ForeShip;
                /// <summary>
                /// 获取确报船舶查询明细数据列表
                /// 身份账号为“harbor“
                case 10:
                    string IndeedShip = "select ship_id,chi_vessel,yjdg,zhmc,zhsl,xhmc,xhsl,S_declare,shipstatus,ys from view_sship_tan where ship_id='{0}'";
                    return IndeedShip;
                /// <summary>
                /// 获取锚地船舶查询明细数据列表
                /// 身份账号为“harbor“
                case 11:
                    string AnchorShip = "select ship_id,chi_vessel,nation_cha,loa,trade,THIS_DRAFT,CHU_DRAFT,zhmc,zhsl,xhmc,xhsl,yjdg,name,ys from view_sship_tan where ship_id='{0}'";
                    return AnchorShip;
                /// <summary>
                /// 获取泊位船舶查询明细数据列表
                /// 身份账号为“harbor“
                case 12:
                    string BerthShip = "select ship_id,chi_vessel,BERTHNO,BERTH_POSITION,zhmc,zhsl,xhmc,xhsl,BerTH_time,dqyd,SHIPSTATUS,name,DepartMent from view_sship_tan where ship_id='{0}'";
                    return BerthShip;
                /// <summary>
                /// 获取已做计划船舶信息列表明细数据列表
                /// 身份账号为“harbor“
                case 13:
                    string PlanedShip = "select dqbw,bwwz,chi_vessel,xhm,xhsl,zhm,zhsl,yjjg,yjlg,remark,pc,cbdl,plandate from ywcplan where p_id='{0}'";
                    return PlanedShip;
                /// <summary>
                /// 获取需要移泊船舶信息列表明细数据列表
                /// 身份账号为“harbor“
                case 14:
                    string MoveShip = "select dqbw,bwwz,chi_vessel,yjyb,ybbw,ybbwwz, yblg,remark,pc from  YwcPlanYB where ship_id='{0}'";
                    return MoveShip;
                /// <summary>
                /// 获取查询引航费用信息明细数据列表
                /// 身份账号为“harbor“
                case 15:
                    string PilotageFee = "select  invoicenum,chi_vessel,ton_net,fcode,amount,invoicedate,fk,fkdjh,fkdw  from (select viewworkbill.CHI_VESSEL,viewworkbill.FCODE,invoice.invoicenum,ton_net,amount,invoicedate,invoice.fkdw,invoice.sid,case when invoice.fk='0' then '否' when invoice.fk='1' then '是' end as fk,case when invoice.fkdjh='00000' then  '  ' else invoice.fkdjh end as fkdjh from inhual.viewworkbill,inhual.invoice ) where invoicenum='{0}'";
                    return PilotageFee;
                /// <summary>
                /// 获取查询高频话费信息明细数据列表
                /// 身份账号为“harbor“
                case 16:
                    string CommunicationFee = "select chi_vessel,sta_time,end_time,fht,sht,fzs,money,INVOICENUM, name,fkdw from ddmis.viewtxgpinfo where id='{0}'";
                    return CommunicationFee;

                /******货主******/
                /// <summary>
                /// 获取货物进港动态信息明细数据列表
                /// 身份账号为“harbor“
                case 17:
                    string CargoIn2 = "select department,operatetype,Cargo,inout,amount,weight,pieceweight,signdate  from vw_stockin_web where id='{0}'";
                    return CargoIn2;
                /// <summary>
                /// 获取货物出港动态信息明细数据列表
                /// 身份账号为“harbor“
                case 18:
                    string CargoOut2 = "select department,operatetype,Cargo,inout,amount,weight,pieceweight,signdate from vw_stockout_web where id='{0}'";
                    return CargoOut2;

                /// <summary>
                /// 获取货物兑动态信息明细数据列表
                /// 身份账号为“harbor“
                case 19:
                    string CargoDui = "select department,operatetype,Cargo,inout,vgdisplay,amount,weight,pieceweight,SignDate   from vw_stockdui_web where id='{0}'";
                    return CargoDui;
                /// <summary>
                /// 获取查询货物港内结存信息明细数据列表
                /// 身份账号为“harbor“
                case 20:
                    string CargoBalance = "select department,Section,inout,trade,storage,Cargo,amount,weight,volume, signdate  from vw_stockdormant_web where id='{0}'";
                    return CargoBalance;
                //case 21:
                //case 22:
                //case 23:
                //case 24:
                //case 25:

                /******货运******/
                //case 26:
                /// <summary>
                /// 获取网页申报未导入车队车辆明细数据列表
                /// 身份账号为“harbor“
                case 27:
                    string VehicleDeclaration = "select VEHICLE,VEH_CLASS_TYPE,VEH_TYPE,USE_KIND,BRAND_CODE,SELF_WEIGHT,LOAD_WEIGHT,VEH_CARD,OWNERNAME,TEL,INSPECT_PERIOD,DECLARETIME  from Transit.s_vehicle_web where id='{0}'";
                    return VehicleDeclaration;
                /// <summary>
                /// 获取已导入车队车辆明细数据列表
                /// 身份账号为“harbor“
                case 28:
                    string VehicleRegistration = "select VEHICLE,VEH_CLASS_TYPE,VEH_TYPE,USE_KIND,BRAND_CODE,SELF_WEIGHT,LOAD_WEIGHT,card_no,mark_validname,TypeName,VEH_CARD, OWNERNAME,OWNER_PHONE,emark,INSPECT_PERIOD,INPUT_TIME  from Transit.s_vehicle_br where id='{0}'";
                    return VehicleRegistration;
                /// <summary>
                /// 获取新路桥公司作业计划明细数据列表
                /// 身份账号为“harbor“
                case 29:
                    string NewLandBridgeWorkPlan = "select plan_no,taskno,GBNO,gbdisplay,plan_amount,PLAN_WEIGHT,PLAN_VEH_NUM,fact_VEH_NUM,balance1,balance2,CLIENT,username,auditorname,CODE_STORAGE,CODE_BOOTH,OPERTIME from v_consign_plan_xlq where plan_no='{0}'";
                    return NewLandBridgeWorkPlan;
                /// <summary>
                /// 获取已放行运输列表明细数据列表
                /// 身份账号为“harbor“
                case 30:
                    string PassedTransportation = "select TASKNO,CARGO,VEHICLE,DRIVERNAME,DRIVERPHONE,VESSEL,PREPTIME,SUBMITERNAME,SUBMITTIME,AUDITTIME from V_CONSIGN_VEHICLE_XLQ_PASS where id='{0}' ";
                    return PassedTransportation;
                /// <summary>
                /// 获取已提交未放行运输列表明细数据列表
                /// 身份账号为“harbor“
                case 31:
                    string NoPassedTransportation = "select TASKNO,CARGO,VEHICLE,DRIVERNAME,DRIVERPHONE,VESSEL,PREPTIME,SUBMITERNAME,SUBMITTIME,AUDITTIME from V_CONSIGN_VEHICLE_XLQ_PASS where id='{0}' ";
                    return NoPassedTransportation; 
                /// <summary>
                /// 获取磅单查询明细数据列表
                /// 身份账号为“BalanceCenter“
                case 32:
                    string WeightNote = "select consign,finishtime,Ticket,Truck,Amount,weight2,weight1,(weight2-weight1) weight ,operator2 from BalanceCenter..vw_Metages  where Ticket='{0}'";
                    return WeightNote; 


                default:
                    throw new Exception("错误的对象索引");
            }
        }
    }
}
