using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PCWSControl
{
    public partial class Control : Form
    {
        public Control()
        {
            InitializeComponent();
        }

        private void Updatecmd_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            UpdateFeeds();
            timer.Enabled = true;
        }

        private void UpdateFeeds()
        {
            PCWS.PocketCampusWebServicesCommunications WebServices = new PCWS.PocketCampusWebServicesCommunications();
            WebServices.Feeds();

            DateTime DT = DateTime.Now;

            LastUpdatelbl.Text = DT.ToShortDateString() + " - " + DT.ToShortTimeString();

            DateTime DT1 = DT.AddHours(1);

            NextUpdatelbl.Text = DT1.ToShortDateString() + " - " + DT1.ToShortTimeString();

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            UpdateFeeds();
        }
    }
}
