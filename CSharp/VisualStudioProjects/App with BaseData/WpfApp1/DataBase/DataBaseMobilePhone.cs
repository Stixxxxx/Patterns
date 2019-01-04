using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.DataBase
{
    class DataBaseMobilePhone
    {
        
        string ConectionDataBaseAuth()
        {

            string ConectionDataBaseAuthString = "Server = localhost; Port = 3306; Database = wpfapp1; Uid = root; Pwd = My_Fa14730d;";
            return ConectionDataBaseAuthString;

        }


        string CreateDataBaseAuth()
        {

            string CreateDataBaseAuthString = "CREATE TABLE `wpfapp1`.`mobile_phone` (`Id` INT NOT NULL AUTO_INCREMENT, `model` VARCHAR(45) NOT NULL, `manufacturer` VARCHAR(45) NOT NULL, `price` VARCHAR(45) NOT NULL, PRIMARY KEY(`Id`));";
            return CreateDataBaseAuthString;

        }


        public void Init()
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
            catch (Exception e)
            {

                connection.Close();
                string error = e.Message;

            }

        }

        public List<MobilePhone> GetDataBase()
        {

            MobilePhone data;

            List<MobilePhone> ListMobilePhone = new List<MobilePhone>();

            string getData = "SELECT * FROM mobile_phone;";

            MySqlConnection connection = new MySqlConnection(); //создаем соединение                 
            connection.ConnectionString = ConectionDataBaseAuth();
            connection.Open(); //соединяемся 
            try
            {
                MySqlCommand command = new MySqlCommand(); //класс для передачи команд в базу
                command.Connection = connection;// указываем через какое соединение

                command.CommandText = getData;
                var reader = command.ExecuteReader(); //выполнить команду


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data = new MobilePhone();
                        data.id = reader.GetInt32(0);
                        data.model = reader.GetString(1);
                        data.manufacturer = reader.GetString(2);
                        data.price = reader.GetString(3);
                        ListMobilePhone.Add(data);          
                        
                    }

                  

                }
                else
                {
                    Console.WriteLine("No rows found.");
                }


                connection.Close();

                return ListMobilePhone;
            }
                 

            catch (Exception e)
            {

                connection.Close();
                string error = e.Message;

                return null;

            }

            

        }

        public bool Add(string name, string manufacturer, string price)
        {
                        

            string addData = "INSERT INTO `wpfapp1`.`mobile_phone` (`model`, `manufacturer`, `price`) VALUES ('"+ name + "', '"+ manufacturer + "', '"+ price + "');";

            MySqlConnection connection = new MySqlConnection(); //создаем соединение                 
            connection.ConnectionString = ConectionDataBaseAuth();
            connection.Open(); //соединяемся 
            try
            {
                MySqlCommand command = new MySqlCommand(); //класс для передачи команд в базу
                command.Connection = connection;// указываем через какое соединение

                command.CommandText = addData;
                command.ExecuteNonQuery();

                connection.Close();

                return true;
            }


            catch (Exception e)
            {

                connection.Close();
                string error = e.Message;

                return false;

            }



        }

        public bool Remove(int idMoblePhone)
        {
            
            string Removedata = "DELETE FROM `mobile_phone` WHERE `id`='" + idMoblePhone + "'";

            MySqlConnection connection = new MySqlConnection(); //создаем соединение                 
            connection.ConnectionString = ConectionDataBaseAuth();
            connection.Open(); //соединяемся 
            try
            {
                MySqlCommand command = new MySqlCommand(); //класс для передачи команд в базу
                command.Connection = connection;// указываем через какое соединение

                command.CommandText = Removedata;
                command.ExecuteNonQuery();

                connection.Close();

                return true;
            }


            catch (Exception e)
            {

                connection.Close();
                string error = e.Message;

                return false;

            }



        }


    }
}