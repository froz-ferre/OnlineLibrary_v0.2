CREATE TABLE [dbo].[History]
(
	[HistoryID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserID] INT NOT NULL FOREIGN KEY REFERENCES dbo.Users(UserID), 
    [BookID] INT NOT NULL FOREIGN KEY REFERENCES dbo.Books(BookID), 
    [TakeDate] DATE NOT NULL, 
    [BackDate] DATE NOT NULL,

)
