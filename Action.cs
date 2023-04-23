using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FinalProject
{
    public class Action
    {
        [XmlAttribute]
        public string NameAction { get; set; }
        [XmlElement]
        public DateTime StartTime { get; set; }
        [XmlElement]
        public TimeSpan DurationAction { get; set; }

        public override string ToString()
        {
            return StartTime.ToString("hh:mm tt") + " - " + NameAction + " - " + DurationAction;
        }
    }
}

