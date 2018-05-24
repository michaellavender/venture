using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Spatial;

namespace Venture.Data.Geography
{
    public class World
    {
		public IDictionary<Tuple<double, double>, GridUnit> GridUnits { get; set; }
	    private readonly Random _random;
	    private IDictionary<byte, TectonicPlate> _plates;

	    public double Circumfrence
	    {
		    get { return 360 * GridUnit.ArcDistance; }
	    }

	    public World()
	    {
			_random = new Random();

			GridUnits = new Dictionary<Tuple<double, double>, GridUnit>();

		    for (var lat = -90; lat < 90; lat++)
		    {
			    for (var lon = -180; lon < 180; lon++)
			    {
				    var gu = new GridUnit(lat, lon);
				    GridUnits[gu.Key] = gu;
				}
			}

			foreach (var gu in GridUnits.Values)
		    {
			    gu.SetNeighbors(GridUnits);
		    }

		    CreateTectonicPlates();
	    }

		/// <summary>
		/// Generate tectonic plates by picking center points at random then expand outward until hitting another
		/// expanding plate. At that point the plates can't go any further and that forms a faultline. Follow the
		/// faultline in a direction away from the centers until there's no space left in which to expand.
		/// </summary>
		public void CreateTectonicPlates()
	    {
		    var numPlates = 8;

			_plates = new Dictionary<byte, TectonicPlate>();
		    var plateDistance = new Dictionary<byte, int>();

		    var calcPlateUnits = new Dictionary<byte, IList<GridUnit>>();
		    for (byte plateId = 0; plateId < numPlates; plateId++)
		    {
				_plates[plateId] = new TectonicPlate(plateId);
			    plateDistance[plateId] = 0;

			    var plateCenterUnit = GridUnits[_plates[plateId].Center];

				calcPlateUnits[plateId] = new List<GridUnit>();
			    calcPlateUnits[plateId].Add(plateCenterUnit);
			}

			while(calcPlateUnits.Values.Any(pu => pu.Count > 0))
			{
				var plateId = (byte)_random.Next(0, numPlates);

				var plate = _plates[plateId];

				var nextPlateUnits = new List<GridUnit>();
				foreach (var gu in calcPlateUnits[plateId])
				{
					if (gu.PlateId != null) continue;

					if (gu.Distance(plate.Center) < plateDistance[plateId])
					{
						gu.PlateId = plateId;
						foreach (var neighbor in gu.Neighbors) //.Where(n => n.PlateId == null))
						{
							if (neighbor.PlateId == null)
							{
								nextPlateUnits.Add(neighbor);
							}
							else if (neighbor.PlateId != gu.PlateId)
							{
								gu.IsPlateBorder = neighbor.IsPlateBorder = true;
							}
						}
					}
					else
					{
						nextPlateUnits.Add(gu);
					}
				}

				calcPlateUnits[plateId] = nextPlateUnits;
				plateDistance[plateId]++;
			}

			//Console.WriteLine($"Geo units: {GridUnits.Count}");
		    //Console.WriteLine($"Plate geo units: {GridUnits.Values.Count(gu => gu.PlateId != null)}");
	    }
	}
}

