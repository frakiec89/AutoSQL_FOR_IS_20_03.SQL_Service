// See https://aka.ms/new-console-template for more information
using AutoSQL_FOR_IS_20_03.SQL_Service;

while (true)
{
    try
    {
        Back();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }
}


void Back()
{
    Console.WriteLine("Привет  я  сделаю  бек апп базы данных");

    string cs = GetConnetionString();

    Console.WriteLine("Введите название базы данных которому надо  сделать  бек апп");

    string namaDb = Console.ReadLine();

    Console.WriteLine(ServiceSQL.BackupDateBase(namaDb, cs));
    
}



string GetConnetionString()
{
    Console.WriteLine("Введите адресс сервера");

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