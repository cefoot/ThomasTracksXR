using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Elements
    {
        public List<Track> tracks { get; set; }
        public List<RailSwitch> railSwitches { get; set; }
        public List<SwitchGroup> switchGroups { get; set; }
    }

    public class Route
    {
        public string direction { get; set; }
        public string start { get; set; }
        public string destination { get; set; }
        public List<object> detours { get; set; }
        public List<string> codepoints { get; set; }
        public List<Path> path { get; set; }
        public bool settable { get; set; }
    }

    public class Path
    {
        public string type { get; set; }
        public string id { get; set; }
        public string position { get; set; }
    }

    public class Global
    {
        public bool iltisConnectionAlive { get; set; }
        public bool localOperation { get; set; }
        public bool valid { get; set; }
    }

    public class RailSwitch
    {
        public string id { get; set; }
        public string station { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public bool valid { get; set; }
        public bool activeRoute { get; set; }
        public bool occupied { get; set; }
        public bool impaired { get; set; }
        public string position { get; set; }
        public bool canTogglePosition { get; set; }
        public bool drivable { get; set; }
        public bool serviceMode { get; set; }
    }

    [Serializable]
    public class DepoState
    {
        public DateTime sentAt { get; set; }
        public State state { get; set; }
    }

    public class State
    {
        public Global global { get; set; }
        public Elements elements { get; set; }
        public List<Route> routes { get; set; }
    }

    public class SwitchGroup
    {
        public string id { get; set; }
        public string station { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public bool valid { get; set; }
        public bool serviceMode { get; set; }
    }

    public class Track
    {
        public string id { get; set; }
        public string station { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public bool valid { get; set; }
        public bool activeRoute { get; set; }
        public bool occupied { get; set; }
        public bool impaired { get; set; }
        public bool drivable { get; set; }
    }


}
