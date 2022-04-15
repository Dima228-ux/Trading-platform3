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
    public class UsersController : ApiController
    {
        [HttpPost]
        public Response GetUserByLoginPassword([FromBody] Request request)
        {
            Dictionary<string, string> userParameters = JsonConvert.DeserializeObject<Dictionary<string, string>>(request.Parameters);

            string login = userParameters["login"];
            string password = userParameters["password"];

            User user = DbManager.GetInstance().TableUsers.GetUserByLoginPassword(login, password);

            return new Response()
            {
                Status = Response.StatusList.OK,
                Data = JsonConvert.SerializeObject(user)
            };
        }

        [HttpPost]
        public Response RegistedNewUser([FromBody] Request request)
        {
            Dictionary<string, string> userParameters = JsonConvert.DeserializeObject<Dictionary<string, string>>(request.Parameters);
            string name = userParameters["name"];
            string login = userParameters["login"];
            string password = userParameters["password"];
            string surname = userParameters["surname"];
            string inn = userParameters["inn"];
            string nameCompany = userParameters["nameCompany"];

            if (DbManager.GetInstance().TableUsers.CheckLoginUser(login))
            {
                DbManager.GetInstance().TableUsers.RegistedNewUser(login, password, inn, name, surname, nameCompany);
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
        public Response UpdateUser([FromBody] Request request)
        {
            Dictionary<string, string> userParameters = JsonConvert.DeserializeObject<Dictionary<string, string>>(request.Parameters);
            string name = userParameters["name"];
            string login = userParameters["login"];
            string password = userParameters["password"];
            string surname = userParameters["surname"];
            string inn = userParameters["inn"];
            string nameCompany = userParameters["nameCompany"];
            int id = int.Parse(userParameters["id"]);

            int answer = DbManager.GetInstance().TableUsers.UpdateUser(id, login, password, inn, name, surname, nameCompany);
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
        public Response DeleteUser([FromBody] Request request)
        {
            Dictionary<string, int> userParameters = JsonConvert.DeserializeObject<Dictionary<string, int>>(request.Parameters);
            int id = userParameters["id"];

            DbManager.GetInstance().TableUsers.DeleteUser(id);

            return new Response()
            {
                Status = Response.StatusList.OK
            };
        }

        [HttpPost]
        public Response ShowParticipants([FromBody] Request request)
        {
            Dictionary<string, int> userParameters = JsonConvert.DeserializeObject<Dictionary<string, int>>(request.Parameters);
            int idCategory = userParameters["idCategory"];
            int idBidding = userParameters["idBidding"];
            List<User> users = DbManager.GetInstance().TableUsers.ShowParticipants(idCategory, idBidding);

            return new Response()
            {
                Status = Response.StatusList.OK,
                Data = JsonConvert.SerializeObject(users)
            };
        }
    }
}


