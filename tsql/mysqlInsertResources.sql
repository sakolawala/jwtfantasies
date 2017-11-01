USE oauth;

INSERT INTO `oauth`.`ApiResources`
           (`Description`
           ,`DisplayName`
           ,`Enabled`
           ,`Name`)
     SELECT 'BranchServices', 'branches', 1, 'branches'
	 UNION ALL
	 SELECT 'BranchSettingServices', 'branchsettings', 1, 'branchsettings'
	 UNION ALL
	 SELECT 'BranchPrinterServices', 'branchprinters', 1, 'branchprinters';

INSERT INTO `oauth`.`ApiScopes`
           (`ApiResourceId`
           ,`Description`
           ,`DisplayName`
           ,`Emphasize`
           ,`Name`
           ,`Required`
           ,`ShowInDiscoveryDocument`)
    Select 3, null, 'branches', 0, 'branches', 0, 1
	UNION ALL
	Select 4, null, 'branchsettings', 0, 'branchsettings', 0, 1
	UNION ALL
	Select 5, null, 'branchprinters', 0, 'branchprinters', 0, 1;

