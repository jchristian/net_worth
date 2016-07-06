using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using core.importers;
using data.models.contexts;
using data.models.write;

namespace api.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class TransactionsController : ApiController
    {
        DataContext context;
        GenericBrokerageTransactionImporter file_importer;

        public TransactionsController(DataContext context,
                                      GenericBrokerageTransactionImporter file_importer)
        {
            this.context = context;
            this.file_importer = file_importer;
        }

        [Route("api/transactions")]
        [HttpGet]
        public IEnumerable<BrokerageTransaction> GetAllTransactions(bool includeTrades = false)
        {
            if (includeTrades)
                return context.BrokerageTransactions
                              .Include(x => x.Trades)
                              .ToList();
            return context.BrokerageTransactions
                          .ToList();
        }

        [Route("api/transactions/import")]
        [HttpPost]
        public async Task<HttpResponseMessage> Import()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            var root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);
            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the file names.
                var file = provider.FileData.FirstOrDefault();
                if (file == null)
                    return Request.CreateResponse(HttpStatusCode.OK);
                Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                Trace.WriteLine("Server file path: " + file.LocalFileName);
                file_importer.Import(new StreamReader(File.OpenRead(file.LocalFileName)).ReadToEnd());
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}