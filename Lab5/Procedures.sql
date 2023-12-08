--Процедура для получения списка заказов для конкретного клиента:
CREATE PROCEDURE GetOrdersByClient
    @Client_Id INT
AS
BEGIN
    SELECT *
    FROM [Order]
    WHERE Client_Id = @Client_Id;
END;

--Процедура для добавления нового пользователя с заданными параметрами:
CREATE PROCEDURE AddNewUser
    @Login NVARCHAR(20),
    @Password NVARCHAR(20),
    @Name NVARCHAR(50),
    @Surname NVARCHAR(50),
    @Status NVARCHAR(10)
AS
BEGIN
    INSERT INTO [User] (Login, Password, Name, Surname, [Status])
    VALUES (@Login, @Password, @Name, @Surname, @Status);
END;

--Процедура для получения списка мотоциклов определенной модели:
CREATE PROCEDURE GetMotorcyclesByModel
    @ModelName NVARCHAR(50)
AS
BEGIN
    SELECT *
    FROM Motorcycle m
    INNER JOIN Model mod ON m.Model_Id = mod.Model_Id
    WHERE mod.[Name] = @ModelName;
END;

--Процедура для получения списка всех заказов, ожидающих доставки:
CREATE PROCEDURE GetPendingOrders
AS
BEGIN
    SELECT *
    FROM [Order]
    WHERE [Status] = 'Pending';
END;