DECLARE @LibraryId UNIQUEIDENTIFIER = NEWID();
DECLARE @RoleId UNIQUEIDENTIFIER = NEWID();
DECLARE @UserId UNIQUEIDENTIFIER = NEWID();

INSERT INTO [LibraryManagerDb].[dbo].[Library]
(Id, Name, Street, Number, District, City, ZipCode, State, CountryCode, Observation, Latitude, Longitude, CreateDate, UpdateDate, IsDeleted)
VALUES (
    @LibraryId, 'LibraryTest', 'Street', '123', 'District', 'City', 'Zipcode', 'State', 'BR',
    'Observation', 0, 0, GETDATE(), GETDATE(), 0
);

INSERT INTO [LibraryManagerDb].[dbo].[Role]
(Id, CreateDate, IsDeleted, Name, RoleType, UpdateDate)
VALUES (
    @RoleId, GETDATE(), 0, 'Admin', 1, GETDATE()
);

INSERT INTO [LibraryManagerDb].[dbo].[User]
(Id, FirstName, LastName, Email, PasswordHash, LastLogin, LibraryId, CreateDate, UpdateDate, IsDeleted)
VALUES (
    @UserId, 'John', 'Doe', 'john.doe@email.com',
    '0BEED112ECA4181A4CFC4915749C8DB32947191328A5FF9F4C16325A1B55CED3-E0C0701F8513F02F5EACB009EBE49456',
    GETDATE(), @LibraryId, GETDATE(), GETDATE(), 0
);

INSERT INTO [LibraryManagerDb].[dbo].[UserRole]
(UserId, RoleId)
VALUES (@UserId, @RoleId);

select * from [LibraryManagerDb].[dbo].[Library]