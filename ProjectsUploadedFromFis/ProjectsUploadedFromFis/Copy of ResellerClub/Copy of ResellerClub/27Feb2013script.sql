USE [InfoWebTestDB]
GO
/****** Object:  User [InfoWebTestUser]    Script Date: 02/27/2013 14:46:27 ******/
EXEC dbo.sp_grantdbaccess @loginame = N'InfoWebTestUser', @name_in_db = N'InfoWebTestUser'
GO
/****** Object:  Table [dbo].[RequestLog]    Script Date: 02/27/2013 14:44:13 ******/
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
/****** Object:  Table [dbo].[CartLog]    Script Date: 02/27/2013 14:41:09 ******/
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
/****** Object:  Table [dbo].[Product]    Script Date: 02/27/2013 14:43:53 ******/
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
/****** Object:  Table [dbo].[SubPlan]    Script Date: 02/27/2013 14:45:03 ******/
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
/****** Object:  Table [dbo].[TopLevelDomain]    Script Date: 02/27/2013 14:46:26 ******/
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
/****** Object:  Table [dbo].[Order]    Script Date: 02/27/2013 14:42:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Order](
	[ID] [uniqueidentifier] NOT NULL,
	[SessionID] [uniqueidentifier] NOT NULL,
	[OrderAmount] [decimal](18, 0) NOT NULL,
	[Status] [smallint] NOT NULL,
	[InsertDate] [smalldatetime] NOT NULL,
	[UpdateDate] [smalldatetime] NOT NULL,
	[OrderNumber] [numeric](18, 0) IDENTITY(5000,1) NOT NULL,
	[PayemtReferenceNumber] [varchar](1000) NULL,
	[PaymentMode] [varchar](50) NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NameServer]    Script Date: 02/27/2013 14:42:08 ******/
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
/****** Object:  Table [dbo].[OrderItem]    Script Date: 02/27/2013 14:43:04 ******/
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
/****** Object:  Table [dbo].[Table_1]    Script Date: 02/27/2013 14:45:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Table_1](
	[count] [numeric](18, 0) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConversionRate]    Script Date: 02/27/2013 14:41:21 ******/
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
/****** Object:  Table [dbo].[Plan]    Script Date: 02/27/2013 14:43:40 ******/
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
/****** Object:  Table [dbo].[Currency]    Script Date: 02/27/2013 14:41:33 ******/
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
/****** Object:  Table [dbo].[SessionLog]    Script Date: 02/27/2013 14:44:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SessionLog](
	[ID] [uniqueidentifier] NOT NULL,
	[AspSessionID] [varchar](50) NOT NULL,
	[UserEmailID] [varchar](50) NOT NULL,
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
/****** Object:  Table [dbo].[State]    Script Date: 02/27/2013 14:44:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[State](
	[ID] [uniqueidentifier] NOT NULL,
	[CountryCode] [nchar](2) NOT NULL,
	[State] [varchar](100) NOT NULL,
 CONSTRAINT [PK_State] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH FILLFACTOR = 85 ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE CLUSTERED INDEX [IX_State] ON [dbo].[State] 
(
	[CountryCode] ASC
)WITH FILLFACTOR = 85 ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ErrorLog]    Script Date: 02/27/2013 14:41:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ErrorLog](
	[f_id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[SessionFID] [uniqueidentifier] NULL,
	[UserIP] [varchar](20) NULL,
	[ErrorMessage] [varchar](5000) NOT NULL,
	[StackTrace] [varchar](7000) NOT NULL,
	[InsertDate] [smalldatetime] NOT NULL,
	[Url] [varchar](1200) NULL,
	[AdditionalInfo] [varchar](7000) NULL,
 CONSTRAINT [PK_ErrorLog] PRIMARY KEY NONCLUSTERED 
(
	[f_id] ASC
)WITH FILLFACTOR = 100 ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AdminQuery]    Script Date: 02/27/2013 14:40:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AdminQuery](
	[ID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Query] [varchar](7000) NOT NULL,
 CONSTRAINT [PK_AdminQuery] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH FILLFACTOR = 85 ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PaymentTransactionLog]    Script Date: 02/27/2013 14:43:21 ******/
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
/****** Object:  Table [dbo].[TicketConversation]    Script Date: 02/27/2013 14:46:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TicketConversation](
	[FID] [uniqueidentifier] NOT NULL,
	[Message] [varchar](max) NULL,
	[Attachment] [varchar](1000) NULL,
	[TicketID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[Staff] [bit] NOT NULL,
	[InsertDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TicketConversation] PRIMARY KEY CLUSTERED 
(
	[FID] ASC
)WITH FILLFACTOR = 85 ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 02/27/2013 14:45:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Ticket](
	[FID] [uniqueidentifier] NOT NULL,
	[TicketNumber] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Status] [char](2) NULL,
	[Priority] [char](2) NULL,
	[Subject] [varchar](2000) NULL,
	[InsertDate] [datetime] NULL,
	[LastUpdate] [datetime] NULL,
	[Type] [char](2) NULL,
	[UserEmail] [varchar](100) NULL,
	[Domain] [varchar](200) NULL,
	[Department] [char](4) NULL,
	[ContactNumber] [varchar](20) NULL,
	[LastReplier] [varchar](50) NULL,
	[UserName] [varchar](50) NULL,
 CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED 
(
	[FID] ASC
)WITH FILLFACTOR = 85 ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_TopLevelDomain]    Script Date: 02/27/2013 14:40:26 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_insert_plan]    Script Date: 02/27/2013 14:40:20 ******/
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
/****** Object:  View [dbo].[VW_Plan]    Script Date: 02/27/2013 14:46:48 ******/
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
/****** Object:  View [dbo].[v_Order]    Script Date: 02/27/2013 14:46:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_Order]
AS
select ID,OrderNumber,SessionID,OrderAmount,InsertDate,UpdateDate,PaymentMode,PayemtReferenceNumber,
	CASE WHEN [Order].Status=1 THEN 'Saved' 
				WHEN [Order].Status=2 THEN 'SentToPaymentProcessor' 
				WHEN [Order].Status=3 THEN 'PaymentVerified' 
				WHEN [Order].Status=4 THEN 'UnProcessed' 
				WHEN [Order].Status=5 THEN 'Processed' 
				WHEN [Order].Status=101 THEN 'PaymentAwaited' 
				WHEN [Order].Status=102 THEN 'UpdatedTranscationNumber' 
				WHEN [Order].Status=-2 THEN 'InvalidPayment'
			    WHEN [Order].Status=-1 THEN 'GenericError'
				ELSE 'None' END AS Status
FROM [Order]
GO
/****** Object:  StoredProcedure [dbo].[sp_update_order_Payemt_Tran_Number]    Script Date: 02/27/2013 14:40:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
Create PROCEDURE [dbo].[sp_update_order_Payemt_Tran_Number]
    @OrderId     uniqueidentifier,
	@paymentTranNumber varchar(50)
AS
BEGIN
		
		Update [order] set PayemtReferenceNumber= @paymentTranNumber where id=@OrderId
END
GO
/****** Object:  StoredProcedure [dbo].[sp_update_order_status]    Script Date: 02/27/2013 14:40:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
Create PROCEDURE [dbo].[sp_update_order_status]
    @OrderId     uniqueidentifier
AS
BEGIN

	DECLARE @unprocessedItem  numeric
	DECLARE @orderUnprocessed numeric
	DECLARE @orderProcessed numeric
	DECLARE @orderItemProcessed numeric
	DECLARE @orderPaymentVerified numeric
	DECLARE @orderStatus numeric
	 
	Set @orderPaymentVerified =3
	set @orderItemProcessed =3
	Set @orderUnprocessed =4 
	Set @orderProcessed =5 
	Set @orderStatus= @orderProcessed

	IF EXISTS(select * from [order] where id = @OrderId and [status] >= @orderPaymentVerified)
	BEGIN
		IF EXISTS (select * from orderItem where OrderId = @OrderId and status <> @orderItemProcessed)
		BEGIN
			Set @orderStatus = @orderUnprocessed
		END
		
		Update [order] set [status]= @orderStatus where id=@OrderId

	END

END
GO
/****** Object:  View [dbo].[v_OrderItem]    Script Date: 02/27/2013 14:46:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_OrderItem]
AS
select  ID,OrderID,SubPlanID,DomainName,Response,EnableSsl,EnableMaintenance,InvoiceNumber,[Description],UpdateDate,
CASE WHEN dbo.OrderItem.Status=1 THEN 'Saved' 
				WHEN dbo.OrderItem.Status=3 THEN 'Processed' 
			    WHEN dbo.OrderItem.Status=-1 THEN 'Failed'
				ELSE 'Unknown' END AS Status
from dbo.[OrderItem]
GO
/****** Object:  View [dbo].[VW_Order]    Script Date: 02/27/2013 14:46:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VW_Order]
AS
SELECT     v_order.OrderAmount, v_order.ID AS OrderID,v_order.SessionID AS SessionID, v_order.Status AS OrderStatus, v_orderItem.DomainName, 
                      v_orderItem.Status AS ItemStatus, v_orderItem.Response, dbo.VW_Plan.ProductName,v_orderItem.SubPlanID, dbo.VW_Plan.PlanSequence, dbo.VW_Plan.Year, 
                      dbo.VW_Plan.Price, v_orderItem.EnableSsl, v_orderItem.EnableMaintenance,v_orderItem.UpdateDate as ItemUpdateDate
FROM         dbo.VW_Plan INNER JOIN
                      v_orderItem ON dbo.VW_Plan.SubPlanID = v_orderItem.SubPlanID INNER JOIN
                      v_order ON v_orderItem.OrderID = v_order.ID
GO
/****** Object:  View [dbo].[VW_OrderDetail]    Script Date: 02/27/2013 14:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VW_OrderDetail]
AS
SELECT   v_orderItem.ID as OrderItemID, v_orderItem.OrderID as OrderID , v_orderItem.DomainName, 
                      v_orderItem.Status , v_orderItem.Response, dbo.VW_Plan.ProductName,v_orderItem.SubPlanID, dbo.VW_Plan.PlanSequence, dbo.VW_Plan.Year, 
                      dbo.VW_Plan.Price, v_orderItem.EnableSsl, v_orderItem.EnableMaintenance,v_orderItem.UpdateDate,InvoiceNumber 
FROM         dbo.VW_Plan INNER JOIN
                      v_orderItem ON dbo.VW_Plan.SubPlanID = v_orderItem.SubPlanID
GO
/****** Object:  View [dbo].[VW_OrderPaymentProcess]    Script Date: 02/27/2013 14:46:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VW_OrderPaymentProcess]
AS
SELECT     dbo.PaymentTransactionLog.OrderID, dbo.PaymentTransactionLog.Request, dbo.PaymentTransactionLog.Response, 
                      dbo.PaymentTransactionLog.UpdateDate, v_order.OrderAmount, v_order.Status AS OrderStatus
		
FROM         v_order INNER JOIN
                      dbo.PaymentTransactionLog ON v_order.ID = dbo.PaymentTransactionLog.OrderID
GO
/****** Object:  View [dbo].[vw_UserOrder]    Script Date: 02/27/2013 14:46:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_UserOrder]
AS
select v_order.ID as OrderID, v_order.OrderNumber as OrderNumber,count(v_orderItem.id)as ItemCount, UserEmailID,v_order.InsertDate,v_order.status ,v_order.PaymentMode,v_order.PayemtReferenceNumber from v_order inner join v_orderItem on v_order.id= v_orderItem.orderId
inner join sessionLog on v_order.sessionId = sessionLog.id 
group by v_order.ID,v_order.OrderNumber,UserEmailID,v_order.InsertDate,v_order.status,v_order.PaymentMode,v_order.PayemtReferenceNumber
GO
