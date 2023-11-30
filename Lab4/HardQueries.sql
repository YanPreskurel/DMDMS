USE MotorcycleDealershipDb;

-- Запрос с несколькими условиями:
SELECT * FROM [User] WHERE Status = 'Client' AND [Surname] LIKE 'Moore%';

--Запрос с вложенными конструкциями:
SELECT * FROM [User] WHERE [User_Id] IN (SELECT [User_Id] FROM [Admin]);

-- Прочие сложные выборки:
SELECT [User].[Name], [User].[Surname], COUNT([Journal].[Journal_Id]) AS ActionCount
FROM [User]
LEFT JOIN [Journal] ON [User].[User_Id] = [Journal].[User_Id]
GROUP BY [User].[Name], [User].[Surname]
HAVING COUNT([Journal].[Journal_Id]) > 0;

--Получение представлений в БД:

--INNER JOIN:
SELECT [User].[Name], [User].[Surname], [Admin].[Admin_Id]
FROM [User]
JOIN [Admin] ON [User].[User_Id] = [Admin].[User_Id];

--LEFT OUTER JOIN:
SELECT [User].[Name], [User].[Surname], [Manager].[Manager_Id]
FROM [User]
LEFT OUTER JOIN [Manager] ON [User].[User_Id] = [Manager].[User_Id];

--Получение сгруппированных данных:

-- GROUP BY + агрегирующие функции:
SELECT [Client].[Client_Id], COUNT([Order].[Order_Id]) AS OrderCount
FROM [Client]
LEFT JOIN [Order] ON [Client].[Client_Id] = [Order].[Client_Id]
GROUP BY [Client].[Client_Id];

--PARTITION, PARTITION OVER + оконные функции:
SELECT [Name], [Price], AVG([Price]) OVER (PARTITION BY Model_Id) AS AvgPricePerModel
FROM [Model];

--HAVING:
SELECT [Client].[Client_Id], AVG([Order].[Amount]) AS AvgOrderAmount
FROM [Client]
LEFT JOIN [Order] ON [Client].[Client_Id] = [Order].[Client_Id]
GROUP BY [Client].[Client_Id]
HAVING AVG([Order].[Amount]) > 5000;

--Сложные операции с данными:

--EXISTS:
SELECT [User].[Name], [User].[Surname]
FROM [User]
WHERE EXISTS (SELECT 1 FROM [Journal] WHERE [User].[User_Id] = [Journal].[User_Id]);

--INSERT INTO SELECT:
INSERT INTO [Journal] (Date_of_action, [Action], [User_Id])
SELECT GETDATE(), 'Login', [User_Id]
FROM [User]
WHERE [Login] = 'client3';

--CASE:
SELECT [Order].[Status],
       CASE 
           WHEN [Order].[Date_of_delivery] IS NULL THEN 'Not Delivered'
           ELSE 'Delivered'
       END AS DeliveryStatus
FROM [Order];
