-- Триггер для автоматического обновления статуса заказа при изменении связанного мотоцикла:
CREATE TRIGGER UpdateOrderStatus
ON Motorcycle
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [Order]
    SET [Status] = 'Updated'
    FROM [Order] o
    INNER JOIN inserted i ON o.Motorcycle_Id = i.Motorcycle_Id;
END;

--Триггер для логирования изменений в таблице "User":
CREATE TRIGGER LogUserChanges
ON [User]
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Journal (Date_of_action, [Action], [User_Id])
    SELECT GETDATE(), 'User modification', User_Id
    FROM inserted;

    INSERT INTO Journal (Date_of_action, [Action], [User_Id])
    SELECT GETDATE(), 'User deletion', User_Id
    FROM deleted;
END;

--Триггер для автоматического обновления даты заказа при изменении даты доставки:
CREATE TRIGGER UpdateOrderDate
ON [Order]
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [Order]
    SET Date_of_order = GETDATE()
    FROM [Order] o
    INNER JOIN inserted i ON o.Order_Id = i.Order_Id
    WHERE i.Date_of_delivery IS NOT NULL;
END;