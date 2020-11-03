//Hodaya Abayev 322758889
//Yiska Ashcenazi 207109190
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BE;
namespace PLConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IBL bl = BLFactory.getBL();

            GuestRequest g = new GuestRequest()
            {
                GuestRequestKey = ++Configuration.guestRequestKeySeq,
                PrivateName = "noam",
                FamilyName = "ben haim",
                MailAddress = "nbc531@gmail.com",
                RegistrationDate = new DateTime(2020, 8, 29),
                EntryDate = new DateTime(2020, 9, 21),
                ReleaseDate = new DateTime(2020, 9, 28),
                SubArea = "tel aviv",
                Adults = 2,
                Children = 6,
                statusRequest = StatusRequest.Active,
                area = Area.Center,
                types = Types.Zimmer,
                pool = Pool.Necesarry,
                jacuzzi = Jacuzzi.Possible,
                garden = Garden.NotInteresed,
                childrensAttractions = ChildrensAttractions.Necesarry,
                tv = TV.Necesarry,
                RoomService = true,
                Playpen = false,
                meals = Meals.All
            };
            HostingUnit h = new HostingUnit()
            {
                Owner = new Host()
                {

                    HostKey = 322758889,
                    PrivateName = "zimer 1",
                    FamilyName = "cohen",
                    FhoneNumber = 039554809,
                    MailAddress = "yiska90@gmail.com",
                    BankBranchDetails = new BankBranch()
                    {
                        BankNumber = 123,
                        BankName = "yahav",
                        BranchNumber = 475,
                        BranchAddress = "rabi akiva",
                        BranchCity = "ramat gan"
                    },
                    BankAccountNumber = 022789,
                    CollectionClearance = true
                },
                HostingUnitKey = 10000002,
                HostingUnitName = "suite1",
                Diary = new bool[12, 31],
                area = Area.Center

            };
            Order o = new Order()
            {
                HostingUnitKey = 10000001,
                GuestRequestKey = 10000008,
                OrderKey =++Configuration.OrderKeyseq,
                statusOrder = StatusOrder.NotYetAddressed,
                CreateDate = new DateTime(2020, 2, 3),
                MailDate = new DateTime(2020, 2, 8),
            };
            DateTime d1 = new DateTime(2020, 9, 25);
            DateTime d2 = new DateTime(2020, 1, 5);


            try
            {
                bl.addClientRequest(g);
            }
            catch (Exceptions ex)
            {
                Console.WriteLine(ex.message + ", " + ex.key + " ," + ex.name);
            }
            try
            {
                bl.updateClientRequestStatus(g, StatusRequest.CloseDeal);
            }
            catch (Exceptions ex)
            {
                Console.WriteLine(ex.message + ", " + ex.key + " ," + ex.name);
            }
            try
            {
                bl.addHostingUnit(h);
            }
            catch (Exceptions ex)
            {
                Console.WriteLine(ex.message + ", " + ex.key + " ," + ex.name);
            }
            try
            {
                bl.deleteHostingUnit(h/*, o*/);
            }
            catch (Exceptions ex)
            {
                Console.WriteLine(ex.message + ", " + ex.key + " ," + ex.name);
            }
            try
            {
                bl.updateHostingUnit(h/*, o*/);
            }
            catch (Exceptions ex)
            {
                Console.WriteLine(ex.message + ", " + ex.key + " ," + ex.name);
            }
            try
            {
                bl.addOrder(h, g, o);
            }
            catch (Exceptions ex)
            {
                Console.WriteLine(ex.message + ", " + ex.key + " ," + ex.name);
            }
            try
            {
                bl.updateOrder(h, o, StatusOrder.CloseOfResponsiveness, g);
            }
            catch (Exceptions ex)
            {
                Console.WriteLine(ex.message + ", " + ex.key + " ," + ex.name);
            }
            IEnumerable<HostingUnit> isAvailable = bl.availableUnits(d1, 3);
            foreach(var item in isAvailable)
                Console.WriteLine(item);
            Console.WriteLine(bl.between(d1));
            IEnumerable<Order> orders = bl.numOfOrders(1);
            foreach (var item in orders)
                Console.WriteLine(item);
            Console.WriteLine(bl.numOrders(g));
            Console.WriteLine(bl.numSucceedOrders(h));
            List<GuestRequest> center = bl.groupRequestByArea(Area.Center);
            foreach (var item in center)
                Console.WriteLine(item);
            List<GuestRequest> customers8 = bl.groupRequestByNumOfCustomers(8);
            foreach (var item in customers8)
                Console.WriteLine(item);
            List<Host> oneHost = bl.groupHostByNumOfHostingUnit(2);
            foreach (var item in oneHost)
                Console.WriteLine(item);
            List<HostingUnit> c = bl.groupHostingUnitByArea(Area.Center);
            foreach (var item in c)
                Console.WriteLine(item);
            //List<BankBranch> b= bl.getBankList();
            //foreach (var item in b)
            //{
            //    Console.WriteLine(item.BankNumber);
            //}
            
            

            //bool isRoomService(GuestRequest g1)
            //{
            //    return g1.RoomService;
            //}
            //Predicate<GuestRequest> func = isRoomService;
            //bool x = func(g);
            //List<GuestRequest> l = bl.condition(x);
            Console.ReadKey();

            

        }
    }
}
