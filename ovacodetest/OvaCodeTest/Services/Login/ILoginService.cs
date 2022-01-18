using System.Threading.Tasks;
using CS.ERP.PL.SYS.RES;

namespace OvaCodeTest.Services.Login
{
    interface ILoginService
    {
        Task<JSN_RES_MOBILE_LOGIN> LoginAsync(dynamic obj);
    }
}
