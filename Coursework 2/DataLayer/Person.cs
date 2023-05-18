using System;

namespace DataLayer
{
    public class Location
    {
        private double latitude, longitude;

        public Location(double la, double lo)
        {
            latitude = la;
            longitude = lo;
        }

        public override string ToString()
        {
            string returnString = latitude + "," + longitude;

            return returnString;
        }
    }

    public class Person
    {
        protected int id;
        protected string firstName, surname, address1, address2;
        protected Location location;

        public int ID
        {
            get { return id; }
        }

        public Person(int i, string f, string s, string a1, string a2, double lo, double la)
        {
            id = i;
            firstName = f;
            surname = s;
            address1 = a1;
            address2 = a2;

            location = new Location(lo, la);
        }

        public virtual string Display()
        {
            String returnString = "ID: " + id + ", "
                                + "Name: " + firstName + " " + surname + ", "
                                + "Address: " + address1 + ", " + address2 + ", "
                                + "Location: " + location.ToString() + "\n";

            return returnString;
        }

        public override string ToString()
        {
            string returnString = id + "," + firstName + "," + surname + "," + address1 + "," + address2 + "," + location.ToString();

            return returnString;
        }
    }

}