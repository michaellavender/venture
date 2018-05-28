using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Venture.Data.Monsters;

namespace Venture.Web.Controllers
{
    [Route("api/[controller]")]
    public class MonstersController : Controller
    {
        private readonly IMonsterList _monsterList;

        public MonstersController(IMonsterList monsterList)
        {
            _monsterList = monsterList;
        }

        [HttpGet]
        public IEnumerable<Monster> Get()
        {
            return _monsterList;

            /*
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            var json = JsonConvert.SerializeObject(product, serializerSettings);
            */
        }

    }
}
