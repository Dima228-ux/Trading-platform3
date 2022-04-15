using DbWorker.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbWorker
{
    public class DbManager
    {
        private static DbManager _instance;
         
        public TableUsers TableUsers { get; private set; }
        public TableBiddings TableBiddings { get; private set; }
        public TableHaggles TableHaggles { get; private set; }
        public TableWinningBiddings TableWinningBiddings { get; private set; }
        public TableParticipantsHaggles TableParticipantsHaggles { get; private set; }

        private DbManager()
        {
            TableUsers = new TableUsers();
            TableBiddings = new TableBiddings();
            TableHaggles = new TableHaggles();
            TableWinningBiddings = new TableWinningBiddings();
            TableParticipantsHaggles = new TableParticipantsHaggles();
        }

        
        public static DbManager GetInstance()
        {
            if (_instance == null)
                _instance = new DbManager();
            return _instance;
        }
    }
}
