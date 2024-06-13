// See https://aka.ms/new-console-template for more information

using AutoSQL_FOR_IS_20_03.SQL_Service;
while (true)
{
    try
    {
        AddUsers();
    }
    catch (Exception ex)
    {
       Console.WriteLine(ex.ToString());
       continue;
    }
}




void AddUsers()
{
    Console.WriteLine("создаем пользователей");
    string cs = GetConnetionString();


    Console.WriteLine("Введите префекс логина (например User)");
    string pref = Console.ReadLine();
    Console.WriteLine("Введите префекс базы данных по  умолчанию  (например DB)");
    string prefDb = Console.ReadLine();


    //лень  делать  проверки  поэтому вежлево  попросим 
    Console.WriteLine("Введите кол-во  пользователей (Пожайлуста целое число  больше нуля)");
    int count = Convert.ToInt32(Console.ReadLine());

    //лень  делать  проверки  поэтому вежлево  попросим 
    Console.WriteLine("Введите длинну пароля (Пожайлуста целое число  больше нуля)");
    int lengthPass = Convert.ToInt32(Console.ReadLine());

    var list = GenericUser.Add(pref, prefDb, count, lengthPass, cs);
    Console.WriteLine("Результат:");

    foreach (var item in list)
        Console.WriteLine(item);
    Console.WriteLine("_____________END___________");
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