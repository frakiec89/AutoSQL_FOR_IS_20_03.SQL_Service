using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSQL_FOR_IS_20_03.SQL_Service
{
    public class GenericUser
    {

        /// <summary>
        /// Создает пользователей  с базами данных 
        /// </summary>
        /// <param name="pref">Префикс пользователя</param>
        /// <param name="prefDb">Префикс  базы данныъ по  умолчанию </param>
        /// <param name="count">кол-во  пользователей </param>
        /// <param name="lengthPass">длинна параля </param>
        /// <param name="cs">строка соединнения  с сервером</param>
        /// <returns></returns>
        public static List<string> Add(string? pref,string prefDb, int count, int lengthPass , string cs)
        {

            try
            {
                List<string> list = new List<string>();
                for (int i = 1; i <= count; i++)
                {
                    string us = pref + i;
                    string pass = GetPass(lengthPass);
                    string db = prefDb + i;

                    ServiceSQL.CreateDateBase(db, cs); // создаим бд
                    ServiceSQL.CreateUser(us, db, pass, cs);// создадим пользователя

                    list.Add($"Пользователь:{us};База данных по умолчанию {db};пароль:{pass}");
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
      

        }


        public static string GetPass(int lengthPass)
        {
            int[] arr = new int[lengthPass]; // сделаем длину пароля в lengthPass символов
            Random rnd = new Random();
            string Password = "";

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rnd.Next(33, 125);
                Password += (char)arr[i];
            }
            return Password;
        }
    }
}
