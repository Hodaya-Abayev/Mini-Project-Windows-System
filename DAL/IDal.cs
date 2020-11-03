using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public interface IDal
    {
        //הוספה , עידכון ,ומחיקה לפי הצורך של יחידות אירוח,דרישות לקוח והזמנות
        void addClientRequest(GuestRequest gr);
        void updateClientRequestStatus(GuestRequest gr, StatusRequest s);   
        void addHostingUnit(HostingUnit ht);
        void deleteHostingUnit(HostingUnit ht);
        void updateHostingUnit(HostingUnit ht );
        void addOrder(Order or, HostingUnit hu, GuestRequest g);
        void updateOrder(Order or, StatusOrder so);
        List<HostingUnit> listHosts();
        List<GuestRequest> listReqs();
        List<Order> listOrs();
        List<BankBranch> listBanks();
        List<int> bankNum();
        List<string> bankName();
        List<int> branchNum();
        List<string> branchAdress();
        List<string> branchCity();

    }
}
