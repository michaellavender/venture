using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Spatial;

namespace Venture.Data.Geography
{
    public class World
    {
		public IList<GridUnit> GridUnits { get; set; }
	    private readonly Random _random;

	    public double Circumfrence
	    {
		    get { return 360 * GridUnit.ArcDistance; }
	    }

	    public World()
	    {
		    _random = new Random();

			GridUnits = new List<GridUnit>();

		    for (var lat = 1; lat <= 90; lat += GridUnit.DegreesOfArc)
		    {
			    for (var lon = 0; lon < 180; lon += GridUnit.DegreesOfArc)
			    {
				    var ne = new GridUnit();
				    ne.Location = GeographyPoint.Create(lat, lon);
				    SetEnvironment(ne);
					GridUnits.Add(ne);

				    var nw = new GridUnit();
				    nw.Location = GeographyPoint.Create(lat, -lon);
				    SetEnvironment(nw);
				    GridUnits.Add(nw);

					var se = new GridUnit();
				    se.Location = GeographyPoint.Create(-lat, lon);
				    SetEnvironment(se);
				    GridUnits.Add(se);

					var sw = new GridUnit();
				    sw.Location = GeographyPoint.Create(-lat, -lon);
				    SetEnvironment(sw);
				    GridUnits.Add(sw);
				}
			}
	    }

	    public void SetEnvironment(GridUnit gu)
	    {
			var neighbors = GridUnits.Where(gu.IsNeighbor).ToList();
		    if (neighbors.Count == 0)
		    {
				// Seed point -- randomize
			    gu.IsLand = _random.NextDouble() > 0.6;
				return;
		    }

		    var pctLand = (double) neighbors.Count(n => n.IsLand) / (double)neighbors.Count;
		    if (pctLand > 0)
		    {
			    gu.IsLand = (_random.NextDouble() * pctLand) > 0.2;
			}
			else
		    {
			    gu.IsLand = _random.NextDouble() > 0.95;
			}
		}
	}
}
