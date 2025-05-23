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
    public partial class AppointmentTaker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Refactored to initialize the session and load available free slots in a separate method.
            Session["freeSlot"] = "";
            freeSlots(sender, e);
        }


        //---------------Function Called whenever a Free Slot is selected from the Grid View----//
        protected void PAppointmentGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Int16 num = Convert.ToInt16(e.CommandArgument);

                string appointment = PAppointmentGrid.Rows[num].Cells[2].Text;

                string[] tokens = appointment.Split(':');

                // Refactored to directly store free slot info in session
                Session["freeSlot"] = tokens[0];

                // Redirecting to appointment request confirmation
                Response.BufferOutput = true;
                Response.Redirect("AppointmentRequestSent.aspx");

                return;
            }
        }


        //-----------------------Function1--------------------------//

        protected void freeSlots(object sender, EventArgs e)
        {
            // Refactored to encapsulate database access and handle free slot retrieval in a single method.
            myDAL objmyDAl = new myDAL();
            DataTable DT = new DataTable();

            // Fetching Doctor and Patient ID from session variables
            string dID1 = (string)Session["dID"];
            int dID = Convert.ToInt32(dID1);
            int pID = (int)Session["idoriginal"];

            // Fetching free slots from DAL layer
            int status = objmyDAl.getFreeSlots(dID, pID, ref DT);

            // Refactored to handle different outcomes with a clearer messaging system
            if (status == -1)
            {
                PAppointment.Text = "There was some error in retrieving the Doctors's Free Slots.";
            }
            else if (status == 0)
            {
                PAppointment.Text = "There is currently no free slot of this doctor.";
            }
            else if (status > 0)
            {
                PAppointment.Text = "The following are the " + status + " free slots of this doctor for today :";
                PAppointmentGrid.DataSource = DT;
                PAppointmentGrid.DataBind();
            }

            return;
        }

        //-----------------------Add a new function here------------------//

    }
}
