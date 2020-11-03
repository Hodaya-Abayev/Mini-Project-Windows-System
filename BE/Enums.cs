using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public enum StatusOrder { SentMail, NotYetAddressed, CloseOfUnresponsivness, CloseOfResponsiveness, closed }//טרם טופל, נשלח מייל, נסגר מחוסר הענות של הלקוח, נסגר בהיענות של הלקוח
    public enum StatusRequest { Active, CloseDeal, CloseExpired }//פתוחה, נסגרה עסקה דרך האתר, נסגרה כי פג תוקפה
    public enum Types { Zimmer, RoomHotel, SuiteHotel, Camping, Apartment }// צימר, דירת אירוח, חדר במלון, מאהל
    public enum Area { All, North, South, Center, Jerusalem }//צפון, דרום, מרכז, ירושלים,הכל
    public enum Pool { Necesarry, Possible, NotInteresed }//הכרחי/אפשרי/לא מעוניין
    public enum Jacuzzi { Necesarry, Possible, NotInteresed }//הכרחי/אפשרי/לא מעוניין
    public enum Garden { Necesarry, Possible, NotInteresed }//הכרחי/אפשרי/לא מעוניין
    public enum ChildrensAttractions { Necesarry, Possible, NotInteresed }//הכרחי/אפשרי/לא מעוניין
    public enum TV { Necesarry, Possible, NotInteresed }//הכרחי/אפשרי/לא מעוניין
    public enum Meals { Morning, Afternoon, Evening, All, NotInteresed }//בוקר/צהריים/ערב/הכל/לא מעוניין
}
