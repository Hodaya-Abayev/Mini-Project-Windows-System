using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.ComponentModel;
using DAL;
using BE;

namespace BL
{
    public class BL_imp: IBL
    {
        IDal dal = DalFactory.getDal();
        public void threadExpire()
        {
            Thread t = new Thread(expire);
            t.Start();
      
        }
        
        public bool rightDate(GuestRequest g)
        {
            if (g.ReleaseDate < g.EntryDate || g.EntryDate < DateTime.Now || g.ReleaseDate < DateTime.Now)
                return false;
            return true;
        }
        public bool isCollectionClearance(Host h, Order or)
        {
            if (h.CollectionClearance == true && or.statusOrder != StatusOrder.CloseOfUnresponsivness)//אם ההזמנה נסגרה אי אפשר לשנות סטטוס
                return true;
            return false;

        }
        public bool createOrder(HostingUnit hu, GuestRequest g)
        {
            if (g.EntryDate.Month == g.ReleaseDate.Month)//תאריכי החופשה בחודש מסוים אחד
            {
                for (int i = g.EntryDate.Day - 1; i < g.ReleaseDate.Day - 1; i++)
                {
                    if (hu.Diary[g.EntryDate.Month, i] == true)
                    {
                        return false;//יש יום או יותר תפוסים
                    }
                }

                return true;
            }
            else//תאריכי החופשה גולשים לחודש הבא 
            {
                int m = g.ReleaseDate.Month - g.EntryDate.Month - 1;
                for (int i = g.EntryDate.Day - 1; i < DateTime.DaysInMonth(g.EntryDate.Year, g.EntryDate.Month) - 1; i++)
                {
                    if (hu.Diary[g.EntryDate.Month, i] == true)
                    {
                        return false;//יש יום או יותר תפוסים
                    }
                }
                int thisM = g.EntryDate.Month - 1;
                while (m > 0)//תאריכי החופשה גולשים לחודש הבא והם יותר מחודש אחד
                {
                    for (int i = 0; i < DateTime.DaysInMonth(g.EntryDate.Year, g.EntryDate.Month) - 1; i++)
                    {
                        if (hu.Diary[thisM, i] == true)
                        {
                            return false;//יש יום או יותר תפוסים

                        }
                    }
                    thisM++;
                    m--;
                }
                //טיפול בתאריכים של החודש האחרון לחופשה
                for (int i = 0; i < g.ReleaseDate.Day - 1; i++)
                {
                    if (hu.Diary[g.ReleaseDate.Month - 1, i] == true)
                    {
                        return false;//יש יום או יותר תפוסים

                    }
                }
                return true;

            }

        }
        public bool orderStatus(Order or, StatusOrder s)
        {
            if (or.statusOrder == StatusOrder.CloseOfResponsiveness||or.statusOrder==StatusOrder.CloseOfUnresponsivness||or.statusOrder==StatusOrder.closed)
            {
                return false;//אם הסטטוס לא סגור, רק אז אפשר לשנות
            }
            return true;
        }
        public int orderFee(Order or, GuestRequest g)
        {
            int totalDays = (g.ReleaseDate - g.EntryDate).Days;
            return Configuration.fee * totalDays;
        }
        public void calender(Order or, HostingUnit hu, GuestRequest g)
        {
            if (or.statusOrder == StatusOrder.CloseOfResponsiveness && createOrder(hu, g))
            {
                if (g.EntryDate.Month == g.ReleaseDate.Month)//תאריכי החופשה בחודש מסוים אחד
                {
                    for (int i = g.EntryDate.Day - 1; i < g.ReleaseDate.Day - 1; i++)
                    {
                        hu.Diary[g.EntryDate.Month, i] = true;

                    }
                }
                else//תאריכי החופשה גולשים לחודש הבא 
                {
                    int m = g.ReleaseDate.Month - g.EntryDate.Month - 1;
                    for (int i = g.EntryDate.Day - 1; i < DateTime.DaysInMonth(g.EntryDate.Year, g.EntryDate.Month) - 1; i++)
                    {
                        hu.Diary[g.EntryDate.Month, i] = true;

                    }
                    int thisM = g.EntryDate.Month - 1;
                    while (m > 0)//תאריכי החופשה גולשים לחודש הבא והם יותר מחודש אחד
                    {
                        for (int i = 0; i < DateTime.DaysInMonth(g.EntryDate.Year, g.EntryDate.Month) - 1; i++)
                        {
                            hu.Diary[thisM, i] = true;

                        }
                        thisM++;
                        m--;
                    }
                    //טיפול בתאריכים של החודש האחרון לחופשה
                    for (int i = 0; i < g.ReleaseDate.Day - 1; i++)
                    {
                        hu.Diary[g.ReleaseDate.Month - 1, i] = true;

                    }
                }
            }
        }
        public void changeStatus(Order or, GuestRequest g)
        {
            if (or.statusOrder == StatusOrder.CloseOfResponsiveness)
            {
                g.statusRequest = StatusRequest.CloseDeal;
                var v = from item in dal.listOrs()
                        where item.OrderKey == or.OrderKey
                        select item;
                foreach (var item in v)
                {
                    item.statusOrder = StatusOrder.CloseOfResponsiveness;
                   
                        dal.updateOrder(item, item.statusOrder);
                  
                }

            }

        }
        public bool checkHostingUnit(HostingUnit hu)
        {
            List<Order> l = new List<Order>();
            var v = from item in dal.listOrs()
                    where (item.HostingUnitKey == hu.HostingUnitKey && (item.statusOrder == StatusOrder.SentMail|| item.statusOrder== StatusOrder.NotYetAddressed))//בודק אם קיימת הזמנה ליחדת אירוח הספציפית. ואם כן בודק אם הסטטוס -פתוח
                    select item;
            foreach (var item in v)
            {
                l.Add(item);
            }
            if (l.Count == 0)
                return true;
            return false;
        }
        public bool cancelCollectionClearance(HostingUnit hu)
        {
            if (hu.Owner.CollectionClearance==false)
            {
               
                    if (!checkHostingUnit(hu))
                        return false;

                
                return true;
                   
            }
            else
                return true;

        }
        public void mailSent(Order or, HostingUnit hu, GuestRequest g)
        {
           
                MailMessage mail = new MailMessage();
                mail.To.Add(g.MailAddress);
                mail.From = new MailAddress(hu.Owner.MailAddress);
                mail.Subject = "Inviting to Zimmer Top";
                mail.Body = "לקוח יקר שלום וברכה!\n ראינו את פנייתך וחשבנו כי הצימר שלנו יתאים בדיוק לבקשתך.\n" +
                    " נשמח שתיצור איתנו קשר לפרטים נוספים במייל:\n" +
                    hu.Owner.MailAddress + "\n" +
                    "או במספר הטלפון \n" +
                    hu.Owner.FhoneNumber + "\n" +

                    "ותצטרף למשפחת הלקוחות המאושרים שבחרו בצימר טופ לחופשה המושלמת שלהם(:";


                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Credentials = new System.Net.NetworkCredential("zimmertop100@gmail.com", "zimmer2020");
                smtp.EnableSsl = true;
             

                    smtp.Send(mail);
                    or.MailDate = DateTime.Today;
                
   
            }


        
        //פונקציית עזר לבדיקת יחידות אירוח פנויות בתאריכים מסוימים
        public bool oneUnit(DateTime d, int n, HostingUnit hu)
        {
            DateTime d1 = d.AddDays(n);
            if (d.Month == d.Month)//תאריכי החופשה בחודש מסוים אחד
            {
                for (int i = d.Day - 1; i < d1.Day - 1; i++)
                {
                    if (hu.Diary[d.Month, i] == true)
                    {
                        return false; //יש יום או יותר תפוסים
                    }
                }

                return true;
            }
            else//תאריכי החופשה גולשים לחודש הבא 
            {
                int m = d1.Month - d.Month - 1;
                for (int i = d.Day - 1; i < DateTime.DaysInMonth(d.Year, d.Month) - 1; i++)
                {
                    if (hu.Diary[d.Month, i] == true)
                    {
                        return false; //יש יום או יותר תפוסים

                    }
                }
                int thisM = d.Month - 1;
                while (m > 0)//תאריכי החופשה גולשים לחודש הבא והם יותר מחודש אחד
                {
                    for (int i = 0; i < DateTime.DaysInMonth(d.Year, d.Month) - 1; i++)
                    {
                        if (hu.Diary[thisM, i] == true)
                        {
                            return false; //יש יום או יותר תפוסים
                        }
                    }
                    thisM++;
                    m--;
                }
                //טיפול בתאריכים של החודש האחרון לחופשה
                for (int i = 0; i < d1.Day - 1; i++)
                {
                    if (hu.Diary[d1.Month - 1, i] == true)
                    {
                        return false; //יש יום או יותר תפוסים
                    }
                }
                return true;

            }
        }
        public IEnumerable<HostingUnit> availableUnits(DateTime d, int n)
        {
            var v = from units in dal.listHosts()
                    where oneUnit(d, n, units)
                    select units;
            return v;

        }
        public int between(params DateTime[] list)
        {
            int totalDays;
            if (list.Length == 2)
            {
                totalDays = (list[1] - list[0]).Days;
            }
            else
            {
                totalDays = (DateTime.Now - list[0]).Days;
            }
            return totalDays;

        }
        public bool oneOrder(Order or, int n)
        {
            int n1 = (DateTime.Today - or.MailDate).Days;
            int n2 = (DateTime.Today - or.CreateDate).Days;
            if (n1 >= n || n2 >= n)
                return true;
            return false;
        }
        public IEnumerable<Order> numOfOrders(int n)
        {
            var v = from orders in dal.listOrs()
                    where oneOrder(orders, n)
                    select orders;
            return v;
        }
        public IEnumerable<HostingUnit> condition(Predicate<HostingUnit> func)
        {
            var v = from units in dal.listHosts()
                    where func(units)
                    select units;
            return v;
        }
        public int numOrders(GuestRequest g)
        {
            int count = 0;
            foreach (var item in dal.listOrs())
            {
                if (item.GuestRequestKey == g.GuestRequestKey)
                    count++;

            }
           
            return count;
        }
        public int numSucceedOrders(HostingUnit h)
        {
            int count = 0;
            foreach (var item in dal.listOrs())
            {
                if (item.statusOrder == StatusOrder.CloseOfResponsiveness || item.statusOrder == StatusOrder.SentMail)
                    count++;

            }
 
            return count;
        }
        public List<GuestRequest> groupRequestByArea(Area ar)
        {
            var v =
            from item in dal.listReqs()
            group item by item.area into list
            select list;
            List<GuestRequest> lt = new List<GuestRequest>();
            foreach (var n in v)
            {
                if (n.Key == ar)
                    foreach (var item in n)
                    {
                        lt.Add(item);
                    }

            }
            return lt;
        }
        public List<GuestRequest> groupRequestByNumOfCustomers(int num)
        {
            var v =
           from item in dal.listReqs()
           group item by item.Adults + item.Children into list
           select list;
            List<GuestRequest> lst = new List<GuestRequest>();
            foreach (var item1 in v)
            {
                if (item1.Key == num)
                    foreach (var t in item1)
                    {
                        lst.Add(t);
                    }
            }
            return lst;
        }
        //פונקציית עזר למציאת מספר יחידות המארחים
        public int numOfHosts(Host h)
        {
            List<HostingUnit> Ls = new List<HostingUnit>();
            Ls = dal.listHosts();
            int num = 0;
            foreach (var item in Ls)
            {
                if (h.HostKey == item.Owner.HostKey)
                    num++;
            }
            return num;
        }
        public List<Host> groupHostByNumOfHostingUnit(int num)
        {
            var v =
            from item in dal.listHosts()
            group item by item.Owner into list
            select list;
            List<Host> lst = new List<Host>();
            foreach (var r in v)
            {
                if (numOfHosts(r.Key) == num)
                    foreach (var item in r)
                    {
                        if (lst.Exists(item1 => item1.HostKey ==item.Owner.HostKey) == false)
                            lst.Add(item.Owner);
                    }
            }
            
            return lst;
        }
        public List<HostingUnit> groupHostingUnitByArea(Area a)
        {
            var v =
            from item in dal.listHosts()
            group item by item.area into list
            select list;
            List<HostingUnit> lst = new List<HostingUnit>();
            foreach (var n in v)
            {
                if (n.Key == a)
                    foreach (var item in n)
                    {
                        lst.Add(item);
                    }

            }
            return lst;
        }
        public List<HostingUnit> unitByHosts(long id)
        {
           var v =
           from item in dal.listHosts()
           group item by item.Owner.HostKey into list
           select list;
            List<HostingUnit> lst = new List<HostingUnit>();
            foreach (var n in v)
            {
                if (n.Key == id)
                    foreach (var item in n)
                    {
                        lst.Add(item);
                    }

            }
            return lst;
        }
        public IEnumerable<GuestRequest> condition(Predicate<GuestRequest> func)
        {
            List<GuestRequest> l = new List<GuestRequest>();
            var v = from item in dal.listReqs()
                    where func(item)
                    select item;
            foreach (var item in v)
            {
                l.Add(item);
            }
            return l;
        }
        public int pool()
        {
           
           return (from  item in dal.listHosts()
                    where item.area == Area.South
                    select item).Count();
                   
        }
        // פונקציות שקוראות לDAL
        #region DAL functions
        public void addClientRequest(GuestRequest gr)
        {
            if (rightDate(gr))
              
                    dal.addClientRequest(gr);
               

            else
                throw new Exceptions("the date is wrong! (BL)", gr.GuestRequestKey, gr.PrivateName);

        }
        public void updateClientRequestStatus(GuestRequest g, StatusRequest s)
        {
         
                dal.updateClientRequestStatus(g, s);
            
          
        }
        public void addHostingUnit(HostingUnit h)
        {
        
                dal.addHostingUnit(h);
            
          
        }
        public void deleteHostingUnit(HostingUnit h)
        {
            if (checkHostingUnit(h))
                
                    dal.deleteHostingUnit(h);
                
               
            else
                throw new Exceptions("you cannot delete this hosting unit, there is an active order for it! (BL)", h.HostingUnitKey, h.HostingUnitName);

        }
        public void updateHostingUnit(HostingUnit h)
        {
            if (cancelCollectionClearance(h))
              
                    dal.updateHostingUnit(h);
                
                
            else
                throw new Exceptions("you cannot cancel your collection clearance, there is an active order for your hosting unit! (BL)", h.Owner.HostKey, h.Owner.PrivateName);
        }
        public void addOrder(HostingUnit hu, GuestRequest g, Order o)
        {
            if (createOrder(hu, g))
              
                    dal.addOrder(o, hu, g);
               

            else
                throw new Exceptions("This hosting unit is not available (BL)", o.OrderKey, o.CreateDate.ToString());


        }
        public void updateOrder(HostingUnit h, Order or, StatusOrder s, GuestRequest g)
        {
            if (orderStatus(or, or.statusOrder))//אם הסטטוס לא סגור
            {
                if (s == StatusOrder.SentMail)// רוצים לשנות לסטטוס שליחת מייל
                {
                    if (isCollectionClearance(h.Owner, or))//אם יש חיוב הרשאה לחשבון
                    {
                       
                            dal.updateOrder(or, s);//אז תשנה את הסטטוס לנשלח מייל
                            
                    }
                    else
                        throw new Exceptions("There is no collection clearance (BL)", or.OrderKey, or.CreateDate.ToString());

                }
                else if (s == StatusOrder.CloseOfResponsiveness)
                {
                  
                        dal.updateOrder(or, s);
                    
                   
                    calender(or, h, g);
                    
                  
                        dal.updateClientRequestStatus(g, StatusRequest.CloseDeal);
                    
                    
                    changeStatus(or, g);



                }
                else if (s == StatusOrder.CloseOfUnresponsivness)
                {
                   
                        dal.updateOrder(or, s);
                    
                   
                }
            }
            else
                throw new Exceptions("the deal is already closed (BL)", or.OrderKey, or.CreateDate.ToString());
        }

        public List<BankBranch> getBankList()
        {

            return dal.listBanks();
        }
        public List<GuestRequest> listReqs()
        {
            return dal.listReqs();
        }
        public List<HostingUnit> listHosts()
        {
            return dal.listHosts();
        }
        public List<Order> listOrs()
        {
            return dal.listOrs();
        }
        public bool notsouth(HostingUnit h)
        {
            if (h.area == Area.South)
                return false;
            return true;
        }
        private void expire()
        {
            foreach (var item in dal.listOrs())
            {
                if (item.MailDate.AddDays(Configuration.expireRequest) < DateTime.Today)
                    dal.updateOrder(item, StatusOrder.CloseOfUnresponsivness);
                Thread.Sleep(86400000);
            }
        }
        #endregion
        #region bank functions
        //public List<int> bankNum()
        //{
        //    List<BankBranch> banks = getBankList();
        //    List<int> nums = new List<int>();
        //    foreach (var item in banks)
        //    {
        //        nums.Add(item.BankNumber)
        //        if (nums.Exists(item1 => item1. == item.Owner.HostKey) == false)
        //            lst.Add(item.Owner);
        //    }
        //}
        public List<int> bankNum()
        {
           return dal.bankNum();
        }
        public List<string> bankName()
        {
            return dal.bankName();
        }
        public List<int> branchNum()
        {
            return dal.branchNum();
        }
        public List<string> branchAdress()
        {
            return dal.branchAdress();
        }
        public List<string> branchCity()
        {
            return dal.branchCity();
        }
        #endregion

    }
}
