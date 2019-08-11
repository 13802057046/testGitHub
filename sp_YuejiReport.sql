
alter procedure [dbo].[sp_YuejiReport]
(
	@ShopID		nvarchar(max),
	@Start		date,
	@End		date
)
as

set @ShopID = N'7100,7200,7205,7206,7298,7299,7201,7202,7203,7204,7221'
set @Start = '2018-11-30 23:59:59'
set @End = '2018-12-01 00:00:00'

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
	F_PosDCLv					decimal(18,2),  --销售达成率
	F_PosActuLY					decimal(18,2),  --去年销售实绩
	F_PosRateLY					decimal(18,2),  --去年销售比
	F_Culi						decimal(18,2),  --毛利
	F_Culilv					decimal(18,2),  --毛利率
	F_CuliLY					decimal(18,2),  --去年毛利
	F_CuliRateLY				decimal(18,2),  --毛利去年比
	F_CulilvLY					decimal(18,2),  --去年毛利率
	F_CustomerActu				decimal(18,2),  --顾客实绩
	F_CustomerRateLY			decimal(18,2),  --顾客去年比
	F_CustomerLv				decimal(18,2),  --顾客率
	F_VipActu					decimal(18,2),  --会员实绩
	F_VipRateLY					decimal(18,2),  --会员去年比
	F_VipLv						decimal(18,2),  --会员率
	F_MensSumPosTarget			decimal(18,2),  --合计目标
	F_MensSumPosActu			decimal(18,2),  --合计实绩
	F_MensSumPosDCLv			decimal(18,2),  --合计达成率
	F_MensSumPosCulilv			decimal(18,2),  --MENS毛利率
	F_MensSumPosLv				decimal(18,2),  --MENS率
	F_MensSumCuliActu			decimal(18,2),  --合计毛利实绩
	F_MensSumCulilvLY			decimal(18,2),  --合计去年毛利率
	F_MensSumCuliRateLY			decimal(18,2),  --合计毛利去年比
	F_MensORPosTarget			decimal(18,2),  --OR目标
	F_MensORPosActu				decimal(18,2),  --OR实绩
	F_MensORPosDCLv				decimal(18,2),  --OR达成率
	F_MensORPosCulilv			decimal(18,2),  --OR毛利率
	F_MensORPosLv				decimal(18,2),  --OR率
	F_MensORCuliActu			decimal(18,2),  --OR毛利实绩
	F_MensORCulilvLY			decimal(18,2),  --OR去年毛利率
	F_MensORCuliRateLY			decimal(18,2),  --OR去年比
	F_MensBRPosTarget			decimal(18,2),  --BR目标
	F_MensBRPosActu				decimal(18,2),  --BR实绩
	F_MensBRPosDCLv				decimal(18,2),  --BR达成率
	F_MensBRPosCulilv			decimal(18,2),  --BR毛利率
	F_MensBRPosLv				decimal(18,2),  --BR率
	F_MensBRCuliActu			decimal(18,2),  --BR毛利实绩
	F_MensBRCulilvLY			decimal(18,2),  --BR去年毛利率
	F_MensBRCuliRateLY			decimal(18,2),  --BR去年比
	F_WomensSumPosTarget		decimal(18,2),  --合计目标
	F_WomensSumPosActu			decimal(18,2),  --合计实绩
	F_WomensSumPosDCLv			decimal(18,2),  --合计达成率
	F_WomensSumPosCulilv		decimal(18,2),  --MENS毛利率
	F_WomensSumPosLv			decimal(18,2),  --MENS率
	F_WomensSumCuliActu			decimal(18,2),  --合计毛利实绩
	F_WomensSumCulilvLY			decimal(18,2),  --合计去年毛利率
	F_WomensSumCuliRateLY		decimal(18,2),  --合计毛利去年比
	F_WomensORPosTarget			decimal(18,2),  --OR目标
	F_WomensORPosActu			decimal(18,2),  --OR实绩
	F_WomensORPosDCLv			decimal(18,2),  --OR达成率
	F_WomensORPosCulilv			decimal(18,2),  --OR毛利率
	F_WomensORPosLv				decimal(18,2),  --OR率
	F_WomensORCuliActu			decimal(18,2),  --OR毛利实绩
	F_WomensORCulilvLY			decimal(18,2),  --OR去年毛利率
	F_WomensORCuliRateLY		decimal(18,2),  --OR去年比
	F_WomensBRPosTarget			decimal(18,2),  --BR目标
	F_WomensBRPosActu			decimal(18,2),  --BR实绩
	F_WomensBRPosDCLv			decimal(18,2),  --BR达成率
	F_WomensBRPosCulilv			decimal(18,2),  --BR毛利率
	F_WomensBRPosLv				decimal(18,2),  --BR率
	F_WomensBRCuliActu			decimal(18,2),  --BR毛利实绩
	F_WomensBRCulilvLY			decimal(18,2),  --BR去年毛利率
	F_WomensBRCuliRateLY		decimal(18,2),  --BR去年比
	F_DiscountActu				decimal(18,2),  --优惠实绩
	F_Discount					decimal(18,2),  --优惠率
	F_DiscountRateLY			decimal(18,2),  --优惠去年比
	F_MensComeShop				decimal(18,2),  --来店数
	F_MensComeShopLY			decimal(18,2),  --去年来店数
	F_MensAvgPrice				decimal(18,2),  --平均单价
	F_MensAvgPriceLY			decimal(18,2),  --去年平均单价
	F_MensBuyRate				decimal(18,2),  --购买率
	F_MensBuyRateLY				decimal(18,2),  --去年购买率
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
	F_WomensBRStockInCostMoney	decimal(18,2)   --WOMENS BR 成本
)

insert into #temp(F_ShopID,F_ShopName,F_MonthTarGet)
select s.F_ShopID,
       t.F_Name,
	   isnull(m.F_MonthTarGet,0)
from #shp s
left join t_Shop t on s.F_ShopID = t.F_ID
left join (
			select F_ShopID, sum(F_Money) as F_MonthTarGet 
			from t_TarGetSet
			where F_Flag = 0 and F_Kind = 1
			and F_Date >= dateadd(month, datediff(month, 0, @Start), 0)
			and F_Date <= dateadd(month, datediff(month, 0, dateadd(month, 1, @End)), -1)
			group by F_ShopID
			) m on s.F_ShopID = m.F_ShopID


--各种预算
update a set
	a.F_PosTarget = isnull(b.F_PosTarget,0),
	a.F_MensSumPosTarget = isnull(m.F_MensSumPosTarget,0),
	a.F_WomensSumPosTarget = isnull(w.F_WomensSumPosTarget,0)
from #temp a
left join (
			select F_ShopID, sum(F_Money) as F_PosTarget
			from t_TarGetSet
			where F_Flag = 0 and F_Kind = 1
			and F_Date >= @Start
			and F_Date <= @End
			group by F_ShopID
			) b on a.F_ShopID = b.F_ShopID
left join (
			select F_ShopID, sum(F_Money) as F_MensSumPosTarget
			from t_TarGetSet
			where F_Flag = 0 and F_Kind = 1 and F_Brand like N'%ﾒﾝｽﾞ'
			and F_Date >= @Start
			and F_Date <= @End
			group by F_ShopID
			) m on a.F_ShopID = m.F_ShopID
left join (
			select F_ShopID, sum(F_Money) as F_WomensSumPosTarget
			from t_TarGetSet
			where F_Flag = 0 and F_Kind = 1 and F_Brand like N'%ｳｨﾒﾝｽﾞ'
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
	and convert(nvarchar(10),a.F_Date,120) >= convert(nvarchar(10),@Start,120)
	and convert(nvarchar(10),a.F_Date,120) >= convert(nvarchar(10),@End,120)
)

update a set
	a.F_PosActu = isnull(b.F_PosActu,0),
	a.F_MensSumPosActu = isnull(c.F_MensSumPosActu,0),
	a.F_MensORPosActu = isnull(d.F_MensORPosActu,0),
	a.F_MensBRPosActu = isnull(e.F_MensBRPosActu,0)
from #temp a
left join (
			select F_ShopID, 
			sum(F_Money) as F_PosActu,
			sum(F_Money - F_Qty * F_CostPrice) as F_Culi
			from pos
			group by F_ShopID
			) b on a.F_ShopID = b.F_ShopID
left join (
			select F_ShopID, sum(F_Money) as F_MensSumPosActu
			from pos
			where F_Brand like '%ﾒﾝｽﾞ'
			group by F_ShopID
			) c on a.F_ShopID = b.F_ShopID
left join (
			select F_ShopID, sum(F_Money) as F_MensORPosActu
			from pos
			where F_Brand like '%ﾒﾝｽﾞ' and F_ItemProperty = 'OR'
			group by F_ShopID
			) d on a.F_ShopID = b.F_ShopID
left join (
			select F_ShopID, sum(F_Money) as F_MensBRPosActu
			from pos
			where F_Brand like '%ﾒﾝｽﾞ' and F_ItemProperty = 'BR'
			group by F_ShopID
			) e on a.F_ShopID = b.F_ShopID




--去年各种实绩
;with posLy as (
	select a.F_ShopID, b.*, c.F_Brand, c.F_ItemProperty
	from t_Pos a
	left join t_PosDetail b on a.F_BillID = b.F_BillID
	left join v_Item_NoPic c on b.F_ItemID =c.f_id and  b.F_ColorID = c.F_ColorID and  b.F_SizeID = c.F_SizeID 
	where a.F_Status = N'正常'
	and convert(nvarchar(10),a.F_Date,120) >= convert(nvarchar(10),dateadd(year,-1,@Start),120)
	and convert(nvarchar(10),a.F_Date,120) >= convert(nvarchar(10),dateadd(year,-1,@End),120)
)


select * from #temp;

drop table #shp
drop table #temp
