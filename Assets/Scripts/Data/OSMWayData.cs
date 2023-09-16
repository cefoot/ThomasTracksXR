using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class WayElement
    {
        public string type { get; set; }
        public int id { get; set; }
        public DateTime timestamp { get; set; }
        public int version { get; set; }
        public int changeset { get; set; }
        public string user { get; set; }
        public int uid { get; set; }
        public List<long> nodes { get; set; }
        public WayTags tags { get; set; }
    }

    public class OSMWayData
    {
        public string version { get; set; }
        public string generator { get; set; }
        public string copyright { get; set; }
        public string attribution { get; set; }
        public string license { get; set; }
        public List<WayElement> elements { get; set; }
    }

    public class WayTags
    {
        public string covered { get; set; }
        public string electrified { get; set; }
        public string frequency { get; set; }
        public string gauge { get; set; }
        public string @operator { get; set; }
        public string railway { get; set; }
        public string service { get; set; }
        public string tracks { get; set; }
        public string voltage { get; set; }
    }


}
