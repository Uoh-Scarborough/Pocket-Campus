using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PocketCampusClasses
{
    public class ClassBase : IComparable
    {

        protected int c_ID;
        protected Boolean c_Deleted;

        public int ID
        {
            get { return c_ID; }
            set { c_ID = value; }
        }

        public Boolean Deleted
        {
            get { return c_Deleted; }
            set { c_Deleted = value; }
        }

        public int CompareTo(object obj)
        {
            int result = this.ToString().CompareTo(obj.ToString());
            return result;
        }

    }
}
