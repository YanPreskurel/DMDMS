--������ ��������� ��������� ��� ������������ �������� UpdateOrderStatus:

-- ��������� ���������
UPDATE Motorcycle
SET Color = 'Blue'
WHERE Motorcycle_Id = 1;
-- ���� ������ ������� ������������ �������� UpdateOrderStatus

--������ �������� ������������ ��� ������������ �������� LogUserChanges:

-- �������� ������������
DELETE FROM [User]
WHERE User_Id = 2;
-- ���� ������ ������� ������������ �������� LogUserChanges


--����� ��������� GetOrdersByClient ��� ��������� ������ ������� ��� ����������� �������:
DECLARE @ClientId INT = 2; -- �������� �� ���������� ������������� �������
EXEC GetOrdersByClient @ClientId;

--����� ��������� AddNewUser ��� ���������� ������ ������������:

DECLARE @Login NVARCHAR(20) = 'newuser2';
DECLARE @Password NVARCHAR(20) = 'newpassword2';
DECLARE @Name NVARCHAR(50) = 'Jane';
DECLARE @Surname NVARCHAR(50) = 'Doe';
DECLARE @Status NVARCHAR(10) = 'Active';

EXEC AddNewUser @Login, @Password, @Name, @Surname, @Status;

--����� ��������� GetMotorcyclesByModel ��� ��������� ������ ���������� ������������ ������:
DECLARE @ModelName NVARCHAR(50) = 'ExampleModel';
EXEC GetMotorcyclesByModel @ModelName;

--����� ��������� GetPendingOrders ��� ��������� ������ �������, ��������� ��������:
EXEC GetPendingOrders;