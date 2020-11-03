using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class GuestRequest// מחלקה שמייצגת דרישות אירוח של לקוחות

    {
        public long GuestRequestKey { get; set; }
        public string PrivateName { get; set; }
        public string FamilyName { get; set; }
        public string MailAddress { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string SubArea { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public StatusRequest statusRequest { get; set; }
        public Area area { get; set; }
        public Types types { get; set; }
        public Pool pool { get; set; }
        public Jacuzzi jacuzzi { get; set; }
        public Garden garden { get; set; }
        public ChildrensAttractions childrensAttractions { get; set; }
        public TV tv { get; set; }
        public bool RoomService { get; set; }
        public bool Playpen { get; set; }
        public Meals meals { get; set; }
        public override string ToString()//for debugging
        {
            return GuestRequestKey.ToString();

        }
    }
}
