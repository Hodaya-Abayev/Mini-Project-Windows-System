using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Xml.Linq;
using System.IO;
using BE;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Net;

namespace DAL
{
    class Dal_XML_imp : IDal
    {
        XElement GuestRequestRoot;
        XElement HostingUnitRoot;
        XElement OrderRoot;
        XElement ConfigurationRoot;
        XElement banksRoot;

        string GuestRequestPath = @"GuestRequestXML.xml";
        string HostingUnitPath = @"HostingUnitXML.xml";
        string OrderPath = @"OrderXML.xml";
        string ConfigurationPath = @"ConfigurationXML.xml";
        string banksPath = @"atm.xml";

        public Dal_XML_imp()
        {
            if (!File.Exists(banksPath))
            {
                
                try
                {
                    downloadAtm();
                    new Thread(downloadAtm).Start();
                    while (!File.Exists(banksPath))
                    {
                        Thread.Sleep(1000 * 10);
                    }
                    banksRoot = XElement.Load(banksPath);
                }
                catch
                {
                    throw new FileLoadException("file upload problem");
                }
            }

            else
            {
                banksRoot = XElement.Load(banksPath);
            }



            if (!File.Exists(HostingUnitPath))

            {
                HostingUnitRoot = new XElement("HostingUnits");
                HostingUnitRoot.Save(HostingUnitPath);

            }
            else
            {
                try { HostingUnitRoot = XElement.Load(HostingUnitPath); }
                catch { Console.WriteLine("File upload problem"); }

            }
            //check if files are exist
            if (!File.Exists(GuestRequestPath))

            {
                GuestRequestRoot = new XElement("GuestRequests");
                GuestRequestRoot.Save(GuestRequestPath);

            }
            else
            {
                try { GuestRequestRoot = XElement.Load(GuestRequestPath); }
                catch { Console.WriteLine("File upload problem"); }

            }
            if (!File.Exists(OrderPath))

            {
                OrderRoot = new XElement("Orders");
                OrderRoot.Save(OrderPath);

            }
            else
            {
                try { OrderRoot = XElement.Load(OrderPath); }
                catch { Console.WriteLine("File upload problem"); }

            }


            if (!File.Exists(ConfigurationPath))

            {
                ConfigurationRoot = new XElement("configuration");
                ConfigurationRoot.Save(ConfigurationPath);

            }
            else
            {
                try { ConfigurationRoot = XElement.Load(ConfigurationPath); }
                catch { Console.WriteLine("File upload problem"); }

            }
          
        }
        void downloadAtm()
        {
            XElement atm;
            try
            {
                atm = new XElement("ATMs");
                atm = XElement.Load("https://drive.google.com/u/0/uc?id=1FpcqslnRD6naLHOjrCvKArCg3Ihkb9hR&export=download");
                atm.Save(banksPath);
            }
            catch (Exception)
            {
                atm = new XElement("ATMs");
                atm = XElement.Load("http://www.boi.org.il/he/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/atm.xml");
                atm.Save(banksPath);
            }
        }
        XElement ConverGRToXElement(GuestRequest gr)
        {
            XElement convered = new XElement("guestRequest");

            convered.Add(new XElement("PrivateName", gr.PrivateName));
            convered.Add(new XElement("Adults", gr.Adults));
            convered.Add(new XElement("SubArea", gr.SubArea));
            convered.Add(new XElement("area", gr.area));
            convered.Add(new XElement("Children", gr.Children));
            convered.Add(new XElement("childrenAttractions", gr.childrensAttractions));
            convered.Add(new XElement("EntryDate", gr.EntryDate));
            convered.Add(new XElement("FamilyName", gr.FamilyName));
            convered.Add(new XElement("garden", gr.garden));
            convered.Add(new XElement("GuestRequestKey", gr.GuestRequestKey));
            convered.Add(new XElement("jacuzzi", gr.jacuzzi));
            convered.Add(new XElement("MailAddress", gr.MailAddress));
            convered.Add(new XElement("pool", gr.pool));
            convered.Add(new XElement("RegistrationDate", gr.RegistrationDate));
            convered.Add(new XElement("ReleaseDate", gr.ReleaseDate));
            convered.Add(new XElement("statusRequest", gr.statusRequest));
            convered.Add(new XElement("types", gr.types));
            convered.Add(new XElement("tv", gr.tv));
            convered.Add(new XElement("RoomService", gr.RoomService));
            convered.Add(new XElement("meals", gr.meals));
            convered.Add(new XElement("Playpen", gr.Playpen));

            return convered;
        }
        XElement ConverHostingUnitToXElement(HostingUnit hu)
        {
            XElement convered = new XElement("hostingUnit");

            
            convered.Add(new XElement("HostingUnitKey", hu.HostingUnitKey));
            convered.Add(new XElement("HostingUnitName", hu.HostingUnitName));
            convered.Add(hu.Diary);
            convered.Add(new XElement("area", hu.area));
            XElement hostXelement = new XElement("host");
            XElement BankBranchXelement = new XElement("BankBranchDetails");
            BankBranchXelement.Add(new XElement("BankNumber", hu.Owner.BankBranchDetails.BankNumber));
            BankBranchXelement.Add(new XElement("BankName", hu.Owner.BankBranchDetails.BankName));
            BankBranchXelement.Add(new XElement("BranchNumber", hu.Owner.BankBranchDetails.BranchNumber));
            BankBranchXelement.Add(new XElement("BranchAddress", hu.Owner.BankBranchDetails.BranchAddress));
            BankBranchXelement.Add(new XElement("BranchCity", hu.Owner.BankBranchDetails.BranchCity));
            hostXelement.Add(new XElement("HostKey", hu.Owner.HostKey));
            hostXelement.Add(new XElement("PrivateName", hu.Owner.PrivateName));
            hostXelement.Add(new XElement("FamilyName", hu.Owner.FamilyName));
            hostXelement.Add(new XElement("FhoneNumber", hu.Owner.FhoneNumber));
            hostXelement.Add(new XElement("MailAddress", hu.Owner.MailAddress));
            hostXelement.Add(BankBranchXelement);
            hostXelement.Add(new XElement("BankAccountNumber", hu.Owner.BankAccountNumber));
            hostXelement.Add(new XElement("CollectionClearance", hu.Owner.CollectionClearance));
            convered.Add(hostXelement);
            return convered;
        }
        XElement ConverOrderToXElement(Order o)
        {
            XElement convered = new XElement("order");

            convered.Add(new XElement("HostingUnitKey", o.HostingUnitKey));
            convered.Add(new XElement("GuestRequestKey", o.GuestRequestKey));
            convered.Add(new XElement("OrderKey", o.OrderKey));
            convered.Add(new XElement("statusOrder", o.statusOrder));
            convered.Add(new XElement("CreateDate", o.CreateDate));
            convered.Add(new XElement("MailDate", o.MailDate));
            return convered;
        }
        public void UpdateConfiguration()
        {

            XElement expireRequest = new XElement("expireRequest", BE.Configuration.expireRequest);
            XElement HostingUnitKeySeq = new XElement("hostingUnitKeySeq", BE.Configuration.hostingUnitKeySeq);
            XElement GuestRequestKeySeq = new XElement("guestRequestKeySeq", BE.Configuration.guestRequestKeySeq);
            XElement OrderKeySeq = new XElement("OrderKeyseq", BE.Configuration.OrderKeyseq);
            XElement fee = new XElement("fee", BE.Configuration.fee);
            ConfigurationRoot = new XElement("configuration", expireRequest, GuestRequestKeySeq, HostingUnitKeySeq, OrderKeySeq, fee);
            ConfigurationRoot.Save(ConfigurationPath);


        }
        public void addClientRequest(GuestRequest gr)
        {
          
            XElement GuestRequestElement = (from p in GuestRequestRoot.Elements()
                                            where Convert.ToInt32(p.Element("GuestRequestKey").Value) == gr.GuestRequestKey
                                            select p).FirstOrDefault();
            if (GuestRequestElement != null)
                throw new Exceptions("This guest request isn't exist, can't update (DAL)", gr.GuestRequestKey, gr.PrivateName);

            else
            {
                int con = Convert.ToInt32(ConfigurationRoot.Element("guestRequestKeySeq").Value);
                con++;
                Configuration.guestRequestKeySeq += 1;
                ConfigurationRoot.Element("guestRequestKeySeq").Value = Convert.ToString(con);
                ConfigurationRoot.Save(ConfigurationPath);


                gr.GuestRequestKey = con;
                UpdateConfiguration();
                gr.GuestRequestKey = Configuration.guestRequestKeySeq;
                gr.RegistrationDate = DateTime.Today;
                gr.statusRequest = StatusRequest.Active;
                GuestRequestRoot.Add(ConverGRToXElement(gr));
                GuestRequestRoot.Save(GuestRequestPath);
              
            }
        }
       



        public void addHostingUnit(HostingUnit hu)
        {
           
            XElement HostingUnitElement = (from p in HostingUnitRoot.Elements()
                                           where Convert.ToInt64(p.Element("HostingUnitKey").Value) == hu.HostingUnitKey
                                           select p).FirstOrDefault();
            if (HostingUnitElement != null)
                throw new Exceptions("This hosting unit is exist, can't add (DAL)", hu.HostingUnitKey, hu.HostingUnitName);
            else
            {
                int con = Convert.ToInt32(ConfigurationRoot.Element("hostingUnitKeySeq").Value);
                con++;
                Configuration.hostingUnitKeySeq += 1;
                ConfigurationRoot.Element("hostingUnitKeySeq").Value = Convert.ToString(con);
                ConfigurationRoot.Save(ConfigurationPath);
                hu.HostingUnitKey = con;
                UpdateConfiguration();
                hu.Diary = new bool[12, 31];
                for (int i = 1; i < 12; i++)
                {
                    for (int j = 1; j < 31; j++)
                    {
                        hu.Diary[i, j] = false;
                    }
                }
                HostingUnitRoot.Add(ConverHostingUnitToXElement(hu));
                HostingUnitRoot.Save(HostingUnitPath);

            }
        }
        
        public void addOrder(Order o, HostingUnit h, GuestRequest g)
        {
           
                XElement OrderElement = (from p in OrderRoot.Elements()
                                         where Convert.ToInt64(p.Element("OrderKey").Value) == o.OrderKey
                                         select p).FirstOrDefault();
                if (OrderElement != null)
                    throw new Exceptions("This order is exist, can't add (DAL)", o.OrderKey, o.CreateDate.ToString());
                else
                {
                    long config = Convert.ToInt64(ConfigurationRoot.Element("OrderKeyseq").Value);
                    config++;
                    ConfigurationRoot.Element("OrderKeyseq").Value = Convert.ToString(config);
                    ConfigurationRoot.Save(ConfigurationPath);

                    o.OrderKey = config;
                    Configuration.OrderKeyseq += 1;
                    UpdateConfiguration();
                    o.OrderKey = Configuration.OrderKeyseq;
                    o.CreateDate = DateTime.Today;
                    o.statusOrder = StatusOrder.NotYetAddressed;

                    OrderRoot.Add(ConverOrderToXElement(o));
                    OrderRoot.Save(OrderPath);
                }
            }
        
        public void deleteHostingUnit(HostingUnit hu)
        {
           
                XElement HostingUnitElement = (from p in HostingUnitRoot.Elements()
                                               where Convert.ToInt32(p.Element("HostingUnitKey").Value) == hu.HostingUnitKey
                                               select p).FirstOrDefault();
                if (HostingUnitElement == null)
                    throw new Exceptions("This hosting unit isn't exist, can't delete (DAL)", hu.HostingUnitKey, hu.HostingUnitName);
                HostingUnitElement.Remove();
                HostingUnitRoot.Save(HostingUnitPath);
               
            }
        
       public List<int> bankNum()
        {
            List<int> l = new List<int>();
            foreach (var item in listBanks())
            {
                if(!l.Contains(item.BankNumber))
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

        public List<BankBranch> listBanks()
        {
        var x = from item in banksRoot.Elements()
                select ConvertBankBranch(item);
                return x.ToList();
        
        }

        BankBranch ConvertBankBranch(XElement element)
        {
            return new BankBranch()
            {
                BankNumber = int.Parse(element.Element("קוד_בנק").Value),
                BankName = element.Element("שם_בנק").Value,
                BranchNumber = int.Parse(element.Element("קוד_סניף").Value),
                BranchAddress = element.Element("כתובת_ה-ATM").Value,
                BranchCity = element.Element("ישוב").Value
            };
        }
        public List<GuestRequest> listReqs()
        {
            try
            {
               

                var x = from item in GuestRequestRoot.Elements()
                        select ConvertGuestRequest(item);
                return x.ToList();

            }
            catch (Exception)
            {
                throw new Exception("בעיה בקובץ בקשות הלקוחות");
            }
          
            
        }

        private GuestRequest ConvertGuestRequest(XElement element)
        {
            GuestRequest t = new GuestRequest();
            
            t.Adults = int.Parse(element.Element("Adults").Value);
            t.area = (Area)Enum.Parse(typeof(Area), element.Element("area").Value);
            t.Children = int.Parse(element.Element("Children").Value);
            t.PrivateName = element.Element("PrivateName").Value;
            t.childrensAttractions = (ChildrensAttractions)Enum.Parse(typeof(ChildrensAttractions), element.Element("childrenAttractions").Value);
            t.EntryDate = DateTime.Parse(element.Element("EntryDate").Value);
            t.FamilyName = element.Element("FamilyName").Value;
            t.GuestRequestKey = int.Parse(element.Element("GuestRequestKey").Value);

            t.types = (Types)Enum.Parse(typeof(Types), element.Element("types").Value);
            t.ReleaseDate = DateTime.Parse(element.Element("ReleaseDate").Value);
            t.MailAddress = element.Element("MailAddress").Value;
            t.SubArea = element.Element("SubArea").Value;
            t.statusRequest = (StatusRequest)Enum.Parse(typeof(StatusRequest), element.Element("statusRequest").Value);
            
            
            t.garden = (Garden)Enum.Parse(typeof(Garden), element.Element("garden").Value);

            t.jacuzzi = (Jacuzzi)Enum.Parse(typeof(Jacuzzi), element.Element("jacuzzi").Value);
            t.pool = (Pool)Enum.Parse(typeof(Pool), element.Element("pool").Value);
            t.RegistrationDate = DateTime.Parse(element.Element("RegistrationDate").Value);
            t.tv = (TV)Enum.Parse(typeof(TV), element.Element("tv").Value);
            t.RoomService = bool.Parse(element.Element("RoomService").Value);
            t.Playpen = bool.Parse(element.Element("Playpen").Value);
            t.meals = (Meals)Enum.Parse(typeof(Meals), element.Element("meals").Value);

            return t;
        }
        private HostingUnit convertHostingUnit(XElement element)
        {
           
            HostingUnit t = new HostingUnit();
            t.HostingUnitKey = int.Parse(element.Element("HostingUnitKey").Value);
            t.area = (Area)Enum.Parse(typeof(Area), element.Element("area").Value);
            t.HostingUnitName = element.Element("HostingUnitName").Value;
            t.Diary = new bool[12, 31];
            t.Owner = new Host();
            t.Owner.BankAccountNumber = int.Parse(element.Element("host").Element("BankAccountNumber").Value);
            t.Owner.CollectionClearance = bool.Parse(element.Element("host").Element("CollectionClearance").Value);
            t.Owner.FamilyName = element.Element("host").Element("FamilyName").Value;
            t.Owner.HostKey = long.Parse(element.Element("host").Element("HostKey").Value);
            t.Owner.MailAddress = element.Element("host").Element("MailAddress").Value;
            t.Owner.FhoneNumber = long.Parse(element.Element("host").Element("FhoneNumber").Value);
            t.Owner.PrivateName = element.Element("host").Element("PrivateName").Value;
            t.Owner.BankBranchDetails = new BankBranch();
            t.Owner.BankBranchDetails.BankName = element.Element("host").Element("BankBranchDetails").Element("BankName").Value;
            t.Owner.BankBranchDetails.BankNumber = int.Parse(element.Element("host").Element("BankBranchDetails").Element("BankNumber").Value);
            t.Owner.BankBranchDetails.BranchAddress = element.Element("host").Element("BankBranchDetails").Element("BranchAddress").Value;
            t.Owner.BankBranchDetails.BranchCity = element.Element("host").Element("BankBranchDetails").Element("BranchCity").Value;
            t.Owner.BankBranchDetails.BranchNumber = int.Parse(element.Element("host").Element("BankBranchDetails").Element("BranchNumber").Value);
            return t;
        }
        private Order convertOrder(XElement element)
        {

            Order t = new Order();
            t.HostingUnitKey = long.Parse(element.Element("HostingUnitKey").Value);
            t.GuestRequestKey= long.Parse(element.Element("GuestRequestKey").Value);
            t.OrderKey= long.Parse(element.Element("OrderKey").Value);
            t.statusOrder = (StatusOrder)Enum.Parse(typeof(StatusOrder), element.Element("statusOrder").Value);
            t.CreateDate = DateTime.Parse(element.Element("CreateDate").Value);
            t.MailDate = DateTime.Parse(element.Element("MailDate").Value);
            return t;
        }

            public List<HostingUnit> listHosts()
        {
            try
            {
               

                var x = from item in HostingUnitRoot.Elements()
                        select convertHostingUnit(item);
                return x.ToList();

            }
            catch (Exception)
            {
                throw new Exception("בעיה בקובץ יחידות האירוח");
            }

        
        }
        public List<Order> listOrs()
        {
            var x = from item in OrderRoot.Elements()
                    select convertOrder(item);
            return x.ToList();

        }
        public void updateClientRequestStatus(GuestRequest gr, StatusRequest s)
        {
          
                XElement GuestRequestElement = (from p in GuestRequestRoot.Elements()
                                                where Convert.ToInt32(p.Element("GuestRequestKey").Value) == gr.GuestRequestKey
                                                select p).FirstOrDefault();
                if (GuestRequestElement == null)
                    throw new Exceptions("This guest request isn't exist, can't update (DAL)", gr.GuestRequestKey, gr.PrivateName);

                GuestRequestElement.Remove();
                GuestRequestRoot.Add(ConverGRToXElement(gr));
                GuestRequestRoot.Save(GuestRequestPath);
            }
        
        public void updateHostingUnit(HostingUnit hu)
        {
          
                XElement HostingUnitElement = (from p in HostingUnitRoot.Elements()
                                               where Convert.ToInt32(p.Element("HostingUnitKey").Value) == hu.HostingUnitKey
                                               select p).FirstOrDefault();
                if (HostingUnitElement == null)
                    throw new Exceptions("This hosting unit isn't exist, can't update (DAL)", hu.HostingUnitKey, hu.HostingUnitName);
                HostingUnitElement.Remove();
                HostingUnitRoot.Add(ConverHostingUnitToXElement(hu));
                HostingUnitRoot.Save(HostingUnitPath);
              
            }
       
      
        public void updateOrder(Order o, StatusOrder os)
        {
          
                XElement OrderElement = (from p in OrderRoot.Elements()
                                         where Convert.ToInt32(p.Element("OrderKey").Value) == o.OrderKey
                                         select p).FirstOrDefault();

                if (OrderElement == null)
                    throw new Exceptions("This order isn't exist, can't can't update (DAL)", o.OrderKey, o.CreateDate.ToString());
                o.statusOrder = os;
                OrderElement.Remove();
                OrderRoot.Add(ConverOrderToXElement(o));
                OrderRoot.Save(OrderPath);

        
        }
        
    }
}



