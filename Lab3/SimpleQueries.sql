--Выборка заказов для конкретного клиента
SELECT * FROM [Order] WHERE Client_Id = 6;
SELECT * FROM [Order] WHERE Client_Id = 7;

--Обновление информации о пользователе
UPDATE [User] SET [Name] = 'UpdatedNameAdmin' WHERE [User_Id] =1;

--Выборка всех администраторов и связанных с ними пользователей
SELECT [User].*, [Admin].Admin_Id
FROM [User]
JOIN [Admin] ON [User].User_Id = [Admin].User_Id;

--Выборка всех заказов и связанных с ними клиентов и менеджеров
SELECT [Order].*, Client.Client_Id AS ClientName, Manager.Manager_Id AS ManagerName
FROM [Order]
JOIN Client ON [Order].Client_Id = Client.Client_Id
JOIN Manager ON [Order].Manager_Id = Manager.Manager_Id;

--Обновление информации о заказе
UPDATE [Order] SET Date_of_delivery = '2023-01-20' WHERE Order_Id = 1;

--Выборка всех поставщиков и связанных с ними организаций
SELECT Supplier.*, Organization.[Name] AS OrganizationName
FROM Supplier
JOIN Organization ON Supplier.Supplier_Id = Organization.Supplier_Id;

--Выборка всех мотоциклов и их характеристик
SELECT Motorcycle.*, Model.Characteristics AS ModelCharacteristics
FROM Motorcycle
JOIN Model ON Motorcycle.Model_Id = Model.Model_Id;
