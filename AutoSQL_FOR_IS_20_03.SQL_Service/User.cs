namespace AutoSQL_FOR_IS_20_03.SQL_Service
{
    public class User
    {
        public string Name { get; set; }
        public string Login { get; set; }
        internal string pasForDB; 
        public string Password { get => GetPass(pasForDB); set=> pasForDB = SetPass(value); }

        private string SetPass(string value)
        {
            return CryptoService.CryptokeyText(value , "Shifr");
        }

        private string GetPass(string pas)
        {
            return CryptoService.DeCryptokeyText(pas , "Shifr");
        }

        public override string ToString()
        {
            return $"Имя:{Name}, Login: {Login}, Пароль: {Password}";
        }

    }
}