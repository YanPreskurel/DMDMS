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
* User management (CRUD) (ADMIN)
* Role system (roles: CLIENT, MANAGER, ADMIN)
* Logging user actions
* Motorcycle management (CRUD) (MANAGER)
* Certificate management (CRUD) (MANAGER)
* Order management (CRUD) (CLIENT, MANAGER)
* Supplier management (CRUD) (MANAGER)
* Part management (CRUD) (MANAGER)
* Viewing order history (CLIENT, MANAGER)
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
- Relationship3: many-to-one with Supplier - One motorcycle can be supplied by one supplier, and one supplier can supply several motorcycles
- Relationship4: many-to-one with Part through the intermediate table Motorcycle_Part

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
- User_Id: int (FK) (NN)
- Relationship1: one-to-one with User

5. *Manager*
- Id: int (PK) - Manager identifier
- User_Id: int (FK) (NN)
- Relationship1: one-to-one with User

6. *Supplier*
- Id: int (PK) - Supplier identifier
- Name: varchar(50) (NN) - Supplier name
- Address: varchar(100) (NN) - Supplier address
- Phone: varchar(15) (NN) - Supplier phone number
- Relationship1: one-to-many with Motorcycle - One supplier can supply several motorcycles, and one motorcycle can be supplied by one supplier

7. *Part*
- Id: int (PK) - Part identifier
- Name: varchar(50) (NN) - Part name
- Price: decimal(10,2) (NN) - Part price in rubles
- Quantity: int (NN) - Number of parts in stock
- Relationship1: many-to-many with Motorcycle through the intermediate table Motorcycle_Part - One part can fit several motorcycles, and one motorcycle can require several parts

8. *Motorcycle_Part*
- Motorcycle_Id: int (FK) (PK) - Motorcycle identifier
- Part_Id: int (FK) (PK) - Part identifier
- Relationship1: many-to-one with Motorcycle - One record in the table corresponds to one part, but one part can be related to several records in the table
- Relationship2: many-to-one with Part - One record in the table corresponds to one motorcycle, but one motorcycle can be related to several records in the table

9. *Admin*
- Id: int (PK) - Admin identifier
- User_Id: int (FK) (NN)
- Relationship1: one-to-one with User

10. *Journal*
- Id: int (PK) - Journal record identifier
- Date_time: datetime (NN) - Date and time of user action
- Action: varchar(100) (NN) - Description of user action (for example, added a motorcycle, changed an order, etc.)
- User_Id: int (FK) (NN) - Identifier of the user who performed the action
- Admin_Id: int (FK) - Identifier of the admin who viewed the record in the journal
- Relationship1: many-to-one with User - One record in the journal corresponds to one user, but one user can be related to several records in the journal

11. *User*
- Id: int (PK) - User identifier
- Login: varchar(20) (NN) (UQ) - User login for authorization
- Password: varchar(20) (NN) - User password for authorization
- Name: varchar(50) (NN) - User name
- Surname: varchar(50) (NN) - User surname
- Relationship1: one-to-one with Admin
- Relationship2: one-to-one with Manager
- Relationship3: one-to-one with Client
- Relationship4: one-to-many with Journal - One user can be related to several records in the journal, but one record in the journal corresponds to one user

![image](https://github.com/YanPreskurel/Labs_1-6_5sem_DMaDMS/assets/90517349/1113e257-aa67-4a4d-8562-4deaacd14cae)
