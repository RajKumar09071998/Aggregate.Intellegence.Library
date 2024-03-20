CREATE TABLE [dbo].[Book] (
    BookId BIGINT PRIMARY KEY Identity(1,1) not null,
    Title NVARCHAR(255) null,
    Author NVARCHAR(255) null,
    Publisher NVARCHAR(255) null,
    PublishYear INT null,
    Genre NVARCHAR(100) null,
    [Language] NVARCHAR(50) null,
    [PageCount] INT null,
    [Description] NVARCHAR(MAX) null,
    [CreatedOn] datetimeoffset null,
	[CreatedBy] bigint null,
	[ModifiedOn] datetimeoffset null,
	[ModifiedBy] bigint null,
	[IsActive] bit null
);