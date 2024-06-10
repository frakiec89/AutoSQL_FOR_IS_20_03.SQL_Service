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
        public static string BackupDateBase(string bd_name, string cs)
        {
            SqlConnection myConn = new SqlConnection(cs);

            var data = DateTime.Now;
            var nameFale = $"{bd_name}-{data.Year}_{data.Month}_{data.Day}_{data.Hour}_{data.Minute}_{data.Second}.bac";

            var str = $"BACKUP DATABASE [{bd_name}] " +
                @$"TO  DISK = N'C:\Users\SA\{nameFale}' WITH NOFORMAT, NOINIT, " +
                $" NAME = N'{nameFale}', " +
                $"SKIP, NOREWIND, NOUNLOAD,  STATS = 10";



            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();
                return @$"Backup создался в директории -> C:\Users\SA -> название файла ->  {nameFale}";
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
