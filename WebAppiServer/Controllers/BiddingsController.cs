using CommunicationEntities;
using DbEntities;
using DbWorker;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebAppiServer.Controllers
{
    public class BiddingsController : ApiController
    {
        [HttpPost]
        public Response InsertBidding([FromBody] Request request)
        {
            Dictionary<string, string> userParameters = JsonConvert.DeserializeObject<Dictionary<string, string>>(request.Parameters);
            string description = userParameters["description"];
            DateTime startDate = DateTime.Parse(userParameters["startDate"]);
            DateTime endDate = DateTime.Parse(userParameters["endDate"]);
            string title = userParameters["title"];
            int idUsers = int.Parse(userParameters["idUsers"]);
            int idCategory = int.Parse(userParameters["idCategory"]);
            int coast = int.Parse(userParameters["coast"]);

            bool answer = DbManager.GetInstance().TableBiddings.InsertBidding(description, startDate, endDate, idUsers, idCategory, title, coast);

            if (answer)
            {
                return new Response()
                {
                    Status = Response.StatusList.OK
                };
            }
            else
            {
                return new Response()
                {
                    Status = Response.StatusList.ERROR
                };
            }
        }

        [HttpPost]
        public Response GetMyBiddings([FromBody] Request request)
        {
            Dictionary<string, int> userParameters = JsonConvert.DeserializeObject<Dictionary<string, int>>(request.Parameters);

            int idUser = userParameters["idUser"];
            int idCategory = userParameters["idCategory"];
            List<Bindding> binddings = DbManager.GetInstance().TableBiddings.GetMyBiddings(idUser,idCategory);

            return new Response()
            {
                Status = Response.StatusList.OK,
                Data = JsonConvert.SerializeObject(binddings)
            };

        }

        [HttpPost]
        public Response GetBiddings([FromBody] Request request)
        {
            Dictionary<string, string> userParameters = JsonConvert.DeserializeObject<Dictionary<string, string>>(request.Parameters);

            int idUser = int.Parse(userParameters["idUser"]);
            DateTime endDate = DateTime.Parse(userParameters["endDate"]);
            List<Bindding> binddings = DbManager.GetInstance().TableBiddings.GetBiddings(idUser, endDate);

            return new Response()
            {
                Status = Response.StatusList.OK,
                Data = JsonConvert.SerializeObject(binddings)
            };

        }

        [HttpPost]
        public Response DeleteBidding([FromBody] Request request)
        {
            Dictionary<string, int> userParameters = JsonConvert.DeserializeObject<Dictionary<string, int>>(request.Parameters);
            int id = userParameters["id"];

           int answer= DbManager.GetInstance().TableBiddings.DeleteBidding(id);

            if (answer > 0)
            {
                return new Response()
                {
                    Status = Response.StatusList.OK
                };
            }
            else
            {
                return new Response()
                {
                    Status = Response.StatusList.ERROR
                };
            }
        }

        [HttpPost]
        public Response UpdateBidding([FromBody] Request request)
        {
            Dictionary<string, string> userParameters = JsonConvert.DeserializeObject<Dictionary<string, string>>(request.Parameters);
            int id = int.Parse(userParameters["id"]);
            string description = userParameters["description"];
            DateTime startDate = DateTime.Parse(userParameters["startDate"]);
            DateTime endDate = DateTime.Parse(userParameters["endDate"]);
            string title = userParameters["title"];
            int idUsers = int.Parse(userParameters["idUsers"]);
            int idCategory = int.Parse(userParameters["idCategory"]);
            int coast = int.Parse(userParameters["coast"]);

            bool answer = DbManager.GetInstance().TableBiddings.UpdateBidding(id,description, startDate, endDate, idUsers, idCategory, title, coast);

            if (answer)
            {
                return new Response()
                {
                    Status = Response.StatusList.OK
                };
            }
            else
            {
                return new Response()
                {
                    Status = Response.StatusList.ERROR
                };
            }
        }

        [HttpPost]
        public Response ShowMyBiddings([FromBody] Request request)
        {
            Dictionary<string, string> userParameters = JsonConvert.DeserializeObject<Dictionary<string, string>>(request.Parameters);

            int idCategory = int.Parse(userParameters["idCategory"]);
            int idUser = int.Parse(userParameters["idUser"]);
            DateTime endDate = DateTime.Parse(userParameters["endDate"]);

            List<Bindding> binddings = DbManager.GetInstance().TableBiddings.ShowMyBiddings(idCategory,idUser, endDate);

            return new Response()
            {
                Status = Response.StatusList.OK,
                Data = JsonConvert.SerializeObject(binddings)
            };

        }
    }
}