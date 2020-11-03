using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Order// מחלקה שמייצגת הזמנה- הקשר בין יחידות האירוח ללקוחות
    {
        public long HostingUnitKey { get; set; }
        public long GuestRequestKey { get; set; }
        public long OrderKey { get; set; }
        public StatusOrder statusOrder { get; set; }// סטטוס ההזמנה
        public DateTime CreateDate { get; set; }//תאריך יצירת הזמנה
        public DateTime MailDate { get; set; }//תאריך משלוח המייל ללקוח
        public override string ToString()//for debugging
        {
            return HostingUnitKey.ToString();
        }
    }
}
