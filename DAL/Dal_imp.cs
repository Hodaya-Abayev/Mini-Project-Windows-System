using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;
namespace DAL
{
    public class Dal_imp : IDal//מחלקה שמממשת את הIdAL
    {
        #region guestRequest functions
        public void addClientRequest(GuestRequest gr)
        {

            if (DataSource.guestRequestList.Exists(item => item.GuestRequestKey == gr.GuestRequestKey) == true)
                throw new Exceptions("This guest request is exist, can't add (DAL)", gr.GuestRequestKey, gr.PrivateName);
            else
            {
               
                DataSource.guestRequestList.Add(gr);
            }
        }

        public void updateClientRequestStatus(GuestRequest gr, StatusRequest sr)
        {
            GuestRequest myGr = DataSource.guestRequestList.FirstOrDefault(item => item.GuestRequestKey == gr.GuestRequestKey);
            if (myGr == null)
                throw new Exceptions("This guest request isn't exist, can't update (DAL)", gr.GuestRequestKey, gr.PrivateName);
            myGr.statusRequest = sr;
        }
        #endregion
        #region HostingUnit functions
        public void addHostingUnit(HostingUnit ht)
        {

            if (DataSource.hostList.Exists(item => item.Owner.HostKey == ht.Owner.HostKey) == true && DataSource.hostList.Exists(item => item.HostingUnitName == ht.HostingUnitName) == true)
            {
                --Configuration.OrderKeyseq;
                throw new Exceptions("This hosting unit is exist, can't add (DAL)", ht.HostingUnitKey, ht.HostingUnitName);

            }
            else
            {
               
                DataSource.hostList.Add(ht);

            }
        }

        public void deleteHostingUnit(HostingUnit ht)
        {
            if (DataSource.hostList.Exists(item => item.HostingUnitKey == ht.HostingUnitKey) == true)
            {
                DataSource.hostList.Remove(ht);
            }
            else
                throw new Exceptions("This hosting unit isn't exist, can't delete (DAL)", ht.HostingUnitKey, ht.HostingUnitName);
        }
        public void updateHostingUnit(HostingUnit ht)
        {
            HostingUnit myHt = DataSource.hostList.FirstOrDefault(item => item.HostingUnitKey == ht.HostingUnitKey);
            {
                if (myHt == null)
                    throw new Exceptions("This hosting unit isn't exist, can't update (DAL)", ht.HostingUnitKey, ht.HostingUnitName);
                DataSource.hostList.Remove(myHt);
                DataSource.hostList.Add(ht);

            }

        }
        #endregion
        #region order functions
        public void addOrder(Order or, HostingUnit hu, GuestRequest g)
        {

            if (DataSource.orderList.Exists(item => item.GuestRequestKey == g.GuestRequestKey) == true && DataSource.orderList.Exists(item => item.HostingUnitKey == hu.HostingUnitKey) == true)
                throw new Exceptions("This order is exist, can't add (DAL)", or.OrderKey, or.CreateDate.ToString());
            else
            {
               
                DataSource.orderList.Add(or);
            }
        }
        public void updateOrder(Order or, StatusOrder so)
        {
            Order myOr = DataSource.orderList.FirstOrDefault(item => item.OrderKey == or.OrderKey);
            if (myOr == null)
                throw new Exceptions("This order isn't exist, can't can't update (DAL)", or.OrderKey, or.CreateDate.ToString());
            myOr.statusOrder = so;
        }
        #endregion
        #region GetLists
        public List<HostingUnit> listHosts()
        {
            return DataSource.hostList;
        }
        public List<GuestRequest> listReqs()
        {
            return DataSource.guestRequestList;
        }
        public List<Order> listOrs()
        {
            return DataSource.orderList;
        }
        public List<BankBranch> listBanks()
        {
            List<BankBranch> bankList = new List<BankBranch>()
        {
            new BankBranch()
             {
            BankNumber = 568,
            BankName = "hapoalim",
            BranchNumber = 352,
            BranchAddress = "balfur",
            BranchCity = "elad"
           },
              new BankBranch()
           {
            BankNumber = 785,
            BankName = "hapoalim",
            BranchNumber = 246,
            BranchAddress = "hamacabim",
            BranchCity = "bat yam"
           },
              new BankBranch()
           {
            BankNumber = 868,
            BankName = "hapoalim",
            BranchNumber = 549,
            BranchAddress = "abarbanel",
            BranchCity = "ramat gan"
           },
              new BankBranch()
           {
            BankNumber = 466,
            BankName = "hapoalim",
            BranchNumber = 792,
            BranchAddress = "jabutinsci",
            BranchCity = "tal aviv"
           },
              new BankBranch()
           {
            BankNumber = 428,
            BankName = "hapoalim",
            BranchNumber = 246,
            BranchAddress = "cahaneman",
            BranchCity = "givataim"
           },
        };
            return DataSource.bankList;
        }
        public List<int> bankNum()
        {
            List<int> l = new List<int>();
            foreach (var item in listBanks())
            {
                if (!l.Contains(item.BankNumber))
                    l.Add(item.BankNumber);
            }
            return l;
        }
        public List<string> bankName()
        {
            List<string> l = new List<string>();
            foreach (var item in listBanks())
            {
                if (!l.Contains(item.BankName))
                    l.Add(item.BankName);
            }
            return l;
        }
        public List<int> branchNum()
        {
            List<int> l = new List<int>();
            foreach (var item in listBanks())
            {
                if (!l.Contains(item.BranchNumber))
                    l.Add(item.BranchNumber);
            }
            return l;
        }
        public List<string> branchAdress()
        {
            List<string> l = new List<string>();
            foreach (var item in listBanks())
            {
                if (!l.Contains(item.BranchAddress))
                    l.Add(item.BranchAddress);
            }
            return l;
        }
        public List<string> branchCity()
        {
            List<string> l = new List<string>();
            foreach (var item in listBanks())
            {
                if (!l.Contains(item.BranchCity))
                    l.Add(item.BranchCity);
            }
            return l;
        }
        #endregion
    }
}







