using System;
using System.Data;
using System.Data.SqlClient;

namespace StandardClasses
{
	/// <summary>
	/// Summary escription for ClassConnection.
	/// </summary>
	public class ClassConnection
	{
		/// <summary>
		/// Initialise Varibles
		/// </summary>
		private SqlConnection c_Connection;

        private string c_Config;
        private string c_ConnectionName;
		

		/// <summary>
        /// Attributes
        /// </summary>
		public SqlConnection connection
		{
			get { return this.c_Connection; }
			set { this.c_Connection = value; }
		}

		/// <summary>
		/// Operations
		/// </summary>
        public ClassConnection(String Config, String ConnectionName)
		{
			//Do Nothing
            c_Config = Config;
            c_ConnectionName = ConnectionName;
		}

		public Boolean Connect()
		{

            
            System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(c_Config);
            System.Configuration.ConnectionStringSettings connString;

            string DBStr;

            if (0 < rootWebConfig.ConnectionStrings.ConnectionStrings.Count)
            {
                connString =
                    
                    rootWebConfig.ConnectionStrings.ConnectionStrings[c_ConnectionName];
                if (null != connString)
                {

                    DBStr = connString.ConnectionString;
                    this.connection = new SqlConnection(DBStr);

                }
                else
                {
                    Console.WriteLine("No connection string");
                }
            }
            
			try 
			{
              
				this.c_Connection.Open();
				return true;
			}
			catch(Exception ex)
			{
				
				return false;
			}
		}

		public void Disconnect()
		{	
			if (this.c_Connection.State == ConnectionState.Open)
			{
				this.c_Connection.Close();
			}
		}

		public ConnectionState ConnectionStatus()
		{
			return this.c_Connection.State;
		}
	}
}
