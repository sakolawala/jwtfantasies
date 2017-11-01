USE [OAuth]
GO

INSERT INTO [dbo].[ApiResources]
           ([Description]
           ,[DisplayName]
           ,[Enabled]
           ,[Name])
     SELECT 'BranchServices', 'branches', 1, 'branches'
	 UNION ALL
	 SELECT 'BranchSettingServices', 'branchsettings', 1, 'branchsettings'
	 UNION ALL
	 SELECT 'BranchPrinterServices', 'branchprinters', 1, 'branchprinters'
GO


