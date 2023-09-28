# Labs_1-6_5sem_DMaDMS

Student: Preskurel Yan Yurievich \
Group number: 153504

# Software tool

*Subject area - motorcycle dealership* will include the main functions of buying and selling motorcycles. 

This software will be developed by language tools *C# and platform .NET version 6.0, DBMS - SQL server*

# Functional Requirments

*Subject area - motorcycle dealership* should include basic functions for work.

Functional requirements for the project of developing a database for a motorcycle dealership:

* User authorization
* User management (CRUD)
* Role system (roles: CLIENT, MANAGER, ADMIN)
* Logging user actions
* Motorcycle management (CRUD) (ADMIN)
* Certificate management (CRUD) (ADMIN)
* Order management (CRUD) (CLIENT, MANAGER)
* Supplier management (CRUD) (ADMIN)
* Part management (CRUD) (ADMIN)
* Viewing order history (CLIENT, MANAGER, ADMIN)
* Viewing action history in the journal (ADMIN)

# Entities:

1. *Motorcycle*
- Id: int (PK) - Motorcycle identifier
- Model: varchar(50) (NN) - Motorcycle model name
- Color: varchar(20) (NN) - Motorcycle color
- Price: decimal(10,2) (NN) - Motorcycle price in rubles
- Year_of_manufacture: int (NN) - Motorcycle year of manufacture
- Certificate_Id: int (FK) - Certificate identifier, confirming the compliance of the motorcycle with quality and safety standards
- Relationship1: one-to-one with Certificate - Each motorcycle has only one certificate, and each certificate belongs to only one motorcycle
- Relationship2: one-to-many with Order - One motorcycle can be sold in several orders, but one order can contain only one motorcycle
- Relationship3: many-to-many with Supplier through the intermediate table Motorcycle_Supplier - One motorcycle can be supplied by several suppliers, and one supplier can supply several motorcycles

2. *Certificate*
- Id: int (PK) - Certificate identifier
- Number: varchar(20) (NN) - Certificate number
- Date_of_issue: date (NN) - Date of issue of the certificate
- Date_of_expiration: date (NN) - Date of expiration of the certificate
- Organization: varchar(50) (NN) - Name of the organization that issued the certificate
- Relationship1: one-to-one with Motorcycle - Each certificate belongs to only one motorcycle, and each motorcycle has only one certificate

3. *Order*
- Id: int (PK) - Order identifier
- Date_of_order: date (NN) - Date of placing the order
- Date_of_delivery: date - Date of delivery of the order
- Status: varchar(20) (NN) - Order status (for example, waiting for payment, paid, delivered, etc.)
- Amount: decimal(10,2) (NN) - Order amount in rubles
- Client_Id: int (FK) (NN) - Identifier of the client who made the order
- Manager_Id: int (FK) (NN) - Identifier of the manager who processed the order
- Motorcycle_Id: int (FK) (NN) - Identifier of the motorcycle sold in the order
- Relationship1: one-to-many with Client - One client can make several orders, but one order belongs to only one client
- Relationship2: one-to-many with Manager - One manager can process several orders, but one order is processed by only one manager
- Relationship3: many-to-one with Motorcycle - One order can contain only one motorcycle, but one motorcycle can be sold in several orders

4. *Client*
- Id: int (PK) - Client identifier
- Login: varchar(20) (NN) (UQ) - Client login for authorization
- Password: varchar(20) (NN) - Client password for authorization
- Name: varchar(50) (NN) - Client name
- Surname: varchar(50) (NN) - Client surname
- Phone: varchar(15) (NN) - Client phone number
- Address: varchar(100) - Client delivery address
- Relationship1: one-to-many with Order - One client can make several orders, but one order belongs to only one client

5. *Manager*
- Id: int (PK) - Manager identifier
- Login: varchar(20) (NN) (UQ) - Manager login for authorization
- Password: varchar(20) (NN) - Manager password for authorization
- Name: varchar(50) (NN) - Manager name
- Surname: varchar(50) (NN) - Manager surname
- Phone: varchar(15) (NN) - Manager phone number
- Salary: decimal(10,2) (NN) - Manager salary in rubles
- Relationship1: one-to-many with Order - One manager can process several orders, but one order is processed by only one manager

6. *Supplier*
- Id: int (PK) - Supplier identifier
- Name: varchar(50) (NN) - Supplier name
- Address: varchar(100) (NN) - Supplier address
- Phone: varchar(15) (NN) - Supplier phone number
- Relationship1: many-to-many with Motorcycle through the intermediate table Motorcycle_Supplier - One supplier can supply several motorcycles, and one motorcycle can be supplied by several suppliers

7. *Motorcycle_Supplier*
- Motorcycle_Id: int (FK)(PK) - Motorcycle identifier 
- Supplier_Id:int(FK)(PK) - Supplier identifier 
- Quantity:int(NN) - Number of motorcycles of this model supplied by this supplier 
- Relationship1: many-to-one with Motorcycle - One record in the table corresponds to one motorcycle, but one motorcycle can be related to several records in the table 
- Relationship2: many-to-one with Supplier - One record in the table corresponds to one supplier, but one supplier can be related to several records in the table

8. *Part*
- Id: int (PK) - Part identifier
- Name: varchar(50) (NN) - Part name
- Price: decimal(10,2) (NN) - Part price in rubles
- Quantity: int (NN) - Number of parts in stock
- Relationship1: many-to-many with Motorcycle through the intermediate table Motorcycle_Part - One part can fit several motorcycles, and one motorcycle can require several parts

9. *Motorcycle_Part*
- Motorcycle_Id: int (FK) (PK) - Motorcycle identifier
- Part_Id: int (FK) (PK) - Part identifier
- Relationship1: many-to-one with Motorcycle - One record in the table corresponds to one part, but one part can be related to several records in the table
- Relationship2: many-to-one with Part - One record in the table corresponds to one motorcycle, but one motorcycle can be related to several records in the table

10. *Admin*
- Id: int (PK) - Admin identifier
- Login: varchar(20) (NN) (UQ) - Admin login for authorization
- Password: varchar(20) (NN) - Admin password for authorization
- Name: varchar(50) (NN) - Admin name
- Surname: varchar(50) (NN) - Admin surname
- Phone: varchar(15) (NN) - Admin phone number
- Relationship1: one-to-many with Journal - One admin can view several records in the journal, but one record in the journal belongs to only one admin

11. *Journal*
- Id: int (PK) - Journal record identifier
- Date_time: datetime (NN) - Date and time of user action
- Action: varchar(100) (NN) - Description of user action (for example, added a motorcycle, changed an order, etc.)
- User_Id: int (FK) (NN) - Identifier of the user who performed the action
- Admin_Id: int (FK) - Identifier of the admin who viewed the record in the journal
- Relationship1: many-to-one with User - One record in the journal corresponds to one user, but one user can be related to several records in the journal
- Relationship2: many-to-one with Admin - One record in the journal corresponds to one admin, but one admin can be related to several records in the journal

12. *User*
- Id: int (PK) - User identifier
- Role: varchar(20) (NN) - User role (admin, manager or client)
- Relationship1: one-to-many with Journal - One user can be related to several records in the journal, but one record in the journal belongs to only one user

![image](https://github.com/YanPreskurel/Labs_1-6_5sem_DMaDMS/assets/90517349/ce6a7f63-19c5-4265-b549-0c436ccd03d3)

