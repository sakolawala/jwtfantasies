Select * from Clients
Where ClientId = 'roclient'

Select * from ClientClaims --Nothing
Select * from ClientCorsOrigins --Nothing for roclient
Select * from ClientGrantTypes where ClientId = 4 --Password for roclient
Select * from ClientIdPRestrictions --Nothing
Select * from ClientPostLogoutRedirectUris where ClientId = 4  --Nothing for roclient
Select * from ClientProperties where ClientId = 4  --Nothing for roclient
Select * from ClientRedirectUris where ClientId = 4 --Nothing for roclient
Select * from ClientScopes where ClientId = 4 --api2.read_only, api1, custom.profile, openid
Select * from ClientSecrets where ClientId = 4 --Secret