using System;
using System.Collections.Generic;

namespace DataLayer
{
    public enum VisitTypes
    {
        Assessment,
        Medication,
        Bath,
        Meal
    }

    public class Visit
    {
        private readonly int id;
        private int clientID;
        private VisitTypes type;
        private DateTime dateTime;

        private List<int> staffID = new List<int>();

        public int ID
        {
            get { return id; }
        }

        public int ClientID
        {
            get { return clientID; }
        }

        public VisitTypes Type
        {
            get { return type; }
        }

        public DateTime DateTime
        {
            get { return dateTime; }
        }

        public List<int> StaffID
        {
            get { return staffID; }
        }

        public Visit(int i, int c, int s, VisitTypes t, DateTime d)
        {
            id = i;
            clientID = c;
            type = t;
            dateTime = d;

            staffID.Add(s);
        }

        public Visit(int i, int c, int s1, int s2, VisitTypes t, DateTime d)
        {
            id = i;
            clientID = c;
            type = t;
            dateTime = d;

            staffID.Add(s1);
            staffID.Add(s2);
        }

        public string Display()
        {
            string returnString = "ID: " + id + ", "
                                + "Visit Type: " + type + ", "
                                + "Date and Time: " + dateTime.ToString() + ", "
                                + "Client ID: " + clientID + ", "
                                + "Staff ID(s): ";

            //Only 2 staff max so no need for a for loop
            if (staffID.Count == 1)
            { 
                returnString += staffID[0];
            }
            else
            {
                returnString += staffID[0] + ", " + staffID[1];
            }

            returnString += "\n";

            return returnString;
        }

        public override string ToString()
        {
            string returnString = id + "," + type + "," + dateTime.ToString() + "," + clientID + ",";

            if (staffID.Count == 1)
            {
                returnString += staffID[0];
            }
            else
            {
                returnString += staffID[0] + "," + staffID[1];
            }

            return returnString;
        }
    }
}
