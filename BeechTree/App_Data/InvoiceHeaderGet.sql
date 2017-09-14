USE [Eagle_PmData]
GO

DROP PROCEDURE [dbo].[InvoiceHeaderGet]
GO

CREATE PROCEDURE [dbo].[InvoiceHeaderGet] @JobNumber Varchar(20)

AS

/*
SELECT * FROM PMJOBEQUIP WHERE SHIFTDATE = '8/23/2017' AND JOBNO = '040875-072-100' AND SHIFTNO = 1ORDER BY ISPACKAGE DESC, ITEM
SELECT * FROM PMJOBSHIFT WHERE SHIFTDATE = '8/23/2017' AND SHIFTNO = 1 AND JOBNO='040875-072-100'
SELECT * FROM eagle..JCJOBS01 WHERE JOBNO='040875-072-100'

exec eagle_pmdata..InvoiceHeaderGet '040875-072-100'


*/


SELECT s.JobNo as JobNumber, s.ShiftPo As PurchaseOrderNumber
, c.CUSTNO as CompanyNumber, c.CCOMPANY as CompanyName, c.CADDR1 as Address1, c.CADDR2 as Address2, CCITY as City, CSTATE as [State], CZIP As Zip
, c.DESCRIP As ShipToCompanyName, c.csite As ShipToFirstName, c.PNAME As ShipToLastName  

, 'Net 30' as terms

From PmJobShift s
Inner Join eagle..JcJobs01 c on s.jobno = c.jobno 
Where s.Jobno = @JobNumber

