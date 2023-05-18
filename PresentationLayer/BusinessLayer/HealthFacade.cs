using System;
using System.Linq;

using DataLayer;

namespace BusinessLayer
{
    public static class visitTypes 
    {
        public const int assessment = 0;
        public const int medication = 1;
        public const int bath = 2;
        public const int meal = 3;
    }

    public class HealthFacade
    {
        public Boolean addStaff(int id, string firstName, string surname, string address1, string address2, string category, double baseLocLat, double baseLocLon)
        {
            try
            {
                return DataSingletonFacade.Instance.NewStaff(category, id, firstName, surname, address1, address2, baseLocLat, baseLocLon);
            }
            catch
            {
                return false;
            }
        }

        public Boolean addClient(int id, string firstName, string surname, string address1, string address2, double locLat, double locLon)
        {
            try
            { 
                return DataSingletonFacade.Instance.NewClient(id, firstName, surname, address1, address2, locLat, locLon);
            }
            catch
            {
                return false;
            }
        }

        public Boolean addVisit(int[] staff, int patient, int type, string dateTime)
        {
            VisitTypes visitType = (VisitTypes)type;

            return DataSingletonFacade.Instance.NewVisit(patient, staff, visitType, Convert.ToDateTime(dateTime));
        }

        public String getStaffList()
        {
            String result = "";

            foreach (Staff s in DataSingletonFacade.Instance.People.OfType<Staff>())
            {
                result = result + s.Display();
            }

            return result;
        }

        public String getClientList()
        {
            String result = "";

            foreach (Client c in DataSingletonFacade.Instance.People.OfType<Client>())
            {
                result = result + c.Display();
            }

            return result;
        }

        public String getVisitList()
        {
            String result = "";

            foreach (Visit v in DataSingletonFacade.Instance.Visits)
            {
                result = result + v.Display();
            }

            return result;
        }

        public void clear()
        {
            DataSingletonFacade.Instance.Clear();
        }

        public Boolean load()
        {
            DataSingletonFacade.Instance.Load();
            return true;
        }

        public bool save()
        {
            DataSingletonFacade.Instance.Save();
            return true;
        }
    }
}
