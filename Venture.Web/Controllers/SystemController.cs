using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Venture.Web.Controllers
{
    [Route("api/[controller]")]
    public class SystemController : Controller
    {
        //private readonly IDocumentDBRepository<TenantDocument> _tenantRepository;

        public SystemController(
            //IDocumentDBRepository<TenantDocument> tenantRepository
            )
        {
            //_tenantRepository = tenantRepository;
        }

        //[HttpGet]
        //public async Task<IEnumerable<TenantDocument>> Get()
        //{
        //    return await _tenantRepository.GetItemsAsync();

        //    /*
        //    var serializerSettings = new JsonSerializerSettings();
        //    serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        //    var json = JsonConvert.SerializeObject(product, serializerSettings);
        //    */
        //}

        //[HttpPost]
        //public async void Post()
        //{
        //    await _tenantRepository.CreateItemAsync(new TenantDocument { Name = "New Tenant" });
        //}
    }
}
