using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetCoreMVC.Models.ViewModel
{
    public class APIResponseViewModel<TModel>
    {
        public TModel Entity { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public HttpRequestMessage RequestMessage { get; set; }
    }
}
