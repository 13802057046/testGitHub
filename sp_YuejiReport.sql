--月计表
alter procedure [dbo].[sp_YuejiReport]
(
	@ShopID		nvarchar(max),
	@Start		datetime,
	@End		datetime
)
as

create table #men
(
	F_Sex	nvarchar(30)
)
insert into #men select N'STﾒﾝｽﾞ' 
			union select N'UTﾒﾝｽﾞ'
			union select N'PTﾒﾝｽﾞ'
			union select N'派生ﾒﾝｽﾞ'

create table #women
(
	F_Sex	nvarchar(30)
)
insert into #women select N'STｳｨﾒﾝｽﾞ' 
			union select N'UTｳｨﾒﾝｽﾞ'
			union select N'PTｳｨﾒﾝｽﾞ'
			union select N'派生ｳｨﾒﾝｽﾞ'

create table #shp
(
	F_ShopID	nvarchar(30)
)

declare @next		int,
		@nowShop	nvarchar(30)  
set @next = 1
while @next <= dbo.Get_StrArrayLength(@ShopID,N',')
begin
	set @nowShop = dbo.Get_StrArrayStrOfIndex(@ShopID,N',',@next)
	set @next=@next+1
	
	insert into #shp values (replace(@nowShop,N'''',N''))
end

create table #temp
(
	F_ShopID					nvarchar(30),	--编号
	F_ShopName					nvarchar(60),   --名称
	F_MonthTarGet				decimal(18,2),  --目标
	F_PosTarget					decimal(18,2),  --销售预算
	F_PosActu					decimal(18,2),  --销售实绩
	F_PosDCLv					decimal(18,4),  --销售达成率
	F_PosActuLY					decimal(18,2),  --去年销售实绩
	F_PosRateLY					decimal(18,4),  --去年销售比
	F_Culi						decimal(18,2),  --毛利
	F_Culilv					decimal(18,4),  --毛利率
	F_CuliLY					decimal(18,2),  --去年毛利
	F_CuliRateLY				decimal(18,4),  --毛利去年比
	F_CulilvLY					decimal(18,4),  --去年毛利率
	F_CustomerActu				decimal(18,2),  --顾客实绩
	F_CustomerRateLY			decimal(18,4),  --顾客去年比
	F_CustomerLv				decimal(18,4),  --顾客率
	F_VipActu					decimal(18,2),  --会员实绩
	F_VipRateLY					decimal(18,4),  --会员去年比
	F_VipLv						decimal(18,4),  --会员率
	F_MensSumPosTarget			decimal(18,2),  --合计目标
	F_MensSumPosActu			decimal(18,2),  --合计实绩
	F_MensSumPosDCLv			decimal(18,4),  --合计达成率
	F_MensSumPosCulilv			decimal(18,4),  --MENS毛利率
	F_MensSumPosLv				decimal(18,4),  --MENS率
	F_MensSumCuliActu			decimal(18,2),  --合计毛利实绩
	F_MensSumCulilvLY			decimal(18,2),  --合计去年毛利率
	F_MensSumCuliRateLY			decimal(18,4),  --合计毛利去年比
	F_MensORPosTarget			decimal(18,2),  --OR目标
	F_MensORPosActu				decimal(18,2),  --OR实绩
	F_MensORPosDCLv				decimal(18,4),  --OR达成率
	F_MensORPosCulilv			decimal(18,2),  --OR毛利率
	F_MensORPosLv				decimal(18,4),  --OR率
	F_MensORCuliActu			decimal(18,2),  --OR毛利实绩
	F_MensORCulilvLY			decimal(18,4),  --OR去年毛利率
	F_MensORCuliRateLY			decimal(18,4),  --OR去年比
	F_MensBRPosTarget			decimal(18,2),  --BR目标
	F_MensBRPosActu				decimal(18,2),  --BR实绩
	F_MensBRPosDCLv				decimal(18,4),  --BR达成率
	F_MensBRPosCulilv			decimal(18,4),  --BR毛利率
	F_MensBRPosLv				decimal(18,4),  --BR率
	F_MensBRCuliActu			decimal(18,2),  --BR毛利实绩
	F_MensBRCulilvLY			decimal(18,4),  --BR去年毛利率
	F_MensBRCuliRateLY			decimal(18,4),  --BR去年比
	F_WomensSumPosTarget		decimal(18,2),  --合计目标
	F_WomensSumPosActu			decimal(18,2),  --合计实绩
	F_WomensSumPosDCLv			decimal(18,4),  --合计达成率
	F_WomensSumPosCulilv		decimal(18,4),  --MENS毛利率
	F_WomensSumPosLv			decimal(18,4),  --MENS率
	F_WomensSumCuliActu			decimal(18,2),  --合计毛利实绩
	F_WomensSumCulilvLY			decimal(18,4),  --合计去年毛利率
	F_WomensSumCuliRateLY		decimal(18,4),  --合计毛利去年比
	F_WomensORPosTarget			decimal(18,2),  --OR目标
	F_WomensORPosActu			decimal(18,2),  --OR实绩
	F_WomensORPosDCLv			decimal(18,4),  --OR达成率
	F_WomensORPosCulilv			decimal(18,4),  --OR毛利率
	F_WomensORPosLv				decimal(18,4),  --OR率
	F_WomensORCuliActu			decimal(18,2),  --OR毛利实绩
	F_WomensORCulilvLY			decimal(18,4),  --OR去年毛利率
	F_WomensORCuliRateLY		decimal(18,4),  --OR去年比
	F_WomensBRPosTarget			decimal(18,2),  --BR目标
	F_WomensBRPosActu			decimal(18,2),  --BR实绩
	F_WomensBRPosDCLv			decimal(18,4),  --BR达成率
	F_WomensBRPosCulilv			decimal(18,4),  --BR毛利率
	F_WomensBRPosLv				decimal(18,4),  --BR率
	F_WomensBRCuliActu			decimal(18,2),  --BR毛利实绩
	F_WomensBRCulilvLY			decimal(18,4),  --BR去年毛利率
	F_WomensBRCuliRateLY		decimal(18,4),  --BR去年比
	F_DiscountActu				decimal(18,2),  --优惠实绩
	F_Discount					decimal(18,4),  --优惠率
	F_DiscountRateLY			decimal(18,4),  --优惠去年比
	F_MensComeShop				decimal(18,2),	--来店数
	F_MensComeShopLY			decimal(18,2),	--去年来店数
	F_MensAvgPrice				decimal(18,2),  --平均单价
	F_MensAvgPriceLY			decimal(18,2),  --去年平均单价
	F_MensBuyRate				decimal(18,4),  --购买率
	F_MensBuyRateLY				decimal(18,4),  --去年购买率
	F_MensLinkBillRate			decimal(18,2),  --联单率
	F_MensLinkBillRateLY		decimal(18,2),  --去年联单率
	F_MensBillCount				decimal(18,2),  --客数
	F_MensBillCountLY			decimal(18,2),  --去年客数
	F_MensBillCountPrice		decimal(18,2),  --客单价
	F_MensBillCountPriceLY		decimal(18,2),  --去年客单价
	F_WomensComeShop			decimal(18,2),  --来店数
	F_WomensComeShopLY			decimal(18,2),  --去年来店数
	F_WomensAvgPrice			decimal(18,2),  --平均单价
	F_WomensAvgPriceLY			decimal(18,2),  --去年平均单价
	F_WomensBuyRate				decimal(18,2),  --购买率
	F_WomensBuyRateLY			decimal(18,2),  --去年购买率
	F_WomensLinkBillRate		decimal(18,2),  --联单率
	F_WomensLinkBillRateLY		decimal(18,2),  --去年联单率
	F_WomensBillCount			decimal(18,2),  --客数
	F_WomensBillCountLY			decimal(18,2),  --去年客数
	F_WomensBillCountPrice		decimal(18,2),  --客单价
	F_WomensBillCountPriceLY	decimal(18,2),  --去年客单价
	F_SumStorageCostMoney		decimal(18,2),  --库存成本
	F_SumStoragePosMoney		decimal(18,2),  --库存销售
	F_ActuTargetDays			decimal(18,2),  --实绩目标日数
	F_FirstTargetDays			decimal(18,2),  --最初目标日数
	F_SumStorageCostMoneyHY		decimal(18,2),  --6月前库存成本
	F_MensStorageCostMoney		decimal(18,2),  --MENS成本
	F_MensStoragePosMoney		decimal(18,2),  --MENS销售
	F_MensORStorageCostMoney	decimal(18,2),  --MENS OR 成本
	F_MensBRStorageCostMoney	decimal(18,2),  --MENS BR 成本
	F_MensStorageCostMoneyHY	decimal(18,2),  --6月前MENS成本
	F_WomensStorageCostMoney	decimal(18,2),  --WOMENS成本
	F_WomensStoragePosMoney		decimal(18,2),  --WOMENS销售
	F_WomensORStorageCostMoney	decimal(18,2),  --WOMENS OR 成本
	F_WomensBRStorageCostMoney	decimal(18,2),  --WOMENS BR 成本
	F_WomensStorageCostMoneyHY	decimal(18,2),  --6月前WOMENS成本
	F_SumStockInCostMoney		decimal(18,2),  --总采购成本
	F_SumStockInPosMoney		decimal(18,2),  --总采购销售
	F_MensStockInCostMoney		decimal(18,2),  --MENS成本
	F_MensStockInPosMoney		decimal(18,2),  --MENS销售
	F_MensORStockInCostMoney	decimal(18,2),  --MENS OR 成本
	F_MensBRStockInCostMoney	decimal(18,2),  --MENS BR 成本
	F_WomensStockInCostMoney	decimal(18,2),  --WOMENS成本
	F_WomensStockInPosMoney		decimal(18,2),  --WOMENS销售
	F_WomensORStockInCostMoney	decimal(18,2),  --WOMENS OR 成本
	F_WomensBRStockInCostMoney	decimal(18,2),  --WOMENS BR 成本


	--以下为隐藏字段
	F_CustomerActuLY			decimal(18,2),  --去年顾客销售实绩
	F_VipActuLY					decimal(18,2),  --去年会员销售实绩
	F_MensSumPosActuLY			decimal(18,2),  --去年合计实绩
	F_MensSumCuliActuLY			decimal(18,2),  --去年合计毛利实绩
	F_MensORPosActuLY			decimal(18,2),  --去年OR实绩
	F_MensORCuliActuLY			decimal(18,2),  --去年OR毛利实绩
	F_MensBRPosActuLY			decimal(18,2),  --去年BR实绩
	F_MensBRCuliActuLY			decimal(18,2),  --去年BR毛利实绩
	F_WomensSumPosActuLY		decimal(18,2),  --去年合计实绩
	F_WomensSumCuliActuLY		decimal(18,2),  --去年合计毛利实绩
	F_WomensORPosActuLY			decimal(18,2),  --去年OR实绩
	F_WomensORCuliActuLY		decimal(18,2),  --去年OR毛利实绩
	F_WomensBRPosActuLY			decimal(18,2),  --去年BR实绩
	F_WomensBRCuliActuLY		decimal(18,2),  --去年BR毛利实绩
	F_DiscountActuLY			decimal(18,2),  --去年优惠实绩
	F_MensSumQtyActu			decimal(18,2),  --男款销售数量
	F_WomensSumQtyActu			decimal(18,2),  --女款销售数量
	F_MensSumQtyActuLY			decimal(18,2),  --去年男款销售数量
	F_WomensSumQtyActuLY		decimal(18,2),  --去年女款销售数量
	F_ActuAvgTarget				decimal(18,2),  --查询起始日前一天所在月份的月目标达成率×查询期间内的日平均目标
	F_FirstAvgTarget			decimal(18,2),  --查询起始日～查询起始日+29天的日平均目标
	F_DisplayOrder				nvarchar(30),	--排序(用于排序，明细行用shopid，合计行用9999)
)

--店铺
insert into #temp(F_ShopID,F_ShopName,F_DisplayOrder)
select s.F_ShopID, t.F_Name, s.F_ShopID
from #shp s
left join t_Shop t on s.F_ShopID = t.F_ID

--目标
update a set
	a.F_MonthTarGet = isnull(b.F_MonthTarGet,0),
	a.F_PosTarget = isnull(c.F_PosTarget,0),
	a.F_MensSumPosTarget = isnull(m.F_MensSumPosTarget,0),
	a.F_WomensSumPosTarget = isnull(w.F_WomensSumPosTarget,0)
from #temp a
--查询范围所涉及月份的销售目标
left join (
			select F_ShopID, sum(F_Money) as F_MonthTarGet 
			from t_TarGetSet
			where F_Flag = 0 and F_Kind = 1
			and F_Date >= dateadd(month, datediff(month, 0, @Start), 0)
			and F_Date <= dateadd(month, datediff(month, 0, dateadd(month, 1, @End)), -1)
			group by F_ShopID
			) b on a.F_ShopID = b.F_ShopID
--查询范围所涉及日期的销售目标
left join (
			select F_ShopID, sum(F_Money) as F_PosTarget
			from t_TarGetSet
			where F_Flag = 0 and F_Kind = 1
			and F_Date >= @Start
			and F_Date <= @End
			group by F_ShopID
			) c on a.F_ShopID = c.F_ShopID
--MENS合计目标
left join (
			select F_ShopID, sum(F_Money) as F_MensSumPosTarget
			from t_TarGetSet
			where F_Flag = 0 and F_Kind = 1 and F_Brand in (select F_Sex from #men)
			and F_Date >= @Start
			and F_Date <= @End
			group by F_ShopID
			) m on a.F_ShopID = m.F_ShopID
--WOMENS合计目标
left join (
			select F_ShopID, sum(F_Money) as F_WomensSumPosTarget
			from t_TarGetSet
			where F_Flag = 0 and F_Kind = 1 and F_Brand in (select F_Sex from #women)
			and F_Date >= @Start
			and F_Date <= @End
			group by F_ShopID
			) w on a.F_ShopID = w.F_ShopID

--今年各种实绩
;with pos as (
	select a.F_ShopID, a.F_IsGuest, a.F_VipNum, b.*, c.F_Brand, c.F_ItemProperty
	from t_Pos a
	left join t_PosDetail b on a.F_BillID = b.F_BillID
	left join v_Item_NoPic c on b.F_ItemID =c.f_id and  b.F_ColorID = c.F_ColorID and  b.F_SizeID = c.F_SizeID 
	where a.F_Status = N'正常'
	and a.F_Date >= @Start
	and a.F_Date <= @End
)

update a set
	a.F_PosActu = isnull(b.F_PosActu,0),
	a.F_Culi = isnull(b.F_Culi,0),
	a.F_CustomerActu = isnull(g.F_CustomerActu,0),
	a.F_VipActu = isnull(c.F_VipActu,0),
	a.F_MensSumPosActu = isnull(m.F_MensSumPosActu,0),
	a.F_MensSumCuliActu = isnull(m.F_MensSumCuliActu,0),
	a.F_MensORPosActu = isnull(m1.F_MensORPosActu,0),
	a.F_MensORCuliActu = isnull(m1.F_MensORCuliActu,0),
	a.F_MensBRPosActu = isnull(m2.F_MensBRPosActu,0),
	a.F_MensBRCuliActu = isnull(m2.F_MensBRCuliActu,0),
	a.F_WomensSumPosActu = isnull(w.F_WomensSumPosActu,0),
	a.F_WomensSumCuliActu = isnull(w.F_WomensSumCuliActu,0),
	a.F_WomensORPosActu = isnull(w1.F_WomensORPosActu,0),
	a.F_WomensORCuliActu = isnull(w1.F_WomensORCuliActu,0),
	a.F_WomensBRPosActu = isnull(w2.F_WomensBRPosActu,0),
	a.F_WomensBRCuliActu = isnull(w2.F_WomensBRCuliActu,0),
	a.F_DiscountActu = isnull(b.F_DiscountActu,0),
	a.F_MensSumQtyActu = isnull(m.F_MensSumQtyActu,0),
	a.F_WomensSumQtyActu = isnull(w.F_WomensSumQtyActu,0)
from #temp a
--销售合计、毛利合计、优惠实绩
left join (
			select F_ShopID, 
			sum(F_Money) as F_PosActu,
			sum(F_Qty) as F_PosQty,
			sum(F_Money - F_Qty * F_CostPrice) as F_Culi,
			sum(F_Qty * F_Price - F_Money) as F_DiscountActu
			from pos
			group by F_ShopID
			) b on a.F_ShopID = b.F_ShopID
--顾客销售合计
left join (
			select F_ShopID, 
			sum(F_Money) as F_CustomerActu
			from pos
			where isnull(F_IsGuest,0) = 1
			group by F_ShopID
			) g on a.F_ShopID = g.F_ShopID
--会员销售合计
left join (
			select F_ShopID, 
			sum(F_Money) as F_VipActu
			from pos
			where isnull(F_VIPNum,'') <> ''
			group by F_ShopID
			) c on a.F_ShopID = c.F_ShopID
--MENS（销售合计、毛利合计）
left join (
			select F_ShopID, 
			sum(F_Money) as F_MensSumPosActu, 
			sum(F_Money - F_Qty * F_CostPrice) as F_MensSumCuliActu,
			sum(F_Qty) as F_MensSumQtyActu
			from pos
			where F_Brand in (select F_Sex from #men)
			group by F_ShopID
			) m on a.F_ShopID = m.F_ShopID
--MENS OR（销售合计、毛利合计）
left join (
			select F_ShopID, 
			sum(F_Money) as F_MensORPosActu,
			sum(F_Money - F_Qty * F_CostPrice) as F_MensORCuliActu
			from pos
			where F_Brand in (select F_Sex from #men) and F_ItemProperty = 'OR'
			group by F_ShopID
			) m1 on a.F_ShopID = m1.F_ShopID
--MENS BR（销售合计、毛利合计）
left join (
			select F_ShopID, 
			sum(F_Money) as F_MensBRPosActu,
			sum(F_Money - F_Qty * F_CostPrice) as F_MensBRCuliActu
			from pos
			where F_Brand in (select F_Sex from #men) and F_ItemProperty = 'BR'
			group by F_ShopID
			) m2 on a.F_ShopID = m2.F_ShopID
--WOMENS（销售合计、毛利合计）
left join (
			select F_ShopID, 
			sum(F_Money) as F_WomensSumPosActu,
			sum(F_Money - F_Qty * F_CostPrice) as F_WomensSumCuliActu,
			sum(F_Qty) as F_WomensSumQtyActu
			from pos
			where F_Brand in (select F_Sex from #women)
			group by F_ShopID
			) w on a.F_ShopID = w.F_ShopID
--WOMENS OR（销售合计、毛利合计）
left join (
			select F_ShopID, 
			sum(F_Money) as F_WomensORPosActu,
			sum(F_Money - F_Qty * F_CostPrice) as F_WomensORCuliActu
			from pos
			where F_Brand in (select F_Sex from #women) and F_ItemProperty = 'OR'
			group by F_ShopID
			) w1 on a.F_ShopID = w1.F_ShopID
--WOMENS BR（销售合计、毛利合计）
left join (
			select F_ShopID, 
			sum(F_Money) as F_WomensBRPosActu,
			sum(F_Money - F_Qty * F_CostPrice) as F_WomensBRCuliActu
			from pos
			where F_Brand in (select F_Sex from #women) and F_ItemProperty = 'BR'
			group by F_ShopID
			) w2 on a.F_ShopID = w2.F_ShopID


--去年各种实绩
;with posLy as (
	select a.F_ShopID, a.F_IsGuest, a.F_VipNum, b.*, c.F_Brand, c.F_ItemProperty
	from t_Pos a
	left join t_PosDetail b on a.F_BillID = b.F_BillID
	left join v_Item_NoPic c on b.F_ItemID =c.f_id and  b.F_ColorID = c.F_ColorID and  b.F_SizeID = c.F_SizeID 
	where a.F_Status = N'正常'
	and a.F_Date >= dateadd(year,-1,@Start)
	and a.F_Date <= dateadd(year,-1,@End)
)

update a set
	a.F_PosActuLY = isnull(b.F_PosActuLY,0),
	a.F_CuliLY = isnull(b.F_CuliLY,0),
	a.F_CustomerActuLY = isnull(g.F_CustomerActuLY,0),
	a.F_VipActuLY = isnull(c.F_VipActuLY,0),
	a.F_MensSumPosActuLY = isnull(m.F_MensSumPosActuLY,0),
	a.F_MensSumCuliActuLY = isnull(m.F_MensSumCuliActuLY,0),
	a.F_MensORPosActuLY = isnull(m1.F_MensORPosActuLY,0),
	a.F_MensORCuliActuLY = isnull(m1.F_MensORCuliActuLY,0),
	a.F_MensBRPosActuLY = isnull(m2.F_MensBRPosActuLY,0),
	a.F_MensBRCuliActuLY = isnull(m2.F_MensBRCuliActuLY,0),
	a.F_WomensSumPosActuLY = isnull(w.F_WomensSumPosActuLY,0),
	a.F_WomensSumCuliActuLY = isnull(w.F_WomensSumCuliActuLY,0),
	a.F_WomensORPosActuLY = isnull(w1.F_WomensORPosActuLY,0),
	a.F_WomensORCuliActuLY = isnull(w1.F_WomensORCuliActuLY,0),
	a.F_WomensBRPosActuLY = isnull(w2.F_WomensBRPosActuLY,0),
	a.F_WomensBRCuliActuLY = isnull(w2.F_WomensBRCuliActuLY,0),
	a.F_DiscountActuLY = isnull(b.F_DiscountActuLY,0),
	a.F_MensSumQtyActuLY = isnull(m.F_MensSumQtyActuLY,0),
	a.F_WomensSumQtyActuLY = isnull(w.F_WomensSumQtyActuLY,0)
from #temp a
--销售合计、毛利合计、优惠实绩
left join (
			select F_ShopID, 
			sum(F_Money) as F_PosActuLY,
			sum(F_Money - F_Qty * F_CostPrice) as F_CuliLY,
			sum(F_Qty * F_Price - F_Money) as F_DiscountActuLY
			from posLy
			group by F_ShopID
			) b on a.F_ShopID = b.F_ShopID
--顾客销售合计
left join (
			select F_ShopID, 
			sum(F_Money) as F_CustomerActuLY
			from posLy
			where isnull(F_IsGuest,0) = 1
			group by F_ShopID
			) g on a.F_ShopID = g.F_ShopID
--会员销售合计
left join (
			select F_ShopID, 
			sum(F_Money) as F_VipActuLY
			from posLy
			where isnull(F_VIPNum,'') <> ''
			group by F_ShopID
			) c on a.F_ShopID = c.F_ShopID
--MENS（销售合计、毛利合计）
left join (
			select F_ShopID, 
			sum(F_Money) as F_MensSumPosActuLY, 
			sum(F_Money - F_Qty * F_CostPrice) as F_MensSumCuliActuLY,
			sum(F_Qty) as F_MensSumQtyActuLY
			from posLy
			where F_Brand in (select F_Sex from #men)
			group by F_ShopID
			) m on a.F_ShopID = m.F_ShopID
--MENS OR（销售合计、毛利合计）
left join (
			select F_ShopID, 
			sum(F_Money) as F_MensORPosActuLY,
			sum(F_Money - F_Qty * F_CostPrice) as F_MensORCuliActuLY
			from posLy
			where F_Brand in (select F_Sex from #men) and F_ItemProperty = 'OR'
			group by F_ShopID
			) m1 on a.F_ShopID = m1.F_ShopID
--MENS BR（销售合计、毛利合计）
left join (
			select F_ShopID, 
			sum(F_Money) as F_MensBRPosActuLY,
			sum(F_Money - F_Qty * F_CostPrice) as F_MensBRCuliActuLY
			from posLy
			where F_Brand in (select F_Sex from #men) and F_ItemProperty = 'BR'
			group by F_ShopID
			) m2 on a.F_ShopID = m2.F_ShopID
--WOMENS（销售合计、毛利合计）
left join (
			select F_ShopID, 
			sum(F_Money) as F_WomensSumPosActuLY,
			sum(F_Money - F_Qty * F_CostPrice) as F_WomensSumCuliActuLY,
			sum(F_Qty) as F_WomensSumQtyActuLY
			from posLy
			where F_Brand in (select F_Sex from #women)
			group by F_ShopID
			) w on a.F_ShopID = w.F_ShopID
--WOMENS OR（销售合计、毛利合计）
left join (
			select F_ShopID, 
			sum(F_Money) as F_WomensORPosActuLY,
			sum(F_Money - F_Qty * F_CostPrice) as F_WomensORCuliActuLY
			from posLy
			where F_Brand in (select F_Sex from #women) and F_ItemProperty = 'OR'
			group by F_ShopID
			) w1 on a.F_ShopID = w1.F_ShopID
--WOMENS BR（销售合计、毛利合计）
left join (
			select F_ShopID, 
			sum(F_Money) as F_WomensBRPosActuLY,
			sum(F_Money - F_Qty * F_CostPrice) as F_WomensBRCuliActuLY
			from posLy
			where F_Brand in (select F_Sex from #women) and F_ItemProperty = 'BR'
			group by F_ShopID
			) w2 on a.F_ShopID = w2.F_ShopID

--来店数
update a set
	a.F_MensComeShop = isnull(b.F_MensComeShop,0),
	a.F_MensComeShopLY = isnull(c.F_MensComeShopLY,0),
	a.F_WomensComeShop = isnull(b.F_WomensComeShop,0),
	a.F_WomensComeShopLY = isnull(c.F_WomensComeShopLY,0)
from #temp a
--今年
left join (
			select F_ShopID, 
			sum(cast(F_OtherShopData5 as int)) as F_MensComeShop,
			sum(cast(F_OtherShopData6 as int)) as F_WomensComeShop
			from t_DayEnd
			where convert(nvarchar(10),F_EndDate,120) >= convert(nvarchar(10),@Start,120)
			and convert(nvarchar(10),F_EndDate,120) <= convert(nvarchar(10),@End,120)
			group by F_ShopID
			) b on a.F_ShopID = b.F_ShopID
--去年
left join (
			select F_ShopID, 
			sum(cast(F_OtherShopData5 as int)) as F_MensComeShopLY,
			sum(cast(F_OtherShopData6 as int)) as F_WomensComeShopLY
			from t_DayEnd
			where convert(nvarchar(10),F_EndDate,120) >= convert(nvarchar(10),dateadd(year,-1,@Start),120)
			and convert(nvarchar(10),F_EndDate,120) <= convert(nvarchar(10),dateadd(year,-1,@End),120)
			group by F_ShopID
			) c on a.F_ShopID = c.F_ShopID



--客数
-- 判断退单是否统计单数
declare @isHaveTui int 
set @isHaveTui = -1

select @isHaveTui = case when isnull(F_CustomerCount,0) = 1  then 0 else -1 end  from t_Parm


;with BillCount_Men as (
	select 
		a.F_ShopID,
		a.F_BillID,
		F_Minus=case when min(isnull(F_Qty,0))<0 then -1 else 0 end,
		F_Plus=case when max(isnull(F_Qty,0))>0 then 1 else 0 end
	from t_Pos a
		left join t_PosDetail b on a.F_BillID = b.F_BillID
		left join v_Item_NoPic c on b.F_ItemID =c.f_id and  b.F_ColorID = c.F_ColorID and  b.F_SizeID = c.F_SizeID 
	where a.F_Status = N'正常'
		and a.F_Date >= @Start
		and a.F_Date <= @End
		and c.F_Brand in (select F_Sex from #men)
	group by a.F_ShopID,a.F_BillID
),
BillCount_Men_LY as (
	select 
		a.F_ShopID,
		a.F_BillID,
		F_Minus=case when min(isnull(F_Qty,0))<0 then -1 else 0 end,
		F_Plus=case when max(isnull(F_Qty,0))>0 then 1 else 0 end
	from t_Pos a
		left join t_PosDetail b on a.F_BillID = b.F_BillID
		left join v_Item_NoPic c on b.F_ItemID =c.f_id and  b.F_ColorID = c.F_ColorID and  b.F_SizeID = c.F_SizeID 
	where a.F_Status = N'正常'
		and a.F_Date >= dateadd(year,-1,@Start)
		and a.F_Date <= dateadd(year,-1,@End)
		and c.F_Brand in (select F_Sex from #men)
	group by a.F_ShopID,a.F_BillID
),
BillCount_Women as (
	select 
		a.F_ShopID,
		a.F_BillID,
		F_Minus=case when min(isnull(F_Qty,0))<0 then -1 else 0 end,
		F_Plus=case when max(isnull(F_Qty,0))>0 then 1 else 0 end
	from t_Pos a
		left join t_PosDetail b on a.F_BillID = b.F_BillID
		left join v_Item_NoPic c on b.F_ItemID =c.f_id and  b.F_ColorID = c.F_ColorID and  b.F_SizeID = c.F_SizeID 
	where a.F_Status = N'正常'
		and a.F_Date >= @Start
		and a.F_Date <= @End
		and c.F_Brand in (select F_Sex from #women)
	group by a.F_ShopID,a.F_BillID
),
BillCount_Women_LY as (
	select 
		a.F_ShopID,
		a.F_BillID,
		F_Minus=case when min(isnull(F_Qty,0))<0 then -1 else 0 end,
		F_Plus=case when max(isnull(F_Qty,0))>0 then 1 else 0 end
	from t_Pos a
		left join t_PosDetail b on a.F_BillID = b.F_BillID
		left join v_Item_NoPic c on b.F_ItemID =c.f_id and  b.F_ColorID = c.F_ColorID and  b.F_SizeID = c.F_SizeID 
	where a.F_Status = N'正常'
		and a.F_Date >= dateadd(year,-1,@Start)
		and a.F_Date <= dateadd(year,-1,@End)
		and c.F_Brand in (select F_Sex from #women)
	group by a.F_ShopID,a.F_BillID
)


--客数
update a set
	a.F_MensBillCount = isnull(m.F_MensBillCount,0),
	a.F_MensBillCountLY = isnull(ml.F_MensBillCountLY,0),
	a.F_WomensBillCount = isnull(w.F_WomensBillCount,0),
	a.F_WomensBillCountLY = isnull(wl.F_WomensBillCountLY,0)
from #temp a
--Mens客数
left join (
			select F_ShopID, count(F_BillID) as F_MensBillCount
			from BillCount_Men
			where (F_Minus+F_Plus) >= @isHaveTui -- >=-1 / >=0
			group by F_ShopID
			) m on a.F_ShopID = m.F_ShopID
--去年Mens客数
left join (
			select F_ShopID, count(F_BillID) as F_MensBillCountLY
			from BillCount_Men_LY
			where (F_Minus+F_Plus) >= @isHaveTui -- >=-1 / >=0
			group by F_ShopID
			) ml on a.F_ShopID = m.F_ShopID
--Womens客数
left join (
			select F_ShopID, count(F_BillID) as F_WomensBillCount
			from BillCount_Women
			where (F_Minus+F_Plus) >= @isHaveTui -- >=-1 / >=0
			group by F_ShopID
			) w on a.F_ShopID = w.F_ShopID
--去年Womens客数
left join (
			select F_ShopID, count(F_BillID) as F_WomensBillCountLY
			from BillCount_Women_LY
			where (F_Minus+F_Plus) >= @isHaveTui -- >=-1 / >=0
			group by F_ShopID
			) wl on a.F_ShopID = w.F_ShopID


create table #kucun
(
  F_ShopID			nvarchar(30),
  F_StorageID		nvarchar(30),
  F_ItemID			nvarchar(30),
  F_ColorID			nvarchar(30),
  F_SizeID			nvarchar(30),
  F_Qty				decimal(18,2),
  F_CostMoney		decimal(18,2),
  F_PosMoney		decimal(18,2)
)

insert into #kucun exec sp_GetHistoryKucun @End;

;with kucun as (
	select a.*, b.F_Brand, b.F_ItemProperty
	from #kucun a
	left join v_Item_NoPic b on a.F_ItemID =b.f_id and a.F_ColorID = b.F_ColorID and a.F_SizeID = b.F_SizeID 
)

--库存
update a set
	a.F_SumStorageCostMoney = isnull(b.F_SumStorageCostMoney,0),
	a.F_SumStoragePosMoney = isnull(b.F_SumStoragePosMoney,0),
	a.F_MensStorageCostMoney = isnull(m.F_MensStorageCostMoney,0),
	a.F_MensStoragePosMoney = isnull(m.F_MensStoragePosMoney,0),
	a.F_MensORStorageCostMoney = isnull(m1.F_MensORStorageCostMoney,0),
	a.F_MensBRStorageCostMoney = isnull(m2.F_MensBRStorageCostMoney,0),
	a.F_WomensStorageCostMoney = isnull(w.F_WomensStorageCostMoney,0),
	a.F_WomensStoragePosMoney = isnull(w.F_WomensStoragePosMoney,0),
	a.F_WomensORStorageCostMoney = isnull(w1.F_WomensORStorageCostMoney,0),
	a.F_WomensBRStorageCostMoney = isnull(w2.F_WomensBRStorageCostMoney,0)

from #temp a
--库存成本
left join (
			select F_ShopID, 
			sum(F_Qty * F_CostMoney) as F_SumStorageCostMoney,
			sum(F_Qty * F_PosMoney) as F_SumStoragePosMoney
			from kucun
			group by F_ShopID
			) b on a.F_ShopID = b.F_ShopID
--MENS成本
left join (
			select F_ShopID, 
			sum(F_Qty * F_CostMoney) as F_MensStorageCostMoney,
			sum(F_Qty * F_PosMoney) as F_MensStoragePosMoney
			from kucun
			where F_Brand in (select F_Sex from #men)
			group by F_ShopID
			) m on a.F_ShopID = m.F_ShopID
--MENS OR 成本
left join (
			select F_ShopID, 
			sum(F_Qty * F_CostMoney) as F_MensORStorageCostMoney
			from kucun
			where F_Brand in (select F_Sex from #men) and F_ItemProperty = 'OR'
			group by F_ShopID
			) m1 on a.F_ShopID = m1.F_ShopID
--MENS BR 成本
left join (
			select F_ShopID, 
			sum(F_Qty * F_CostMoney) as F_MensBRStorageCostMoney
			from kucun
			where F_Brand in (select F_Sex from #men) and F_ItemProperty = 'BR'
			group by F_ShopID
			) m2 on a.F_ShopID = m2.F_ShopID
--WOMENS成本
left join (
			select F_ShopID, 
			sum(F_Qty * F_CostMoney) as F_WomensStorageCostMoney,
			sum(F_Qty * F_PosMoney) as F_WomensStoragePosMoney
			from kucun
			where F_Brand in (select F_Sex from #women)
			group by F_ShopID
			) w on a.F_ShopID = w.F_ShopID
--WOMENS OR 成本
left join (
			select F_ShopID, 
			sum(F_Qty * F_CostMoney) as F_WomensORStorageCostMoney
			from kucun
			where F_Brand in (select F_Sex from #women) and F_ItemProperty = 'OR'
			group by F_ShopID
			) w1 on a.F_ShopID = w1.F_ShopID
--WOMENS BR 成本
left join (
			select F_ShopID, 
			sum(F_Qty * F_CostMoney) as F_WomensBRStorageCostMoney
			from kucun
			where F_Brand in (select F_Sex from #women) and F_ItemProperty = 'BR'
			group by F_ShopID
			) w2 on a.F_ShopID = w2.F_ShopID


--为了计算实绩目标日数和最初目标日数，需要提前求取如下字段
update a set
	--查询起始日前一天所在月份的月目标达成率×查询期间内的日平均目标
	a.F_ActuAvgTarget = case when isnull(a.F_PosTarget,0)=0 then 0 else isnull((c.F_PosActu / b.F_PosTarget) * d.F_AvgTarget,0) end,
	--查询起始日～查询起始日+29天的日平均目标
	a.F_FirstAvgTarget = isnull(e.F_AvgTarget,0)
from #temp a
--查询起始日前一天所在月份的月目标
left join (
			select F_ShopID, sum(F_Money) as F_PosTarget
			from t_TarGetSet
			where F_Flag = 0 and F_Kind = 1
			and convert(nvarchar(7),F_Date,120) = convert(nvarchar(7),dateadd(day,-1,@Start),120)
			group by F_ShopID
			) b on a.F_ShopID = b.F_ShopID
--查询起始日前一天所在月份的月实绩
left join (
	select a.F_ShopID, sum(b.F_Money) as F_PosActu
	from t_Pos a
	left join t_PosDetail b on a.F_BillID = b.F_BillID
	where a.F_Status = N'正常'
	and convert(nvarchar(7),a.F_Date,120) = convert(nvarchar(7),dateadd(day,-1,@Start),120)
	group by F_ShopID
) c on a.F_ShopID = c.F_ShopID
--查询期间内的日平均目标
left join (
	select F_ShopID, sum(F_Money)/count(F_Money) as F_AvgTarget
			from t_TarGetSet
			where F_Flag = 0 and F_Kind = 1
			and F_Date >= @Start
			and F_Date <= @End
			group by F_ShopID
) d on a.F_ShopID = d.F_ShopID
--查询起始日～查询起始日+29天的日平均目标
left join (
	select F_ShopID, sum(F_Money)/count(1) as F_AvgTarget
			from t_TarGetSet
			where F_Flag = 0 and F_Kind = 1
			and F_Date >= @Start
			and F_Date <= dateadd(day, 29, @Start)
			group by F_ShopID
) e on a.F_ShopID = e.F_ShopID


--查询截止日(查询起始日-6个月)
delete from #kucun
declare @before6month datetime = convert(varchar(10), dateadd(month, -6, @Start), 120) + ' 23:59:59';
insert into #kucun exec sp_GetHistoryKucun @before6month;

--6月前库存
;with kucunHY as (
	select a.*, b.F_Brand, b.F_ItemProperty
	from #kucun a
	left join v_Item_NoPic b on a.F_ItemID =b.f_id and a.F_ColorID = b.F_ColorID and a.F_SizeID = b.F_SizeID 
)

--6月前库存
update a set
	a.F_SumStorageCostMoneyHY = isnull(b.F_SumStorageCostMoneyHY,0),
	F_MensStorageCostMoneyHY = isnull(m.F_MensStorageCostMoneyHY,0),
	a.F_WomensStorageCostMoneyHY = isnull(w.F_WomensStorageCostMoneyHY,0)
from #temp a
--6月前库存成本
left join (
			select F_ShopID, 
			sum(F_Qty * F_CostMoney) as F_SumStorageCostMoneyHY
			from kucunHY
			group by F_ShopID
			) b on a.F_ShopID = b.F_ShopID
--6月前MENS成本
left join (
			select F_ShopID, 
			sum(F_Qty * F_CostMoney) as F_MensStorageCostMoneyHY
			from kucunHY
			where F_Brand in (select F_Sex from #men)
			group by F_ShopID
			) m on a.F_ShopID = m.F_ShopID
--6月前WOMENS成本
left join (
			select F_ShopID, 
			sum(F_Qty * F_CostMoney) as F_WomensStorageCostMoneyHY
			from kucunHY
			where F_Brand in (select F_Sex from #women)
			group by F_ShopID
			) w on a.F_ShopID = w.F_ShopID


--采购
;with caigo as (
select F_ShopID,F_StorageID,b.F_ItemID,b.F_ColorID,b.F_SizeID, 
(case a.F_BillType when N'退货' then -b.F_Qty else b.F_Qty end) as F_Qty,
(case a.F_BillType when N'退货' then -b.F_Qty else b.F_Qty end) * isnull(b.F_CostPrice,0) as F_CostMoney,
(case a.F_BillType when N'退货' then -b.F_Qty else b.F_Qty end) * isnull(b.F_PosPrice,0) as F_PosMoney,
 c.F_Brand, c.F_ItemProperty
from t_StockIn a
left join t_StockInDetail b on a.F_BillID = b.F_BillID
left join v_Item_NoPic c on b.F_ItemID =c.f_id and  b.F_ColorID = c.F_ColorID and  b.F_SizeID = c.F_SizeID 
where a.F_Date between @Start and @End
and F_Check = 1
)

--采购
update a set
	a.F_SumStockInCostMoney = isnull(b.F_SumStockInCostMoney,0),
	a.F_SumStockInPosMoney = isnull(b.F_SumStockInPosMoney,0),
	a.F_MensStockInCostMoney = isnull(m.F_MensStockInCostMoney,0),
	a.F_MensStockInPosMoney = isnull(m.F_MensStockInPosMoney,0),
	a.F_MensORStockInCostMoney = isnull(m1.F_MensORStockInCostMoney,0),
	a.F_MensBRStockInCostMoney = isnull(m2.F_MensBRStockInCostMoney,0),
	a.F_WomensStockInCostMoney = isnull(w.F_WomensStockInCostMoney,0),
	a.F_WomensStockInPosMoney = isnull(w.F_WomensStockInPosMoney,0),
	a.F_WomensORStockInCostMoney = isnull(w1.F_WomensORStockInCostMoney,0),
	a.F_WomensBRStockInCostMoney = isnull(w2.F_WomensBRStockInCostMoney,0)
from #temp a
--成本
left join (
			select F_ShopID, 
			sum(F_Qty * F_CostMoney) as F_SumStockInCostMoney,
			sum(F_Qty * F_PosMoney) as F_SumStockInPosMoney
			from caigo
			group by F_ShopID
			) b on a.F_ShopID = b.F_ShopID
--MENS成本
left join (
			select F_ShopID, 
			sum(F_Qty * F_CostMoney) as F_MensStockInCostMoney,
			sum(F_Qty * F_PosMoney) as F_MensStockInPosMoney
			from caigo
			where F_Brand in (select F_Sex from #men)
			group by F_ShopID
			) m on a.F_ShopID = m.F_ShopID
--MENS OR 成本
left join (
			select F_ShopID, 
			sum(F_Qty * F_CostMoney) as F_MensORStockInCostMoney
			from caigo
			where F_Brand in (select F_Sex from #men) and F_ItemProperty = 'OR'
			group by F_ShopID
			) m1 on a.F_ShopID = m1.F_ShopID
--MENS BR 成本
left join (
			select F_ShopID, 
			sum(F_Qty * F_CostMoney) as F_MensBRStockInCostMoney
			from caigo
			where F_Brand in (select F_Sex from #men) and F_ItemProperty = 'BR'
			group by F_ShopID
			) m2 on a.F_ShopID = m2.F_ShopID
--WOMENS成本
left join (
			select F_ShopID, 
			sum(F_Qty * F_CostMoney) as F_WomensStockInCostMoney,
			sum(F_Qty * F_PosMoney) as F_WomensStockInPosMoney
			from caigo
			where F_Brand in (select F_Sex from #women)
			group by F_ShopID
			) w on a.F_ShopID = w.F_ShopID
--WOMENS OR 成本
left join (
			select F_ShopID, 
			sum(F_Qty * F_CostMoney) as F_WomensORStockInCostMoney
			from caigo
			where F_Brand in (select F_Sex from #women) and F_ItemProperty = 'OR'
			group by F_ShopID
			) w1 on a.F_ShopID = w1.F_ShopID
--WOMENS BR 成本
left join (
			select F_ShopID, 
			sum(F_Qty * F_CostMoney) as F_WomensBRStockInCostMoney
			from caigo
			where F_Brand in (select F_Sex from #women) and F_ItemProperty = 'BR'
			group by F_ShopID
			) w2 on a.F_ShopID = w2.F_ShopID


--合计行
insert into #temp(	
	 F_ShopID					
	,F_ShopName					
	,F_MonthTarGet				
	,F_PosTarget					
	,F_PosActu					
	,F_PosActuLY					
	,F_Culi						
	,F_CuliLY					
	,F_CustomerActu				
	,F_VipActu					
	,F_MensSumPosTarget			
	,F_MensSumPosActu			
	,F_MensSumCuliActu			
	--,F_MensORPosTarget			
	,F_MensORPosActu				
	,F_MensORCuliActu			
	--,F_MensBRPosTarget			
	,F_MensBRPosActu				
	,F_MensBRCuliActu			
	,F_WomensSumPosTarget		
	,F_WomensSumPosActu			
	,F_WomensSumCuliActu			
	--,F_WomensORPosTarget			
	,F_WomensORPosActu			
	,F_WomensORCuliActu			
	--,F_WomensBRPosTarget			
	,F_WomensBRPosActu			
	,F_WomensBRCuliActu			
	,F_DiscountActu				
	,F_MensComeShop				
	,F_MensComeShopLY			
	,F_MensBillCount				
	,F_MensBillCountLY			
	,F_WomensComeShop			
	,F_WomensComeShopLY			
	,F_WomensBillCount			
	,F_WomensBillCountLY			
	,F_SumStorageCostMoney		
	,F_SumStoragePosMoney		
	,F_SumStorageCostMoneyHY		
	,F_MensStorageCostMoney		
	,F_MensStoragePosMoney		
	,F_MensORStorageCostMoney	
	,F_MensBRStorageCostMoney	
	,F_MensStorageCostMoneyHY	
	,F_WomensStorageCostMoney	
	,F_WomensStoragePosMoney		
	,F_WomensORStorageCostMoney	
	,F_WomensBRStorageCostMoney	
	,F_WomensStorageCostMoneyHY	
	,F_SumStockInCostMoney		
	,F_SumStockInPosMoney		
	,F_MensStockInCostMoney		
	,F_MensStockInPosMoney		
	,F_MensORStockInCostMoney	
	,F_MensBRStockInCostMoney	
	,F_WomensStockInCostMoney	
	,F_WomensStockInPosMoney		
	,F_WomensORStockInCostMoney	
	,F_WomensBRStockInCostMoney	
	,F_CustomerActuLY			
	,F_VipActuLY					
	,F_MensSumPosActuLY			
	,F_MensSumCuliActuLY			
	,F_MensORPosActuLY			
	,F_MensORCuliActuLY			
	,F_MensBRPosActuLY			
	,F_MensBRCuliActuLY			
	,F_WomensSumPosActuLY		
	,F_WomensSumCuliActuLY		
	,F_WomensORPosActuLY			
	,F_WomensORCuliActuLY		
	,F_WomensBRPosActuLY			
	,F_WomensBRCuliActuLY		
	,F_DiscountActuLY			
	,F_MensSumQtyActu			
	,F_WomensSumQtyActu			
	,F_MensSumQtyActuLY			
	,F_WomensSumQtyActuLY	
	,F_ActuAvgTarget
	,F_FirstAvgTarget
	,F_DisplayOrder
)
select 
	 ''
	,'∑'
	,isnull(sum(F_MonthTarGet),0)				
	,isnull(sum(F_PosTarget),0)						
	,isnull(sum(F_PosActu),0)						
	,isnull(sum(F_PosActuLY),0)						
	,isnull(sum(F_Culi),0)							
	,isnull(sum(F_CuliLY),0)						
	,isnull(sum(F_CustomerActu),0)					
	,isnull(sum(F_VipActu),0)						
	,isnull(sum(F_MensSumPosTarget),0)				
	,isnull(sum(F_MensSumPosActu),0)				
	,isnull(sum(F_MensSumCuliActu),0)				
	--,isnull(sum(F_MensORPosTarget),0)				
	,isnull(sum(F_MensORPosActu),0)					
	,isnull(sum(F_MensORCuliActu),0)				
	--,isnull(sum(F_MensBRPosTarget),0)				
	,isnull(sum(F_MensBRPosActu),0)					
	,isnull(sum(F_MensBRCuliActu),0)				
	,isnull(sum(F_WomensSumPosTarget),0)			
	,isnull(sum(F_WomensSumPosActu),0)				
	,isnull(sum(F_WomensSumCuliActu),0)				
	--,isnull(sum(F_WomensORPosTarget),0)				
	,isnull(sum(F_WomensORPosActu),0)				
	,isnull(sum(F_WomensORCuliActu),0)				
	--,isnull(sum(F_WomensBRPosTarget),0)				
	,isnull(sum(F_WomensBRPosActu),0)				
	,isnull(sum(F_WomensBRCuliActu),0)				
	,isnull(sum(F_DiscountActu),0)					
	,isnull(sum(F_MensComeShop),0)					
	,isnull(sum(F_MensComeShopLY),0)				
	,isnull(sum(F_MensBillCount),0)					
	,isnull(sum(F_MensBillCountLY),0)				
	,isnull(sum(F_WomensComeShop),0)				
	,isnull(sum(F_WomensComeShopLY),0)				
	,isnull(sum(F_WomensBillCount),0)				
	,isnull(sum(F_WomensBillCountLY),0)				
	,isnull(sum(F_SumStorageCostMoney),0)			
	,isnull(sum(F_SumStoragePosMoney),0)			
	,isnull(sum(F_SumStorageCostMoneyHY),0)			
	,isnull(sum(F_MensStorageCostMoney),0)			
	,isnull(sum(F_MensStoragePosMoney),0)			
	,isnull(sum(F_MensORStorageCostMoney),0)		
	,isnull(sum(F_MensBRStorageCostMoney),0)		
	,isnull(sum(F_MensStorageCostMoneyHY),0)		
	,isnull(sum(F_WomensStorageCostMoney),0)		
	,isnull(sum(F_WomensStoragePosMoney),0)		
	,isnull(sum(F_WomensORStorageCostMoney),0)		
	,isnull(sum(F_WomensBRStorageCostMoney),0)		
	,isnull(sum(F_WomensStorageCostMoneyHY),0)	
	,isnull(sum(F_SumStockInCostMoney),0)			
	,isnull(sum(F_SumStockInPosMoney),0)			
	,isnull(sum(F_MensStockInCostMoney),0)			
	,isnull(sum(F_MensStockInPosMoney),0)			
	,isnull(sum(F_MensORStockInCostMoney),0)		
	,isnull(sum(F_MensBRStockInCostMoney),0)		
	,isnull(sum(F_WomensStockInCostMoney),0)		
	,isnull(sum(F_WomensStockInPosMoney),0)			
	,isnull(sum(F_WomensORStockInCostMoney),0)		
	,isnull(sum(F_WomensBRStockInCostMoney),0)		
	,isnull(sum(F_CustomerActuLY),0)				
	,isnull(sum(F_VipActuLY),0)						
	,isnull(sum(F_MensSumPosActuLY),0)				
	,isnull(sum(F_MensSumCuliActuLY),0)				
	,isnull(sum(F_MensORPosActuLY),0)				
	,isnull(sum(F_MensORCuliActuLY),0)				
	,isnull(sum(F_MensBRPosActuLY),0)				
	,isnull(sum(F_MensBRCuliActuLY),0)				
	,isnull(sum(F_WomensSumPosActuLY),0)			
	,isnull(sum(F_WomensSumCuliActuLY),0)			
	,isnull(sum(F_WomensORPosActuLY),0)
	,isnull(sum(F_WomensORCuliActuLY),0)
	,isnull(sum(F_WomensBRPosActuLY),0)
	,isnull(sum(F_WomensBRCuliActuLY),0)
	,isnull(sum(F_DiscountActuLY),0)
	,isnull(sum(F_MensSumQtyActu),0)
	,isnull(sum(F_WomensSumQtyActu),0)
	,isnull(sum(F_MensSumQtyActuLY),0)
	,isnull(sum(F_WomensSumQtyActuLY),0)
	,isnull(sum(F_ActuAvgTarget),0)
	,isnull(sum(F_FirstAvgTarget),0)
	,'9999'
from #temp	


--计算列
update a set
	a.F_PosDCLv = case when isnull(a.F_PosTarget,0)=0 then 0 else isnull(a.F_PosActu/a.F_PosTarget,0) end,	--销售达成率
	a.F_PosRateLY = case when isnull(a.F_PosActuLY,0)=0 then 0 else isnull(a.F_PosActu/a.F_PosActuLY,0) end,	--去年销售比
	a.F_Culilv = case when isnull(a.F_PosActu,0)=0 then 0 else isnull(a.F_Culi/a.F_PosActu,0) end,	--毛利率	
	a.F_CuliRateLY = case when isnull(a.F_CuliLY,0)=0 then 0 else isnull(a.F_Culi/a.F_CuliLY,0) end,	--毛利去年比
	a.F_CulilvLY = case when isnull(a.F_PosActuLY,0)=0 then 0 else isnull(a.F_CuliLY/a.F_PosActuLY,0) end,	--去年毛利率
	a.F_CustomerRateLY = case when isnull(a.F_CustomerActuLY,0)=0 then 0 else isnull(a.F_CustomerActu/a.F_CustomerActuLY,0) end,	--顾客去年比
	a.F_CustomerLv = case when isnull(a.F_PosActu,0)=0 then 0 else isnull(a.F_CustomerActu/a.F_PosActu,0) end,	--顾客率
	a.F_VipRateLY = case when isnull(a.F_VipActuLY,0)=0 then 0 else isnull(a.F_VipActu/a.F_VipActuLY,0) end,	--会员去年比
	a.F_VipLv = case when isnull(a.F_PosActu,0)=0 then 0 else isnull(a.F_VipActu/a.F_PosActu,0) end,	--会员率
	a.F_MensSumPosDCLv = case when isnull(a.F_MensSumPosTarget,0)=0 then 0 else isnull(a.F_MensSumPosActu/a.F_MensSumPosTarget,0) end,	--MENS合计达成率
	a.F_MensSumPosCulilv = case when isnull(a.F_MensSumPosActu,0)=0 then 0 else isnull(a.F_MensSumCuliActu/a.F_MensSumPosActu,0) end,	--MENS毛利率
	a.F_MensSumPosLv = case when isnull(a.F_PosActu,0)=0 then 0 else isnull(a.F_MensSumPosActu/a.F_PosActu,0) end,	--MENS率
	a.F_MensSumCulilvLY = case when isnull(a.F_MensSumPosActuLY,0)=0 then 0 else isnull(a.F_MensSumCuliActuLY/a.F_MensSumPosActuLY,0) end,	  --MENS合计去年毛利率
	a.F_MensSumCuliRateLY = case when isnull(a.F_MensSumCuliActuLY,0)=0 then 0 else isnull(a.F_MensSumCuliActu/a.F_MensSumCuliActuLY,0) end,	--MENS合计毛利去年比
	--a.F_MensORPosDCLv = case when isnull(a.F_MensORPosTarget,0)=0 then 0 else isnull(a.F_MensORPosActu/a.F_MensORPosTarget,0) end,	--MENS OR 合计达成率
	a.F_MensORPosCulilv = case when isnull(a.F_MensORPosActu,0)=0 then 0 else isnull(a.F_MensORCuliActu/a.F_MensORPosActu,0) end,	--MENS OR 毛利率
	a.F_MensORPosLv = case when isnull(a.F_MensSumPosActu,0)=0 then 0 else isnull(a.F_MensORPosActu/a.F_MensSumPosActu,0) end,	--MENS OR 率
	a.F_MensORCulilvLY = case when isnull(a.F_MensORPosActuLY,0)=0 then 0 else isnull(a.F_MensORCuliActuLY/a.F_MensORPosActuLY,0) end,	  --MENS OR 合计去年毛利率
	a.F_MensORCuliRateLY = case when isnull(a.F_MensORCuliActuLY,0)=0 then 0 else isnull(a.F_MensORCuliActu/a.F_MensORCuliActuLY,0) end,	--MENS OR 合计毛利去年比
	--a.F_MensBRPosDCLv = case when isnull(a.F_MensBRPosTarget,0)=0 then 0 else isnull(a.F_MensBRPosActu/a.F_MensBRPosTarget,0) end,	--MENS BR 合计达成率
	a.F_MensBRPosCulilv = case when isnull(a.F_MensBRPosActu,0)=0 then 0 else isnull(a.F_MensBRCuliActu/a.F_MensBRPosActu,0) end,	--MENS BR 毛利率
	a.F_MensBRPosLv = case when isnull(a.F_MensSumPosActu,0)=0 then 0 else isnull(a.F_MensBRPosActu/a.F_MensSumPosActu,0) end,	--MENS BR 率
	a.F_MensBRCulilvLY = case when isnull(a.F_MensBRPosActuLY,0)=0 then 0 else isnull(a.F_MensBRCuliActuLY/a.F_MensBRPosActuLY,0) end,	  --MENS BR 合计去年毛利率
	a.F_MensBRCuliRateLY = case when isnull(a.F_MensBRCuliActuLY,0)=0 then 0 else isnull(a.F_MensBRCuliActu/a.F_MensBRCuliActuLY,0) end,	--MENS BR 合计毛利去年比
	a.F_WomensSumPosDCLv = case when isnull(a.F_WomensSumPosTarget,0)=0 then 0 else isnull(a.F_WomensSumPosActu/a.F_WomensSumPosTarget,0) end,	--WOMENS合计达成率
	a.F_WomensSumPosCulilv = case when isnull(a.F_WomensSumPosActu,0)=0 then 0 else isnull(a.F_WomensSumCuliActu/a.F_WomensSumPosActu,0) end,	--WOMENS毛利率
	a.F_WomensSumPosLv = case when isnull(a.F_PosActu,0)=0 then 0 else isnull(a.F_WomensSumPosActu/a.F_PosActu,0) end,	--WOMENS率
	a.F_WomensSumCulilvLY = case when isnull(a.F_WomensSumPosActuLY,0)=0 then 0 else isnull(a.F_WomensSumCuliActuLY/a.F_WomensSumPosActuLY,0) end,	  --WOMENS合计去年毛利率
	a.F_WomensSumCuliRateLY = case when isnull(a.F_WomensSumCuliActuLY,0)=0 then 0 else isnull(a.F_WomensSumCuliActu/a.F_WomensSumCuliActuLY,0) end,	--WOMENS合计毛利去年比
	--a.F_WomensORPosDCLv = case when isnull(a.F_WomensORPosTarget,0)=0 then 0 else isnull(a.F_WomensORPosActu/a.F_WomensORPosTarget,0) end,	--WOMENS OR 合计达成率
	a.F_WomensORPosCulilv = case when isnull(a.F_WomensORPosActu,0)=0 then 0 else isnull(a.F_WomensORCuliActu/a.F_WomensORPosActu,0) end,	--WOMENS OR 毛利率
	a.F_WomensORPosLv = case when isnull(a.F_WomensSumPosActu,0)=0 then 0 else isnull(a.F_WomensORPosActu/a.F_WomensSumPosActu,0) end,	--WOMENS OR 率
	a.F_WomensORCulilvLY = case when isnull(a.F_WomensORPosActuLY,0)=0 then 0 else isnull(a.F_WomensORCuliActuLY/a.F_WomensORPosActuLY,0) end,	  --WOMENS OR 合计去年毛利率
	a.F_WomensORCuliRateLY = case when isnull(a.F_WomensORCuliActuLY,0)=0 then 0 else isnull(a.F_WomensORCuliActu/a.F_WomensORCuliActuLY,0) end,	--WOMENS OR 合计毛利去年比
	--a.F_WomensBRPosDCLv = case when isnull(a.F_WomensBRPosTarget,0)=0 then 0 else isnull(a.F_WomensBRPosActu/a.F_WomensBRPosTarget,0) end,	--WOMENS BR 合计达成率
	a.F_WomensBRPosCulilv = case when isnull(a.F_WomensBRPosActu,0)=0 then 0 else isnull(a.F_WomensBRCuliActu/a.F_WomensBRPosActu,0) end,	--WOMENS BR 毛利率
	a.F_WomensBRPosLv = case when isnull(a.F_WomensSumPosActu,0)=0 then 0 else isnull(a.F_WomensBRPosActu/a.F_WomensSumPosActu,0) end,	--WOMENS BR 率
	a.F_WomensBRCulilvLY = case when isnull(a.F_WomensBRPosActuLY,0)=0 then 0 else isnull(a.F_WomensBRCuliActuLY/a.F_WomensBRPosActuLY,0) end,	  --WOMENS BR 合计去年毛利率
	a.F_WomensBRCuliRateLY = case when isnull(a.F_WomensBRCuliActuLY,0)=0 then 0 else isnull(a.F_WomensBRCuliActu/a.F_WomensBRCuliActuLY,0) end,	--WOMENS BR 合计毛利去年比
	a.F_Discount = case when isnull(a.F_DiscountActu+a.F_PosActu,0)=0 then 0 else isnull(a.F_DiscountActu/(a.F_DiscountActu+a.F_PosActu),0) end,	--优惠率
	a.F_DiscountRateLY = case when isnull(a.F_DiscountActuLY,0)=0 then 0 else isnull(a.F_DiscountActu/a.F_DiscountActuLY,0) end,	--优惠去年比
	a.F_MensAvgPrice = case when isnull(a.F_MensSumQtyActu,0)=0 then 0 else isnull(a.F_MensSumPosActu/a.F_MensSumQtyActu,0) end,	--MENS 平均单价
	a.F_MensAvgPriceLY = case when isnull(a.F_MensSumQtyActuLY,0)=0 then 0 else isnull(a.F_MensSumPosActuLY/a.F_MensSumQtyActuLY,0) end,	--MENS 去年平均单价
	a.F_MensBuyRate = case when isnull(a.F_MensComeShop,0)=0 then 0 else isnull(a.F_MensBillCount/a.F_MensComeShop,0) end,	--MENS 购买率
	a.F_MensBuyRateLY = case when isnull(a.F_MensComeShopLY,0)=0 then 0 else isnull(a.F_MensBillCountLY/a.F_MensComeShopLY,0) end,	--MENS 去年购买率
	a.F_MensLinkBillRate = case when isnull(a.F_MensBillCount,0)=0 then 0 else isnull(a.F_MensSumQtyActu/a.F_MensBillCount,0) end,	--MENS 联单率
	a.F_MensLinkBillRateLY = case when isnull(a.F_MensBillCountLY,0)=0 then 0 else isnull(a.F_MensSumQtyActuLY/a.F_MensBillCountLY,0) end,	--MENS 去年联单率
	a.F_MensBillCountPrice = case when isnull(a.F_MensBillCount,0)=0 then 0 else isnull(a.F_MensSumPosActu/a.F_MensBillCount,0) end,	--MENS 客单价
	a.F_MensBillCountPriceLY = case when isnull(a.F_MensBillCountLY,0)=0 then 0 else isnull(a.F_MensSumPosActuLY/a.F_MensBillCountLY,0) end,	--MENS 去年客单价
	a.F_WomensAvgPrice = case when isnull(a.F_WomensSumQtyActu,0)=0 then 0 else isnull(a.F_WomensSumPosActu/a.F_WomensSumQtyActu,0) end,	--WOMENS 平均单价
	a.F_WomensAvgPriceLY = case when isnull(a.F_WomensSumQtyActuLY,0)=0 then 0 else isnull(a.F_WomensSumPosActuLY/a.F_WomensSumQtyActuLY,0) end,	--WOMENS 去年平均单价
	a.F_WomensBuyRate = case when isnull(a.F_WomensComeShop,0)=0 then 0 else isnull(a.F_WomensBillCount/a.F_WomensComeShop,0) end,	--WOMENS 购买率
	a.F_WomensBuyRateLY = case when isnull(a.F_WomensComeShopLY,0)=0 then 0 else isnull(a.F_WomensBillCountLY/a.F_WomensComeShopLY,0) end,	--WOMENS 去年购买率
	a.F_WomensLinkBillRate = case when isnull(a.F_WomensBillCount,0)=0 then 0 else isnull(a.F_WomensSumQtyActu/a.F_WomensBillCount,0) end,	--WOMENS 联单率
	a.F_WomensLinkBillRateLY = case when isnull(a.F_WomensBillCountLY,0)=0 then 0 else isnull(a.F_WomensSumQtyActuLY/a.F_WomensBillCountLY,0) end,	--MENS 去年联单率
	a.F_WomensBillCountPrice = case when isnull(a.F_WomensBillCount,0)=0 then 0 else isnull(a.F_WomensSumPosActu/a.F_WomensBillCount,0) end,	--WOMENS 客单价
	a.F_WomensBillCountPriceLY = case when isnull(a.F_WomensBillCountLY,0)=0 then 0 else isnull(a.F_WomensSumPosActuLY/a.F_WomensBillCountLY,0) end,	--WOMENS 去年客单价
	a.F_ActuTargetDays = case when isnull(a.F_ActuAvgTarget,0)=0 then 0 else isnull(a.F_SumStoragePosMoney/a.F_ActuAvgTarget,0) end,	--实绩目标日数
	a.F_FirstTargetDays = case when isnull(a.F_FirstAvgTarget,0)=0 then 0 else isnull(a.F_SumStoragePosMoney/a.F_FirstAvgTarget,0) end	--最初目标日数
from #temp a


select * from #temp order by F_DisplayOrder

drop table #shp
drop table #temp
