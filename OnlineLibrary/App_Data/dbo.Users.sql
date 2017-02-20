CREATE TABLE [dbo].[Users] (
    [UserID] INT           IDENTITY (1, 1) NOT NULL,
    [Name]   NVARCHAR (50) NOT NULL,
    [Email]  NVARCHAR (50) NOT NULL,
    [Password] NVARCHAR(50) NOT NULL, 
    PRIMARY KEY CLUSTERED ([UserID] ASC),
    UNIQUE NONCLUSTERED ([Email] ASC)
);

