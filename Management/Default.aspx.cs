using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using StandardClasses;
using System.Net;
using System.Net.NetworkInformation;


namespace Management
{
    public partial class _Default : System.Web.UI.Page
    {
        ClassConnection NC;

        protected void Page_Load(object sender, EventArgs e)
        {
            NC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.managementconnectionname);

            ClassAppDetails.managementcurrentconnection = NC;

            //ClassAppDetails.managementcurrentconnection.Connect();

            LoadTable();
        }

        public void LoadTable()
        {
            DataSet DS = ClassKiosks.loadDataset();

            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                TableRow TR = new TableRow();

                TableCell[] TC = new TableCell[5];
                TC[0] = new TableCell();
                TC[0].Text = DR[2].ToString();
                TC[1] = new TableCell();
                TC[1].Text = DR[1].ToString();
                TC[2] = new TableCell();
                TC[2].Text = isKioskAlive(DR[1].ToString().Trim());
                TC[3] = new TableCell();
                TC[3].Text = ClassStats.TodaysHits(Convert.ToInt16(DR[0].ToString())).ToString();
                TC[4] = new TableCell();
                TC[4].Text = ClassStats.WeeksHits(Convert.ToInt16(DR[0].ToString())).ToString();
                
                foreach (TableCell TabCel in TC)
                {
                    TR.Cells.Add(TabCel);
                }

                KiosksTable.Rows.Add(TR);
            }
        }

        public string isKioskAlive(string kioskIP)
        {
            try
            {
                
                Ping ping = new Ping();
                PingReply pingreply = ping.Send(kioskIP);

                if (pingreply.Status.ToString() == "Success")
                {
                    return "online";
                }
                else
                {
                    return "offline";
                }
            }
            catch (Exception err)
           {
                return "offline " + err;
            }


        }
    }

}
