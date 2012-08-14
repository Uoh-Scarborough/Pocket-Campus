using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Xml;
using StandardClasses;
using PocketCampusClasses;
using System.Data;

namespace PCWS
{
    /// <summary>
    /// Summary description for Pages
    /// </summary>
    [WebService(Namespace = "http://pcws.scar.hull.ac.uk/", Description = "PCWS Pocket Campus Web Service pushes out information relating to Communication Pages", Name = "Pocket Campus Web Services - Pages")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Pages : System.Web.Services.WebService
    {

        [WebMethod(CacheDuration = 1, Description = "List Root Pages")]
        public XmlDocument RootPages()
        {
            ClassAppDetails.commscurrentconnection = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.noticesconnectionname);

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            XmlDocument Output = new XmlDocument();

            XmlDeclaration dec = Output.CreateXmlDeclaration("1.0", null, null);
            Output.AppendChild(dec);

            XmlElement root = Output.CreateElement("RootPages");
            Output.AppendChild(root);

            RQ.RunQuery("SELECT * FROM Pages WHERE Page_Parent_IDLNK = 1  AND Page_Deleted = 0 ORDER BY Page_Title");

            foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
            {
                XmlElement category = Output.CreateElement("Page");
                category.SetAttribute("ID", Convert.ToInt32(DR["Page_IDLNK"]).ToString());
                category.SetAttribute("Tag", DR["Page_Tag"].ToString());
                category.InnerText = DR["Page_Title"].ToString();

                root.AppendChild(category);
            }

            return Output;
        }

        [WebMethod(CacheDuration = 1, Description = "List Child Pages")]
        public XmlDocument ChildPages(int ParentID)
        {
            ClassAppDetails.commscurrentconnection = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.noticesconnectionname);

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            XmlDocument Output = new XmlDocument();

            XmlDeclaration dec = Output.CreateXmlDeclaration("1.0", null, null);
            Output.AppendChild(dec);

            XmlElement root = Output.CreateElement("Childpages");
            Output.AppendChild(root);

            RQ.RunQuery("SELECT * FROM Pages WHERE Page_Parent_IDLNK = " + ParentID + " AND Page_Deleted = 0 ORDER BY Page_Title");

            foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
            {
                XmlElement category = Output.CreateElement("Page");
                category.SetAttribute("ID", Convert.ToInt32(DR["Page_IDLNK"]).ToString());
                category.SetAttribute("Tag", DR["Page_Tag"].ToString());
                category.InnerText = DR["Page_Title"].ToString();

                root.AppendChild(category);
            }

            return Output;
        }

        [WebMethod(CacheDuration = 1, Description = "Page Content")]
        public XmlDocument Page(int PageID)
        {
            ClassAppDetails.commscurrentconnection = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.noticesconnectionname);

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            XmlDocument Output = new XmlDocument();

            XmlDeclaration dec = Output.CreateXmlDeclaration("1.0", null, null);
            Output.AppendChild(dec);

            XmlElement root = Output.CreateElement("Pages");
            Output.AppendChild(root);

            RQ.RunQuery("SELECT * FROM Pages WHERE Page_IDLNK = " + PageID + " AND Page_Deleted = 0");

            DataRow DR = RQ.dataset.Tables[0].Rows[0];

            XmlElement spage = Output.CreateElement("Page");
            spage.SetAttribute("ID", Convert.ToInt32(DR["Page_IDLNK"]).ToString());
            spage.SetAttribute("Tag", DR["Page_Tag"].ToString());
            spage.SetAttribute("Title", DR["Page_Title"].ToString());
            spage.SetAttribute("Type", "Standard");
            spage.SetAttribute("NoticeCateogry" , Convert.ToInt16(DR["Page_Notices_Category_ID_LNK"]).ToString());
            spage.SetAttribute("EventsCateogry", Convert.ToInt16(DR["Page_Events_Category_ID_LNK"]).ToString());
            spage.InnerText = DR["Page_StandardContent"].ToString();

            root.AppendChild(spage);

            XmlElement mpage = Output.CreateElement("Page");
            mpage.SetAttribute("ID", Convert.ToInt32(DR["Page_IDLNK"]).ToString());
            mpage.SetAttribute("Tag", DR["Page_Tag"].ToString());
            mpage.SetAttribute("Title", DR["Page_Title"].ToString());
            mpage.SetAttribute("Type", "Standard");
            mpage.SetAttribute("NoticeCateogry", Convert.ToInt16(DR["Page_Notices_Category_ID_LNK"]).ToString());
            mpage.SetAttribute("EventsCateogry", Convert.ToInt16(DR["Page_Events_Category_ID_LNK"]).ToString());
            mpage.InnerText = DR["Page_MobileContent"].ToString();

            root.AppendChild(mpage);

           
            return Output;
        }
    }
}
