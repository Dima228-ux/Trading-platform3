using DbEntities;
using DbWorker.Tools;
using ExceptionEntities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbWorker.Tables
{
    public class TableUsers
    {
        public User GetUserByLoginPassword(string login, string password)
        {
            try
            {
                User user = null;

                using (MySqlConnection mySqlConnection = DbConnection.GetConnection())
                {
                    mySqlConnection.Open();

                    using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
                    {
                        mySqlCommand.CommandText = $"SELECT * FROM `users` WHERE `login`='{login}' AND `password`='{password}';";

                        using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                        {
                            if (mySqlDataReader.HasRows == true)
                            {
                                mySqlDataReader.Read();

                                user = new User
                                {
                                    Id = mySqlDataReader.GetInt32("id"),
                                    Login = mySqlDataReader.GetString("login"),
                                    Password = mySqlDataReader.GetString("password"),
                                    NameCompany = mySqlDataReader.GetString("name_company"),
                                    Name = mySqlDataReader.GetString("name"),
                                    Inn = mySqlDataReader.GetString("inn"),
                                    Surname = mySqlDataReader.GetString("surname")

                                };
                            }
                            else
                            {
                                throw new WrongLoginPasswordException();
                            }
                        }
                    }
                }

                return user;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void RegistedNewUser(string login, string password, string inn, string name, string surname, string nameCompany)
        {
            try
            {
                using (MySqlConnection mySqlConnection = DbConnection.GetConnection())
                {
                    mySqlConnection.Open();

                    using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
                    {
                        mySqlCommand.CommandText = $"INSERT users(login,password,inn,name,surname,nameCompany) VALUES('{login}','{password}','{inn}','{name}','{surname}','{nameCompany}')";
                        mySqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool CheckLoginUser(string login)
        {

            using (MySqlConnection mySqlConnection = DbConnection.GetConnection())
            {
                mySqlConnection.Open();

                using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
                {
                    mySqlCommand.CommandText = $"SELECT * FROM `users` WHERE `login`='{login}';";

                    using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                    {
                        if (mySqlDataReader.HasRows == true)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
        }

        public int UpdateUser(int id, string login, string password, string inn, string name, string surname, string nameCompany)
        {
            int updatedRowsCount;
            try
            {
                using (MySqlConnection mySqlConnection = DbConnection.GetConnection())
                {
                    mySqlConnection.Open();

                    using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
                    {
                        mySqlCommand.CommandText = $"UPDATE users SET login='{login}',password='{password}',inn='{inn}',name='{name}',surname='{surname}',nameCompany='{nameCompany}' WHERE id={id};";
                        updatedRowsCount = mySqlCommand.ExecuteNonQuery();
                    }
                }
                return updatedRowsCount;
            }
            catch (Exception e)
            {
                throw new WrongLoginPasswordException();
            }

        }

        public int DeleteUser(int idUser)
        {
            if (DeleteSpecificUser(idUser) > 0 && DeleteBiddingUser(idUser) > 0 && DeleteHaggleUser(idUser) > 0)
                return DeleteWinningBiddingUser(idUser);
            return 0;

        }

        private static int DeleteSpecificUser(int id)
        {
            int updatedRowsCount;
            try
            {
                using (MySqlConnection mySqlConnection = DbConnection.GetConnection())
                {
                    mySqlConnection.Open();

                    using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
                    {
                        mySqlCommand.CommandText = $"DELETE FROM users WHERE id={id};";
                        updatedRowsCount = mySqlCommand.ExecuteNonQuery();
                    }
                }
                return updatedRowsCount;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private static int DeleteBiddingUser(int idUser)
        {
            int updatedRowsCount;
            try
            {
                using (MySqlConnection mySqlConnection = DbConnection.GetConnection())
                {
                    mySqlConnection.Open();

                    using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
                    {
                        mySqlCommand.CommandText = $"DELETE FROM bidding WHERE id_users={idUser};";
                        updatedRowsCount = mySqlCommand.ExecuteNonQuery();
                    }
                }
                return updatedRowsCount;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private static int DeleteHaggleUser(int idUser)
        {
            int updatedRowsCount;
            try
            {
                using (MySqlConnection mySqlConnection = DbConnection.GetConnection())
                {
                    mySqlConnection.Open();

                    using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
                    {
                        mySqlCommand.CommandText = $"DELETE FROM haggle WHERE id_users={idUser};";
                        updatedRowsCount = mySqlCommand.ExecuteNonQuery();
                    }
                }
                return updatedRowsCount;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private static int DeleteWinningBiddingUser(int idUser)
        {
            int updatedRowsCount;
            try
            {
                using (MySqlConnection mySqlConnection = DbConnection.GetConnection())
                {
                    mySqlConnection.Open();

                    using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
                    {
                        mySqlCommand.CommandText = $"DELETE FROM winning_bidding WHERE id_users={idUser};";
                        updatedRowsCount = mySqlCommand.ExecuteNonQuery();
                    }
                }
                return updatedRowsCount;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public List<User> ShowParticipants(int idCategory, int idBidding)
        {
            try
            {
                List<User> users = new List<User>();

                using (MySqlConnection mySqlConnection = DbConnection.GetConnection())
                {
                    mySqlConnection.Open();

                    using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
                    {
                        mySqlCommand.CommandText = $"SELECT  users.id, users.login,users.password," +
                            $"users.name,users.inn,users.surname,users.name_company From users,haggle " +
                            $" Where users.id_category={idCategory} AND haggle.admission_trading={true} " +
                            $" AND haggle.id_bidding={idBidding} AND users.id=haggle.id_users ";

                        using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                        {
                            while (mySqlDataReader.Read() == true)
                            {
                                users.Add(new User
                                {
                                    Id = mySqlDataReader.GetInt32("id"),
                                    Login = mySqlDataReader.GetString("login"),
                                    Password = mySqlDataReader.GetString("password"),
                                    NameCompany = mySqlDataReader.GetString("name_company"),
                                    Name = mySqlDataReader.GetString("name"),
                                    Inn = mySqlDataReader.GetString("inn"),
                                    Surname = mySqlDataReader.GetString("surname")
                                });
                            }
                        }
                    }
                }

                return users;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}

