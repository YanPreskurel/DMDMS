-- Выборка данных таблицы Пользователь
SELECT [User_Id]
      ,[Login]
      ,[Password]
      ,[Name]
      ,[Surname]
      ,[Status]
  FROM [MotorcycleDealershipDb].[dbo].[User]

-- Выборка данных таблицы Админ
SELECT [Admin_Id]
      ,[User_Id]
  FROM [MotorcycleDealershipDb].[dbo].[Admin]

-- Выборка данных таблицы Менеджер
SELECT [Manager_Id]
      ,[User_Id]
  FROM [MotorcycleDealershipDb].[dbo].[Manager]

-- Выборка данных таблицы Клиент
SELECT [Client_Id]
      ,[User_Id]
  FROM [MotorcycleDealershipDb].[dbo].[Client]

-- Выборка данных таблицы Журнал
SELECT [Journal_Id]
      ,[Date_of_action]
      ,[Action]
      ,[User_Id]
  FROM [MotorcycleDealershipDb].[dbo].[Journal]

-- Выборка данных таблицы Поставщик
SELECT [Supplier_Id]
      ,[Name]
      ,[Address]
      ,[Phone]
  FROM [MotorcycleDealershipDb].[dbo].[Supplier]


-- Выборка данных таблицы Организация
SELECT TOP (1000) [Organization_Id]
      ,[Name]
      ,[Phone]
      ,[Description]
      ,[Supplier_Id]
  FROM [MotorcycleDealershipDb].[dbo].[Organization]

-- Выборка данных таблицы Сертификат
SELECT [Certificate_Id]
      ,[Number]
      ,[Date_of_issue]
      ,[Date_of_expiration]
      ,[Organization_Id]
  FROM [MotorcycleDealershipDb].[dbo].[Certificate]

-- Выборка данных таблицы Модель
SELECT [Model_Id]
      ,[Name]
      ,[Price]
      ,[Characteristics]
  FROM [MotorcycleDealershipDb].[dbo].[Model]

-- Выборка данных таблицы Запчасть
SELECT [Part_Id]
      ,[Name]
      ,[Price]
      ,[Quantity]
  FROM [MotorcycleDealershipDb].[dbo].[Part]


-- Выборка данных таблицы Мотоцикл
SELECT [Motorcycle_Id]
      ,[Color]
      ,[Year_of_manufacture]
      ,[Model_Id]
      ,[Certificate_Id]
  FROM [MotorcycleDealershipDb].[dbo].[Motorcycle]

-- Выборка данных таблицы Мотоцикл_Запчасть
SELECT [Motorcycle_Id]
      ,[Part_Id]
  FROM [MotorcycleDealershipDb].[dbo].[Motorcycle_Part]

-- Выборка данных таблицы Заказ
SELECT [Order_Id]
      ,[Date_of_order]
      ,[Date_of_delivery]
      ,[Status]
      ,[Amount]
      ,[Client_Id]
      ,[Manager_Id]
      ,[Motorcycle_Id]
  FROM [MotorcycleDealershipDb].[dbo].[Order]