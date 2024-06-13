// See https://aka.ms/new-console-template for more information

using AutoSQL_FOR_IS_20_03.SQL_Service;

Console.WriteLine("Создам базу данных и  таблицу юзер с  пользователями");
while (true)
{
    try
    {
        Metod();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
}

void Metod()
{
    string cs = GetConnetionString();

    Console.WriteLine("Укажите название  будуюшей БД");
    string nameDb = Console.ReadLine();


    try
    {
        ServiceSQL.CreateDateBase(nameDb, cs);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Такая база уже есть попробую создать в ней  таблицу User");
    }

    try
    {
        ServiceSQL.CreteTableUser(nameDb, cs);
    }
    catch (Exception ex)
    {
        Console.WriteLine("В этой базе уже есть  таблица User, попробую добавить пользователей туда");
    }
    
    for (int i = 1; i <= 10; i++)
    {
        ServiceSQL.AddUsersForTableUser(nameDb, cs, new User()
        {
            Login = "Login" + i,
            Name = "User" + i,
            Password = GenericUser.GetPass(5)
        }); 
    }
    Console.WriteLine($"Создано 10 юзеров  в  Таблице User Базы данных {nameDb}");

    foreach (var s in ServiceSQL.GetUsers(nameDb, cs)) 
        Console.WriteLine(s);

}

string GetConnetionString()
{
    Console.WriteLine("Введите адресс сервера");

    // string adress = "(localdb)\\mssqllocaldb"; //Console.ReadLine();

    string adress = Console.ReadLine();


    Console.WriteLine("Введите логин для SA");
    string login = Console.ReadLine();

    Console.WriteLine("Введите пароль для  SA");

    string pasword = Console.ReadLine();

    return $"server={adress};" +
        $"user id={login};" +
        $"password={pasword};" +
        $"trustservercertificate=true;";

}


