delete from Product
delete from [Plan]
delete from SubPlan
delete from TopLevelDomain


EXECUTE sp_insert_plan   'linux_hosting_plan'  ,1  ,1  ,100
EXECUTE sp_insert_plan   'linux_hosting_plan'  ,2  ,2  ,100

EXECUTE sp_insert_plan   'linux_hosting_plan'  ,2  ,1  ,100
EXECUTE sp_insert_plan   'linux_hosting_plan'  ,2  ,2  ,100

EXECUTE sp_insert_plan   'linux_hosting_plan'  ,3  ,1  ,100
EXECUTE sp_insert_plan   'linux_hosting_plan'  ,3  ,2  ,100

EXECUTE sp_insert_plan   'linux_hosting_plan'  ,4  ,1  ,100
EXECUTE sp_insert_plan   'linux_hosting_plan'  ,4  ,2  ,100



EXECUTE sp_insert_plan   'windows_hosting_plan'  ,1  ,1  ,100
EXECUTE sp_insert_plan   'windows_hosting_plan'  ,2  ,2  ,100

EXECUTE sp_insert_plan   'windows_hosting_plan'  ,2  ,1  ,100
EXECUTE sp_insert_plan   'windows_hosting_plan'  ,2  ,2  ,100

EXECUTE sp_insert_plan   'windows_hosting_plan'  ,3  ,1  ,100
EXECUTE sp_insert_plan   'windows_hosting_plan'  ,3  ,2  ,100

EXECUTE sp_insert_plan   'windows_hosting_plan'  ,4  ,1  ,100
EXECUTE sp_insert_plan   'windows_hosting_plan'  ,4  ,2  ,100


EXECUTE sp_insert_plan   'domain_registration'  ,1 ,1,150
EXECUTE sp_insert_plan   'domain_registration'  ,1 ,2,250
EXECUTE sp_insert_plan   'domain_registration'  ,1 ,3,300

EXECUTE sp_insert_plan   'domain_registration'  ,2 ,1,250
EXECUTE sp_insert_plan   'domain_registration'  ,2 ,2,350
EXECUTE sp_insert_plan   'domain_registration'  ,2 ,3,500


DECLARE @productId  uniqueidentifier
DECLARE @planId  uniqueidentifier
DECLARE @subPlanId  uniqueidentifier
DECLARE   @ProductName     nvarchar(50)
DECLARE @PlanSquence smallint
set @ProductName = 'domain_registration'
set @PlanSquence =1

set @productId = ( select ID from product where ProductName = @ProductName)
set @planId = (select ID from [Plan] where ProductID = @productId and Sequence=@PlanSquence)

Insert Into TopLevelDomain values(NewID(),'.com','recommend',@planId)
Insert Into TopLevelDomain values(NewID(),'.net','recommend',@planId)
Insert Into TopLevelDomain values(NewID(),'.org','recommend',@planId)

Insert Into TopLevelDomain values(NewID(),'.oc','',@planId)
Insert Into TopLevelDomain values(NewID(),'.in','',@planId)

set @PlanSquence =2
set @planId = (select ID from [Plan] where ProductID = @productId and Sequence=@PlanSquence)

Insert Into TopLevelDomain values(NewID(),'.info','sidebar',@planId)
Insert Into TopLevelDomain values(NewID(),'.biz','sidebar',@planId)
Insert Into TopLevelDomain values(NewID(),'.tv','sidebar',@planId)
Insert Into TopLevelDomain values(NewID(),'.name','sidebar',@planId)




Select * from VW_Plan