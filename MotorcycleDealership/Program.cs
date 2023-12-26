using MotorcycleDealership.Entities;
using MotorcycleDealership.Services;
using System.Data;

namespace MotorcycleDealership
{
    class Program
    {
        static void Main()
        {
            // Замените на свою строку подключения
            string connectionString = "Data Source=LAPTOP-1S1SMA72;Initial Catalog=MotorcycleDealershipdb;Integrated Security=True";
            DataBase dataBase = new DataBase(connectionString);
            UserManagement userManagement = new UserManagement(dataBase);
            AdminManagement adminManagement = new AdminManagement(dataBase);
            ClientManagement clientManagement = new ClientManagement(dataBase);
            ManagerManagement managerManagement = new ManagerManagement(dataBase);

            while (true)
            {
                Console.WriteLine("1. Войти");
                Console.WriteLine("2. Зарегистрироваться");
                Console.WriteLine("3. Выйти");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Введите логин: ");
                        string login1 = Console.ReadLine();
                        Console.Write("Введите пароль: ");
                        string password1 = Console.ReadLine();

                        DataTable userTable = userManagement.GetUserByLoginAndPassword(login1, password1);

                        if (userTable.Rows.Count > 0) // проверка наличия пользователя
                        {
                            DataRow userRow = userTable.Rows[0];

                            if (userRow != null)
                            {
                                int userId = (int)userRow["User_Id"];
                                string status = userRow["Status"].ToString();

                                if (status == "Client")
                                {
                                    // Код для клиента
                                    Console.WriteLine($"Пользователь - {status}");

                                    while (true)
                                    {
                                        Console.WriteLine("Меню клиента:");
                                        Console.WriteLine("1. Просмотр списка мотоциклов в мотосалоне");
                                        Console.WriteLine("2. Просмотр списка заказов оформленных текущим клиентом");
                                        Console.WriteLine("3. Просмотр списка запчастей для отдельных моделей");
                                        Console.WriteLine("4. Создание заказа на покупку мотоцикла или запчастей");
                                        Console.WriteLine("5. Информация о мотоцикле из заказа клиента и всех зависящих сущностей");
                                        Console.WriteLine("6. Удаление аккаунта");
                                        Console.WriteLine("7. Выход");
                                        Console.Write("Выберите действие: ");

                                        string ClientChoice = Console.ReadLine();

                                        switch (ClientChoice)
                                        {
                                            case "1":
                                                DataTable motorcycleTable = clientManagement.ViewMotorcycleList();
                                                clientManagement.DisplayTable(motorcycleTable, "Список мотоциклов");
                                                break;

                                            case "2":
                                                DataTable orderTable = clientManagement.ViewOrderList(userId);
                                                clientManagement.DisplayTable(orderTable, "Список заказов");
                                                break;

                                            case "3":
                                                Console.Write("Введите Id мотоцикла: ");

                                                if (int.TryParse(Console.ReadLine(), out int motorcycleId))
                                                {
                                                    if (clientManagement.IsMotorcycleIdValid(motorcycleId))
                                                    {
                                                        DataTable partTable = clientManagement.ViewMototrcyclePartList(motorcycleId);
                                                        clientManagement.DisplayTable(partTable, "Список запчастей для мотоцикла");
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Некорректный ввод. Мотоцикл с указанным Id не существует.");
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Некорректный ввод. Введите целочисленный Id мотоцикла.");
                                                }
                                                break;

                                            case "4":
                                                Console.Write("Введите Id мотоцикла, который хотите купить: ");

                                                if (int.TryParse(Console.ReadLine(), out int motorcycleId1))
                                                {
                                                    if (clientManagement.IsMotorcycleIdValid(motorcycleId1))
                                                    {
                                                        DataTable managersTable = clientManagement.ViewManagerList();
                                                        clientManagement.DisplayTable(managersTable, "Список менеджеров");

                                                        Console.Write("Введите Id менеджера: ");
                                                        if (int.TryParse(Console.ReadLine(), out int managerId))
                                                        {
                                                            if (clientManagement.IsManagerIdValid(managerId))
                                                            {
                                                                decimal motorcycleCost = clientManagement.GetMotorcycleCost(motorcycleId1);
                                                                if (motorcycleCost > 0)
                                                                {
                                                                    clientManagement.CreateOrder(motorcycleCost, userId, motorcycleId1, managerId);
                                                                    Console.WriteLine("Заказ успешно создан.");
                                                                }
                                                                else
                                                                {
                                                                    Console.WriteLine("Ошибка при получении стоимости мотоцикла. Проверьте введенный Id мотоцикла.");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Некорректный ввод. Менеджер с указанным Id не существует.");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Некорректный ввод. Введите целочисленный Id менеджера.");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Некорректный ввод. Мотоцикл с указанным Id не существует.");
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Некорректный ввод. Введите целочисленный Id мотоцикла.");
                                                }
                                                break;

                                            case "5":
                                                DataTable detailedInfoTable = clientManagement.ViewDetailedInfoForUserOrder(userId);
                                                clientManagement.DisplayTable(detailedInfoTable, "Подробная информация о заказах клиента");
                                                break;

                                            case "6":
                                                clientManagement.DeleteAccount(userId);
                                                Console.WriteLine("Аккаунт удален.");
                                                return;

                                            case "7":
                                                return;

                                            default:
                                                Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                                                break;
                                        }
                                    }

                                }
                                else if (status == "Manager")
                                {
                                    // Код для менеджера
                                    Console.WriteLine($"Пользователь - {status}");

                                    bool exit = false;

                                    while (!exit)
                                    {
                                        Console.WriteLine("\nМеню менеджера:");
                                        Console.WriteLine("1. Добавление");
                                        Console.WriteLine("2. Удаление");
                                        Console.WriteLine("3. Просмотр");
                                        Console.WriteLine("4. Выход");

                                        Console.Write("Выберите опцию: ");
                                        string ManagerChoice = Console.ReadLine();
                                        int a;

                                        switch (ManagerChoice)
                                        {
                                            case "1":
                                                Console.WriteLine("\nДобавление:");
                                                Console.WriteLine("1. Добавить поставщика");
                                                Console.WriteLine("2. Добавить организацию");
                                                Console.WriteLine("3. Добавить сертификат");
                                                Console.WriteLine("4. Добавить модель мотоцикла");
                                                Console.WriteLine("5. Добавить мотоцикл");
                                                Console.WriteLine("6. Добавить запчасть для мотоцикла");
                                                Console.WriteLine("7. Добавить мотоцикл целиком со всеми зависимостями");
                                                Console.WriteLine("8. Вернуться в предыдущее меню");

                                                Console.Write("Выберите опцию: ");
                                                string choice2 = Console.ReadLine();

                                                switch (choice2)
                                                {
                                                    case "1":
                                                        Console.Write("Введите имя поставщика: ");
                                                        string supplierName = Console.ReadLine();

                                                        Console.Write("Введите адрес поставщика: ");
                                                        string supplierAddress = Console.ReadLine();

                                                        Console.Write("Введите телефон поставщика: ");
                                                        string supplierPhone = Console.ReadLine();

                                                        a = managerManagement.AddSupplier(supplierName, supplierAddress, supplierPhone);
                                                        break;

                                                    case "2":
                                                        Console.Write("Введите имя организации: ");
                                                        string orgName = Console.ReadLine();

                                                        Console.Write("Введите описание организации: ");
                                                        string orgDescription = Console.ReadLine();

                                                        Console.Write("Введите телефон организации: ");
                                                        string orgPhone = Console.ReadLine();

                                                        Console.Write("Введите ID поставщика: ");
                                                        int supplierIdForOrg = int.Parse(Console.ReadLine());

                                                        a = managerManagement.AddOrganization(orgName, orgDescription, orgPhone, supplierIdForOrg);
                                                        break;

                                                    case "3":
                                                        Console.Write("Введите имя сертификата: ");
                                                        string certName = Console.ReadLine();

                                                        Console.Write("Введите дату выдачи сертификата (гггг-мм-дд): ");
                                                        DateTime certDateOfIssue = DateTime.Parse(Console.ReadLine());

                                                        Console.Write("Введите дату окончания сертификата (гггг-мм-дд): ");
                                                        DateTime certDateOfExpiration = DateTime.Parse(Console.ReadLine());

                                                        Console.Write("Введите ID организации: ");
                                                        int orgIdForCert = int.Parse(Console.ReadLine());

                                                        a = managerManagement.AddCertificate(certName, certDateOfIssue, certDateOfExpiration, orgIdForCert);
                                                        break;

                                                    case "4":
                                                        Console.Write("Введите имя модели мотоцикла: ");
                                                        string modelName = Console.ReadLine();

                                                        Console.Write("Введите цену модели мотоцикла: ");
                                                        decimal modelPrice = decimal.Parse(Console.ReadLine());

                                                        Console.Write("Введите характеристики модели мотоцикла: ");
                                                        string modelCharacteristics = Console.ReadLine();

                                                        a = managerManagement.AddModel(modelName, modelPrice, modelCharacteristics);
                                                        break;

                                                    case "5":
                                                        Console.Write("Введите ID модели мотоцикла: ");
                                                        int modelIdForMotorcycle = int.Parse(Console.ReadLine());

                                                        Console.Write("Введите цвет мотоцикла: ");
                                                        string motorcycleColor = Console.ReadLine();

                                                        Console.Write("Введите год производства мотоцикла: ");
                                                        int motorcycleYearOfManufacture = int.Parse(Console.ReadLine());

                                                        Console.Write("Введите ID сертификата: ");
                                                        int certIdForMotorcycle = int.Parse(Console.ReadLine());

                                                        managerManagement.AddMotorcycle(modelIdForMotorcycle, motorcycleColor, motorcycleYearOfManufacture, certIdForMotorcycle);
                                                        break;

                                                    case "6":
                                                        Console.Write("Введите ID мотоцикла: ");
                                                        int motorcycleIdForPart = int.Parse(Console.ReadLine());

                                                        Console.Write("Введите ID запчасти: ");
                                                        int partIdForMotorcycle = int.Parse(Console.ReadLine());

                                                        managerManagement.AddMotorcyclePart(motorcycleIdForPart, partIdForMotorcycle);
                                                        break;

                                                    case "7":
                                                        Console.WriteLine("Введите данные для добавления мотоцикла со всеми зависимостями:");

                                                        Console.Write("Введите имя модели мотоцикла: ");
                                                        string modelName1 = Console.ReadLine();

                                                        Console.Write("Введите цену модели мотоцикла: ");
                                                        decimal modelPrice1 = decimal.Parse(Console.ReadLine());

                                                        Console.Write("Введите характеристики модели мотоцикла: ");
                                                        string modelCharacteristics1 = Console.ReadLine();

                                                        int modelId = managerManagement.AddModel(modelName1, modelPrice1, modelCharacteristics1);

                                                        Console.Write("Введите имя поставщика: ");
                                                        string supplierName1 = Console.ReadLine();

                                                        Console.Write("Введите адрес поставщика: ");
                                                        string supplierAddress1 = Console.ReadLine();

                                                        Console.Write("Введите телефон поставщика: ");
                                                        string supplierPhone1 = Console.ReadLine();

                                                        int supplierId = managerManagement.AddSupplier(supplierName1, supplierAddress1, supplierPhone1);

                                                        Console.Write("Введите имя организации: ");
                                                        string orgName1 = Console.ReadLine();

                                                        Console.Write("Введите описание организации: ");
                                                        string orgDescription1 = Console.ReadLine();

                                                        Console.Write("Введите телефон организации: ");
                                                        string orgPhone1 = Console.ReadLine();

                                                        int orgId = managerManagement.AddOrganization(orgName1, orgDescription1, orgPhone1, supplierId);

                                                        Console.Write("Введите имя сертификата: ");
                                                        string certName1 = Console.ReadLine();

                                                        Console.Write("Введите дату выдачи сертификата (гггг-мм-дд): ");
                                                        DateTime certDateOfIssue1 = DateTime.Parse(Console.ReadLine());

                                                        Console.Write("Введите дату окончания сертификата (гггг-мм-дд): ");
                                                        DateTime certDateOfExpiration1 = DateTime.Parse(Console.ReadLine());

                                                        int certId = managerManagement.AddCertificate(certName1, certDateOfIssue1, certDateOfExpiration1, orgId);

                                                        Console.Write("Введите цвет мотоцикла: ");
                                                        string motorcycleColor1 = Console.ReadLine();

                                                        Console.Write("Введите год производства мотоцикла: ");
                                                        int motorcycleYearOfManufacture1 = int.Parse(Console.ReadLine());

                                                        managerManagement.AddMotorcycle(modelId, motorcycleColor1, motorcycleYearOfManufacture1, certId);
                                                        break;

                                                    case "8":
                                                        break;

                                                    default:
                                                        Console.WriteLine("Некорректный ввод. Попробуйте еще раз.");
                                                        break;
                                                }
                                                break;

                                            case "2":
                                                Console.WriteLine("\nУдаление:");
                                                Console.WriteLine("1. Удалить поставщика");
                                                Console.WriteLine("2. Удалить организацию");
                                                Console.WriteLine("3. Удалить сертификат");
                                                Console.WriteLine("4. Удалить модель мотоцикла");
                                                Console.WriteLine("5. Удалить мотоцикл");
                                                Console.WriteLine("6. Удалить запчасть для мотоцикла");
                                                Console.WriteLine("7. Вернуться в предыдущее меню");

                                                Console.Write("Выберите опцию: ");
                                                string choice3 = Console.ReadLine();

                                                switch (choice3)
                                                {
                                                    case "1":
                                                        Console.WriteLine("\nУдаление поставщика:");
                                                        Console.Write("Введите Id поставщика для удаления: ");
                                                        int supplierIdToDelete = int.Parse(Console.ReadLine());
                                                        managerManagement.DeleteSupplier(supplierIdToDelete);
                                                        Console.WriteLine("Поставщик успешно удален.");
                                                        break;

                                                    case "2":
                                                        Console.WriteLine("\nУдаление организации:");
                                                        Console.Write("Введите Id организации для удаления: ");
                                                        int orgIdToDelete = int.Parse(Console.ReadLine());
                                                        managerManagement.DeleteOrganization(orgIdToDelete);
                                                        Console.WriteLine("Организация успешно удалена.");
                                                        break;

                                                    case "3":
                                                        Console.WriteLine("\nУдаление сертификата:");
                                                        Console.Write("Введите Id сертификата для удаления: ");
                                                        int certIdToDelete = int.Parse(Console.ReadLine());
                                                        managerManagement.DeleteCertificate(certIdToDelete);
                                                        Console.WriteLine("Сертификат успешно удален.");
                                                        break;

                                                    case "4":
                                                        Console.WriteLine("\nУдаление модели мотоцикла:");
                                                        Console.Write("Введите Id модели мотоцикла для удаления: ");
                                                        int modelIdToDelete = int.Parse(Console.ReadLine());
                                                        managerManagement.DeleteModel(modelIdToDelete);
                                                        Console.WriteLine("Модель мотоцикла успешно удалена.");
                                                        break;

                                                    case "5":
                                                        Console.WriteLine("\nУдаление мотоцикла:");
                                                        Console.Write("Введите Id мотоцикла для удаления: ");
                                                        int motorcycleIdToDelete = int.Parse(Console.ReadLine());
                                                        managerManagement.DeleteMotorcycle(motorcycleIdToDelete);
                                                        Console.WriteLine("Мотоцикл успешно удален.");
                                                        break;

                                                    case "6":
                                                        Console.WriteLine("\nУдаление запчасти для мотоцикла:");
                                                        Console.Write("Введите Id мотоцикла для удаления запчасти: ");
                                                        int motorcycleIdForPart = int.Parse(Console.ReadLine());

                                                        Console.Write("Введите Id запчасти для удаления: ");
                                                        int partIdToDelete = int.Parse(Console.ReadLine());

                                                        managerManagement.DeleteMotorcyclePart(motorcycleIdForPart, partIdToDelete);
                                                        Console.WriteLine("Запчасть успешно удалена.");
                                                        break;

                                                    case "7":
                                                        break;

                                                    default:
                                                        Console.WriteLine("Некорректный ввод. Попробуйте еще раз.");
                                                        break;
                                                }
                                                break;

                                            case "3":
                                                Console.WriteLine("\nПросмотр:");
                                                Console.WriteLine("1. Просмотр списка мотоциклов");
                                                Console.WriteLine("2. Просмотр списка запчастей для мотоцикла");
                                                Console.WriteLine("3. Просмотр списка моделей мотоциклов");
                                                Console.WriteLine("4. Просмотр списка организаций");
                                                Console.WriteLine("5. Просмотр списка поставщиков");
                                                Console.WriteLine("6. Просмотр списка заказов");
                                                Console.WriteLine("7. Просмотр списка клиентов");
                                                Console.WriteLine("8. Вернуться в предыдущее меню");

                                                Console.Write("Выберите опцию: ");
                                                string choice4 = Console.ReadLine();

                                                switch (choice4)
                                                {
                                                    case "1":
                                                        DataTable motorcycleList = managerManagement.ViewMotorcycleList();
                                                        managerManagement.DisplayTable(motorcycleList, "Список мотоциклов");
                                                        break;

                                                    case "2":
                                                        int motorcycleIdForParts = managerManagement.GetMotorcycleIdFromUser();
                                                        DataTable motorcyclePartsList = managerManagement.ViewMotorcyclePartList(motorcycleIdForParts);
                                                        managerManagement.DisplayTable(motorcyclePartsList, "Список запчастей для мотоцикла");
                                                        break;

                                                    case "3":
                                                        DataTable modelList = managerManagement.ViewModelList();
                                                        managerManagement.DisplayTable(modelList, "Список моделей мотоциклов");
                                                        break;

                                                    case "4":
                                                        DataTable organizationList = managerManagement.ViewOrganizationList();
                                                        managerManagement.DisplayTable(organizationList, "Список организаций");
                                                        break;

                                                    case "5":
                                                        DataTable supplierList = managerManagement.ViewSupplierList();
                                                        managerManagement.DisplayTable(supplierList, "Список поставщиков");
                                                        break;

                                                    case "6":
                                                        int managerIdForOrders = managerManagement.GetManagerIdFromUser(userId);
                                                        DataTable orderList = managerManagement.ViewOrderList(managerIdForOrders);
                                                        managerManagement.DisplayTable(orderList, "Список заказов");
                                                        break;

                                                    case "7":
                                                        DataTable clientList = managerManagement.ViewClientList();
                                                        managerManagement.DisplayTable(clientList, "Список клиентов");
                                                        break;

                                                    case "8":
                                                        break;

                                                    default:
                                                        Console.WriteLine("Некорректный ввод. Попробуйте еще раз.");
                                                        break;
                                                }
                                                break;

                                            case "4":
                                                exit = true;
                                                break;

                                            default:
                                                Console.WriteLine("Некорректный ввод. Попробуйте еще раз.");
                                                break;
                                        }
                                    }
                                }
                                else if (status == "Admin")
                                {
                                    // Код для админа
                                    Console.WriteLine($"Пользователь - {status} ");

                                    while (true)
                                    {
                                        Console.WriteLine("Выберите действие:");
                                        Console.WriteLine("1. Добавить пользователя");
                                        Console.WriteLine("2. Удалить пользователя");
                                        Console.WriteLine("3. Просмотреть журнал");
                                        Console.WriteLine("4. Просмотреть список менеджеров");
                                        Console.WriteLine("5. Просмотреть список клиентов");
                                        Console.WriteLine("6. Просмотреть список всех пользователей");
                                        Console.WriteLine("0. Выйти");

                                        Console.Write("Введите номер действия: ");
                                        string adminInput = Console.ReadLine();

                                        switch (adminInput)
                                        {
                                            case "1":
                                                // Добавление пользователя
                                                Console.WriteLine("Введите информацию о новом пользователе:");

                                                string login = "";
                                                while (string.IsNullOrWhiteSpace(login))
                                                {
                                                    Console.Write("Введите логин: ");
                                                    login = Console.ReadLine().Trim();

                                                    if (string.IsNullOrWhiteSpace(login))
                                                    {
                                                        Console.WriteLine("Логин не может быть пустым. Пожалуйста, введите корректный логин.");
                                                    }
                                                }

                                                // Проверка на уникальность логина
                                                DataTable existingUser = adminManagement.GetUserByLogin(login);
                                                if (existingUser.Rows.Count > 0)
                                                {
                                                    Console.WriteLine("Пользователь с таким логином уже существует. Выберите другой логин.");
                                                    break;
                                                }

                                                string password = "";
                                                while (string.IsNullOrWhiteSpace(password))
                                                {
                                                    Console.Write("Введите пароль: ");
                                                    password = Console.ReadLine().Trim();

                                                    if (string.IsNullOrWhiteSpace(password))
                                                    {
                                                        Console.WriteLine("Пароль не может быть пустым. Пожалуйста, введите корректный пароль.");
                                                    }
                                                }

                                                string name = "";
                                                while (string.IsNullOrWhiteSpace(name))
                                                {
                                                    Console.Write("Введите имя: ");
                                                    name = Console.ReadLine().Trim();

                                                    if (string.IsNullOrWhiteSpace(name))
                                                    {
                                                        Console.WriteLine("Имя не может быть пустым. Пожалуйста, введите корректное имя.");
                                                    }
                                                }

                                                string surname = "";
                                                while (string.IsNullOrWhiteSpace(surname))
                                                {
                                                    Console.Write("Введите фамилию: ");
                                                    surname = Console.ReadLine().Trim();

                                                    if (string.IsNullOrWhiteSpace(surname))
                                                    {
                                                        Console.WriteLine("Фамилия не может быть пустой. Пожалуйста, введите корректную фамилию.");
                                                    }
                                                }

                                                Console.WriteLine("Выберите статус пользователя:");
                                                Console.WriteLine("1. Клиент");
                                                Console.WriteLine("2. Менеджер");

                                                Console.Write("Введите номер статуса: ");
                                                string statusInput = Console.ReadLine();

                                                string status2 = "";
                                                switch (statusInput)
                                                {
                                                    case "1":
                                                        status2 = "Client";
                                                        break;
                                                    case "2":
                                                        status2 = "Manager";
                                                        break;
                                                    default:
                                                        Console.WriteLine("Некорректный выбор статуса. Пользователь не будет добавлен.");
                                                        break;
                                                }

                                                if (!string.IsNullOrEmpty(status))
                                                {
                                                    User newUser1 = new User
                                                    {
                                                        Login = login,
                                                        Password = password,
                                                        Name = name,
                                                        Surname = surname,
                                                        Status = status2
                                                    };

                                                    if (statusInput == "1")
                                                        clientManagement.AddClient(adminManagement.AddUser(newUser1));
                                                    else if (statusInput == "2")
                                                        managerManagement.AddManager(adminManagement.AddUser(newUser1));

                                                    Console.WriteLine("Пользователь добавлен.");
                                                }
                                                break;

                                            case "2":
                                                // Удаление пользователя
                                                Console.Write("Введите Id пользователя для удаления: ");
                                                if (int.TryParse(Console.ReadLine(), out int userId4))
                                                {
                                                    adminManagement.DeleteUser(userId4);
                                                    //  можно дописать удаление также с двух таблиц Client и Manager
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Некорректный ввод. Введите целочисленный ID пользователя.");
                                                }
                                                break;

                                            case "3":
                                                // Просмотр журнала
                                                DataTable journalTable = adminManagement.ViewTheJournal();
                                                adminManagement.DisplayTable(journalTable, "Журнал действий");
                                                break;

                                            case "4":
                                                // Просмотр списка менеджеров
                                                DataTable managersTable = adminManagement.ViewManagerList();
                                                adminManagement.DisplayTable(managersTable, "Список менеджеров");
                                                break;

                                            case "5":
                                                // Просмотр списка клиентов
                                                DataTable clientsTable = adminManagement.ViewClientList();
                                                adminManagement.DisplayTable(clientsTable, "Список клиентов");
                                                break;

                                            case "6":
                                                // Просмотр списка всех пользователей
                                                DataTable allUsersTable = adminManagement.GetAllUsers();
                                                adminManagement.DisplayTable(allUsersTable, "Список пользователей");
                                                break;

                                            case "0":
                                                return;

                                            default:
                                                Console.WriteLine("Некорректный ввод. Пожалуйста, выберите действие из списка.");
                                                break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Пользователь не найден.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Пользователь не найден.");
                        }

                        break;

                    case "2":

                        string login2 = "";
                        while (true)
                        {
                            Console.Write("Введите логин: ");
                            login2 = Console.ReadLine().Trim();

                            if (string.IsNullOrWhiteSpace(login2))
                            {
                                Console.WriteLine("Логин не может быть пустым. Пожалуйста, введите корректный логин.");
                            }
                            else
                            {
                                DataTable existingUser = userManagement.GetUserByLogin(login2);

                                if (existingUser.Rows.Count > 0)
                                {
                                    Console.WriteLine("Пользователь с таким логином уже существует. Выберите другой логин.");
                                }
                                else
                                {
                                    // Логин корректен и уникален, выходим из цикла
                                    break;
                                }
                            }
                        }

                        string password2 = "";
                        while (string.IsNullOrWhiteSpace(password2))
                        {
                            Console.Write("Введите пароль: ");
                            password2 = Console.ReadLine().Trim();

                            if (string.IsNullOrWhiteSpace(password2))
                            {
                                Console.WriteLine("Пароль не может быть пустым. Пожалуйста, введите корректный пароль.");
                            }
                        }

                        string name2 = "";
                        while (string.IsNullOrWhiteSpace(name2))
                        {
                            Console.Write("Введите имя: ");
                            name2 = Console.ReadLine().Trim();

                            if (string.IsNullOrWhiteSpace(name2))
                            {
                                Console.WriteLine("Имя не может быть пустым. Пожалуйста, введите корректное имя.");
                            }
                        }

                        string surname2 = "";
                        while (string.IsNullOrWhiteSpace(surname2))
                        {
                            Console.Write("Введите фамилию: ");
                            surname2 = Console.ReadLine().Trim();

                            if (string.IsNullOrWhiteSpace(surname2))
                            {
                                Console.WriteLine("Фамилия не может быть пустой. Пожалуйста, введите корректную фамилию.");
                            }
                        }

                        User newUser = new User
                        {
                            Login = login2,
                            Password = password2,
                            Name = name2,
                            Surname = surname2,
                            Status = "Client"
                        };

                        clientManagement.AddClient(userManagement.AddUser(newUser));
                        Console.WriteLine("Пользователь добавлен\n");

                        break;

                    case "3":
                        Console.WriteLine("Выход из программы.");
                        return;

                    default:
                        Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                        break;
                }
            }
        }

    }
}