Use OAuth
GO

Select * from Clients
Where ClientId = 'cexservices'

Select * from ClientClaims --Nothing
Select * from ClientCorsOrigins where ClientId = 4 --Nothing for roclient
Select * from ClientGrantTypes where ClientId = 4 --Password for roclient
Select * from ClientIdPRestrictions --Nothing
Select * from ClientPostLogoutRedirectUris where ClientId = 4  --Nothing for roclient
Select * from ClientProperties where ClientId = 4  --Nothing for roclient
Select * from ClientRedirectUris where ClientId = 4 --Nothing for roclient
Select * from ClientScopes where ClientId = 4 --api2.read_only, api1, custom.profile, openid
Select * from ClientSecrets where ClientId = 4 --Secret

Select * from ApiResources
Select * from ApiScopes
Select * from ApiClaims --Nothing for 1
Select * from ApiScopeClaims --Nothing
Select * from ApiSecrets