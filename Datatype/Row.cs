using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoLogViewer.Datatype
{
    public class DecisionsRows
    {
        public List<Row> Rows { get; set; }
    }

    public class Row
    {
        public string Category { get; set; }
        public string Level { get; set; }
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }
        public string entity_id { get; set; }
        public string entity_type_name { get; set; }
    }



    public class Done
    {
        public string Rows { get; set; }
    }

    public class DecisionsDone
    {
        public Done Done { get; set; }
    }
}
