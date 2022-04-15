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
    public class TableWinningBiddings
    {
        public bool InsertWinningBidding(int idUsers, int idBidding)
        {
            try
            {
                using (MySqlConnection mySqlConnection = DbConnection.GetConnection())
                {
                    mySqlConnection.Open();

                    using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
                    {
                        mySqlCommand.CommandText = $"INSERT winning_bidding (id_users,id_bidding, executed) " +
                            $"VALUES({idUsers},{idBidding},{false})";
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

        public bool UpdateStatus(int idUser, int idBidding)
        {
            try
            {
                using (MySqlConnection mySqlConnection = DbConnection.GetConnection())
                {
                    mySqlConnection.Open();

                    using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
                    {
                        mySqlCommand.CommandText = $"UPDATE winning_bidding SET `executed` = {true}" +
                            $" WHERE `id_users` = {idUser} AND `id_bidding`={idBidding} ;";
                        mySqlCommand.ExecuteNonQuery();

                    }
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<WiningBidding> SelectAllWiningBiddings(int idUsers, int idBidding,int idCategory)
        {
            try
            {
                List<WiningBidding> winingBiddings = new List<WiningBidding>();

                using (MySqlConnection mySqlConnection = DbConnection.GetConnection())
                {
                    mySqlConnection.Open();

                    using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
                    {
                        mySqlCommand.CommandText = $"SELECT winning_bidding.id,winning_bidding.id_users,winning_bidding.id_bidding ," +
                            $"winning_bidding.executed,users.name_company,bidding.title,bidding.description,bidding.coast From winning_bidding,bidding,users " +
                            $" Where winning_bidding.id_users={idUsers} AND bidding.id_category={idCategory} " +
                            $"AND winning_bidding.id_bidding={idBidding} AND bidding.id_users=users.id  " +
                            $" AND winning_bidding.id_bidding=bidding.id ";
                        
                        using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                        {
                            while (mySqlDataReader.Read() == true)
                            {
                                winingBiddings.Add(new WiningBidding
                                {
                                    Id = mySqlDataReader.GetInt32("id"),
                                    NameCompany = mySqlDataReader.GetString("name_company"),
                                    Description = mySqlDataReader.GetString("description"),
                                    TitleBindding = mySqlDataReader.GetString("title"),
                                    Coast = mySqlDataReader.GetInt32("coast"),
                                    IdUsers = mySqlDataReader.GetInt32("id_users"),
                                    IdBindding = mySqlDataReader.GetInt32("id_bidding"),
                                    Executed = mySqlDataReader.GetBoolean("executed")
                                });
                            }
                        }
                    }
                }

                return winingBiddings;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
