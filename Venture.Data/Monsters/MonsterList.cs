using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace Venture.Data.Monsters
{
	public interface IMonsterList : IEnumerable<Monster>
	{
	}

	public class MonsterList : IMonsterList
    {
	    private readonly IList<Monster> _monsters;

	    public MonsterList()
	    {
		    //var serializerSettings = new JsonSerializerSettings();
		    //serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

			using (var stream = Assembly.GetAssembly(GetType()).GetManifestResourceStream(@"Venture.Data.Monsters.monsters.json"))
		    {
				using (var reader = new StreamReader(stream))
				{
					_monsters = JsonConvert.DeserializeObject<List<Monster>>(reader.ReadToEnd());
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
