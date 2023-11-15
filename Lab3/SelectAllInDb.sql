-- ������� ������ ������� ������������
SELECT [User_Id]
      ,[Login]
      ,[Password]
      ,[Name]
      ,[Surname]
      ,[Status]
  FROM [MotorcycleDealershipDb].[dbo].[User]

-- ������� ������ ������� �����
SELECT [Admin_Id]
      ,[User_Id]
  FROM [MotorcycleDealershipDb].[dbo].[Admin]

-- ������� ������ ������� ��������
SELECT [Manager_Id]
      ,[User_Id]
  FROM [MotorcycleDealershipDb].[dbo].[Manager]

-- ������� ������ ������� ������
SELECT [Client_Id]
      ,[User_Id]
  FROM [MotorcycleDealershipDb].[dbo].[Client]

-- ������� ������ ������� ������
SELECT [Journal_Id]
      ,[Date_of_action]
      ,[Action]
      ,[User_Id]
  FROM [MotorcycleDealershipDb].[dbo].[Journal]

-- ������� ������ ������� ���������
SELECT [Supplier_Id]
      ,[Name]
      ,[Address]
      ,[Phone]
  FROM [MotorcycleDealershipDb].[dbo].[Supplier]


-- ������� ������ ������� �����������
SELECT TOP (1000) [Organization_Id]
      ,[Name]
      ,[Phone]
      ,[Description]
      ,[Supplier_Id]
  FROM [MotorcycleDealershipDb].[dbo].[Organization]

-- ������� ������ ������� ����������
SELECT [Certificate_Id]
      ,[Number]
      ,[Date_of_issue]
      ,[Date_of_expiration]
      ,[Organization_Id]
  FROM [MotorcycleDealershipDb].[dbo].[Certificate]

-- ������� ������ ������� ������
SELECT [Model_Id]
      ,[Name]
      ,[Price]
      ,[Characteristics]
  FROM [MotorcycleDealershipDb].[dbo].[Model]

-- ������� ������ ������� ��������
SELECT [Part_Id]
      ,[Name]
      ,[Price]
      ,[Quantity]
  FROM [MotorcycleDealershipDb].[dbo].[Part]


-- ������� ������ ������� ��������
SELECT [Motorcycle_Id]
      ,[Color]
      ,[Year_of_manufacture]
      ,[Model_Id]
      ,[Certificate_Id]
  FROM [MotorcycleDealershipDb].[dbo].[Motorcycle]

-- ������� ������ ������� ��������_��������
SELECT [Motorcycle_Id]
      ,[Part_Id]
  FROM [MotorcycleDealershipDb].[dbo].[Motorcycle_Part]

-- ������� ������ ������� �����
SELECT [Order_Id]
      ,[Date_of_order]
      ,[Date_of_delivery]
      ,[Status]
      ,[Amount]
      ,[Client_Id]
      ,[Manager_Id]
      ,[Motorcycle_Id]
  FROM [MotorcycleDealershipDb].[dbo].[Order]