using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace InfrastructureData
{
    public static class SqlAccess
    {
		private static IConfiguration _configuration;
		private static int _defaultTimeout { get; set; }
		private static string _defaultConnection { get; set; }
		private static Dictionary<string, string> DictionaryConnections = new Dictionary<string, string>();
		public static IConfiguration Configuration
		{
			set
			{
				if (_configuration == null)
				{
					_configuration = value;
					init();
				}
			}
		}

		/// <summary>
		/// InitData
		/// </summary>
		private static void init()
		{
			List<SqlListConnection> connectionStrings = new List<SqlListConnection>();
			_configuration.GetSection("AppSettings:ConnectionStrings").Bind(connectionStrings);

			if (connectionStrings.Count > 0)
			{
				foreach (SqlListConnection connectionString in connectionStrings)
				{
					if (connectionString.Name.Equals("DefaultConnection"))
					{
						_defaultConnection = connectionString.Value;
					}

					if (!DictionaryConnections.ContainsKey(connectionString.Name))
					{
						DictionaryConnections.Add(connectionString.Name, connectionString.Value);
					}
				}
			}
		}

		/// <summary>
		/// Default connetion
		/// </summary>
		/// <param name="commandText"></param>
		/// <returns></returns>
		public static string ExecuteNonQuery(string commandText)
		{
			return ExecuteNonQuery(_defaultConnection, commandText, null, -1);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="commandText"></param>
		/// <param name="Parameters"></param>
		/// <returns></returns>
		public static string ExecuteNonQuery(string commandText, SqlParameter[] Parameters)
		{
			return ExecuteNonQuery(_defaultConnection, commandText, Parameters, -1);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="commandText"></param>
		/// <param name="Parameters"></param>
		/// <returns></returns>
		public static string ExecuteNonQuery(string connectionString, string commandText, SqlParameter[] Parameters)
		{
			return ExecuteNonQuery(connectionString, commandText, Parameters, -1);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="commandText"></param>
		/// <param name="Parameters"></param>
		/// <param name="commandTimeout"></param>
		/// <returns></returns>
		public static string ExecuteNonQuery(string connectionString, string commandText, SqlParameter[] Parameters = null, int commandTimeout = -1)
		{
			int ret;
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();
					using (SqlCommand command = new SqlCommand(commandText, con))
					{
						command.CommandTimeout = commandTimeout != -1 ? commandTimeout : _defaultTimeout;

						if (Parameters != null && Parameters.Length > 0)
						{
							command.Parameters.AddRange(Parameters);
						}
						ret = command.ExecuteNonQuery();
						command.Parameters.Clear();
					}
				}
				return "Success";
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="commandText"></param>
		/// <returns></returns>
		public static object ExecuteScalar(string commandText)
		{
			return ExecuteScalar(_defaultConnection, commandText);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="commandText"></param>
		/// <param name="Parameters"></param>
		/// <param name="commandTimeout"></param>
		/// <returns></returns>
		public static object ExecuteScalar(string connectionString, string commandText, SqlParameter[] Parameters = null, int commandTimeout = -1)
		{
			object obj = new object();
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();
					using (SqlCommand command = new SqlCommand(commandText, con))
					{
						command.CommandTimeout = commandTimeout != -1 ? commandTimeout : _defaultTimeout;

						if (Parameters != null && Parameters.Length > 0)
						{
							command.Parameters.AddRange(Parameters);
						}

						obj = command.ExecuteScalar();

						command.Parameters.Clear();
					}
				}
				return obj;
			}
			catch (Exception ex)
			{
				throw new SqlAccessException("Exception", ex);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="commandText"></param>
		/// <returns></returns>
		public static DataTable FillDataset(string commandText)
		{
			return FillDataset(_defaultConnection, commandText, null, -1);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="commandText"></param>
		/// <param name="Parameters"></param>
		/// <returns></returns>
		public static DataTable FillDataset(string commandText, SqlParameter[] Parameters)
		{
			return FillDataset(_defaultConnection, commandText, Parameters, -1);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="commandText"></param>
		/// <param name="Parameters"></param>
		/// <returns></returns>
		public static DataTable FillDataset(string connectionString, string commandText, SqlParameter[] Parameters)
		{
			return FillDataset(connectionString, commandText, Parameters);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="commandText"></param>
		/// <param name="Parameters"></param>
		/// <param name="commandTimeout"></param>
		/// <returns></returns>
		public static DataTable FillDataset(string connectionString, string commandText, SqlParameter[] Parameters = null, int commandTimeout = -1)
		{
			DataTable dataTable = new DataTable();

			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();

				using (var command = new SqlCommand(commandText, connection))
				{
					command.CommandTimeout = commandTimeout != -1 ? commandTimeout : _defaultTimeout;

					if (Parameters != null && Parameters.Length > 0)
					{
						command.Parameters.AddRange(Parameters);
					}

					SqlDataAdapter adapter = new SqlDataAdapter(command);

					adapter.Fill(dataTable);
					command.Parameters.Clear();
				}
				connection.Close();
			}

			return dataTable;
		}
	}
}
