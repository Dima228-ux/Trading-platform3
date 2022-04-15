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
    public class TableHaggles
    {
        public bool InsertHaggles(int idUsers, int idBidding)
        {
            try
            {
                using (MySqlConnection mySqlConnection = DbConnection.GetConnection())
                {
                    mySqlConnection.Open();

                    using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
                    {
                        mySqlCommand.CommandText = $"INSERT haggle (id_users,id_bidding,admission_trading) " +
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

        public bool UpdateStatusUser(int idUsers, int idBidding)
        {
            try
            {
                using (MySqlConnection mySqlConnection = DbConnection.GetConnection())
                {
                    mySqlConnection.Open();

                    using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
                    {
                        mySqlCommand.CommandText = $"UPDATE haggle SET `admission_trading` = {true}" +
                            $" WHERE `id_users` = {idUsers} AND `id_bidding`={idBidding} ;";
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

        public  int DeleteHaggles(int id)
        {
            int updatedRowsCount=0;
            try
            {
                using (MySqlConnection mySqlConnection = DbConnection.GetConnection())
                {
                    mySqlConnection.Open();

                    using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
                    {
                        mySqlCommand.CommandText = $"DELETE FROM haggle WHERE id={id};";
                        updatedRowsCount = mySqlCommand.ExecuteNonQuery();
                    }
                }
                return updatedRowsCount;
            }
            catch (Exception e)
            {
                return updatedRowsCount;
            }
        }

        public List<Haggle> ShowApplications(int idUsers, int idBidding,int idCategory)
        {
            try
            {
                List<Haggle> haggles = new List<Haggle>();

                using (MySqlConnection mySqlConnection = DbConnection.GetConnection())
                {
                    mySqlConnection.Open();

                    using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
                    {
                        mySqlCommand.CommandText = $" SELECT haggle.id,haggle.id_users,haggle.id_bidding,haggle.admission_trading," +
                            $"users.name,users.surname,users.inn,users.name_company,bidding.title From haggle,bidding,users " +
                            $" Where haggle.id_users={idUsers} AND haggle.id_bidding={idBidding} AND haggle.admission_trading={false} " +
                            $" AND haggle.id_users=users.id AND haggle.id_bidding=bidding.id AND bidding.id_category={idCategory}";

                        using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                        {
                            while (mySqlDataReader.Read() == true)
                            {
                                haggles.Add(new Haggle
                                {
                                    Id = mySqlDataReader.GetInt32("id"),
                                    NameCompany = mySqlDataReader.GetString("name_company"),
                                    NameUser = mySqlDataReader.GetString("name"),
                                    TitleTender = mySqlDataReader.GetString("title"),
                                    Inn = mySqlDataReader.GetString("inn"),
                                    IdUsers = mySqlDataReader.GetInt32("id_users"),
                                    Surname = mySqlDataReader.GetString("surname"),
                                    IdBidding = mySqlDataReader.GetInt32("id_bidding"),
                                    AdmissionTrading=mySqlDataReader.GetBoolean("admission_trading")
                                });
                            }
                        }
                    }
                }

                return haggles;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }

}

