create procedure dbo.spCreateUserAndPasswordWitReturn(@Password nvarchar(max),
                                                      @Salt nvarchar(max),
                                                      @FirstName nvarchar(200),
                                                      @LastName nvarchar(100),
                                                      @id int OUTPUT) as
BEGIN
    SET NOCOUNT ON;
    insert into Users (Password, Salt, FirstName, LastName) values (@Password, @Salt, @FirstName, @LastName);

    select @id = id
    from Users
    where FirstName = @FirstName;
END;
go

alter procedure dbo.spReturnUser(@Firstname nvarchar(200),
                                  @LastName nvarchar(100),
                                  @Password nvarchar(max) output,
                                  @Salt nvarchar(max) output) as

BEGIN
    set nocount on;
    select @Password = Password, @Salt = Salt
    from Users
    where FirstName = @Firstname
      and LastName = @LastName;

END;


