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
    '350000.916DA6D25F25C226A4C90A2417924ACB.C6B8C9007BB5217CD5E33EE54A3139C6A117DBC659FE24FC5488ED32F143E67A',
    GETDATE(), @LibraryId, GETDATE(), GETDATE(), 0
);

INSERT INTO [LibraryManagerDb].[dbo].[UserRole]
(UserId, RoleId)
VALUES (@UserId, @RoleId);