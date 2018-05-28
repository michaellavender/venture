using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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

		    for (var lat = 90; lat > -90; lat--)
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
		    SetElevation();
	    }

		/// <summary>
		/// Generate tectonic plates by picking center points at random then expand outward until hitting another
		/// expanding plate. At that point the plates can't go any further and that forms a faultline. Follow the
		/// faultline in a direction away from the centers until there's no space left in which to expand.
		/// </summary>
		public void CreateTectonicPlates()
	    {
		    var numPlates = 8;

			var sw = new Stopwatch();
			sw.Start();

			_plates = new Dictionary<byte, TectonicPlate>();
		    var plateDistance = new Dictionary<byte, double>();

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
			    //for (byte plateId = 0; plateId < numPlates; plateId++)
			    {
				    var plate = _plates[plateId];

				    var nextPlateUnits = new List<GridUnit>();
				    foreach (var gu in calcPlateUnits[plateId])
				    {
					    if (gu.PlateId != null) continue;

					    if (gu.Distance(plate.Center) <= plateDistance[plateId])
					    {
						    gu.PlateId = plateId;
							plate.PlateUnits.Add(gu);

						    foreach (var neighbor in gu.Neighbors)
						    {
							    if (neighbor.PlateId == null)
							    {
									nextPlateUnits.Add(neighbor);
							    }
							    else if (neighbor.PlateId != gu.PlateId)
							    {
								    gu.IsOnFault = neighbor.IsOnFault = true;
							    }
						    }
					    }
					    else
					    {
						    nextPlateUnits.Add(gu);
					    }
				    }

				    calcPlateUnits[plateId] = nextPlateUnits;
				    if (nextPlateUnits.Count > 0)
				    {
					    plateDistance[plateId] = nextPlateUnits.Average(pu => pu.Distance(plate.Center));
					}
				}
		    }

		    Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds}");

		    //Console.WriteLine($"Geo units: {GridUnits.Count}");
		    //Console.WriteLine($"Plate geo units: {GridUnits.Values.Count(gu => gu.PlateId != null)}");
	    }

	    public void SetElevation()
	    {
		    var plate = _plates[0];
		    //foreach (var plate in _plates.Values)
		    {
				// TODO: Make random whether the plate has continental crust or is
				// purely oceanic, which is unlikely, but possible.

			    if (plate.PlateUnits.Count == 0) return;

			    var crustStart = plate.PlateUnits[_random.Next(plate.PlateUnits.Count - 1)];
			    var plateUnits = new List<GridUnit>();
			    plateUnits.Add(crustStart);
				plateUnits.AddRange(crustStart.Neighbors.Where(n => n.PlateId == plate.Id));
				plateUnits.ForEach(gu => gu.CrustType = CrustType.Continental);

				crustStart.CrustType = CrustType.Continental;
			    while (plateUnits.Count > 0)
			    {
				    var nextNeighbors = new List<GridUnit>();
				    foreach (var gu in plateUnits)
				    {
					    foreach (var neighbor in gu.Neighbors.Where(n =>
						    n.PlateId == plate.Id
						    && n.CrustType == CrustType.Unknown))
					    {
						    var continentalCnt = neighbor.Neighbors.Count(n => n.CrustType == CrustType.Continental);
						    var crustTypeCnt = neighbor.Neighbors.Count(n => n.CrustType != CrustType.Unknown);
						    var pctCrustLand = continentalCnt / 4D;

						    if (pctCrustLand > 0.7 || _random.Next(0, 2) == 0)
						    {
							    neighbor.CrustType = CrustType.Continental;
							}
						    else
						    {
								neighbor.CrustType = CrustType.Oceanic;
							}

						    nextNeighbors.Add(neighbor);
						}
						//crustStart.Neighbors.Where(n => !n.IsContinentalCrust && n.PlateId == plate.Id)
						//    .ToList();
					}


					plateUnits = nextNeighbors;
			    }
		    }

		    /*

		    //var plate = _plates[0];
		    foreach (var plate in _plates.Values)
		    {
			    var plateUnits = plate.PlateUnits.Where(pu => pu.IsOnFault).ToList();
			    while (plateUnits.Count > 0)
			    {
				    var nextPlateUnits = new List<GridUnit>();
				    foreach (var pu in plateUnits)
				    {
					    if (pu.Elevation != null) continue;

					    nextPlateUnits.AddRange(pu.Neighbors.Where(n => n.PlateId == pu.PlateId && n.Elevation == null));
					    if (pu.IsOnFault)
					    {
						    pu.Elevation = 220;
					    }
					    else
					    {
						    pu.Elevation = pu.Neighbors.Average(n => n.Elevation)
							    + _random.Next(-1, 2);
					    }
				    }

				    plateUnits = nextPlateUnits;
			    }
		    }
		    */
			}
		}
}

