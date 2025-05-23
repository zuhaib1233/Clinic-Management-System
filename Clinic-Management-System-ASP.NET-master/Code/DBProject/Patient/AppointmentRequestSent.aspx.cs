using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBProject.DAL;
using System.Data;



namespace DBProject
{
    public partial class AppointmentNotificationSent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        //-----------------------Function1--------------------------//
        protected void sendARequest(object sender, EventArgs e)
        {
            string message = SendAppointmentRequest();
            Message.Text = message;
        }

        // New function that handles the appointment request logic
        public string SendAppointmentRequest()
        {
            myDAL objmyDAl = new myDAL();

            string dID1 = (string)Session["dID"];
            int dID = Convert.ToInt32(dID1);

            int pID = (int)Session["idoriginal"];

            string temp = (string)Session["freeSlot"];
            int freeSlot = Convert.ToInt32(temp);

            string mes = "";
            int status = objmyDAl.insertAppointment(dID, pID, freeSlot, ref mes);

            if (status == -1)
            {
                return "There was some error in sending appointment request to the Doctor.";
            }
            else
            {
                return mes;
            }
        }
    }
}
