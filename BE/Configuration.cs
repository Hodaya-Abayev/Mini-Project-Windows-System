using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Configuration//מחלקה שכוללת  את כל המשתנים הגלובליים כשדות סטטיים
    {
        public static int expireRequest = 30;// תאריך תפוגה של הזמנה
        public static int fee = 10;// עמלה
        public static long guestRequestKeySeq = 10000000;
        public static long hostingUnitKeySeq = 10000000;
        public static long OrderKeyseq = 10000000;
    }
}
