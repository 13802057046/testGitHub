USE [TestL]
GO

/****** Object:  Table [dbo].[t_OrderLogisticsInformation]    Script Date: 10/27/2019 8:31:57 PM ******/
DROP TABLE [dbo].[t_OrderLogisticsInformation]
GO

/****** Object:  Table [dbo].[t_OrderLogisticsInformation]    Script Date: 10/27/2019 8:31:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[t_OrderLogisticsInformation](
	[F_BillID] [nvarchar](50) NOT NULL,
	[F_ExpressCode] [nvarchar](30) NULL,
	[F_expressNum] [nvarchar](50) NULL,
	[F_DeliveryPlanTime] [nvarchar](19) NULL,
	[F_ItemAmount] [decimal](18, 2) NULL,
	[F_Freight] [decimal](18, 2) NULL,
	[F_TotalAmount] [decimal](18, 2) NULL,
	[F_Payment] [decimal](18, 2) NULL,
	[F_InsuranceValue] [decimal](18, 2) NULL,
	[F_CommunitySyncCode] [nvarchar](50) NULL,
	[F_BuyerNick] [nvarchar](50) NULL,
	[F_TradeId] [nvarchar](50) NULL,
	[F_SourcePlatformCode] [nvarchar](50) NULL,
	[F_Remark] [nvarchar](200) NULL,
	[F_isPrintInvoice] [bit] NULL,
	[F_country] [nvarchar](100) NULL,
	[F_dropoffType] [nvarchar](50) NULL,
	[F_serviceType] [nvarchar](50) NULL,
	[F_packagingType] [nvarchar](50) NULL,
	[F_name] [nvarchar](32) NULL,
	[F_zipCode] [nvarchar](10) NULL,
	[F_phone] [nvarchar](32) NULL,
	[F_mobilePhone] [nvarchar](32) NULL,
	[F_province] [nvarchar](32) NULL,
	[F_city] [nvarchar](32) NULL,
	[F_county] [nvarchar](32) NULL,
	[F_address] [nvarchar](150) NULL,
	[F_address2] [nvarchar](500) NULL,
	[F_senderName] [nvarchar](32) NULL,
	[F_senderPhone] [nvarchar](32) NULL,
	[F_senderMobile] [nvarchar](32) NULL,
	[F_BillStatus] [nvarchar](32) NULL,
 CONSTRAINT [PK_t_OrderLogisticsInformation] PRIMARY KEY CLUSTERED 
(
	[F_BillID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


