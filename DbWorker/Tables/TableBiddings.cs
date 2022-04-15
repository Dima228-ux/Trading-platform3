using DbEntities;
using DbWorker.Tools;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbWorker.Tables
{
    public class TableBiddings
    {
        public bool InsertBidding(string description, DateTime startDate, DateTime endDate, int idUsers, int idCategory, string title, int coast)
        {
            try
            {
                using (MySqlConnection mySqlConnection = DbConnection.GetConnection())
                {
                    mySqlConnection.Open();

                    using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
                    {
                        mySqlCommand.CommandText = $"INSERT bidding (description,start_date, end_date,id_users,id_category,title,coast) " +
                            $"VALUES('{description}','{startDate}','{endDate}',{idUsers},{idCategory},'{title}',{coast})";
                        mySqlCommand.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch
            {
                return false;

            }
        }

        public List<Bindding> GetMyBiddings(int idUser, int idCategory)
        {
            try
            {
                List<Bindding> binddings = new List<Bindding>();

                using (MySqlConnection mySqlConnection = DbConnection.GetConnection())
                {
                    mySqlConnection.Open();

                    using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
                    {
                        mySqlCommand.CommandText = $"SELECT * From bidding Where id_users={idUser} AND id_category={idCategory} ";

                        using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                        {
                            while (mySqlDataReader.Read() == true)
                            {
                                binddings.Add(new Bindding
                                {
                                    Id = mySqlDataReader.GetInt32("id"),
                                    Description = mySqlDataReader.GetString("description"),
                                    StartDate = mySqlDataReader.GetDateTime("start_date"),
                                    EndDate = mySqlDataReader.GetDateTime("end_date"),
                                    Coast = mySqlDataReader.GetInt32("coast"),
                                    IdUsers = mySqlDataReader.GetInt32("id_users"),
                                    Title = mySqlDataReader.GetString("title"),
                                    IdCategory = mySqlDataReader.GetInt32("id_category"),
                                    Company = null,

                                });
                            }
                        }
                    }
                }

                return binddings;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public List<Bindding> GetBiddings(int idCategory, DateTime endDate)
        {
            try
            {
                List<Bindding> binddings = new List<Bindding>();

                using (MySqlConnection mySqlConnection = DbConnection.GetConnection())
                {
                    mySqlConnection.Open();

                    using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
                    {
                        mySqlCommand.CommandText = $"SELECT  bidding.id, bidding.description,bidding.start_date,bidding.end_date," +
                            $"bidding.coast,bidding.id_users,bidding.title,users.name_company From bidding,users " +
                            $" Where NOW()>='{endDate}' AND id_category={idCategory} AND bidding.id_users=users.id  ";

                        using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                        {
                            while (mySqlDataReader.Read() == true)
                            {
                                binddings.Add(new Bindding
                                {
                                    Id = mySqlDataReader.GetInt32("id"),
                                    Description = mySqlDataReader.GetString("description"),
                                    StartDate = mySqlDataReader.GetDateTime("start_date"),
                                    EndDate = mySqlDataReader.GetDateTime("end_date"),
                                    Coast = mySqlDataReader.GetInt32("coast"),
                                    IdUsers = mySqlDataReader.GetInt32("id_users"),
                                    Company = mySqlDataReader.GetString("name_company"),
                                    Title = mySqlDataReader.GetString("title"),
                                    IdCategory = mySqlDataReader.GetInt32("id_category")
                                });
                            }
                        }
                    }
                }

                return binddings;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public int DeleteBidding(int id)
        {
            int updatedRowsCount;
            try
            {
                using (MySqlConnection mySqlConnection = DbConnection.GetConnection())
                {
                    mySqlConnection.Open();

                    using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
                    {
                        mySqlCommand.CommandText = $"DELETE FROM bidding WHERE id={id};";
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

        public bool UpdateBidding(int id, string description, DateTime startDate, DateTime endDate, int idUsers, int idCategory, string title, int coast)
        {
            try
            {
                using (MySqlConnection mySqlConnection = DbConnection.GetConnection())
                {
                    mySqlConnection.Open();

                    using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
                    {
                        mySqlCommand.CommandText = $"UPDATE bidding SET description='{description}',start_dat='{startDate}', end_date='{endDate}',id_users={idUsers}," +
                            $"id_category={idCategory},title='{title}',coast={coast} WHERE id={id};";

                        mySqlCommand.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch
            {
                return false;

            }
        }

        public List<Bindding> ShowMyBiddings(int idCategory, int idUsers, DateTime endDate)
        {
            try
            {
                List<Bindding> binddings = new List<Bindding>();

                using (MySqlConnection mySqlConnection = DbConnection.GetConnection())
                {
                    mySqlConnection.Open();

                    using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
                    {
                        mySqlCommand.CommandText = $"SELECT  bidding.id, bidding.description,bidding.start_date,bidding.end_date," +
                            $"bidding.coast,bidding.id_users,bidding.title,users.name_company From bidding,users,haggle " +
                            $" Where NOW()>='{endDate}' AND id_category={idCategory} AND bidding.id_users=users.id " +
                            $" AND haggle.id_users={idUsers} AND haggle.admission_trading={true} AND bidding.id=haggle.id_bidding ";

                        using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                        {
                            while (mySqlDataReader.Read() == true)
                            {
                                binddings.Add(new Bindding
                                {
                                    Id = mySqlDataReader.GetInt32("id"),
                                    Description = mySqlDataReader.GetString("description"),
                                    StartDate = mySqlDataReader.GetDateTime("start_date"),
                                    EndDate = mySqlDataReader.GetDateTime("end_date"),
                                    Coast = mySqlDataReader.GetInt32("coast"),
                                    IdUsers = mySqlDataReader.GetInt32("id_users"),
                                    Company = mySqlDataReader.GetString("name_company"),
                                    Title = mySqlDataReader.GetString("title"),
                                    IdCategory = mySqlDataReader.GetInt32("id_category")
                                });
                            }
                        }
                    }
                }

                return binddings;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
