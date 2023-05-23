// See https://aka.ms/new-console-template for more information

using System.Text;


Console.WriteLine("Добро пожаловать на склад!\r\n");

commands();


static void commands()
{
    Console.WriteLine("Выберите пожалуйста действие:\r\n");
    Console.WriteLine("1 - Показать список продуктов");
    Console.WriteLine("2 - Добавить новый продукт");
    Console.WriteLine("3 - Продать продукт");
    int command = int.Parse(Console.ReadLine());

    switch (command)
    {
        case 0:
            //nothing
            break;
        case 1:
            //Показать список продуктов
            ListOfProducts();
            commands();
            break;
        case 2:
            //Добавить новый продукт

            Console.WriteLine("\r\nДобавление нового продукта!");

            Console.WriteLine("Введите название продукта");
            string name = Console.ReadLine();
            Console.WriteLine("Введите количество продукта");
            int count = int.Parse( Console.ReadLine());

            AddProduct(name,count);
            commands();
            break;
        case 3:
            //Продать продукт

            Console.WriteLine("\r\nПродажа продукта!");

            Console.WriteLine("Введите название продукта");
            string name2 = Console.ReadLine();
            Console.WriteLine("Введите количество продукта");
            int count2 = int.Parse(Console.ReadLine());
            sellProduct(name2,count2);
            commands();
            break;
        default:
            commands();
            break;
    }
}


static void ListOfProducts()
{
    string[] products = File.ReadAllLines("store.txt", Encoding.UTF8);
    for (int i = 0; i < products.Length; i++)
    {
        Console.WriteLine(products[i]);
    }
}


static void AddProduct(string newlyadded, int count)
{
    string[] products = File.ReadAllLines("store.txt", Encoding.UTF8);
    Boolean isNew = true;
    for (int i = 0; i < products.Length; i++)
    {
        string product = products[i];
        var splitted = product.Split(' ');

        string productName = splitted[0];
        int countOfProduct = int.Parse(splitted[1]);
        if (productName == newlyadded)
        {
            countOfProduct += count;
            isNew = false;
        }
        products[i] = $"{productName} {countOfProduct}";
    }

    if (isNew)
    {
        string[] added = new string[products.Length + 1];

        for (int i = 0; i < products.Length; i++)
        {
            added[i] = products[i];
        }
        added[products.Length] = newlyadded + " " + count ;

        File.WriteAllLines("store.txt", added);
    }
}

static void sellProduct(string newlyadded, int count)
{
    string[] products = File.ReadAllLines("store.txt", Encoding.UTF8);
    Boolean isSold = false;
    for (int i = 0; i < products.Length; i++)
    {
        string product = products[i];
        var splitted = product.Split(' ');

        string productName = splitted[0];
        int countOfProduct = int.Parse(splitted[1]);
        if (productName == newlyadded)
        {
            if (countOfProduct >= count)
            {
                countOfProduct -= count;
                isSold = true;
            }
        }
        products[i] = $"{productName} {countOfProduct}";

    }
    File.WriteAllLines("store.txt", products);
    if (isSold)
    {
        Console.WriteLine("Товар успешно продан.");
    }else {
        Console.WriteLine("Товар не продан!!!\n\rВозможно товар не существует на складе либо его количество меньше чем требуется!");
    }
}
