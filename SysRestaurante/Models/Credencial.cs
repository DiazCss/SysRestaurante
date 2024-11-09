using SysRestaurante.BL.Interfaces;
using System.Security.Claims;

namespace SysRestaurante.Models
{
    public class Credencial
    {
       
        readonly IRolBL httpClient;
        public Credencial(IRolBL pHttpClient)
        {
            httpClient = pHttpClient;
        }
        public void Refrescar(ClaimsPrincipal principal)
        {
            var claimExpired = principal.FindFirst(ClaimTypes.GroupSid);
            if (claimExpired != null)
            {
            }
        }
    }
}
