using System;
using System.Collections.Generic;
using System.Linq;

using LaYumba.Functional;
using static LaYumba.Functional.F;
using QuickGraph;

namespace game {
    public class World {
        private List<Zone> Zones;
        private QuickGraph.BidirectionalGraph<Zone, Edge<Zone>> ZoneMap;

        public World(List<Zone> zones) {
            this.Zones = zones;
            this.ZoneMap = new QuickGraph.BidirectionalGraph<Zone, Edge<Zone>>();
            Update();
        }

        public void AddZone(Zone zone) {
            Zones.Add(zone);
            ZoneMap.AddVertex(zone);
        }

        public void Update() {
            foreach (Zone a in Zones) {
                foreach (Zone b in Zones) {
                    if (Zone.IsAdjacent(a, b) && !ZoneMap.ContainsEdge(a, b))
                    {
                        ZoneMap.AddEdge(new Edge<Zone>(a, b));
                    }
                }
            }
        }
    }
}
