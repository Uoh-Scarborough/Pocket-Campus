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
using PocketCampusClasses;

namespace CampusInfo
{
    public partial class _Default : System.Web.UI.Page
    {

        ClassConnection NC;


       

        protected void Page_Load(object sender, EventArgs e)
        {

            NC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.commsconnectionname);

            ClassAppDetails.commscurrentconnection = NC;

            string Tag = "";

            Tag = Request["page"];


            if (Tag != null)
            {

                if (ClassPages.PageExists(Tag))
                {



                    //Page to Load

                    //HomeButtonlbl.InnerHtml = "<img src=\"http://pocketcampusimages.scar.hull.ac.uk/CampusInfoLogoBlue.png\" alt=\"Campus Info Logo\" />";

                    ClassPages LivePage = new ClassPages(Tag);

                    

                    if (LivePage.NoticesCateogryID != 0 || LivePage.EventsCateogryID != 0)
                    {

                        String Events = "";
                        String Notices = "";

                        int Mode = 0;

                        if (LivePage.NoticesCateogryID != 0)
                        {

                            ClassCategory NoticesCat = new ClassCategory(LivePage.NoticesCateogryID);

                            Notices = ClassCommsBase.loadNoticesList(NoticesCat);

                            Mode = 1;
                        }

                        if (LivePage.EventsCateogryID != 0)
                        {
                            ClassCategory EventsCat = new ClassCategory(LivePage.EventsCateogryID);

                            Events = ClassEvent.loadEventsList(EventsCat);

                            if (Mode == 1)
                            {
                                Mode = 3;
                            }
                            else
                            {
                                Mode = 2;
                            }
                        }

                        if (Mode == 0)
                        {
                            // Should never happen.
                        }
                        else if (Mode == 1)
                        {
                            //Show Notices Only

                            contentnoticeslbl.Text = LivePage.outputPage();

                            NoticesOnlylbl.Text = Notices;

                            Multiview.SetActiveView(ContentNoticesView);
                        }
                        else if (Mode == 2)
                        {
                            //Show Events Only

                            contenteventslbl.Text = LivePage.outputPage();

                            EventsOnlylbl.Text = Events;

                            Multiview.SetActiveView(ContentEventsView);
                        }
                        else if (Mode == 3)
                        {
                            //Show Notices and Events
                            contentlbl.Text = LivePage.outputPage();

                            Noticeslbl.Text = Notices;

                            Eventslbl.Text = Events;

                            Multiview.SetActiveView(ContentView);
                        }

                    }
                    else
                    {
                        //Plain Content

                        plaincontentlbl.Text = LivePage.outputPage();

                        Multiview.SetActiveView(ContentPlainView);
                    }


                    Page.Header.Title = LivePage.Title;

                    
                }
                else
                {

                    //Load Homepage

                    //HomeButtonlbl.InnerHtml = "<img src=\"http://pocketcampusimages.scar.hull.ac.uk/CampusInfoLogo.png\" alt=\"Campus Info Logo\" />";
                    Multiview.SetActiveView(DefaultView);

                }
            }
            else
            {
                //Load Homepage

                //HomeButtonlbl.InnerHtml = "<img src=\"http://pocketcampusimages.scar.hull.ac.uk/CampusInfoLogo.png\" alt=\"Campus Info Logo\" />";
                Multiview.SetActiveView(DefaultView);
            }

            
        }
    }
}
