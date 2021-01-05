alter procedure dbo.spReturnUser(@Firstname nvarchar(200),
                                     @LastName nvarchar(100),
                                     @Password nvarchar(max) output
                                     ) as

    BEGIN
        set nocount on;
        select @Password = Password
        from Users
        where FirstName = @Firstname
          and LastName = @LastName;

    END;
go

