using System.Data;
using System.Data.SqlClient;


namespace AutoSQL_FOR_IS_20_03.SQL_Service
{

    /// <summary>
    /// Создает  пользователь базы данных и   backUpp
    /// </summary>
    public class ServiceSQL
    {
        public static void CreateUser(string user_name, string defDb , string pasword, string cs)
        {
            SqlConnection myConn = new SqlConnection(cs);

            var str = $"CREATE LOGIN [{user_name}] WITH PASSWORD=N'{pasword}', " +
                $"DEFAULT_DATABASE=[{defDb}] , " +
                $"CHECK_EXPIRATION=OFF, " +
                $"CHECK_POLICY=OFF " +
                $"USE[{defDb}]  " +
                $"CREATE USER[{user_name}]  FOR LOGIN[{user_name}]  " +
                $"USE [{defDb}] " +
                $"ALTER ROLE[db_owner] ADD MEMBER [{user_name}]  ";

            SqlCommand myCommand = new SqlCommand(str, myConn);

            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }

        /// <summary>
        /// Передать  имя бд  и  Connection to a SQL Server instance
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cs"></param>
        public static void CreateDateBase(string bd_name, string cs)
        {
            SqlConnection myConn = new SqlConnection(cs);

            var str = $"CREATE DATABASE {bd_name}";

            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }


        /// <summary>
        /// Передать  имя бд  и  Connection to a SQL Server instance
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cs"></param>
        public static string BackupDateBase(string bd_name, string dir , string cs)
        {
            SqlConnection myConn = new SqlConnection(cs);

            var data = DateTime.Now;
            var nameFale = $"{bd_name}-{data.Year}_{data.Month}_{data.Day}_{data.Hour}_{data.Minute}_{data.Second}.bac";

            var str = $"BACKUP DATABASE [{bd_name}] " +
                @$"TO  DISK = N'{dir}\{nameFale}' WITH NOFORMAT, NOINIT, " +
                $" NAME = N'{nameFale}', " +
                $"SKIP, NOREWIND, NOUNLOAD,  STATS = 10";

            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();
                return @$"Backup создался в директории -> {dir} -> название файла ->  {nameFale}";
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }

        public static void CreteTableUser(string nameDb, string cs)
        {
            SqlConnection myConn = new SqlConnection(cs);
            
            var str = $"USE {nameDb}\n " +
                $"CREATE TABLE [dbo].[User] " +
                $"(UserId int Identity(1,1), " +
                $"Name nvarchar(50) not null, " +
                $"Login nvarchar(50) not null, " +
                $"Password nvarchar(50) not null) ";

            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();
                
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }

        public static void AddUsersForTableUser(string nameDb, string cs , User  user)
        {
            SqlConnection myConn = new SqlConnection(cs);

            var str = $"USE {nameDb}\n " +
                $@"INSERT INTO [{nameDb}].[dbo].[User] (Name ,Login, Password ) VALUES ( '{user.Name}', 
                '{user.Login}', '{user.pas}')";
                SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }


        public static List<User>  GetUsers (string  nameDB , string cs)
        {
            List<User> strings = new List<User>();  

            SqlConnection myConn = new SqlConnection(cs);

            var str = $@"SELECT TOP (1000) [UserId]
                        ,[Name]
                        ,[Login]
                        ,[Password]
                        FROM [{nameDB}].[dbo].[User]";

            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();
                SqlDataReader reader = myCommand.ExecuteReader();
                if (reader!=null) 
                { 
                    while (reader.Read())
                    {
                        User us = new User();
                        us.Name =  reader.GetValue(1).ToString();
                        us.Login = reader.GetValue(2).ToString();
                        us.pas = reader.GetValue(3).ToString();
                        strings.Add(us);
                    }
                }
                return strings;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }

    }
}
