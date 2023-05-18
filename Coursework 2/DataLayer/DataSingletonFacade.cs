using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataLayer
{
    public class DataSingletonFacade
    {
        const string SAVEPATH = @"..\..\..\DataLayer\SaveData\";
        private int[] VISITTIMES = {60, 20, 30, 30}; //Holds visit duration times for each respective visit category

        private List<Person> people = new List<Person>();
        private List<Visit> visits = new List<Visit>();
        private int visitID = 0;

        private static DataSingletonFacade instance;

        public List<Person> People
        {
            get { return people; }
        }

        public List<Visit> Visits
        {
            get { return visits; }
        }

        public static DataSingletonFacade Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataSingletonFacade();
                }
                return instance;
            }
        }

        private DataSingletonFacade()
        { }

        public bool NewStaff(string c, int i, string f, string s, string a1, string a2, double la, double lo)
        {
            StaffCategory staffCategory;

            foreach (Staff j in people.OfType<Staff>())
            {
                if (j.ID == i)
                    throw new Exception("Error - The id given was already assigned to a staff member in new staff (id " + i + " fname " + f + " sname " + s + ")");
            }

            switch (c)
            {
                case "General Practitioner":
                    staffCategory = StaffCategory.GeneralPractitioner;
                    break;
                case "Community Nurse":
                    staffCategory = StaffCategory.CommunityNurse;
                    break;
                case "Social Worker":
                    staffCategory = StaffCategory.SocialWorker;
                    break;
                case "Care Worker":
                    staffCategory = StaffCategory.CareWorker;
                    break;
                default:
                    throw new Exception("Error - The category given did not exist in new staff (id " + i + " fname " + f + " sname " + s + ")");
            }

            people.Add(new Staff(i, staffCategory, f, s, a1, a2, lo, la));

            return true;
        }

        public bool NewClient(int i, string f, string s, string a1, string a2, double la, double lo)
        {
            foreach (Client c in people.OfType<Client>())
            {
                if (c.ID == i)
                {
                    throw new Exception("Error - The id given was already assigned to a client in new client (id " + i + " fname " + f + " sname " + s + ")");
                }
            }

            people.Add(new Client(i, f, s, a1, a2, lo, la));
            return true;
        }

        private int GetNewVisitID()
        {
            visitID++;
            return visitID;
        }

        public bool NewVisit(int c, int[] s, VisitTypes t, DateTime d)
        {
            Staff staff0 = null;
            Staff staff1 = null;
            Client client = null;

            //Check staff exist for given id(s)
            foreach (Staff i in people.OfType<Staff>())
            {
                if (i.ID == s[0])
                {
                    staff0 = i;
                }
            }
            if (staff0 == null)
            {
                throw new Exception("Error - The staff id given did not exist in visit (Patient " + c + " @" + d + ")");
            }
            if (s.Length > 1)
            {
                foreach (Staff i in people.OfType<Staff>())
                {
                    if (i.ID == s[1])
                    {
                        staff1 = i;
                    }
                }
                if (staff1 == null)
                {
                    throw new Exception("Error - The staff id given did not exist in visit (Patient " + c + " @" + d + ")");
                }
            }

            //Check client exists for given id
            foreach (Client i in people.OfType<Client>())
            {
                if (i.ID == c)
                {
                    client = i;
                }
            }
            if (client == null)
            {
                throw new Exception("Error - The client id given did not exist in visit (Patient " + c + " @" + d + ")");
            }

            switch (s.Length)
            {
                case 1:
                    switch (t)
                    {
                        case VisitTypes.Medication:
                            if (staff0.Category != StaffCategory.CommunityNurse)
                            {
                                throw new Exception("Error - The staff given is not of the correct type for visit (Patient " + c + " @" + d + ")");
                            }
                            break;
                        case VisitTypes.Meal:
                            if (staff0.Category != StaffCategory.CareWorker)
                            {
                                throw new Exception("Error - The staff given is not of the correct type for visit (Patient " + c + " @" + d + ")");
                            }
                            break;
                        default:
                            throw new Exception("Error - The type of visit given was not possible for the number of staff in visit (Patient " + c + " @" + d + ")");
                    }
                    break;
                case 2:
                    switch (t)
                    {
                        case VisitTypes.Assessment:
                            if (!(staff1.Category == StaffCategory.SocialWorker && staff0.Category == StaffCategory.GeneralPractitioner || staff1.Category == StaffCategory.GeneralPractitioner && staff0.Category == StaffCategory.SocialWorker))
                            {
                                throw new Exception("Error - One or more staff given is not of the correct type for visit (Patient " + c + " @" + d + ")");
                            }
                            break;
                        case VisitTypes.Bath:
                            if (staff1.Category != StaffCategory.CareWorker && staff0.Category != StaffCategory.CareWorker)
                            {
                                throw new Exception("Error - One or more staff given is not of the correct type for visit (Patient " + c + " @" + d + ")");
                            }
                            break;
                        default:
                            throw new Exception("Error - The type of visit given was not possible for the number of staff in visit (Patient " + c + " @" + d + ")");
                    }
                    break;
                default:
                    throw new Exception("Error - The number of staff given was not valid in visit (Patient " + c + " @" + d + ")");
            }

            DateTime finishDateTime = d.AddMinutes(VISITTIMES[(int)t]);

            //Finish date/time clash check
            foreach (Visit v in visits)
            {
                DateTime vFinishDateTime = v.DateTime.AddMinutes(VISITTIMES[(int)v.Type]);
                if (s.Length == 1)
                {
                    foreach (int i in v.StaffID)
                    {
                        if (i == s[0])
                        {
                            if ((d >= v.DateTime && d < vFinishDateTime) || (finishDateTime > v.DateTime && finishDateTime <= vFinishDateTime))
                            {
                                throw new Exception("Error - The staff member was already scheduled for the given date/time in visit (Patient " + c + " @" + d + ")");
                            }
                        }
                    }
                }
                else
                {
                    foreach (int i in v.StaffID)
                    {
                        if (i == s[0] || i == s[1])
                        {
                            if ((d >= v.DateTime && d < vFinishDateTime) || (finishDateTime > v.DateTime && finishDateTime <= vFinishDateTime))
                            {
                                throw new Exception("Error - One or more staff was already scheduled for the given date/time in visit (Patient " + c + " @" + d + ")");
                            }
                        }
                    }
                }

                if (c == v.ClientID)
                {
                    if ((d >= v.DateTime && d < vFinishDateTime) || (finishDateTime > v.DateTime && finishDateTime <= vFinishDateTime))
                    {
                        throw new Exception("Error - The client was already scheduled for the given date/time in visit (Patient " + c + " @" + d + ")");
                    }
                }
            }

            int id = GetNewVisitID();

            switch (s.Length)
            {
                case 1:
                    visits.Add(new Visit(id, c, s[0], t, d));
                    break;
                case 2:
                    visits.Add(new Visit(id, c, s[0], s[1], t, d));
                    break;
            }

            return true;
        }

        public void Clear()
        {
            people.Clear();
            visits.Clear();
        }

        public void Save()
        {
            try
            {
                StreamReader r = new StreamReader(SAVEPATH);
            }
            catch
            {
                Directory.CreateDirectory(SAVEPATH);
            }

            using (StreamWriter file = new StreamWriter(SAVEPATH + "staff.csv"))
            {
                file.AutoFlush = true;
                foreach (Staff s in people.OfType<Staff>())
                {
                    file.WriteLine(s.ToString());
                }
            }

            using (StreamWriter file = new StreamWriter(SAVEPATH + "clients.csv"))
            {
                file.AutoFlush = true;
                foreach (Client c in people.OfType<Client>())
                {
                    file.WriteLine(c.ToString());
                }
            }

            using (StreamWriter file = new StreamWriter(SAVEPATH + "visits.csv"))
            {
                file.AutoFlush = true;
                foreach (Visit v in visits)
                {
                    file.WriteLine(v.ToString());
                }
            }
        }

        public bool Load()
        {
            try
            {
                using (StreamReader file = new StreamReader(SAVEPATH + "staff.csv"))
                {
                    for (string line = file.ReadLine(); line != null; line = file.ReadLine())
                    {
                        string[] staffAttribs = line.Split(',');

                        int id = int.Parse(staffAttribs[0]);
                        string firstName = staffAttribs[1];
                        string surname = staffAttribs[2];
                        string address1 = staffAttribs[3];
                        string address2 = staffAttribs[4];
                        double longitude = double.Parse(staffAttribs[5]);
                        double latitude = double.Parse(staffAttribs[6]);
                        StaffCategory category = (StaffCategory)Enum.Parse(typeof(StaffCategory), staffAttribs[7]);

                        people.Add(new Staff(id, category, firstName, surname, address1, address2, longitude, latitude));
                    }
                }
            }
            catch
            {
                return false;
            }

            try
            {
                using (StreamReader file = new StreamReader(SAVEPATH + "clients.csv"))
                {
                    for (string line = file.ReadLine(); line != null; line = file.ReadLine())
                    {
                        string[] clientAttribs = line.Split(',');

                        int id = int.Parse(clientAttribs[0]);
                        string firstName = clientAttribs[1];
                        string surname = clientAttribs[2];
                        string address1 = clientAttribs[3];
                        string address2 = clientAttribs[4];
                        double longitude = double.Parse(clientAttribs[5]);
                        double latitude = double.Parse(clientAttribs[6]);

                        people.Add(new Client(id, firstName, surname, address1, address2, longitude, latitude));
                    }
                }
            }
            catch
            {
                return false;
            }

            try
            {
                using (StreamReader file = new StreamReader(SAVEPATH + "visits.csv"))
                {
                    for (string line = file.ReadLine(); line != null; line = file.ReadLine())
                    {
                        string[] visitAttribs = line.Split(',');

                        int id = int.Parse(visitAttribs[0]);

                        if (visitID < id)
                            visitID = id;

                        VisitTypes type = (VisitTypes)Enum.Parse(typeof(VisitTypes), visitAttribs[1]);
                        DateTime dateTime = Convert.ToDateTime(visitAttribs[2]);
                        int clientID = int.Parse(visitAttribs[3]);
                        int staffID1 = int.Parse(visitAttribs[4]);

                        if (visitAttribs.Length > 5)
                        {
                            int staffID2 = int.Parse(visitAttribs[5]);
                            visits.Add(new Visit(id, clientID, staffID1, staffID2, type, dateTime));
                        }
                        else
                        {
                            visits.Add(new Visit(id, clientID, staffID1, type, dateTime));
                        }
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
