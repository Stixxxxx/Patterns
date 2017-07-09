using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Dialogs;

namespace WpfApp1.DataBase
{
    class DataBaseAuth
    {


        string ConectionDataBaseAuth()
        {

            string ConectionDataBaseAuthString = "Server = localhost; Port = 3306; Database = wpfapp1; Uid = root; Pwd = My_Fa14730d;";
            return ConectionDataBaseAuthString;

        }


        string CreateDataBaseAuth()
        {

            string CreateDataBaseAuthString = "CREATE TABLE `wpfapp1`.`auth` ( `Id` INT NOT NULL AUTO_INCREMENT, `Login` VARCHAR(45) NULL, `Password` VARCHAR(45) NULL, PRIMARY KEY(`Id`)) ENGINE = InnoDB;";
            return CreateDataBaseAuthString;

        }


        public  void Init()
        {
            
            MySqlConnection connection = new MySqlConnection(); //создаем соединение                 
            connection.ConnectionString = ConectionDataBaseAuth();
            connection.Open(); //соединяемся 
            try
            {
            MySqlCommand command = new MySqlCommand(); //класс для передачи команд в базу
            command.Connection = connection;// указываем через какое соединение

            command.CommandText = CreateDataBaseAuth();
            command.ExecuteNonQuery(); //выполнить команду


           }
            catch (Exception e) {

            connection.Close();
            string  error = e.Message;
                
            }

 

        }

        public void Registration(string Login, string Password)
        {
            bool UserExists = false;

            bool UserExist = TryUser(Login);

            if(!UserExist)
            { 



            string RegistrationString = "INSERT INTO `wpfapp1`.`auth` (`Login`, `Password`) VALUES ('"+ Login + "', '" + Password + "');";

            MySqlConnection connection = new MySqlConnection(); //создаем соединение                 
            connection.ConnectionString = ConectionDataBaseAuth();
            connection.Open(); //соединяемся 
            try
            {
                MySqlCommand command = new MySqlCommand(); //класс для передачи команд в базу
                command.Connection = connection;// указываем через какое соединение

                command.CommandText = RegistrationString;
                command.ExecuteNonQuery(); //выполнить команду


            }
            catch (Exception e)
            {

                connection.Close();
                string error = e.Message;

            }
            connection.Close();
                
            }

            else
            {

                ErrorWindow Error = new ErrorWindow();
                Error.Show();
            }

        }

        public bool TryUser(string Login)
        {
            //SELECT * FROM `auth` WHERE `Login`='Stix' AND `Password` = '123';

            string AuthorisationString = "SELECT* FROM `auth` WHERE `Login`= '"+ Login + "';";

            MySqlConnection connection = new MySqlConnection(); //создаем соединение                 
            connection.ConnectionString = ConectionDataBaseAuth();
            connection.Open(); //соединяемся 
            try
            {
                MySqlCommand command = new MySqlCommand(); //класс для передачи команд в базу
                command.Connection = connection;// указываем через какое соединение

                command.CommandText = AuthorisationString;

                int countRows = (int)command.ExecuteScalar();

                command.ExecuteNonQuery(); //выполнить команду


            }
            catch (Exception e)
            {

                connection.Close();
                string error = e.Message;



                return false;

            }

            connection.Close();

            return true;


        }


        public bool Authorisation(string Login, string Password)
        {
            //SELECT * FROM `auth` WHERE `Login`='Stix' AND `Password` = '123';
            
            string AuthorisationString = "SELECT* FROM `auth` WHERE `Login`= '"+Login+"' AND `Password` = '" +Password+"';";

            MySqlConnection connection = new MySqlConnection(); //создаем соединение                 
            connection.ConnectionString = ConectionDataBaseAuth();
            connection.Open(); //соединяемся 
            try
            {
                MySqlCommand command = new MySqlCommand(); //класс для передачи команд в базу
                command.Connection = connection;// указываем через какое соединение

                command.CommandText = AuthorisationString;

                int countRows = (int)command.ExecuteScalar();

                command.ExecuteNonQuery(); //выполнить команду


            }
            catch (Exception e)
            {

                connection.Close();
                string error = e.Message;

                

                return false;

            }

            connection.Close();

            return true;


        }





    }
}
