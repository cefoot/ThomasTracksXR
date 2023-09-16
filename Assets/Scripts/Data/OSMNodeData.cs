using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Element
    {
        public string type { get; set; }
        public long id { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public DateTime timestamp { get; set; }
        public int version { get; set; }
        public int changeset { get; set; }
        public string user { get; set; }
        public int uid { get; set; }
        public Tags tags { get; set; }
    }

    public class OSMNodeData
    {
        public string version { get; set; }
        public string generator { get; set; }
        public string copyright { get; set; }
        public string attribution { get; set; }
        public string license { get; set; }
        public List<Element> elements { get; set; }
    }

    public class Tags
    {
        public string barrier { get; set; }
    }

}
