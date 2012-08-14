using System;
using System.Data;
using System.Data.SqlClient;

namespace StandardClasses 
{
	/// <summary>
	/// Summary description for ClassReadQuery.
	/// </summary>
    public class ClassReadQuery
    //public class ClassReadQuery: IDisposable

	{
		/// <summary>
		/// Initalise Varibles
		/// </summary>
		private ClassConnection c_Connection;
		private string c_Query;
		private SqlDataAdapter c_DataAdapter;
		private DataSet c_DataSet;
		private int c_NumberOfResults;

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

		public SqlDataAdapter dataadapter
		{
			get { return this.c_DataAdapter; }
			set { this.c_DataAdapter = value; }
		}

		public DataSet dataset
		{
			get { return this.c_DataSet; }
			set { this.c_DataSet = value; }
		}

		public int numberofresults
		{
			get { return this.c_NumberOfResults; }
			set { this.c_NumberOfResults = value; }
		}


		/// <summary>
		/// Operations
		/// </summary>
		public ClassReadQuery(ClassConnection Conn)
		{
			//this.c_Connection = ClassAppDetails.currentconnection;
            this.c_Connection = Conn;
		}

		public void RunQuery()
		{
			if (this.query != null)
			{
				//this.c_Connection.Connect("/StudentServices","SSConn");

                this.c_Connection.Connect();

				this.c_DataSet = new DataSet();

				this.c_DataAdapter = new SqlDataAdapter();
				this.c_DataAdapter.SelectCommand = new SqlCommand(this.query, this.connection.connection);
				this.c_DataAdapter.Fill(this.c_DataSet);

				this.c_NumberOfResults = this.c_DataSet.Tables[0].Rows.Count;

				this.c_Connection.Disconnect();
			}
		}

        public void RunQuery(string Query)
		{
			this.c_Query = Query;

			this.RunQuery();
		}

        public void RunQuery(string Query, SqlParameter Params)
        {
            if (this.query != null)
            {
                //this.c_Connection.Connect("/StudentServices","SSConn");

                this.c_Connection.Connect();

                this.c_DataSet = new DataSet();

                this.c_DataAdapter = new SqlDataAdapter();
                this.c_DataAdapter.SelectCommand = new SqlCommand(this.query, this.connection.connection);
                this.c_DataAdapter.SelectCommand.Parameters.Add(Params);
                this.c_DataAdapter.Fill(this.c_DataSet);

                this.c_NumberOfResults = this.c_DataSet.Tables[0].Rows.Count;

                this.c_Connection.Disconnect();
            }
        }

		public void RunQuery(string Query, SqlTransaction Trans, ClassConnection Conn)
		{
			this.c_Query = Query;

			this.c_DataSet = new DataSet();

			this.c_DataAdapter = new SqlDataAdapter();
			this.c_DataAdapter.SelectCommand = new SqlCommand(this.query, Conn.connection, Trans);
			this.c_DataAdapter.Fill(this.c_DataSet);

			this.c_NumberOfResults = this.c_DataSet.Tables[0].Rows.Count;
		}

       


	}
}
