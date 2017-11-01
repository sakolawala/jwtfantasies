USE oauth;
-- Declare the variable to be used.

SET @ClientIdentifier = 'cexservices';
SET @ClientLongName = @ClientIdentifier;
SET @ClientDescription = 'cexservices services account client';
SET @ClientSecret = 'z6I6EkoCd8b8HOCAOsBqLYXGo3/7iIVHC5cO8g98PDM=';

-- SET @ClientIdentifier = 'testclient';
-- SET @ClientLongName = @ClientIdentifier;
-- SET @ClientDescription = 'test client use in testing automation';
-- SET @ClientSecret = 'g9nRa3K4p0kJoqLj2G0iE/3ra5V44tbp7fzLCFChnH8=';

INSERT INTO `oauth`.`clients`
			(`AbsoluteRefreshTokenLifetime`,
			`AccessTokenLifetime`,
			`AccessTokenType`,
			`AllowAccessTokensViaBrowser`,
			`AllowOfflineAccess`,
			`AllowPlainTextPkce`,
			`AllowRememberConsent`,
			`AlwaysIncludeUserClaimsInIdToken`,
			`AlwaysSendClientClaims`,
			`AuthorizationCodeLifetime`,
			`BackChannelLogoutSessionRequired`,
			`BackChannelLogoutUri`,
			`ClientId`,
			`ClientName`,
			`ClientUri`,
			`ConsentLifetime`,
			`Description`,
			`EnableLocalLogin`,
			`Enabled`,
			`FrontChannelLogoutSessionRequired`,
			`FrontChannelLogoutUri`,
			`IdentityTokenLifetime`,
			`IncludeJwtId`,
			`LogoUri`,
			`NormalizedClientId`,
			`PrefixClientClaims`,
			`ProtocolType`,
			`RefreshTokenExpiration`,
			`RefreshTokenUsage`,
			`RequireClientSecret`,
			`RequireConsent`,
			`RequirePkce`,
			`SlidingRefreshTokenLifetime`,
			`UpdateAccessTokenClaimsOnRefresh`)
     VALUES
           (2592000,  /*AbsoluteRefreshTokenLifetime*/
			3600,  /*AccessTokenLifetime*/
			0,  /*AccessTokenType*/
			0,  /*AllowAccessTokensViaBrowser*/
			1,  /*AllowOfflineAccess*/
			0,  /*AllowPlainTextPkce*/
			1,  /*AllowRememberConsent*/
			0,  /*AlwaysIncludeUserClaimsInIdToken*/
			0,  /*AlwaysSendClientClaims*/
			300,  /*AuthorizationCodeLifetime*/
			1,  /*BackChannelLogoutSessionRequired*/
			NULL,  /*BackChannelLogoutUri*/
			@ClientIdentifier,  /*ClientId*/
			@ClientLongName,  /*ClientName*/
			NULL,  /*ClientUri*/
			NULL,  /*ConsentLifetime*/
			@ClientDescription,  /*Description*/
			1,  /*EnableLocalLogin*/
			1,  /*Enabled*/
			1,  /*FrontChannelLogoutSessionRequired*/
			NULL,  /*FrontChannelLogoutUri*/
			300,  /*IdentityTokenLifetime*/
			0,  /*IncludeJwtId*/
			NULL,  /*LogoUri*/
			NULL,  /*NormalizedClientId*/
			1,  /*PrefixClientClaims, If set, the prefix client claim types will be prefixed with. Defaults to client_.*/
			'oidc',  /*ProtocolType*/
			1,  /*RefreshTokenExpiration*/
			1,  /*RefreshTokenUsage*/
			1,  /*RequireClientSecret*/
			1,  /*RequireConsent*/
			0,  /*RequirePkce*/
			1296000,  /*SlidingRefreshTokenLifetime*/
			0  /*UpdateAccessTokenClaimsOnRefresh*/
			);

SET @ClientId = LAST_INSERT_ID();

INSERT INTO `oauth`.`ClientGrantTypes`
           (`ClientId`
           ,`GrantType`)
     SELECT @ClientId, 'client_credentials'
	 UNION ALL
	 SELECT @ClientId, 'password';

INSERT INTO `oauth`.`ClientScopes`
           (`ClientId`
           ,`Scope`)
SELECT @ClientId, 'branches'
UNION ALL
SELECT @ClientId, 'branchsettings'
UNION ALL
SELECT @ClientId, 'branchprinters';

INSERT INTO `oauth`.`ClientSecrets`
           (`ClientId`
           ,`Description`
           ,`Expiration`
           ,`Type`
           ,`Value`)
     VALUES (
           @ClientId,
           null,
           null,
           'SharedSecret',
           @ClientSecret);