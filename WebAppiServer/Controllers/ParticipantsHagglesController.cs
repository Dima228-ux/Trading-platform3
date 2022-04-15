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
    public class ParticipantsHagglesController: ApiController
    {
        [HttpPost]
        public Response InsertParticipantsHaggles([FromBody] Request request)
        {
            Dictionary<string, string> userParameters = JsonConvert.DeserializeObject<Dictionary<string,string>>(request.Parameters);

            int idUser = int.Parse(userParameters["idUser"]);
            int idBidding = int.Parse(userParameters["idBidding"]);
            string nameCompany = userParameters["nameCompany"];

           bool answer= DbManager.GetInstance().TableParticipantsHaggles.InsertParticipantsHaggles(idUser, idBidding,nameCompany);

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
        public Response DeleteParticipantsHaggle([FromBody] Request request)
        {
            Dictionary<string, int> userParameters = JsonConvert.DeserializeObject<Dictionary<string, int>>(request.Parameters);
            int idBidding = userParameters["idBidding"];

            int answer = DbManager.GetInstance().TableParticipantsHaggles.DeleteParticipantsHaggle(idBidding);

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
        public Response UpdateBitUser([FromBody] Request request)
        {
            Dictionary<string, string> userParameters = JsonConvert.DeserializeObject<Dictionary<string, string>>(request.Parameters);

            int idCategory = int.Parse(userParameters["idCategory"]);
            int idUser = int.Parse(userParameters["idUser"]);
            float bet= float.Parse(userParameters["bet"]);
            
            List<ParticipantsHaggle> participantsHaggles = DbManager.GetInstance().TableParticipantsHaggles.UpdateBitUser(idCategory, idUser,bet);

            return new Response()
            {
                Status = Response.StatusList.OK,
                Data = JsonConvert.SerializeObject(participantsHaggles)
            };

        }
    }
}