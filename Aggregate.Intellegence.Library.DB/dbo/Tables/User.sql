CREATE TABLE [dbo].[User]
(
	[Id] bigint NOT NULL PRIMARY KEY identity(1,1),
	[FirstName] varchar(max) null,
	[LastName] varchar(max) null,
	[Email] varchar(max) not null,
	[Phone] varchar(max) not null,
	[RoleId] bigint not null,
	[PasswordHash] varchar(max),
	[PasswordSalt] varchar(max),
	[CreatedOn] datetimeoffset null,
	[CreatedBy] bigint null,
	[ModifiedOn] datetimeoffset null,
	[ModifiedBy] bigint null,
	[IsActive] bit null,
	FOREIGN KEY (RoleId) REFERENCES Role(Id)
)
