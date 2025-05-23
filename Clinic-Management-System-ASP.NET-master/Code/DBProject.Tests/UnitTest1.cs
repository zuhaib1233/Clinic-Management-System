using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using System.Web;

using System.Web.SessionState;

namespace DBProject.Tests
{
    [TestClass]
    public class AppointmentNotificationSentTests
    {
        [DataTestMethod]
        [DataRow(1, 1, 10, "Appointment request sent successfully")]
        [DataRow(2, 1, 20, "There was some error in sending appointment request to the Doctor.")]
        public void TestSendAppointmentRequest(int doctorID, int patientID, int slot, string expectedMessage)
        {
            // Arrange
            var mockDAL = new Mock<myDAL>();
            var objAppointmentNotification = new AppointmentNotificationSent();


            // Mock the insertAppointment method of the DAL
            mockDAL.Setup(m => m.insertAppointment(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), ref It.Ref<string>.IsAny))
                   .Returns((int dID, int pID, int freeSlot, ref string message) =>
                   {
                       message = "Appointment request sent successfully"; // Simulating a success message
                       if (dID == 2) // Mocking failure for certain doctor ID
                       {
                           message = "There was some error in sending appointment request to the Doctor.";
                           return -1; // Simulate error
                       }
                       return 1; // Simulate success
                   });

            // Act
            string message = objAppointmentNotification.SendAppointmentRequest();

            // Assert
            Assert.AreEqual(expectedMessage, message);
        }
    }
}
