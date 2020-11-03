using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public interface IBL
    {
        bool rightDate(GuestRequest g);//תאריך תחילת הנופש קודם לפחות ביום אחד לתאריך סיום הנופש
        bool isCollectionClearance(Host h, Order or);// בעל יחידת אירוח יוכל לשלוח הזמנה ללקוח (שינוי הסטטוס ל "נשלח מייל"), רק אם חתם על הרשאה לחיוב חשבון בנק
        bool createOrder(HostingUnit hu, GuestRequest g);//יש לוודא בעת יצירת הזמנה ללקוח, שהתאריכים המבוקשים פנויים ביחידת האירוח שמוצעת לו
        bool orderStatus(Order or, StatusOrder s);//לאחר שסטטוס ההזמנה השתנה לסגירת עיסקה – לא ניתן לשנות יותר את הסטטוס שלה.
        int orderFee(Order or, GuestRequest g);//כאשר סטטוס ההזמנה משתנה בגלל סגירת עסקה – יש לבצע חישוב עמלה בגובה של 10 שח ליום אירוח
        void calender(Order or, HostingUnit hu, GuestRequest g);//כאשר סטטוס ההזמנה משתנה בגלל סגירת עסקה – יש לסמן במטריצה את התאריכים הרלוונטים
        void changeStatus(Order or, GuestRequest g);//כאשר סטטוס הזמנה משתנה עקב סגירת עסקה – יש לשנות את הסטטוס של דרישת  הלקוח בהתאם, וכן לשנות את הסטטוס של כל ההזמנות האחרות של אותו לקוח.
        bool checkHostingUnit(HostingUnit hu/*, Order or*/); //לא ניתן למחוק יחידת אירוח כל עוד יש הצעה הקשורה אליה במצב פתוח.
        bool cancelCollectionClearance(HostingUnit hu/*,Order or*/);//לא ניתן לבטל הרשאה לחיוב חשבון כאשר יש הצעה הקשורה אליה במצב פתוח.
        void mailSent(Order or, HostingUnit h, GuestRequest g);// כאשר סטטוס ההזמנה משתנה ל"נשלח מייל" – המערכת תשלח באופן אוטומטי מייל  ללקוח עם פרטי ההזמנה.
        bool oneUnit(DateTime d, int n, HostingUnit hu);//פונקציית עזר לבדיקת יחידות אירוח פנויות בתאריכים מסוימים
        IEnumerable<HostingUnit> availableUnits(DateTime d, int n);//פונקציה שמקבלת תאריך ומספר ימי נופש ומחזירה את רשימת כל יחידות האירוח הפנויות  בתאריך זה.
        int between(params DateTime[] list);// פונקציה שמקבלת תאריך אחד או שניים. הפונקציה מחזירה את מספר הימים שעברו מהתאריך הראשון ועד לשני, או במידה והתקבל רק תאריך אחד – מהתאריך ראשון ועד היום
        bool oneOrder(Order or, int n);//פונקציית עזר לבדיקת מספר הימים שעברו מיום שליחת המייל או מתאריך ההרשמה בהזמנה אחת
        IEnumerable<Order> numOfOrders(int n);//פונקציה שמקבלת מספר ימים, ומחזירה את כל ההזמנות שמשך הזמן שעבר מאז שנוצרו /  מאז שנשלח מייל ללקוח גדול או שווה למספר הימים שהפונקציה קיבלה.
        IEnumerable<GuestRequest> condition(Predicate<GuestRequest> func);//פונקציה שיכולה להחזיר את כל דרישות הלקוח שמתאימים לתנאי מסוים
        int numOrders(GuestRequest g);// פונקציה שמקבלת דרישת לקוח, ומחזירה את מספר ההזמנות שנשלחו אליו.
        int numSucceedOrders(HostingUnit h);// פונקציה שמקבלת יחידת אירוח ומחזירה את מספר ההזמנות שנשלחו / מספר ההזמנות שנסגרו בהצלחה עבור יחידה זו דרך האתר.
        List<GuestRequest> groupRequestByArea(Area ar);// רשימת דרישות לקוח מקובצת ע"פ אזור הנופש הנדרש. 
        List<GuestRequest> groupRequestByNumOfCustomers(int num);//רשימת דרישות לקוח מקובצות ע"פ ע"פ מספר הנופשים.
        int numOfHosts(Host h);//פונקציית עזר למציאת מספר יחידות המארחים
        List<Host> groupHostByNumOfHostingUnit(int num);//רשימת מארחים מקובצת ע"פ מספר יחידות האירוח שהם מחזיקים
        List<HostingUnit> groupHostingUnitByArea(Area a);//רשימת יחידות אירוח מקובצת ע"פ איזור הנופש הנדרש
        void addClientRequest(GuestRequest gr);// הוספת דרישת לקוח
        void updateClientRequestStatus(GuestRequest g, StatusRequest s);//עדכון דרישת לקוח
        void addHostingUnit(HostingUnit h);//הוספת יחידת אירוח
        void deleteHostingUnit(HostingUnit h/*, Order o*/);//מחיקת יחידת אירוח
        void updateHostingUnit(HostingUnit h/*, Order o*/);//עדכון יחידת אירוח
        void addOrder(HostingUnit hu, GuestRequest g, Order o);//הוספת הזמנה
        void updateOrder(HostingUnit h, Order or, StatusOrder s, GuestRequest g);//עדכון הזמנה
        List<HostingUnit> unitByHosts(long id);//רשימת יחידות אירוח לפי מארח
        List<GuestRequest> listReqs();
        List<HostingUnit> listHosts();
        List<Order> listOrs();
        List<BankBranch> getBankList();
        bool notsouth(HostingUnit h);
        List<int> bankNum();
        List<string> bankName();
        List<int> branchNum();
        List<string> branchAdress();
        List<string> branchCity();



    }
}
