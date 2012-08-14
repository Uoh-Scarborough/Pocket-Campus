using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using StandardClasses;

namespace PocketCampusClasses
{
    public class ClassRequestForms
    {

        private int c_ID;
        private string c_FormName;

        public int ID
        {
            get { return c_ID; }
        }

        public string FormName
        {
            get { return c_FormName.Trim(); }
            set { c_FormName = value.Trim(); }
        }

        public ClassRequestForms()
        {
             //Initialise New Class
        }

        public ClassRequestForms(int ID)
        {
            //Initialise New Class
            c_ID = ID;
            string Query = "SELECT * From StudentRequestForm WHERE StudentRequestForm_ID_LNK = " + ID;
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.studentservicesconnection);
            RQ.RunQuery(Query);

            FormName = RQ.dataset.Tables[0].Rows[0].ItemArray[1].ToString();

        }

        public bool Create()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.studentservicesconnection);
            bool Result;
            string Query = "INSERT INTO StudentRequestForm (StudentRequestForm_Name) VALUES ('" + FormName + "') SELECT @@IDENTITY;";
            try
            {
                RQ.RunQuery(Query);
                c_ID = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[0]);
                Result = true;
            }
            catch (Exception ex)
            {
                Result = false;
            }
            return Result;
        }

        public bool Save()
        {
            ClassWriteQuery WQ = new ClassWriteQuery(ClassAppDetails.studentservicesconnection);
            bool Result;
            string Query = "UPDATE StudentRequestForm SET StudentRequestForm_Name = '" + FormName + "' WHERE StudentRequestForm_ID_LNK = " + ID + ";";
            try
            {
                WQ.RunQuery(Query);
                Result = true;
            }
            catch (Exception ex)
            {
                Result = false;
            }
            return Result;
        }

        
        public static ClassRequestForms[] Load()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.studentservicesconnection);
            RQ.RunQuery("SELECT * FROM StudentRequestForm;");
            ClassRequestForms[] TempSet = new ClassRequestForms[RQ.dataset.Tables[0].Rows.Count];
            int Counter = 0;
            foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
            {
                int ID = (int)DR[0];
                ClassRequestForms TempForm = new ClassRequestForms(ID);
                TempSet[Counter] = TempForm;
                Counter++;
            }
            return TempSet;
        }
    }
}
