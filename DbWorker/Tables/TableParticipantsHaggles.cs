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
    public class TableParticipantsHaggles
    {
        public bool InsertParticipantsHaggles(int idUsers, int idBidding, string nameCompany)
        {
            try
            {
                using (MySqlConnection mySqlConnection = DbConnection.GetConnection())
                {
                    mySqlConnection.Open();

                    using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
                    {
                        mySqlCommand.CommandText = $"INSERT participants_haggle (id_users,id_bidding, name_company,bet) " +
                            $"VALUES({idUsers},{nameCompany},{0})";
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

        public int DeleteParticipantsHaggle(int idBidding)
        {
            int updatedRowsCount;
            try
            {
                using (MySqlConnection mySqlConnection = DbConnection.GetConnection())
                {
                    mySqlConnection.Open();

                    using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
                    {
                        mySqlCommand.CommandText = $"DELETE FROM bidding WHERE id_bidding={idBidding};";
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

        public List<ParticipantsHaggle> UpdateBitUser(int idUser, int idBidding, float bet)
        {
            if (UpdateBet(idUser, idBidding, bet))
            {
                List<ParticipantsHaggle> participantsHaggles = GetParticipantsHaggles(idBidding, idUser);
                return participantsHaggles;
            }
            return null;

        }

        private bool UpdateBet(int idUser, int idBidding, float bet)
        {
            try
            {
                using (MySqlConnection mySqlConnection = DbConnection.GetConnection())
                {
                    mySqlConnection.Open();

                    using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
                    {
                        mySqlCommand.CommandText = $"UPDATE participants_haggle SET `bet` = {bet}" +
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

        private List<ParticipantsHaggle> GetParticipantsHaggles(int idBidding, int idUser)
        {
            try
            {
                List<ParticipantsHaggle> participantsHaggles = new List<ParticipantsHaggle>();

                using (MySqlConnection mySqlConnection = DbConnection.GetConnection())
                {
                    mySqlConnection.Open();

                    using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
                    {
                        mySqlCommand.CommandText = $"SELECT * FROM `participants_haggle` WHERE `id_bidding`={idBidding} AND `id_users`!={idUser};";

                        using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                        {
                            while (mySqlDataReader.Read() == true)
                            {
                                participantsHaggles.Add(new ParticipantsHaggle
                                {
                                    Id = mySqlDataReader.GetInt32("id"),
                                    NameCompany = mySqlDataReader.GetString("name_company"),
                                    IdBidding = mySqlDataReader.GetInt32("id_bidding"),
                                    IdUser = mySqlDataReader.GetInt32("id_users"),
                                    Bet = mySqlDataReader.GetFloat("bet")
                                });
                            }
                        }
                    }
                }

                return participantsHaggles;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
