
alter procedure [dbo].[sp_YuejiReport]
(
	@ShopID		nvarchar(max),
	@Start		date,
	@End		date,
	@Men		nvarchar(max),
	@Women		nvarchar(max)
)
as

set @ShopID = N'7100,7200,7205,7206,7298,7299,7201,7202,7203,7204,7221'
set @Start = '2018-11-30 23:59:59'
set @End = '2018-12-01 00:00:00'
set @Men = N'STﾒﾝｽﾞ,UTﾒﾝｽﾞ,PTﾒﾝｽﾞ,派生ﾒﾝｽﾞ'
set @Women = N'STｳｨﾒﾝｽﾞ,UTｳｨﾒﾝｽﾞ,PTｳｨﾒﾝｽﾞ,派生ｳｨﾒﾝｽﾞ'

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

create table #men
(
	F_Sex	nvarchar(30)
)
create table #women
(
	F_Sex	nvarchar(30)
)

insert into #men select N'STﾒﾝｽﾞ' 
			union select N'UTﾒﾝｽﾞ'
			union select N'PTﾒﾝｽﾞ'
			union select N'派生ﾒﾝｽﾞ'

insert into #women select N'STｳｨﾒﾝｽﾞ' 
			union select N'UTｳｨﾒﾝｽﾞ'
			union select N'PTｳｨﾒﾝｽﾞ'
			union select N'派生ｳｨﾒﾝｽﾞ'

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
	F_WomensBRStockInCostMoney	decimal(18,2),  --WOMENS BR 成本


	--以下为隐藏字段
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


--各种目标
update a set
	a.F_PosTarget = isnull(b.F_PosTarget,0),
	a.F_MensSumPosTarget = isnull(m.F_MensSumPosTarget,0),
	a.F_WomensSumPosTarget = isnull(w.F_WomensSumPosTarget,0)
from #temp a
--合计目标
left join (
			select F_ShopID, sum(F_Money) as F_PosTarget
			from t_TarGetSet
			where F_Flag = 0 and F_Kind = 1
			and F_Date >= @Start
			and F_Date <= @End
			group by F_ShopID
			) b on a.F_ShopID = b.F_ShopID
--男款目标
left join (
			select F_ShopID, sum(F_Money) as F_MensSumPosTarget
			from t_TarGetSet
			where F_Flag = 0 and F_Kind = 1 and F_Brand in (select F_Sex from #men)
			and F_Date >= @Start
			and F_Date <= @End
			group by F_ShopID
			) m on a.F_ShopID = m.F_ShopID
--女款目标
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
	and convert(nvarchar(10),a.F_Date,120) >= convert(nvarchar(10),@Start,120)
	and convert(nvarchar(10),a.F_Date,120) >= convert(nvarchar(10),@End,120)
)

update a set
	a.F_PosActu = isnull(b.F_PosActu,0),
	a.F_Culi = isnull(b.F_Culi,0),
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
	a.F_WomensBRPosCulilv = isnull(w2.F_WomensBRCuliActu,0)
from #temp a
--销售合计、毛利合计
left join (
			select F_ShopID, 
			sum(F_Money) as F_PosActu,
			sum(F_Money - F_Qty * F_CostPrice) as F_Culi
			from pos
			group by F_ShopID
			) b on a.F_ShopID = b.F_ShopID
--男款（销售合计、毛利合计）
left join (
			select F_ShopID, 
			sum(F_Money) as F_MensSumPosActu, 
			sum(F_Money - F_Qty * F_CostPrice) as F_MensSumCuliActu
			from pos
			where F_Brand in (select F_Sex from #men)
			group by F_ShopID
			) m on a.F_ShopID = m.F_ShopID
--男款OR（销售合计、毛利合计）
left join (
			select F_ShopID, 
			sum(F_Money) as F_MensORPosActu,
			sum(F_Money - F_Qty * F_CostPrice) as F_MensORCuliActu
			from pos
			where F_Brand in (select F_Sex from #men) and F_ItemProperty = 'OR'
			group by F_ShopID
			) m1 on a.F_ShopID = m1.F_ShopID
--男款BR（销售合计、毛利合计）
left join (
			select F_ShopID, 
			sum(F_Money) as F_MensBRPosActu,
			sum(F_Money - F_Qty * F_CostPrice) as F_MensBRCuliActu
			from pos
			where F_Brand in (select F_Sex from #men) and F_ItemProperty = 'BR'
			group by F_ShopID
			) m2 on a.F_ShopID = m2.F_ShopID
--女款（销售合计、毛利合计）
left join (
			select F_ShopID, 
			sum(F_Money) as F_WomensSumPosActu,
			sum(F_Money - F_Qty * F_CostPrice) as F_WomensSumCuliActu
			from pos
			where F_Brand in (select F_Sex from #women)
			group by F_ShopID
			) w on a.F_ShopID = w.F_ShopID
--女款OR（销售合计、毛利合计）
left join (
			select F_ShopID, 
			sum(F_Money) as F_WomensORPosActu,
			sum(F_Money - F_Qty * F_CostPrice) as F_WomensORCuliActu
			from pos
			where F_Brand in (select F_Sex from #women) and F_ItemProperty = 'OR'
			group by F_ShopID
			) w1 on a.F_ShopID = w1.F_ShopID
--女款BR（销售合计、毛利合计）
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
	select a.F_ShopID, b.*, c.F_Brand, c.F_ItemProperty
	from t_Pos a
	left join t_PosDetail b on a.F_BillID = b.F_BillID
	left join v_Item_NoPic c on b.F_ItemID =c.f_id and  b.F_ColorID = c.F_ColorID and  b.F_SizeID = c.F_SizeID 
	where a.F_Status = N'正常'
	and convert(nvarchar(10),a.F_Date,120) >= convert(nvarchar(10),dateadd(year,-1,@Start),120)
	and convert(nvarchar(10),a.F_Date,120) >= convert(nvarchar(10),dateadd(year,-1,@End),120)
)

update a set
	a.F_PosActuLY = isnull(b.F_PosActuLY,0),
	a.F_CuliLY = isnull(b.F_CuliLY,0),
	a.F_MensSumCuliActuLY = isnull(m.F_MensSumCuliActuLY,0),
	a.F_MensORCuliActuLY = isnull(m1.F_MensORCuliActuLY,0),
	a.F_MensBRCuliActuLY = isnull(m2.F_MensBRCuliActuLY,0),
	a.F_WomensSumCuliActuLY = isnull(w.F_WomensSumCuliActuLY,0),
	a.F_WomensORCuliActuLY = isnull(w1.F_WomensORCuliActuLY,0),
	a.F_WomensBRCuliActuLY = isnull(w2.F_WomensBRCuliActuLY,0)
from #temp a
--销售合计、毛利合计
left join (
			select F_ShopID, 
			sum(F_Money) as F_PosActuLY,
			sum(F_Money - F_Qty * F_CostPrice) as F_CuliLY
			from posLy
			group by F_ShopID
			) b on a.F_ShopID = b.F_ShopID
--男款（销售合计、毛利合计）
left join (
			select F_ShopID, 
			sum(F_Money) as F_MensSumPosActuLY, 
			sum(F_Money - F_Qty * F_CostPrice) as F_MensSumCuliActuLY
			from posLy
			where F_Brand in (select F_Sex from #men)
			group by F_ShopID
			) m on a.F_ShopID = m.F_ShopID
--男款OR（销售合计、毛利合计）
left join (
			select F_ShopID, 
			sum(F_Money) as F_MensORPosActuLY,
			sum(F_Money - F_Qty * F_CostPrice) as F_MensORCuliActuLY
			from posLy
			where F_Brand in (select F_Sex from #men) and F_ItemProperty = 'OR'
			group by F_ShopID
			) m1 on a.F_ShopID = m1.F_ShopID
--男款BR（销售合计、毛利合计）
left join (
			select F_ShopID, 
			sum(F_Money) as F_MensBRPosActuLY,
			sum(F_Money - F_Qty * F_CostPrice) as F_MensBRCuliActuLY
			from posLy
			where F_Brand in (select F_Sex from #men) and F_ItemProperty = 'BR'
			group by F_ShopID
			) m2 on a.F_ShopID = m2.F_ShopID
--女款（销售合计、毛利合计）
left join (
			select F_ShopID, 
			sum(F_Money) as F_WomensSumPosActuLY,
			sum(F_Money - F_Qty * F_CostPrice) as F_WomensSumCuliActuLY
			from posLy
			where F_Brand in (select F_Sex from #women)
			group by F_ShopID
			) w on a.F_ShopID = w.F_ShopID
--女款OR（销售合计、毛利合计）
left join (
			select F_ShopID, 
			sum(F_Money) as F_WomensORPosActuLY,
			sum(F_Money - F_Qty * F_CostPrice) as F_WomensORCuliActuLY
			from posLy
			where F_Brand in (select F_Sex from #women) and F_ItemProperty = 'OR'
			group by F_ShopID
			) w1 on a.F_ShopID = w1.F_ShopID
--女款BR（销售合计、毛利合计）
left join (
			select F_ShopID, 
			sum(F_Money) as F_WomensBRPosActuLY,
			sum(F_Money - F_Qty * F_CostPrice) as F_WomensBRCuliActuLY
			from posLy
			where F_Brand in (select F_Sex from #women) and F_ItemProperty = 'BR'
			group by F_ShopID
			) w2 on a.F_ShopID = w2.F_ShopID


--达成率
update a set
	a.F_PosDCLv = case when isnull(a.F_PosTarget,0)=0 then 0 else isnull(a.F_PosActu/a.F_PosTarget,0) end,
	a.F_MensSumPosDCLv = case when isnull(a.F_MensSumPosTarget,0)=0 then 0 else isnull(a.F_MensSumPosActu/a.F_MensSumPosTarget,0) end,
	a.F_WomensSumPosDCLv = case when isnull(a.F_WomensSumPosTarget,0)=0 then 0 else isnull(a.F_WomensSumPosActu/a.F_WomensSumPosTarget,0) end
from #temp a

--毛利率
update a set
	a.F_Culilv = case when isnull(a.F_PosActu,0)=0 then 0 else isnull(a.F_Culi/a.F_PosActu,0) end,
	a.F_CulilvLY = case when isnull(a.F_PosActuLY,0)=0 then 0 else isnull(a.F_CuliLY/a.F_PosActuLY,0) end,
	a.F_MensSumPosCulilv = case when isnull(a.F_MensSumPosActu,0)=0 then 0 else isnull(a.F_MensSumCuliActu/a.F_MensSumPosActu,0) end,
	a.F_MensSumCulilvLY = case when isnull(a.F_MensSumPosActuLY,0)=0 then 0 else isnull(a.F_MensSumCuliActuLY/a.F_MensSumPosActuLY,0) end,
	a.F_MensORPosCulilv = case when isnull(a.F_MensORCuliActu,0)=0 then 0 else isnull(a.F_MensORCuliActu/a.F_MensORPosActu,0) end,
	a.F_MensORCulilvLY = case when isnull(a.F_MensORPosActuLY,0)=0 then 0 else isnull(a.F_MensORCuliActuLY/a.F_MensORPosActuLY,0) end,
	a.F_MensBRPosCulilv = case when isnull(a.F_MensBRCuliActu,0)=0 then 0 else isnull(a.F_MensBRCuliActu/a.F_MensBRPosActu,0) end,
	a.F_MensBRCulilvLY = case when isnull(a.F_MensBRPosActuLY,0)=0 then 0 else isnull(a.F_MensBRCuliActuLY/a.F_MensBRPosActuLY,0) end,
	a.F_WomensSumCulilvLY = case when isnull(a.F_WomensSumPosActuLY,0)=0 then 0 else isnull(a.F_WomensSumCuliActuLY/a.F_WomensSumPosActuLY,0) end,
	a.F_WomensORPosCulilv = case when isnull(a.F_WomensORCuliActu,0)=0 then 0 else isnull(a.F_WomensORCuliActu/a.F_WomensORPosActu,0) end,
	a.F_WomensORCulilvLY = case when isnull(a.F_WomensORPosActuLY,0)=0 then 0 else isnull(a.F_WomensORCuliActuLY/a.F_WomensORPosActuLY,0) end,
	a.F_WomensBRPosCulilv = case when isnull(a.F_WomensBRCuliActu,0)=0 then 0 else isnull(a.F_WomensBRCuliActu/a.F_WomensBRPosActu,0) end,
	a.F_WomensBRCulilvLY = case when isnull(a.F_WomensBRPosActuLY,0)=0 then 0 else isnull(a.F_WomensBRCuliActuLY/a.F_WomensBRPosActuLY,0) end
from #temp a

--去年比
update a set
	a.F_PosRateLY = case when isnull(a.F_PosActuLY,0)=0 then 0 else isnull(a.F_PosActu/a.F_PosActuLY,0) end,
	a.F_CuliRateLY = case when isnull(a.F_CuliLY,0)=0 then 0 else isnull(a.F_Culi/a.F_CuliLY,0) end,
	a.F_MensSumCuliRateLY = case when isnull(a.F_MensSumCuliActuLY,0)=0 then 0 else isnull(a.F_MensSumCuliActu/a.F_MensSumCuliActuLY,0) end,
	a.F_MensOrCuliRateLY = case when isnull(a.F_MensOrCuliActuLY,0)=0 then 0 else isnull(a.F_MensOrCuliActu/a.F_MensOrCuliActuLY,0) end,
	a.F_MensBrCuliRateLY = case when isnull(a.F_MensBrCuliActuLY,0)=0 then 0 else isnull(a.F_MensBrCuliActu/a.F_MensBrCuliActuLY,0) end,
	a.F_WomensSumCuliRateLY = case when isnull(a.F_WomensSumCuliActuLY,0)=0 then 0 else isnull(a.F_WomensSumCuliActu/a.F_WomensSumCuliActuLY,0) end,
	a.F_WomensOrCuliRateLY = case when isnull(a.F_WomensOrCuliActuLY,0)=0 then 0 else isnull(a.F_WomensOrCuliActu/a.F_WomensOrCuliActuLY,0) end,
	a.F_WomensBrCuliRateLY = case when isnull(a.F_WomensBrCuliActuLY,0)=0 then 0 else isnull(a.F_WomensBrCuliActu/a.F_WomensBrCuliActuLY,0) end
from #temp a

select * from #temp;

drop table #shp
drop table #temp
