using System;
using System.Collections.Generic;
using System.Linq;

using LaYumba.Functional;
using static LaYumba.Functional.F;
using QuickGraph;

namespace game {
    public class World {
        public List<Zone> Zones { get; private set; }
        public QuickGraph.BidirectionalGraph<Zone, Edge<Zone>> ZoneMap { get; private set; }

        public World(List<Zone> zones) {
            this.Zones = zones;
            this.ZoneMap = new QuickGraph.BidirectionalGraph<Zone, Edge<Zone>>();
            zones.ForEach(z => ZoneMap.AddVertex(z));
            UpdateZoneMap();
        }

        public void AddZone(Zone zone) {
            Zones.Add(zone);
            ZoneMap.AddVertex(zone);
            UpdateZoneMap();
        }

        public void UpdateZoneMap() {
            foreach (Zone a in Zones) {
                foreach (Zone b in Zones) {
                    if (Zone.IsAdjacent(a, b) && !ZoneMap.ContainsEdge(a, b))
                        ZoneMap.AddEdge(new Edge<Zone>(a, b));
                }
            }
        }

        public List<Direction> ZoneExits(Zone a) =>
             Zones.Where(m => ZoneMap.ContainsEdge(a, m))
                  .Select(n => a.DirectionTo(n))
                  .Bind(p => p)
                  .ToList();
    }
}
