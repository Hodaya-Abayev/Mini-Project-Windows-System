using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;




namespace BE
{
    public class HostingUnit// מייצגת יחידת אירוח
    {
        public long HostingUnitKey { get; set; }

        public Host Owner { get; set; }
        public string HostingUnitName { get; set; }
        public Area area { get; set; }
        [XmlIgnore]
    
        public bool[,] Diary { get; set; }
        [XmlArray("Diary")]
        public bool[] DiaryDto
        {
            get { return Diary.Flatten(); }
            set { Diary = value.Expand(12); }
        }
        




        public override string ToString()//for debugging
        {
            return HostingUnitKey.ToString();
        }

       

    }
}
