using System;

namespace DataLayer
{
    public enum StaffCategory
    {
        GeneralPractitioner,
        CommunityNurse,
        SocialWorker,
        CareWorker
    }

    public class Staff : Person
    {
        private StaffCategory category;

        public StaffCategory Category
        {
            get { return category; }
        }

        public Staff(int i, StaffCategory c, string f, string s, string a1, string a2, double lo, double la) : base(i,f,s,a1,a2,lo,la)
        {
            category = c;
        }

        public override string Display()
        {
            String returnString = "ID: " + id + ", "
                                + "Name: " + firstName + " " + surname + ", "
                                + "Staff Type: " + category.ToString() + ", "
                                + "Address: " + address1 + ", " + address2 + ", "
                                + "Location: " + location.ToString() + "\n";

            return returnString;
        }

        public override string ToString()
        {
            string returnString = base.ToString() + "," + category.ToString();

            return returnString;
        }
    }
}
