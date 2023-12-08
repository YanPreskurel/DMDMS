--Пример изменения мотоцикла для срабатывания триггера UpdateOrderStatus:

-- Изменение мотоцикла
UPDATE Motorcycle
SET Color = 'Blue'
WHERE Motorcycle_Id = 1;
-- Этот запрос вызовет срабатывание триггера UpdateOrderStatus

--Пример удаления пользователя для срабатывания триггера LogUserChanges:

-- Удаление пользователя
DELETE FROM [User]
WHERE User_Id = 2;
-- Этот запрос вызовет срабатывание триггера LogUserChanges


--Вызов процедуры GetOrdersByClient для получения списка заказов для конкретного клиента:
DECLARE @ClientId INT = 2; -- Замените на конкретный идентификатор клиента
EXEC GetOrdersByClient @ClientId;

--Вызов процедуры AddNewUser для добавления нового пользователя:

DECLARE @Login NVARCHAR(20) = 'newuser2';
DECLARE @Password NVARCHAR(20) = 'newpassword2';
DECLARE @Name NVARCHAR(50) = 'Jane';
DECLARE @Surname NVARCHAR(50) = 'Doe';
DECLARE @Status NVARCHAR(10) = 'Active';

EXEC AddNewUser @Login, @Password, @Name, @Surname, @Status;

--Вызов процедуры GetMotorcyclesByModel для получения списка мотоциклов определенной модели:
DECLARE @ModelName NVARCHAR(50) = 'ExampleModel';
EXEC GetMotorcyclesByModel @ModelName;

--Вызов процедуры GetPendingOrders для получения списка заказов, ожидающих доставки:
EXEC GetPendingOrders;