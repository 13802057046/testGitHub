using KssApi.Common;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using NLog;
using System;

namespace KssApi.Logics
{
    /// <summary>
    /// logisticsLogic
    /// </summary>
    public class LogisticsLogic
    {
        private SqlDataHelp sd = new SqlDataHelp();

        /// <summary>
        /// 发货回传接口，修改物流信息失败
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="logisticsno"></param>
        /// <param name="logisticsid"></param>
        /// <returns></returns>
        public int UpdateDelivery(string orderid, string logisticsno, string logisticsid)
        {
            try
            {
                ////更新t_client
                //StringBuilder sbTemp = new StringBuilder();

                ////发货单
                //sbTemp.AppendLine(" UPDATE t_Delivery SET                           ");
                //sbTemp.AppendLine("     F_ExpressNo = '" + logisticsno + "'         "); //快递单号
                //sbTemp.AppendLine("    ,F_ExpressCode = '" + logisticsid + "'       "); //物流公司代码
                //sbTemp.AppendLine("    ,F_DeliveryDate = getdate()                  "); //发货时间
                //sbTemp.AppendLine(" WHERE F_BillID = '" + orderid + "';             ");

                //int ret = sd.ExecuteNonQuery(sbTemp.ToString());

                //sbTemp = new StringBuilder();
                ////已删除发货单
                //sbTemp.AppendLine(" UPDATE t_DelDelivery SET                        ");
                //sbTemp.AppendLine("     F_ExpressNo = '" + logisticsno + "'         "); //快递单号
                //sbTemp.AppendLine("    ,F_ExpressCode = '" + logisticsid + "'       "); //物流公司代码
                //sbTemp.AppendLine("    ,F_DeliveryDate = getdate()                  "); //发货时间
                //sbTemp.AppendLine(" WHERE F_BillID = '" + orderid + "';             ");

                //sd.ExecuteNonQuery(sbTemp.ToString());

                //sbTemp = new StringBuilder();
                ////待发货的销售单转成正常
                //sbTemp.AppendLine(" UPDATE t_Pos SET                                                                          ");
                //sbTemp.AppendLine("     F_Date = getdate()                                                                    ");
                //sbTemp.AppendLine(" where F_BillID in (select F_PosBillID from t_Delivery where F_BillID = '" + orderid + "');");

                //sd.ExecuteNonQuery(sbTemp.ToString());

                string sql = @"
                declare @WLBill		nvarchar(30),   --发货单号
		                @POBill		nvarchar(30),   --销售单号
		                @ExNo		nvarchar(30),   --快递单号
		                @ExCode		nvarchar(30),   --物流公司代码
		                @getdate	datetime,       --发货时间
		                @sql		nvarchar(max),  --拼SQL
		                @cols		nvarchar(max),  --列s
		                @msg		nvarchar(max)   --返回信息
		
                set @WLBill = '" + orderid + @"'
                set @ExNo = '" + logisticsno + @"'
                set @ExCode = '" + logisticsid + @"'
                set @getdate = getdate()
                set @sql = ''
                set @msg = ''

                --发货单部分
                if exists(select * from t_Delivery where F_BillID = @WLBill)
                begin
                    select @POBill = F_PosBillID from t_Delivery where F_BillID = @WLBill
                end
                else
                begin
                    if exists(select * from t_DelDelivery where F_BillID = @WLBill)
                    begin
                        set @msg += ' 已删除发货单 ' + @WLBill + ' 还原 '

                        --已删除发货单还原
                        set @cols = ''
                        select @cols+= name + ',' from syscolumns   
                        where id = object_id('t_Delivery')
                        and name in (select name from syscolumns where id = object_id('t_DelDelivery'))
		                order by colid

                        select @sql += '
                        insert into t_Delivery
                        (
                            '+left(@cols,len(@cols)-1)+'
                        )
                        select
                            '+replace(left(@cols,len(@cols)-1),'F_BuyerRemark','isnull(F_BuyerRemark, '''') + ''已删除单据还原''')+'
                        from t_DelDelivery where F_BillID = '''+@WLBill+'''
                        '

                        --已删除发货明细还原
                        set @cols = ''
                        select @cols+= name + ',' from syscolumns   
                        where id = object_id('t_DeliveryDetail')
                        and name in (select name from syscolumns where id = object_id('t_DelDeliveryDetail'))
		                order by colid

                        select @sql += '
                        insert into t_DeliveryDetail
                        (
                            '+left(@cols,len(@cols)-1)+'
                        )
                        select
                            '+left(@cols,len(@cols)-1)+'
                        from t_DelDeliveryDetail where F_BillID = '''+@WLBill+'''
                        '

                        select @POBill = F_PosBillID from t_DelDelivery where F_BillID = @WLBill
                    end
                end

                --销售单部分
                if isnull(@POBill, '') <> ''
                begin
                    if not exists(select * from t_Pos where F_BillID = @POBill)
                    begin
                        if exists(select * from t_PosDel where F_BillID = @POBill)
                        begin
                            set @msg += ' 已删除销售单 ' + @POBill + ' 还原 '

                            --已删除销售单还原
                            set @cols = ''
                            select @cols+= name + ',' from syscolumns   
                            where id = object_id('t_Pos')
                            and name in (select name from syscolumns where id = object_id('t_PosDel'))
			                order by colid

                            select @sql += '
                            insert into t_Pos
                            (
                                '+left(@cols,len(@cols)-1)+'
                            )
                            select
                                '+replace(left(@cols,len(@cols)-1),'F_Remark','isnull(F_Remark, '''') + ''已删除单据还原''')+'
                            from t_PosDel where F_BillID = '''+@POBill+'''
                            '

                            --已删除销售明细还原
                            set @cols = ''
                            select @cols+= name + ',' from syscolumns   
                            where id = object_id('t_PosDetail')
                            and name in (select name from syscolumns where id = object_id('t_PosDelDetail'))
			                order by colid

                            select @sql += '
                            insert into t_PosDetail
                            (
                                '+left(@cols,len(@cols)-1)+'
                            )
                            select
                                '+left(@cols,len(@cols)-1)+'
                            from t_PosDelDetail where F_BillID = '''+@POBill+'''
                            '
                        end
                    end
                end

                exec(@sql)

                --更新WMS回传信息
                UPDATE t_Delivery SET
                    F_ExpressNo = @ExNo,
	                F_ExpressCode = @ExCode,
	                F_DeliveryDate = @getdate
                WHERE F_BillID = @WLBill

                UPDATE t_Pos SET
                    F_Status = N'正常',
                    F_Date = @getdate
                where F_BillID = @POBill
                
                exec sp_UpdateStorage @POBill, 'PS', 1

                select @msg as F_Msg
                ";

                DataTable dt = sd.GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["F_Msg"].ToString() != "")
                    {
                        Logger logger = LogManager.GetCurrentClassLogger();
                        logger.Error(dt.Rows[0]["F_Msg"].ToString());

                        return 0;
                    }

                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex.Message);

                return 0;
            }
        }
    }
}
