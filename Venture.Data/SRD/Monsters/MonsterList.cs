using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Venture.Data.SRD.Monsters
{
	public interface IMonsterList : IEnumerable<Monster>
	{
	}

	public class MonsterList : IMonsterList
    {
	    private readonly IList<Monster> _monsters;

	    public MonsterList()
	    {
		    var settings = new JsonSerializerSettings();
		    settings.ContractResolver = new DefaultContractResolver
		    {
			    NamingStrategy = new SnakeCaseNamingStrategy()
		    };

			using (var stream = Assembly.GetAssembly(GetType()).GetManifestResourceStream(@"Venture.Data.SRD.json.monsters.json"))
		    {
				using (var reader = new StreamReader(stream))
				{
					_monsters = JsonConvert.DeserializeObject<List<Monster>>(reader.ReadToEnd(), settings);
				}
			}
		}

		public IEnumerator<Monster> GetEnumerator()
		{
			return _monsters.GetEnumerator();
	    }

	    IEnumerator IEnumerable.GetEnumerator()
	    {
		    return GetEnumerator();
	    }
    }
}
