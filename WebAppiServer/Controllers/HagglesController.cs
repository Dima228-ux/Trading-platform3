using CommunicationEntities;
using DbEntities;
using DbWorker;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebAppiServer.Controllers
{
    public class HagglesController : ApiController
    {
        [HttpPost]
        public Response InsertHaggles([FromBody] Request request)
        {
            Dictionary<string, int> userParameters = JsonConvert.DeserializeObject<Dictionary<string, int>>(request.Parameters);

            int idUser = userParameters["idUser"];
            int idBidding = userParameters["idBidding"];

            bool answer = DbManager.GetInstance().TableHaggles.InsertHaggles(idUser, idBidding);

            if (answer)
            {
                return new Response()
                {
                    Status = Response.StatusList.OK,
                    Data = ""
                };
            }
            else
            {
                return new Response()
                {
                    Status = Response.StatusList.ERROR,
                    Data = ""
                };
            }
        }

        [HttpPost]
        public Response ShowApplications([FromBody] Request request)
        {
            Dictionary<string, int> userParameters = JsonConvert.DeserializeObject<Dictionary<string, int>>(request.Parameters);

            int idUser = userParameters["idUser"];
            int idBidding = userParameters["idBidding"];
            int idCategory = userParameters["idCategory"];
            List<Haggle> haggles = DbManager.GetInstance().TableHaggles.ShowApplications(idUser, idBidding, idCategory);

            return new Response()
            {
                Status = Response.StatusList.OK,
                Data = JsonConvert.SerializeObject(haggles)
            };

        }

        [HttpPost]
        public Response UpdateStatusUser([FromBody] Request request)
        {
            Dictionary<string, int> userParameters = JsonConvert.DeserializeObject<Dictionary<string, int>>(request.Parameters);

            int idUser = userParameters["idUser"];
            int idBidding = userParameters["idBidding"];

            DbManager.GetInstance().TableHaggles.UpdateStatusUser(idUser, idBidding);

            return new Response()
            {
                Status = Response.StatusList.OK,
                Data = ""
            };

        }

        [HttpPost]
        public Response DeleteHaggle([FromBody] Request request)
        {
            Dictionary<string, int> userParameters = JsonConvert.DeserializeObject<Dictionary<string, int>>(request.Parameters);
            int id = userParameters["id"];

            DbManager.GetInstance().TableHaggles.DeleteHaggles(id);

            return new Response()
            {
                Status = Response.StatusList.OK
            };
        }
    }
}
