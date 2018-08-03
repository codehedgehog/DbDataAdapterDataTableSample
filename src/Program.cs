namespace NetCoreDbDataAdapterDataTableSample
{
	using System;
	using System.Data;
	using System.Data.Common;
	using System.Data.SqlClient;
	using static System.Configuration.ConfigurationManager;

	class Program
	{
		static void Main(string[] args)
		{
			using (SqlConnection theSqlConn = new SqlConnection(connectionString: ConnectionStrings["theDatabaseDbContext"].ConnectionString))
			{

				theSqlConn.Open();
				DataTable resultDataTable = FillDataTableUsingSqlDataAdapter(
					conn: theSqlConn, cmdType: CommandType.Text, 
				  cmdText:	"SELECT [t].[Description] [TermName] FROM [dbo].[tblTerm] [t] ORDER BY [t].[TermCode] DESC", cmdParms: null);
				foreach (DataRow term in resultDataTable.Rows)
				{
					Console.WriteLine(term[0]);
				}
				//try
				//{
				//	using (SqlCommand theCommand = new SqlCommand("SELECT * FROM SAMPLETABLE", theSqlConn))
				//	{
				//		theCommand.ExecuteNonQuery();
				//	}
				//}
				//catch
				//{
				//	Console.WriteLine("Something went wrong");
				//}
				//finally
				//{
				//	if (theSqlConn != null && theSqlConn.State != ConnectionState.Closed) theSqlConn.Close();
				//}
				Console.ReadLine();
			}
		}

		public static DataTable FillDataTableUsingSqlDataAdapter(SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
		{
			DataTable result = new DataTable();
			SqlDataAdapter theDataAdapter = new SqlDataAdapter(selectCommandText: cmdText, selectConnection: conn);
			theDataAdapter.Fill(dataTable: result);
			return result;
		}


		//public static DataTable ExecuteDataTable(SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
		//{
		//	DataTable dt = new DataTable();
		//	// just doing this cause dr.load fails
		//	dt.Columns.Add("CustomerID");
		//	dt.Columns.Add("CustomerName");
		//	DbDataReader dr = ExecuteReader(conn, cmdType, cmdText, cmdParms);
		//	// dt.Load(dr);
		//	while (dr.Read())
		//	{
		//		dt.Rows.Add(dr[0], dr[1]);
		//	}
		//	return dt;
		//}


	}
}
