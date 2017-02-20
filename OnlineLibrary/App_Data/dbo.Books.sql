CREATE TABLE [dbo].[Books]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NVARCHAR(50) NOT NULL, 
    [Authors] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(500) NULL, 
    [Availible] SMALLINT NOT NULL DEFAULT 1  
)
