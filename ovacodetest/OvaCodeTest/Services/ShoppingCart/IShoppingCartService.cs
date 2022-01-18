using System.Threading.Tasks;
using CS.ERP.PL.POS.RES;

namespace OvaCodeTest.Services.ShoppingCart
{
    interface IShoppingCartService
    {
        Task<JSN_SHOPPING> GetDataAsync(dynamic obj);
        Task<JSN_SHOPPING> SaveDataAsync(dynamic obj);
    }
}
