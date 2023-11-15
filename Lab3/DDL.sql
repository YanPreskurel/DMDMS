--Создание таблицы  Пользователь
CREATE TABLE [User] 
(
 [User_Id] INT PRIMARY KEY IDENTITY,

 [Login] NVARCHAR (20) NOT NULL UNIQUE,
 [Password] NVARCHAR (20) NOT NULL,
 [Name] NVARCHAR (50) NOT NULL,
 Surname NVARCHAR (50) NOT NULL,
 [Status] NVARCHAR (10) NOT NULL
);

--Создание таблицы  Админ
CREATE TABLE [Admin]
(
 Admin_Id INT PRIMARY KEY IDENTITY,

 [User_Id] INT REFERENCES [User] ([User_Id]) ON DELETE CASCADE
);

--Создание таблицы  Менеджер
CREATE TABLE Manager 
(
 Manager_Id INT PRIMARY KEY IDENTITY,

 [User_Id] INT REFERENCES [User] ([User_Id]) ON DELETE CASCADE
);

--Создание таблицы  Клиент
CREATE TABLE Client
(
 Client_Id INT PRIMARY KEY IDENTITY,

 [User_Id] INT REFERENCES [User] ([User_Id]) ON DELETE CASCADE
);

--Создание таблицы Журнал
CREATE TABLE Journal
(
 Journal_Id INT PRIMARY KEY IDENTITY,

 Date_of_action DATETIME NOT NULL,
 [Action] NVARCHAR (100) NOT NULL,
 [User_Id] INT REFERENCES [User] ([User_Id])
);

--Создание таблицы Поставщик
CREATE TABLE Supplier
(
 Supplier_Id INT PRIMARY KEY IDENTITY,

 [Name] NVARCHAR(50) NOT NULL,
 [Address] NVARCHAR(100) NOT NULL,
 Phone NVARCHAR(15),
);

--Создание таблицы Организация
CREATE TABLE Organization
(
 Organization_Id INT PRIMARY KEY IDENTITY,

 [Name] NVARCHAR(50) NOT NULL,
 Phone NVARCHAR(15),
 [Description] NVARCHAR(1000),
 Supplier_Id INT REFERENCES Supplier (Supplier_Id) ON DELETE CASCADE
);

--Создание таблицы Сертификат
CREATE TABLE [Certificate]
(
 Certificate_Id INT PRIMARY KEY IDENTITY,

 Number NVARCHAR (20) NOT NULL,
 Date_of_issue DATETIME NOT NULL,
 Date_of_expiration DATETIME NOT NULL,
 Organization_Id INT REFERENCES Organization (Organization_Id)
);

--Создание таблицы Модель
CREATE TABLE Model
(
 Model_Id INT PRIMARY KEY IDENTITY,

 [Name] NVARCHAR (50) NOT NULL,
 Price DECIMAL (10, 2) NOT NULL,
 Characteristics NVARCHAR (1000)
);

--Создание таблицы Запчасть
CREATE TABLE Part
(
 Part_Id INT PRIMARY KEY IDENTITY,

 [Name] NVARCHAR (50) NOT NULL,
 Price DECIMAL (10, 2) NOT NULL,
 Quantity INT NOT NULL
);

--Создание таблицы Мотоцикл
CREATE TABLE Motorcycle
(
 Motorcycle_Id INT PRIMARY KEY IDENTITY,

 Color NVARCHAR(20),
 Year_of_manufacture DATETIME NOT NULL,
 Model_Id INT REFERENCES Model (Model_Id),
 Certificate_Id INT REFERENCES [Certificate] (Certificate_Id)
);


--Создание таблицы Мотоцикл_Запчасть
CREATE TABLE Motorcycle_Part
(
  Motorcycle_Id INT NOT NULL REFERENCES Motorcycle(Motorcycle_Id),
  Part_Id INT NOT NULL REFERENCES Part(Part_Id),

  PRIMARY KEY (Motorcycle_Id, Part_Id)
);

--Создание таблицы Заказ
CREATE TABLE [Order]
(
 Order_Id INT PRIMARY KEY IDENTITY,

 Date_of_order DATETIME NOT NULL,
 Date_of_delivery DATETIME,
 [Status] NVARCHAR (20) NOT NULL,
 Amount DECIMAL (10, 2) NOT NULL,
 Client_Id INT REFERENCES Client (Client_Id),
 Manager_Id INT REFERENCES Manager (Manager_Id),
 Motorcycle_Id INT REFERENCES Motorcycle (Motorcycle_Id)
);