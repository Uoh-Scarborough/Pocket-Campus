using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using StandardClasses;
using PocketCampusClasses;
using System.Data;

namespace PCWS
{
    /// <summary>
    /// PCWS Pocket Campus Web Service Pushs out informtion relating to Campus Communications
    /// </summary>
    [WebService(Namespace = "http://pcws.scar.hull.ac.uk/", Description = "PCWS Pocket Campus Web Service pushes out information relating to Production Booking", Name = "Pocket Campus Web Services - ProductionBooking")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class ProductionBooking : System.Web.Services.WebService
    {

        [WebMethod(CacheDuration = 1, Description = "Returns Current Room Activity")]
        public XmlDocument CurrentActivity(string Room = null)
        {
            ClassAppDetails.bookingcurrentconnection = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.bookingconnectionname);

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            XmlDocument Output = new XmlDocument();

            XmlDeclaration dec = Output.CreateXmlDeclaration("1.0", null, null);
            Output.AppendChild(dec);

            XmlElement root = Output.CreateElement("Categorys");
            Output.AppendChild(root);

            RQ.RunQuery("SELECT * FROM Categories WHERE Category_Deleted = 0 ORDER BY Category_Title");

            foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
            {
                XmlElement category = Output.CreateElement("Category");
                category.SetAttribute("ID", Convert.ToInt32(DR["Category_ID_LNK"]).ToString());
                category.InnerText = DR["Category_Title"].ToString();

                root.AppendChild(category);
            }

            return Output;
        }

    }
}
