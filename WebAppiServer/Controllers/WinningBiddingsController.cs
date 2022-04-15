using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CommunicationEntities;
using DbEntities;
using DbWorker;
using Newtonsoft.Json;

namespace WebAppiServer.Controllers
{
    public class WinningBiddingsController : ApiController
    {
        [HttpPost]
        public Response UpdateStatusWinningBiddings([FromBody] Request request)
        {
            Dictionary<string, int> userParameters = JsonConvert.DeserializeObject<Dictionary<string, int>>(request.Parameters);

            int idUser = userParameters["idUser"];
            int idBidding = userParameters["idBidding"];

            DbManager.GetInstance().TableWinningBiddings.UpdateStatus(idUser, idBidding);

            return new Response()
            {
                Status = Response.StatusList.OK,
                Data = ""
            };

        }

        [HttpPost]
        public Response InsertWinningBiddings([FromBody] Request request)
        {
            Dictionary<string, int> userParameters = JsonConvert.DeserializeObject<Dictionary<string, int>>(request.Parameters);

            int idUser = userParameters["idUser"];
            int idBidding = userParameters["idBidding"];

            DbManager.GetInstance().TableWinningBiddings.InsertWinningBidding(idUser, idBidding);

            return new Response()
            {
                Status = Response.StatusList.OK,
                Data = ""
            };

        }

        [HttpPost]
        public Response SlectAllWinningBiddings([FromBody] Request request)
        {
            Dictionary<string, int> userParameters = JsonConvert.DeserializeObject<Dictionary<string, int>>(request.Parameters);

            int idUser = userParameters["idUser"];
            int idBidding = userParameters["idBidding"];
            int idCategory= userParameters["idCategory"];
           List<WiningBidding> winingBiddings= DbManager.GetInstance().TableWinningBiddings.SelectAllWiningBiddings(idUser, idBidding,idCategory);

            return new Response()
            {
                Status = Response.StatusList.OK,
                Data = JsonConvert.SerializeObject(winingBiddings)
            };

        }
    }
}