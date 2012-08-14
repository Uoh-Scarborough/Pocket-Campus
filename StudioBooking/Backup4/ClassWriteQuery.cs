using System;
using System.Data.SqlClient;

namespace StandardClasses
{
	/// <summary>
	/// Write Querys to SQL Server
	/// </summary>
	public class ClassWriteQuery
	{

		/// <summary>
		/// Initialise Varibles
		/// </summary>
		private ClassConnection c_Connection;
		private string c_Query;
		private SqlCommand c_Command;

		/// <summary>
		/// Attributes
		/// </summary>
		public ClassConnection connection
		{
			get { return this.c_Connection; }
			set { this.c_Connection = value; }
		}

		public string query
		{
			get { return this.c_Query.Trim(); }
			set { this.c_Query = value.Trim(); } 
		}

		public SqlCommand command
		{
			get { return this.c_Command; }
			set { this.c_Command = value; }
		}

		/// <summary>
		/// Operations
		/// </summary>
        /// 
        public ClassWriteQuery()
        {
            //this.c_Connection = ClassAppDetails.currentconnection;
            //this.c_Connection = Conn;
        }

		public ClassWriteQuery(ClassConnection Conn)
		{
			//this.c_Connection = ClassAppDetails.currentconnection;
            this.c_Connection = Conn;
		}

		public void RunQuery()
		{
			if (this.query != null)
			{
                //this.c_Connection.Connect("/StudentServices", "SSConn");
                this.c_Connection.Connect();

				this.c_Command = new SqlCommand();
				this.c_Command.Connection = this.c_Connection.connection;
				this.c_Command.CommandType = System.Data.CommandType.Text;
				this.c_Command.CommandText = this.query;
				this.c_Command.ExecuteNonQuery();

				this.c_Connection.Disconnect();
			}
		}

        public void RunQuery(string Query)
		{
			this.c_Query = Query;
			
			this.RunQuery();
		}

		public void RunQuery(string Query, SqlTransaction Trans, ClassConnection Conn)
		{
			this.c_Query = Query;

			//this.c_Connection.Connect();

			this.c_Command = new SqlCommand();
			this.c_Command.Connection = Conn.connection;
			this.c_Command.CommandType = System.Data.CommandType.Text;
			this.c_Command.Transaction = Trans;
			this.c_Command.CommandText = this.query;
            this.c_Command.ExecuteNonQuery();

			//this.c_Connection.Disconnect();
		}


	}
}
