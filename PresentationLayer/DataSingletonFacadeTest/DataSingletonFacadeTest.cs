using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

using DataLayer;

namespace DataLayerTest
{
    [TestClass]
    public class DataSingletonFacadeTest
    {
        #region NewStaff Tests
        [TestMethod]
        public void NewStaff_ExistingID_ThrowsException()
        {
            string category = "General Practitioner";
            int id = 12;
            string firstName = "John";
            string surname = "Smith";
            string address1 = "17 Laine Lane";
            string address2 = "Taunton";
            double latitude = 12.345678;
            double longitude = 87.654321;

            DataSingletonFacade.Instance.NewStaff(category, id, firstName, surname, address1, address2, latitude, longitude);

            try
            {
                DataSingletonFacade.Instance.NewStaff(category, id, firstName, surname, address1, address2, latitude, longitude);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "The id given was already assigned to a staff member", "Unexpected Exception was thrown");
                return;
            }
            Assert.Fail("No exception was thrown");
        }

        [TestMethod]
        public void NewStaff_InvalidCategory_ThrowsException()
        {
            string category = "Genal Practioner";
            int id = 13;
            string firstName = "John";
            string surname = "Smith";
            string address1 = "17 Laine Lane";
            string address2 = "Taunton";
            double latitude = 12.345678;
            double longitude = 87.654321;

            try
            {
                DataSingletonFacade.Instance.NewStaff(category, id, firstName, surname, address1, address2, latitude, longitude);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "The category given did not exist", "Unexpected Exception was thrown");
                return;
            }
            Assert.Fail("No exception was thrown");
        }
        #endregion
        #region NewClient Tests
        [TestMethod]
        public void NewClient_ExistingID_ThrowsException()
        {
            int id = 22;
            string firstName = "John";
            string surname = "Smith";
            string address1 = "17 Laine Lane";
            string address2 = "Taunton";
            double latitude = 12.345678;
            double longitude = 87.654321;

            DataSingletonFacade.Instance.NewClient(id, firstName, surname, address1, address2, latitude, longitude);

            try
            {
                DataSingletonFacade.Instance.NewClient(id, firstName, surname, address1, address2, latitude, longitude);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "The id given was already assigned to a client", "Unexpected Exception was thrown");
                return;
            }
            Assert.Fail("No exception was thrown");
        }
        #endregion
        #region NewVisit Tests
        #region First Overload
        [TestMethod]
        public void NewVisit_FirstOverloadStaffDoesNotExist_ThrowsException()
        {
            int cid = 33;
            string firstName = "John";
            string surname = "Smith";
            string address1 = "17 Laine Lane";
            string address2 = "Taunton";
            double latitude = 12.345678;
            double longitude = 87.654321;

            int[] sid = { 34 };

            VisitTypes type = VisitTypes.Medication;
            DateTime dateTime = new DateTime(2020, 1, 1, 9, 0, 0);

            DataSingletonFacade.Instance.NewClient(cid, firstName, surname, address1, address2, longitude, latitude);

            try
            {
                DataSingletonFacade.Instance.NewVisit(cid, sid, type, dateTime);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "The staff id given did not exist", "Unexpected Exception was thrown");
                return;
            }
        }

        [TestMethod]
        public void NewVisit_FirstOverloadClientDoesNotExist_ThrowsException()
        {
            int cid = 35;

            int[] sid = { 36 };
            string firstName = "John";
            string surname = "Smith";
            string address1 = "17 Laine Lane";
            string address2 = "Taunton";
            double latitude = 12.345678;
            double longitude = 87.654321;
            string category = "Community Nurse";

            VisitTypes type = VisitTypes.Medication;
            DateTime dateTime = new DateTime(2020, 1, 1, 9, 0, 0);

            DataSingletonFacade.Instance.NewStaff(category, sid[0], firstName, surname, address1, address2, longitude, latitude);

            try
            {
                DataSingletonFacade.Instance.NewVisit(cid, sid, type, dateTime);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "The client id given did not exist", "Unexpected Exception was thrown");
                return;
            }
        }

        [TestMethod]
        public void NewVisit_FirstOverloadStaffTypeIncorrect_ThrowsException()
        {
            int cid = 37;
            string firstName = "John";
            string surname = "Smith";
            string address1 = "17 Laine Lane";
            string address2 = "Taunton";
            double latitude = 12.345678;
            double longitude = 87.654321;

            int[] sid = { 38 };
            string category = "Social Worker";

            VisitTypes type = VisitTypes.Medication;
            DateTime dateTime = new DateTime(2020, 1, 1, 9, 0, 0);

            DataSingletonFacade.Instance.NewStaff(category, sid[0], firstName, surname, address1, address2, longitude, latitude);
            DataSingletonFacade.Instance.NewClient(cid, firstName, surname, address1, address2, longitude, latitude);

            try
            {
                DataSingletonFacade.Instance.NewVisit(cid, sid, type, dateTime);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "The staff given is not of the correct type", "Unexpected Exception was thrown");
                return;
            }

            cid = 39;

            sid[0] = 310;

            type = VisitTypes.Meal;

            DataSingletonFacade.Instance.NewStaff(category, sid[0], firstName, surname, address1, address2, longitude, latitude);
            DataSingletonFacade.Instance.NewClient(cid, firstName, surname, address1, address2, longitude, latitude);

            try
            {
                DataSingletonFacade.Instance.NewVisit(cid, sid, type, dateTime);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "Error - The staff given is not of the correct type", "Unexpected Exception was thrown");
                return;
            }
        }

        [TestMethod]
        public void NewVisit_FirstOverloadVisitTypeIncorrect_ThrowsException()
        {
            int cid = 311;
            string firstName = "John";
            string surname = "Smith";
            string address1 = "17 Laine Lane";
            string address2 = "Taunton";
            double latitude = 12.345678;
            double longitude = 87.654321;

            int[] sid = { 312 };
            string category = "Community Nurse";

            VisitTypes type = VisitTypes.Assessment;
            DateTime dateTime = new DateTime(2020, 1, 1, 9, 0, 0);

            DataSingletonFacade.Instance.NewStaff(category, sid[0], firstName, surname, address1, address2, longitude, latitude);
            DataSingletonFacade.Instance.NewClient(cid, firstName, surname, address1, address2, longitude, latitude);

            try
            {
                DataSingletonFacade.Instance.NewVisit(cid, sid, type, dateTime);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "The type of visit given was not possible for the number of staff", "Unexpected Exception was thrown");
                return;
            }
        }

        [TestMethod]
        public void NewVisit_FirstOverloadStaffBusy_ThrowsException()
        {
            int cid1 = 313;
            int cid2 = 314;
            string firstName = "John";
            string surname = "Smith";
            string address1 = "17 Laine Lane";
            string address2 = "Taunton";
            double latitude = 12.345678;
            double longitude = 87.654321;

            int[] sid = { 315 };
            string category = "Community Nurse";

            VisitTypes type = VisitTypes.Medication;
            DateTime dateTime = new DateTime(2020, 1, 1, 9, 0, 0);

            DataSingletonFacade.Instance.NewStaff(category, sid[0], firstName, surname, address1, address2, longitude, latitude);
            DataSingletonFacade.Instance.NewClient(cid1, firstName, surname, address1, address2, longitude, latitude);
            DataSingletonFacade.Instance.NewClient(cid2, firstName, surname, address1, address2, longitude, latitude);

            DataSingletonFacade.Instance.NewVisit(cid1, sid, type, dateTime);

            try
            {
                DataSingletonFacade.Instance.NewVisit(cid2, sid, type, dateTime);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "The staff member was already scheduled for the given date/time", "Unexpected Exception was thrown");
                return;
            }
        }

        [TestMethod]
        public void NewVisit_FirstOverloadClientBusy_ThrowsException()
        {
            int cid = 316;
            string firstName = "John";
            string surname = "Smith";
            string address1 = "17 Laine Lane";
            string address2 = "Taunton";
            double latitude = 12.345678;
            double longitude = 87.654321;

            int[] sid1 = { 317 };
            int[] sid2 = { 318 };
            string category = "Community Nurse";

            VisitTypes type = VisitTypes.Medication;
            DateTime dateTime = new DateTime(2020, 1, 1, 9, 0, 0);

            DataSingletonFacade.Instance.NewStaff(category, sid1[0], firstName, surname, address1, address2, longitude, latitude);
            DataSingletonFacade.Instance.NewStaff(category, sid2[0], firstName, surname, address1, address2, longitude, latitude);
            DataSingletonFacade.Instance.NewClient(cid, firstName, surname, address1, address2, longitude, latitude);

            DataSingletonFacade.Instance.NewVisit(cid, sid1, type, dateTime);

            try
            {
                DataSingletonFacade.Instance.NewVisit(cid, sid2, type, dateTime);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "The client was already scheduled for the given date/time", "Unexpected Exception was thrown");
                return;
            }
        }
        #endregion
        #region Second Overload
        [TestMethod]
        public void NewVisit_SecondOverloadStaffDoesNotExist_ThrowsException()
        {
            int cid = 322;
            string firstName = "John";
            string surname = "Smith";
            string address1 = "17 Laine Lane";
            string address2 = "Taunton";
            double latitude = 12.345678;
            double longitude = 87.654321;

            int[] sid = { 323, 324 };

            VisitTypes type = VisitTypes.Assessment;
            DateTime dateTime = new DateTime(2020, 1, 1, 9, 0, 0);

            DataSingletonFacade.Instance.NewClient(cid, firstName, surname, address1, address2, longitude, latitude);

            try
            {
                DataSingletonFacade.Instance.NewVisit(cid, sid, type, dateTime);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "The staff id given did not exist", "Unexpected Exception was thrown");
                return;
            }
        }

        [TestMethod]
        public void NewVisit_SecondOverloadClientDoesNotExist_ThrowsException()
        {
            int cid = 325;

            int[] sid = { 326, 327 };
            string firstName = "John";
            string surname = "Smith";
            string address1 = "17 Laine Lane";
            string address2 = "Taunton";
            double latitude = 12.345678;
            double longitude = 87.654321;
            string category1 = "Social Worker";
            string category2 = "General Practitioner";

            VisitTypes type = VisitTypes.Medication;
            DateTime dateTime = new DateTime(2020, 1, 1, 9, 0, 0);

            DataSingletonFacade.Instance.NewStaff(category1, sid[0], firstName, surname, address1, address2, longitude, latitude);
            DataSingletonFacade.Instance.NewStaff(category2, sid[1], firstName, surname, address1, address2, longitude, latitude);

            try
            {
                DataSingletonFacade.Instance.NewVisit(cid, sid, type, dateTime);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "The client id given did not exist", "Unexpected Exception was thrown");
                return;
            }
        }

        [TestMethod]
        public void NewVisit_SecondOverloadStaffTypeIncorrect_ThrowsException()
        {
            string firstName = "John";
            string surname = "Smith";
            string address1 = "17 Laine Lane";
            string address2 = "Taunton";
            double latitude = 12.345678;
            double longitude = 87.654321;

            int cid = 328;
            int[] sid = { 329, 330 };
            string category1 = "Care Worker";
            string category2 = "General Practitioner";

            VisitTypes type = VisitTypes.Assessment;
            DateTime dateTime = new DateTime(2020, 1, 1, 9, 0, 0);

            DataSingletonFacade.Instance.NewStaff(category1, sid[0], firstName, surname, address1, address2, longitude, latitude);
            DataSingletonFacade.Instance.NewStaff(category2, sid[1], firstName, surname, address1, address2, longitude, latitude);
            DataSingletonFacade.Instance.NewClient(cid, firstName, surname, address1, address2, longitude, latitude);

            try
            {
                DataSingletonFacade.Instance.NewVisit(cid, sid, type, dateTime);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "One or more staff given is not of the correct type", "Unexpected Exception was thrown");
                return;
            }
        }

        [TestMethod]
        public void NewVisit_SecondOverloadVisitTypeIncorrect_ThrowsException()
        {
            string firstName = "John";
            string surname = "Smith";
            string address1 = "17 Laine Lane";
            string address2 = "Taunton";
            double latitude = 12.345678;
            double longitude = 87.654321;

            int cid = 331;
            int[] sid = { 332, 333 };
            string category1 = "Social Worker";
            string category2 = "General Practitioner";

            VisitTypes type = VisitTypes.Medication;
            DateTime dateTime = new DateTime(2020, 1, 1, 9, 0, 0);

            DataSingletonFacade.Instance.NewStaff(category1, sid[0], firstName, surname, address1, address2, longitude, latitude);
            DataSingletonFacade.Instance.NewStaff(category2, sid[1], firstName, surname, address1, address2, longitude, latitude);
            DataSingletonFacade.Instance.NewClient(cid, firstName, surname, address1, address2, longitude, latitude);

            try
            {
                DataSingletonFacade.Instance.NewVisit(cid, sid, type, dateTime);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "The type of visit given was not possible for the number of staff", "Unexpected Exception was thrown");
                return;
            }
        }

        [TestMethod]
        public void NewVisit_SecondOverloadStaffBusy_ThrowsException()
        {
            string firstName = "John";
            string surname = "Smith";
            string address1 = "17 Laine Lane";
            string address2 = "Taunton";
            double latitude = 12.345678;
            double longitude = 87.654321;

            int cid1 = 334;
            int cid2 = 335;
            int[] sid = { 336, 337 };
            string category1 = "Social Worker";
            string category2 = "General Practitioner";

            VisitTypes type = VisitTypes.Assessment;
            DateTime dateTime = new DateTime(2020, 1, 1, 9, 0, 0);

            DataSingletonFacade.Instance.NewStaff(category1, sid[0], firstName, surname, address1, address2, longitude, latitude);
            DataSingletonFacade.Instance.NewStaff(category2, sid[1], firstName, surname, address1, address2, longitude, latitude);
            DataSingletonFacade.Instance.NewClient(cid1, firstName, surname, address1, address2, longitude, latitude);
            DataSingletonFacade.Instance.NewClient(cid2, firstName, surname, address1, address2, longitude, latitude);
            DataSingletonFacade.Instance.NewVisit(cid1, sid, type, dateTime);

            try
            {
                DataSingletonFacade.Instance.NewVisit(cid2, sid, type, dateTime);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "One or more staff was already scheduled for the given date/time", "Unexpected Exception was thrown");
                return;
            }
        }

        [TestMethod]
        public void NewVisit_SecondOverloadClientBusy_ThrowsException()
        {
            string firstName = "John";
            string surname = "Smith";
            string address1 = "17 Laine Lane";
            string address2 = "Taunton";
            double latitude = 12.345678;
            double longitude = 87.654321;

            int cid = 338;
            int[] sid1 = { 339, 340 };
            int[] sid2 = { 341, 342 };
            string category1 = "Social Worker";
            string category2 = "General Practitioner";
            string category3 = "Social Worker";
            string category4 = "General Practitioner";

            VisitTypes type = VisitTypes.Assessment;
            DateTime dateTime = new DateTime(2020, 1, 1, 9, 0, 0);

            DataSingletonFacade.Instance.NewStaff(category1, sid1[0], firstName, surname, address1, address2, longitude, latitude);
            DataSingletonFacade.Instance.NewStaff(category2, sid1[1], firstName, surname, address1, address2, longitude, latitude);
            DataSingletonFacade.Instance.NewStaff(category3, sid2[0], firstName, surname, address1, address2, longitude, latitude);
            DataSingletonFacade.Instance.NewStaff(category4, sid2[1], firstName, surname, address1, address2, longitude, latitude);
            DataSingletonFacade.Instance.NewClient(cid, firstName, surname, address1, address2, longitude, latitude);
            DataSingletonFacade.Instance.NewVisit(cid, sid2, type, dateTime);

            try
            {
                DataSingletonFacade.Instance.NewVisit(cid, sid1, type, dateTime);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "The client was already scheduled for the given date/time", "Unexpected Exception was thrown");
                return;
            }
        }
        #endregion
        #endregion
        #region Clear Tests
        [TestMethod]
        public void Clear_RemovesAllValues()
        {
            int cid = 41;
            string firstName = "John";
            string surname = "Smith";
            string address1 = "17 Laine Lane";
            string address2 = "Taunton";
            double latitude = 12.345678;
            double longitude = 87.654321;

            int[] sid = { 42 };
            string category = "Community Nurse";

            VisitTypes type = VisitTypes.Medication;
            DateTime dateTime = new DateTime(2020, 1, 1, 9, 0, 0);

            Visit expected = new Visit(1, cid, sid[0], type, dateTime);

            DataSingletonFacade.Instance.NewClient(cid, firstName, surname, address1, address2, longitude, latitude);
            DataSingletonFacade.Instance.NewStaff(category, sid[0], firstName, surname, address1, address2, longitude, latitude);
            DataSingletonFacade.Instance.NewVisit(cid, sid, type, dateTime);

            DataSingletonFacade.Instance.Clear();

            Staff staff = null;
            Client client = null;
            Visit visit = null;

            foreach (Staff s in DataSingletonFacade.Instance.People.OfType<Staff>())
            {
                staff = s;
            }
            foreach (Client c in DataSingletonFacade.Instance.People.OfType<Client>())
            {
                client = c;
            }
            foreach (Visit v in DataSingletonFacade.Instance.People.OfType<Visit>())
            {
                visit = v;
            }

            if (staff != null)
            {
                Assert.Fail("Clear function failed to remove all staff");
            }
            else if (client != null)
            {
                Assert.Fail("Clear function failed to remove all clients");
            }
            else if (visit != null)
            {
                Assert.Fail("Clear function failed to remove all visits");
            }
        }
        #endregion
        #region Load Tests
        [TestMethod]
        public void Load_LoadsCorrectValues()
        {
            List<Person> expectedPeople = new List<Person>();
            List<Visit> expectedVisits = new List<Visit>();

            int[] s = { 1 };

            DataSingletonFacade.Instance.Clear();
            DataSingletonFacade.Instance.NewClient(1, "John", "Smith", "17 Laine Lane", "TaunTon", 12.345678, 87.654321);
            DataSingletonFacade.Instance.NewStaff("Care Worker", 1, "John", "Smith", "17 Laine Lane", "TaunTon", 12.345678, 87.654321);
            DataSingletonFacade.Instance.NewVisit(1, s, VisitTypes.Meal, new DateTime(2020, 1, 1, 9, 0, 0));

            expectedPeople = DataSingletonFacade.Instance.People;
            expectedVisits = DataSingletonFacade.Instance.Visits;

            DataSingletonFacade.Instance.Save();
            DataSingletonFacade.Instance.Load();

            Assert.AreEqual(expectedPeople, DataSingletonFacade.Instance.People, "Load did not load person values correctly");
            Assert.AreEqual(expectedVisits, DataSingletonFacade.Instance.Visits, "Load did not load visit values correctly");
        }
        #endregion
    }
}
