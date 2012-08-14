using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using StandardClasses;

namespace PocketCampusClasses
{
    class ClassFiles
    {

        private int c_ID;
        private FileStream c_File;

        public int ID
        {
            get { return c_ID; }
            set { c_ID = value; }
        }

        public FileStream File
        {
            get { return c_File; }
            set { c_File = value; }
        }

        public void SaveFile()
        {
            byte[] buffer = new byte[File.Length];
            File.Read(buffer, 0, (int)File.Length);
            File.Close();

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            RQ.RunQuery("INSERT INTO FILES (File) VALUES (@File); SELECT @@IDENTITY;", new System.Data.SqlClient.SqlParameter("@File", buffer));

            c_ID = Convert.ToInt32(RQ.dataset.Tables[0].Rows[0].ItemArray[0]);
        }
        
    }
}
