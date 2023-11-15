--������� ������� ��� ����������� �������
SELECT * FROM [Order] WHERE Client_Id = 6;
SELECT * FROM [Order] WHERE Client_Id = 7;

--���������� ���������� � ������������
UPDATE [User] SET [Name] = 'UpdatedNameAdmin' WHERE [User_Id] =1;

--������� ���� ��������������� � ��������� � ���� �������������
SELECT [User].*, [Admin].Admin_Id
FROM [User]
JOIN [Admin] ON [User].User_Id = [Admin].User_Id;

--������� ���� ������� � ��������� � ���� �������� � ����������
SELECT [Order].*, Client.Client_Id AS ClientName, Manager.Manager_Id AS ManagerName
FROM [Order]
JOIN Client ON [Order].Client_Id = Client.Client_Id
JOIN Manager ON [Order].Manager_Id = Manager.Manager_Id;

--���������� ���������� � ������
UPDATE [Order] SET Date_of_delivery = '2023-01-20' WHERE Order_Id = 1;

--������� ���� ����������� � ��������� � ���� �����������
SELECT Supplier.*, Organization.[Name] AS OrganizationName
FROM Supplier
JOIN Organization ON Supplier.Supplier_Id = Organization.Supplier_Id;

--������� ���� ���������� � �� �������������
SELECT Motorcycle.*, Model.Characteristics AS ModelCharacteristics
FROM Motorcycle
JOIN Model ON Motorcycle.Model_Id = Model.Model_Id;
