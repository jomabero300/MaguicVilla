using System.Net;

namespace MaguicVilla.Api.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool Existoso { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public object Resultado { get; set; }

    }
}
