using BaseInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using KssService.Logic;

namespace KssService
{
    class SyncData
    {
        public static bool GlobalSuccessFlag;   //是否本次service运行全部成功
        public const int ConstPageSize = 10;    //每页大小
        private int _pageNo;                    //页码
        private int _recordCount;               //查询取得的记录数
        private DateTime _endTime;              //查询开始时刻
        private DateTime _startTime;            //查询结束时刻
        SqlDataHelp sd = new SqlDataHelp();

        public void MainProcess()
        {
            GlobalSuccessFlag = true; //初始化true

            string lastExecTime = G_INI.ReadValue("Local", "LastExecTime"); //上次执行完成时刻
            _endTime = DateTime.Now;    //查询结束时刻
            _startTime = StrUtil.IsEmptyStr(lastExecTime) ? _endTime.AddMinutes(-10) : DateTime.Parse(lastExecTime); //查询开始时刻

            //商品资料
            SyncItem();

            //采购
            SyncOutIn();

            //全量更新库存
            SyncStock();

            //销售订单
            SyncOrder();

            //销售订单
            SyncRightsOrder();

            //如果没发生过调用API错误，更新上次执行完成时刻
            if (GlobalSuccessFlag)
            {
                //G_INI.Writue("Local", "lastExecTime", _endTime.ToString("yyyy-MM-dd HH:mm:ss"));
            }

            Log.WriteLog("★ 执行完成 " + _endTime.ToString("yyyy-MM-dd HH:mm:ss"), 1);
        }

        /// <summary>
        /// 商品资料
        /// </summary>
        private void SyncItem()
        {
            ItemLogic itemlogic = new ItemLogic();

            itemlogic.AddProductToFw(_startTime, _endTime);

            itemlogic.AddGoodsToWm(_startTime, _endTime);

            itemlogic.UpdateGoodsToWm(_startTime, _endTime);

            itemlogic.UpdateGoodsShelfStatusToWm(_startTime, _endTime);

        }

        /// <summary>
        /// 采购
        /// </summary>
        private void SyncOutIn()
        {
            OutInLogic outInLogic = new OutInLogic();

            outInLogic.AddOutInToFw(_startTime, _endTime);

            outInLogic.CancelOutInToFw(_startTime, _endTime);
        }

        /// <summary>
        /// 微盟-全量更新库存
        /// </summary>
        private void SyncStock()
        {
            StockLogic stockLogic = new StockLogic();

            stockLogic.WholeUpdateStockToWm(_startTime, _endTime);
        }

        /// <summary>
        /// 销售订单
        /// </summary>
        private void SyncOrder()
        {
            OrderLogic orderLogic = new OrderLogic();

            orderLogic.QueryOrderListFromWm(_startTime, _endTime);

            orderLogic.QueryOrderDetailFromWm(_startTime, _endTime);

            orderLogic.AddTradesToFw(_startTime, _endTime);

            orderLogic.UpdateOrderFlagToWm(_startTime, _endTime);

            orderLogic.DeliveryOrderToWm(_startTime, _endTime);

        }

        /// <summary>
        /// 退货单
        /// </summary>
        private void SyncRightsOrder()
        {
            RightsOrderLogic rightsOrderLogic = new RightsOrderLogic();

            rightsOrderLogic.SearchRightsOrderListFromWm(_startTime, _endTime);

            rightsOrderLogic.QueryOrderDetailFromWm(_startTime, _endTime);

            rightsOrderLogic.ConfirmReceivedRightsGoodsToWm(_startTime, _endTime);

        }

    }
}