using PakClassified;
using PakClassified.Entities.PakClassified;
using PakClassified.Entities.UserEntities;

//using PakClassified.Handlers.;
using PakClassified.Handlers.AdvertisementHandler;
using PakClassified.Handlers.AdvertisementHandler.SubCategoryHandler;



void SignIn()
{
    Console.Clear();
    Console.WriteLine("Login your Account\n");
    Console.Write("Enter Username: ");
    string? userName = Console.ReadLine();
    Console.Write("Enter Password: ");
    string? Password = Console.ReadLine();
    Thread.Sleep(1000);
    Console.WriteLine("login in ...");
    Thread.Sleep(2000);
    Console.WriteLine("Login Was Successfull.");
    Console.WriteLine("Redirecting to application....");
    Thread.Sleep(2500);
}
void Register()
{
    Console.Clear();
    Console.WriteLine("Resgiter New User\n");
    Console.Write("Enter Username: ");
    string? userName = Console.ReadLine();
    Console.Write("Enter Email: ");
    string? Email = Console.ReadLine();
    Console.Write("Enter Password: ");
    string? Password = Console.ReadLine();
    Thread.Sleep(1000);
    Console.WriteLine("Registering new user...");
    Thread.Sleep(2000);
    Console.WriteLine("User Was Successfully Registered.");
    Console.WriteLine("Redirecting to application....");
    Thread.Sleep(2500);
}

void GetEntity()
{
    Console.Clear();
    bool isContinue = true;
    while (isContinue)
    {
        Console.Clear();
        Console.WriteLine("\t\tGet or Display All Entities\n");
        Console.WriteLine("Please Choose option To continue: ");
        Console.WriteLine("1. Get CityArea");
        Console.WriteLine("2. Get City");
        Console.WriteLine("3. Get Country");
        Console.WriteLine("4. Get Country");
        Console.WriteLine("5. Back to Main Menu.");
        Console.Write("Please select an option: ");
        int? input = int.Parse(Console.ReadLine());
        switch (input)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                isContinue = false;
                break;
        }
    }
}

void GetEntityById()
{
    Console.Clear();
    bool isContinue = true;
    while (isContinue)
    {
        Console.Clear();
        Console.WriteLine("\t\tGet or Display Entity By Id.\n");
        Console.WriteLine("Please Choose option To continue: ");
        Console.WriteLine("1. Get CityArea By ID");
        Console.WriteLine("2. Get City By ID");
        Console.WriteLine("3. Get Country By ID");
        Console.WriteLine("4. Get Country By ID");
        Console.WriteLine("5. Back to Main Menu.");
        Console.Write("Please select an option: ");
        int? input = int.Parse(Console.ReadLine());
        switch (input)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                isContinue = false;
                break;
        }
    }
}

void AddEntity()
{
    Console.Clear();
    bool isContinue = true;
    while (isContinue)
    {
        Console.Clear();
        Console.WriteLine("\t\tAdd New Entity.\n");
        Console.WriteLine("Please Choose option To continue: ");
        Console.WriteLine("1. Add CityArea.");
        Console.WriteLine("2. Add City.");
        Console.WriteLine("3. Add Provnice.");
        Console.WriteLine("4. Add Country.");
        Console.WriteLine("5. Back to Main Menu.");
        Console.Write("Please select an option: ");
        int? input = int.Parse(Console.ReadLine());
        switch (input)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                isContinue = false;
                break;
        }
    }
}

void UpdateEntity()
{
    Console.Clear();
    bool isContinue = true;
    while (isContinue)
    {
        Console.Clear();
        Console.WriteLine("\t\tUpdate Existing Entity.\n");
        Console.WriteLine("Please Choose option To continue: ");
        Console.WriteLine("1. Update CityArea.");
        Console.WriteLine("2. Update City.");
        Console.WriteLine("3. Update Provnice.");
        Console.WriteLine("4. Update Country.");
        Console.WriteLine("5. Back to Main Menu.");
        Console.Write("Please select an option: ");
        int? input = int.Parse(Console.ReadLine());
        switch (input)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                isContinue = false;
                break;
        }
    }
}

void DeleteEntity()
{
    Console.Clear();
    bool isContinue = true;
    while (isContinue)
    {
        Console.Clear();
        Console.WriteLine("\t\tDelete Entity.\n");
        Console.WriteLine("Please Choose option To continue: ");
        Console.WriteLine("1. Delete CityArea.");
        Console.WriteLine("2. Delete City.");
        Console.WriteLine("3. Delete Provnice.");
        Console.WriteLine("4. Delete Country.");
        Console.WriteLine("5. Back to Main Menu.");
        Console.Write("Please select an option: ");
        int? input = int.Parse(Console.ReadLine());
        switch (input)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                isContinue = false;
                break;
        }
    }
}

void CRUD()
{
    Console.Clear();
    bool isContinue = true;
    while (isContinue)
    {
        Console.Clear();
        Console.WriteLine("=========Welcome to PakClassified=========\n");
        Console.WriteLine("Please Choose option To continue: ");
        Console.WriteLine("1. Get Entity");
        Console.WriteLine("2. Get Entity By Id");
        Console.WriteLine("3. Add Entity"); 
        Console.WriteLine("4. Update Entity");
        Console.WriteLine("5. Delete Entity");
        Console.WriteLine("6. Sign Out");
        Console.Write("Please select an option: ");
        int? input = int.Parse(Console.ReadLine());
        switch (input)
        {
            case 1:
                Console.WriteLine("\nFetching entities, please wait...");
                Thread.Sleep(2500);
                GetEntity();
                break;
            case 2:
                Console.WriteLine("\nPreparing to fetch entity by ID, please wait...");
                Thread.Sleep(2500);
                GetEntityById();
                break;
            case 3:
                Console.WriteLine("\nLoading add entity options, please wait...");
                Thread.Sleep(2500);
                AddEntity();
                break;
            case 4:
                Console.WriteLine("\nLoading update entity options, please wait...");
                Thread.Sleep(2000);
                UpdateEntity();
                break;
            case 5:
                Console.WriteLine("\nLoading delete entity options, please wait...");
                Thread.Sleep(2000);
                DeleteEntity();
                break;
            case 6:
                Console.WriteLine("\nSigning out...");
                Thread.Sleep(2000);
                isContinue = false;
                break;
        }

    }

}

bool isContinue = true;
while (isContinue)
{
    Console.Clear();
    Console.WriteLine("=========Welcome to PakClassified=========\n");
    Console.WriteLine("Please Login or Register To continue: ");
    Console.WriteLine("1. Register");
    Console.WriteLine("2. Login");
    Console.WriteLine("3. Exit");
    Console.Write("Please select an option: ");
    string? input = Console.ReadLine();
    switch (input)
    {
        case "1":
            Register();
            CRUD();
            break;
        case "2":
            SignIn();
            CRUD();
            break;
        case "3":
            isContinue = false;
            break;
        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }
}

Console.Write("Press any key to Continue...");
Console.ReadKey();




//User user = new User
//{
//    Id = 1,
//    Name = "ALi",
//    DateOfBirth = Convert.ToDateTime("2002-09-11"),



//};
//Advertisement advertisement = new Advertisement 
//{
//    Id = 1,
//    Name ="Adv1",
//    Description="Description",
//    Hits = 0,
//};

//AdvertisementSubCategory advertisementSubCategory = new AdvertisementSubCategory();
//SubCategoryHandler subCategoryHandler = new SubCategoryHandler
//{

//};

//foreach (var item in subCategoryHandler.GetAll())
//{
//Console.WriteLine("Item Name: " + item.Name);
//Console.WriteLine("Item Category Id: " + item.CategoryId);
//Console.WriteLine("Item Ctaegory Name: " + item.Category.Name);
//Console.WriteLine("Itesm Category Description: " + item.Category.Description);
//Console.WriteLine("------------------------");
//}

//AdvertisementSubCategory item = subCategoryHandler.GetById(2);
//if (item != null)
//{
//    Console.WriteLine("Item Name: " + item.Name);
//    Console.WriteLine("Item Category Id: " + item.CategoryId);
//    Console.WriteLine("Item Ctaegory Name: " + item.Category.Name);
//    Console.WriteLine("Itesm Category Description: " + item.Category.Description);
//    Console.WriteLine("------------------------");
//}
//else
//{
//    Console.WriteLine("Item Not Found, Incorrect ID");
//}

//AdvertisementSubCategory request = new AdvertisementSubCategory
//{
//    Name = "Huawei",
//    CategoryId = 3
//};

//AdvertisementSubCategory itemU = subCategoryHandler.Update(2, request);
//Console.WriteLine("After --------");
//if (itemU != null)
//{
//    Console.WriteLine("Item Name: " + itemU.Name);
//    Console.WriteLine("Item Category Id: " + itemU.CategoryId);
//    Console.WriteLine("Item Ctaegory Name: " + itemU.Category.Name);
//    Console.WriteLine("Itesm Category Description: " + itemU.Category.Description);
//    Console.WriteLine("------------------------");
//}

//subCategoryHandler.SoftDelete(7);

//subCategoryHandler.Delete(6);

//AdvertisementSubCategory item = subCategoryHandler.GetById(6);
//if (item != null)
//{
//    Console.WriteLine(item.Name);
//    Console.WriteLine(item.CategoryId);
//    Console.WriteLine(item.Category.Name);
//    Console.WriteLine(item.Category.Description);
//}
//else
//{
//    Console.WriteLine("NOT FOUND!!");
//}

//AdvertisementSubCategory advertisementSubCategory1 = new AdvertisementSubCategory
//{
//    Name = "Honda Cra New model",
//    CategoryId = 7
//};

//AdvertisementSubCategory advSubCat = subCategoryHandler.Create(advertisementSubCategory1);
//if (advSubCat != null)
//    Console.WriteLine("Created");


//Console.Write("Press any key to Continue...");
//Console.ReadKey();
