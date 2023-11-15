-- Вставка тестовых данных в таблицу [User]
INSERT INTO [User] ([Login], [Password], [Name], Surname, [Status])
VALUES 
    ('admin', 'adminpassword', 'Admin', 'Adminovich', 'Admin'),
    ('manager1', 'manager1password', 'Manager1', 'Smith', 'Manager'),
    ('manager2', 'manager2password', 'Manager2', 'Johnson', 'Manager'),
    ('manager3', 'manager3password', 'Manager3', 'Williams', 'Manager'),
    ('manager4', 'manager4password', 'Manager4', 'Jones', 'Manager'),
    ('client1', 'client1password', 'Client1', 'Brown', 'Client'),
    ('client2', 'client2password', 'Client2', 'Davis', 'Client'),
    ('client3', 'client3password', 'Client3', 'Miller', 'Client'),
    ('client4', 'client4password', 'Client4', 'Wilson', 'Client'),
    ('client5', 'client5password', 'Client5', 'Moore', 'Client'),
    ('client6', 'client6password', 'Client6', 'Taylor', 'Client'),
    ('client7', 'client7password', 'Client7', 'Anderson', 'Client'),
    ('client8', 'client8password', 'Client8', 'Thomas', 'Client'),
    ('client9', 'client9password', 'Client9', 'Jackson', 'Client'),
    ('client10', 'client10password', 'Client10', 'White', 'Client');

-- Вставка тестовых данных в таблицу [Admin]
INSERT INTO [Admin] ([User_Id])
VALUES (1);

-- Вставка тестовых данных в таблицу Manager
INSERT INTO Manager ([User_Id])
VALUES (2), (3), (4), (5);

-- Вставка тестовых данных в таблицу Client
INSERT INTO Client ([User_Id])
VALUES (6), (7), (8), (9), (10), (11), (12), (13), (14), (15);

-- Вставка тестовых данных в таблицу Journal
INSERT INTO Journal (Date_of_action, [Action], [User_Id])
VALUES 
    (GETDATE(), 'Login', 1),
    (GETDATE(), 'Login', 2),
    (GETDATE(), 'Login', 6);

-- Вставка тестовых данных в таблицу Supplier
INSERT INTO Supplier ([Name], [Address], Phone)
VALUES 
    ('Supplier1', 'Minsk', '+375(29)9240569'),
    ('Supplier2', 'Moscow', '375(29)1255840');

-- Вставка тестовых данных в таблицу Organization
INSERT INTO Organization ([Name], Phone, [Description], Supplier_Id)
VALUES 
    ('Organization1', '+375(29)9250569', 'Description of Organization1', 1),
    ('Organization2', '375(29)1265840', 'Description of Organization2', 2);

-- Вставка тестовых данных в таблицу Certificate
INSERT INTO [Certificate] (Number, Date_of_issue, Date_of_expiration, Organization_Id)
VALUES 
    ('Cert1', GETDATE(), DATEADD(YEAR, 1, GETDATE()), 1),
    ('Cert2', GETDATE(), DATEADD(YEAR, 1, GETDATE()), 2);

-- Вставка тестовых данных в таблицу Model
INSERT INTO Model ([Name], Price, Characteristics)
VALUES 
    ('Model1', 15000.00, 'Characteristics of Model1'),
    ('Model2', 20000.00, 'Characteristics of Model2');

-- Вставка тестовых данных в таблицу Part
INSERT INTO Part ([Name], Price, Quantity)
VALUES 
    ('Part1', 100.00, 10),
    ('Part2', 150.00, 5);

-- Вставка тестовых данных в таблицу Motorcycle
INSERT INTO Motorcycle (Color, Year_of_manufacture, Model_Id, Certificate_Id)
VALUES 
    ('Red', '2022-01-01', 1, 1),
    ('Blue', '2022-02-01', 2, 2);

-- Вставка тестовых данных в таблицу Motorcycle_Part
INSERT INTO Motorcycle_Part (Motorcycle_Id, Part_Id)
VALUES 
    (1, 1),
    (2, 2);

-- Вставка тестовых данных в таблицу Order
INSERT INTO [Order] (Date_of_order, Date_of_delivery, [Status], Amount, Client_Id, Manager_Id, Motorcycle_Id)
VALUES 
    ('2023-01-01', '2023-02-01', 'Pending', 16000.00, 6, 2, 1),
    ('2023-02-01', '2023-03-01', 'Shipped', 20750.00, 7, 3, 2);
