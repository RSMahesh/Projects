USE [InfoWebTestDB]
GO
/****** Object:  Table [dbo].[RequestLog]    Script Date: 11/19/2012 18:00:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RequestLog](
	[FID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[RequestText] [varchar](max) NULL,
	[SessionID] [nvarchar](50) NULL,
	[ScreenName] [nvarchar](50) NULL,
	[ErrorInfo] [varchar](max) NULL,
	[InsertDate] [smalldatetime] NULL,
	[UserIP] [nvarchar](20) NULL,
 CONSTRAINT [PK_RequestLog] PRIMARY KEY CLUSTERED 
(
	[FID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CartLog]    Script Date: 11/19/2012 17:57:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CartLog](
	[FID] [uniqueidentifier] NOT NULL,
	[Item] [varchar](40) NOT NULL,
	[Domain] [varchar](50) NULL,
	[Quantity] [smallint] NOT NULL,
	[UnitAmount] [decimal](18, 0) NOT NULL,
	[SessionFID] [uniqueidentifier] NOT NULL,
	[Status] [varchar](10) NULL,
	[InsertDate] [smalldatetime] NOT NULL CONSTRAINT [DF_CartLog_InsertDate]  DEFAULT (getdate()),
 CONSTRAINT [PK_CartLog] PRIMARY KEY CLUSTERED 
(
	[FID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Product]    Script Date: 11/19/2012 18:00:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [uniqueidentifier] NOT NULL,
	[ProductName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Plan] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[ProductName] ASC
) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SubPlan]    Script Date: 11/19/2012 18:01:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubPlan](
	[ID] [uniqueidentifier] NOT NULL,
	[PlanID] [uniqueidentifier] NOT NULL,
	[Year] [smallint] NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_SubPlanItem] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopLevelDomain]    Script Date: 11/19/2012 18:01:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TopLevelDomain](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [varchar](10) NOT NULL,
	[Category] [varchar](15) NULL,
	[PlanID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_TopLevelDomain] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Order]    Script Date: 11/19/2012 17:58:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[ID] [uniqueidentifier] NOT NULL,
	[SessionID] [uniqueidentifier] NOT NULL,
	[OrderAmount] [decimal](18, 0) NOT NULL,
	[Status] [smallint] NOT NULL,
	[InsertDate] [smalldatetime] NOT NULL,
	[UpdateDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NameServer]    Script Date: 11/19/2012 17:58:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NameServer](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_NameServer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItem]    Script Date: 11/19/2012 17:59:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OrderItem](
	[ID] [uniqueidentifier] NOT NULL,
	[OrderID] [uniqueidentifier] NOT NULL,
	[SubPlanID] [uniqueidentifier] NOT NULL,
	[DomainName] [varchar](50) NOT NULL,
	[Status] [smallint] NOT NULL,
	[Response] [varchar](max) NULL,
	[UpdateDate] [smalldatetime] NOT NULL,
	[EnableSsl] [bit] NOT NULL,
	[EnableMaintenance] [bit] NOT NULL,
	[InvoiceNumber] [varchar](50) NULL,
	[Description] [varchar](8000) NULL,
 CONSTRAINT [PK_OrderItem] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ConversionRate]    Script Date: 11/19/2012 17:58:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConversionRate](
	[ID] [uniqueidentifier] NOT NULL,
	[USDToRupee] [decimal](16, 2) NOT NULL,
 CONSTRAINT [PK_ConversionRate] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Plan]    Script Date: 11/19/2012 17:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Plan](
	[ID] [uniqueidentifier] NOT NULL,
	[ProductID] [uniqueidentifier] NOT NULL,
	[Sequence] [smallint] NOT NULL CONSTRAINT [DF_Plan_Sequence]  DEFAULT ((0)),
	[Name] [varchar](50) NULL,
	[CurrencyID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_SubPlan] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Currency]    Script Date: 11/19/2012 17:58:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Currency](
	[ID] [uniqueidentifier] NOT NULL,
	[CurrencyName] [varchar](10) NOT NULL,
	[CurrencySymbol] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PaymentTransactionLog]    Script Date: 11/19/2012 17:59:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PaymentTransactionLog](
	[ID] [uniqueidentifier] NOT NULL,
	[OrderID] [uniqueidentifier] NOT NULL,
	[Request] [varchar](max) NOT NULL,
	[Response] [varchar](max) NULL,
	[UpdateDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_PaymentTarnsactionLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SessionLog]    Script Date: 11/19/2012 18:00:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SessionLog](
	[ID] [uniqueidentifier] NOT NULL,
	[AspSessionID] [varchar](50) NOT NULL,
	[UserEnmailID] [varchar](50) NOT NULL,
	[UserIP] [varchar](20) NOT NULL,
	[InsertDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_SessionLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_TopLevelDomain]    Script Date: 11/19/2012 17:57:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_insert_TopLevelDomain]
    @ProductName     nvarchar(50),
    @PlanName varchar(50),
    @TopLevelDomain varchar(50),
	@CurrencyName varchar(50)
AS
BEGIN
  DECLARE @productId  uniqueidentifier
  DECLARE @planId  uniqueidentifier
  DECLARE @tldId  uniqueidentifier
  DECLARE @currencyId  uniqueidentifier
  
  
set @productId = ( select ID from product where ProductName = @ProductName)
set @currencyId = ( select ID from Currency where CurrencyName = @CurrencyName)
set @planId = (select ID from [Plan] where ProductID = @productId and [Name]=@PlanName and CurrencyID=@currencyId)
set @tldId = (select ID from [TopLevelDomain] where [Name] =@TopLevelDomain and PlanID=@planId)


IF (@tldId IS NULL)
 BEGIN
 INSERT INTO [TopLevelDomain] VALUES(NewID(),@TopLevelDomain,'',@planId)
set @tldId = (select ID from [TopLevelDomain] where [Name] =@TopLevelDomain and PlanID=@planId)
 END

UPDATE [TopLevelDomain] set PlanID=@planId  where ID = @tldId

END


select * from topLevelDomain
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_plan]    Script Date: 11/19/2012 17:57:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_insert_plan]
    @ProductName     nvarchar(50),
    @PlanSquence smallint,
    @year  smallint,
    @price decimal,
    @PlanName varchar(50),
    @CurrencyName varchar(10),
    @CurrencySymbol nvarchar(10),
    @subPlanId  uniqueidentifier
AS
BEGIN
  DECLARE @productId  uniqueidentifier
  DECLARE @planId  uniqueidentifier
  DECLARE @currencyId  uniqueidentifier
  
  
 set @productId = ( select ID from product where ProductName = @ProductName)
 IF (@productId IS NULL)
 BEGIN
 INSERT INTO Product VALUES(NewID(),@ProductName)
 set @productId =  (select ID from product where ProductName = @ProductName)
 END


 set @currencyId = ( select ID from Currency where CurrencyName = @CurrencyName)
 IF (@currencyId IS NULL)
 BEGIN
 INSERT INTO Currency VALUES(NewID(),@CurrencyName,@CurrencySymbol)
 set @currencyId = ( select ID from Currency where CurrencyName = @CurrencyName)
 END
 

 
 set @planId = (select ID from [Plan] where ProductID = @productId and Sequence=@PlanSquence and CurrencyID=@currencyId)
 IF (@planId IS NULL)
 BEGIN
 print  @productId
 INSERT INTO [Plan]VALUES (NEWID(),@productId,@PlanSquence,@PlanName,@currencyId)
 set @planId = (select ID from [Plan] where ProductID = @productId and Sequence=@PlanSquence and CurrencyID=@currencyId)
 END


 IF (@subPlanId IS NULL)
 BEGIN
 
	 set @subPlanId = (select ID from SubPlan where PlanID = @planId and [YEAR] =@year and Price = @price)
	 IF (@subPlanId IS NULL)
	 BEGIN
	 INSERT INTO [SubPlan]VALUES (NEWID(),@planId,@year,@price)
	 set @subPlanId = (select ID from SubPlan where PlanID = @planId and [YEAR] =@year and Price = @price)
	 END
END

Update SubPlan set [YEAR]=@year,Price=@price where ID=@subPlanId
  
END
GO
/****** Object:  View [dbo].[VW_Plan]    Script Date: 11/19/2012 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VW_Plan]
AS
SELECT     dbo.Product.ID AS ProductID, dbo.Product.ProductName, dbo.[Plan].ID AS PlanID, dbo.[Plan].Sequence AS PlanSequence, dbo.SubPlan.ID AS SubPlanID,
                       dbo.SubPlan.Year, dbo.SubPlan.Price, dbo.Currency.CurrencyName, dbo.Currency.CurrencySymbol, dbo.[Plan].Name AS PlanName
FROM         dbo.[Plan] INNER JOIN
                      dbo.Currency ON dbo.[Plan].CurrencyID = dbo.Currency.ID LEFT OUTER JOIN
                      dbo.SubPlan ON dbo.[Plan].ID = dbo.SubPlan.PlanID RIGHT OUTER JOIN
                      dbo.Product ON dbo.[Plan].ProductID = dbo.Product.ID
GO
/****** Object:  View [dbo].[VW_OrderPaymentProcess]    Script Date: 11/19/2012 18:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VW_OrderPaymentProcess]
AS
SELECT     dbo.PaymentTransactionLog.OrderID, dbo.PaymentTransactionLog.Request, dbo.PaymentTransactionLog.Response, 
                      dbo.PaymentTransactionLog.UpdateDate, dbo.[Order].OrderAmount, dbo.[Order].Status,
				CASE WHEN [Order].Status=1 THEN 'Saved' 
				WHEN [Order].Status=2 THEN 'SentToPaymentProcessor' 
				WHEN [Order].Status=3 THEN 'PaymentVerified' 
				WHEN [Order].Status=-2 THEN 'InvalidPayment'
			    WHEN [Order].Status=-1 THEN 'GenericError'
				ELSE 'None' END AS OrderStatus

FROM         dbo.[Order] INNER JOIN
                      dbo.PaymentTransactionLog ON dbo.[Order].ID = dbo.PaymentTransactionLog.OrderID
GO
/****** Object:  View [dbo].[VW_Order]    Script Date: 11/19/2012 18:01:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VW_Order]
AS
SELECT     dbo.[Order].OrderAmount, dbo.[Order].ID AS OrderID, dbo.[Order].Status AS OrderStatus, dbo.OrderItem.DomainName, 
                      dbo.OrderItem.Status AS ItemStatus, dbo.OrderItem.Response, dbo.VW_Plan.ProductName, dbo.VW_Plan.PlanSequence, dbo.VW_Plan.Year, 
                      dbo.VW_Plan.Price, dbo.OrderItem.EnableSsl, dbo.OrderItem.EnableMaintenance
FROM         dbo.VW_Plan INNER JOIN
                      dbo.OrderItem ON dbo.VW_Plan.SubPlanID = dbo.OrderItem.SubPlanID INNER JOIN
                      dbo.[Order] ON dbo.OrderItem.OrderID = dbo.[Order].ID
GO
