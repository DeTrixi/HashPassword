create procedure dbo.spCreateUserAndPassword(@Password nvarchar(max),
                                             @Salt nvarchar(max),
                                             @FirstName nvarchar(200),
                                             @LastName nvarchar(100)) as
BEGIN
    SET NOCOUNT ON;
    insert into Users (Password, Salt, FirstName, LastName) values (@Password, @Salt, @FirstName, @LastName);
END;
go

exec dbo.spCreateUserAndPassword @Password = '', @Salt = '', @FirstName = ' ', @LastName = ''