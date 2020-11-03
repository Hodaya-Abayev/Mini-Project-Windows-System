using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DS
{
    public class DataSource// מחלקה שממכילה את נתוני הרשימות של המארחים, יחידות האירוח, בקשות הלקוח, וההזמנות- 
    {
        public static List<HostingUnit> hostList = new List<HostingUnit>()
        {
            new HostingUnit()
        {
            HostingUnitKey = 22222222,
            Owner = new Host()
            {
                HostKey = 322758889,
                PrivateName = "zimer 1",
                FamilyName = "cohen",
                FhoneNumber = 039554809,
                MailAddress = "hodaya020500@gmail.com",
                BankBranchDetails = new BankBranch()
                {
                    BankNumber = 12,
                    BankName = "yahav",
                    BranchNumber = 475,
                    BranchAddress = "rabi akiva",
                    BranchCity = "ramat gan"
                },
                BankAccountNumber = 022789,
                CollectionClearance = true
            },
            HostingUnitName="kasum",
            Diary=new bool[12,31],
            area=Area.All



        },
             new HostingUnit()
        {
            HostingUnitKey = 99999999,
            Owner = new Host()
            {
                HostKey = 322758889,
                PrivateName = "zimer 1",
                FamilyName = "cohen",
                FhoneNumber = 039554809,
                MailAddress = "hodaya020500@gmail.com",
                BankBranchDetails = new BankBranch()
                {
                    BankNumber = 12,
                    BankName = "yahav",
                    BranchNumber = 475,
                    BranchAddress = "rabi akiva",
                    BranchCity = "ramat gan"
                },
                BankAccountNumber = 022789,
                CollectionClearance = true
            },

            HostingUnitName = "suite1",
            Diary = new bool[12, 31],
            area = Area.Center
             },
            new HostingUnit()
        {
            HostingUnitKey = 10000003,
                 Owner = new Host()
                 {
                     HostKey = 207109190,
                     PrivateName = "zimer 2",
                     FamilyName = "levi",
                     FhoneNumber = 039517140,
                     MailAddress = "hodaya020500@gmail.com",
                     BankBranchDetails = new BankBranch()
                     {
                         BankNumber = 32,
                         BankName = "hapoalim",
                         BranchNumber = 778,
                         BranchAddress = "harif",
                         BranchCity = "elad"
                     },
                     BankAccountNumber = 123456,
                     CollectionClearance = false

                 },
            HostingUnitName = "suite2",
            Diary = new bool[12, 31],
            area = Area.Jerusalem
            }
    };
        public static List<GuestRequest> guestRequestList = new List<GuestRequest>()
         {
         new GuestRequest()
    {
        GuestRequestKey = 10000002,
         PrivateName = "noam",
             FamilyName = "ben haim",
             MailAddress = "hodaya020500@gmail.com",
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
             meals = Meals.All,
         },
          new GuestRequest()
    {
        GuestRequestKey = 44444444,
         PrivateName = "avi",
             FamilyName = "hayut",
             MailAddress = "janet0166@gmail.com",
             RegistrationDate = new DateTime(2020, 5, 14),
             EntryDate = new DateTime(2020, 5, 16),
             ReleaseDate = new DateTime(2020, 7, 1),
             SubArea = "eilat",
             Adults = 2,
             Children = 2,
             statusRequest = StatusRequest.Active,
             area = Area.Jerusalem,
             types = Types.Zimmer,
             pool = Pool.NotInteresed,
             jacuzzi = Jacuzzi.Necesarry,
             garden = Garden.NotInteresed,
             childrensAttractions = ChildrensAttractions.Possible,
             tv = TV.NotInteresed,
             RoomService = true,
             Playpen = true,
             meals = Meals.Evening,
         },
           new GuestRequest()
    {
        GuestRequestKey = 12345678,
         PrivateName = "avi",
             FamilyName = "hayut",
             MailAddress = "janet0166@gmail.com",
             RegistrationDate = new DateTime(2020, 5, 14),
             EntryDate = new DateTime(2020, 5, 16),
             ReleaseDate = new DateTime(2020, 7, 1),
             SubArea = "eilat",
             Adults = 2,
             Children = 2,
             statusRequest = StatusRequest.Active,
             area = Area.Jerusalem,
             types = Types.Zimmer,
             pool = Pool.NotInteresed,
             jacuzzi = Jacuzzi.Necesarry,
             garden = Garden.NotInteresed,
             childrensAttractions = ChildrensAttractions.Possible,
             tv = TV.NotInteresed,
             RoomService = true,
             Playpen = true,
             meals = Meals.Evening,
         },
            new GuestRequest()
    {
        GuestRequestKey = 77777777,
         PrivateName = "avi",
             FamilyName = "hayut",
             MailAddress = "janet0166@gmail.com",
             RegistrationDate = new DateTime(2020, 5, 14),
             EntryDate = new DateTime(2020, 5, 16),
             ReleaseDate = new DateTime(2020, 7, 1),
             SubArea = "eilat",
             Adults = 2,
             Children = 2,
             statusRequest = StatusRequest.Active,
             area = Area.Jerusalem,
             types = Types.Zimmer,
             pool = Pool.NotInteresed,
             jacuzzi = Jacuzzi.Necesarry,
             garden = Garden.NotInteresed,
             childrensAttractions = ChildrensAttractions.Possible,
             tv = TV.NotInteresed,
             RoomService = true,
             Playpen = true,
             meals = Meals.Evening,
         }


};

public static List<Order> orderList = new List<Order>()
          {
              new Order()
{
    HostingUnitKey = 22222222,
                  GuestRequestKey = 12345678,
                OrderKey = 55555555,
                 statusOrder = StatusOrder.NotYetAddressed,
                 CreateDate = new DateTime(2020, 2, 3),
                  MailDate = new DateTime(2020, 2, 8),
              },
           new Order()
{
    HostingUnitKey = 10000003,
                  GuestRequestKey = 77777777,
                 OrderKey = 66666666,
              statusOrder = StatusOrder.CloseOfResponsiveness,
                 CreateDate = new DateTime(2020, 2, 4),
                 MailDate = new DateTime(2020, 2, 6),

          }

        };
        public static List<BankBranch> bankList = new List<BankBranch>();
    }
  

}
