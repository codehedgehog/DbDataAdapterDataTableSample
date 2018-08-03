namespace NetCoreSqlClientPlay
{
	using System;
	using System.Data;
	using System.Data.SqlClient;
	using static System.Configuration.ConfigurationManager;

	internal class Program
	{
		private static void Main(string[] args)
		{
			string sqlCommandText = "SELECT TOP 5 [t].[Description] [TermName] FROM [dbo].[tblTerm] [t] ORDER BY [t].[TermCode] DESC";
			using (SqlConnection theSqlConn = new SqlConnection(connectionString: ConnectionStrings["theDatabaseDbContext"].ConnectionString))
			{
				Console.WriteLine("\nPlaying with System.Data.SqlClient in .NET Core 2.x\n==================================\n");
				Console.WriteLine("\n\nOpen SQL Server Database Connection\n===================================");
				theSqlConn.Open();
				Console.WriteLine("\n\nFill DataTable using SqlDataAdapter\n===================================");
				DataTable resultDataTable = FillDataTableUsingSqlDataAdapter(conn: theSqlConn, cmdType: CommandType.Text, cmdText: sqlCommandText, cmdParms: null);
				foreach (DataRow term in resultDataTable.Rows) { Console.WriteLine(term[0]); }
				resultDataTable = null;
				Console.WriteLine("\n\nLoad DataTable using SqlCommand ExecuteReader\n=============================================");
				resultDataTable = LoadDataTableUsingExecuteReader(conn: theSqlConn, cmdType: CommandType.Text, cmdText: sqlCommandText, cmdParms: null);
				foreach (DataRow term in resultDataTable.Rows) { Console.WriteLine(term[0]); }
				if (theSqlConn != null && theSqlConn.State != ConnectionState.Closed) theSqlConn.Close();
				Console.WriteLine("\n\nPress any key to continue...");
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

		public static DataTable LoadDataTableUsingExecuteReader(SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
		{
			DataTable result = new DataTable();
			using (SqlCommand theSqlCommand = conn.CreateCommand())
			{
				theSqlCommand.CommandType = cmdType;
				theSqlCommand.CommandText = cmdText;
				result.Load(reader: theSqlCommand.ExecuteReader(CommandBehavior.CloseConnection), loadOption: LoadOption.Upsert);
			}
			return result;
		}
	}
}