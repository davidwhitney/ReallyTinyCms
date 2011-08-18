using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using ReallyTinyCms.Core.Model;

namespace ReallyTinyCms.Core.Storage
{
	public class SqlCmsContentRepository : ICmsContentRepository
	{
		private readonly string _connectionString;

		public SqlCmsContentRepository(string connectionString)
		{
			_connectionString = connectionString;

			VerifySchema();
		}

		public IList<CmsContentItem> RetrieveAll()
		{
			return ConnectAnd(c =>
			                  	{
			                  		var items = c.Query<CmsContentItem>("select");
			                  		return items.ToList();
			                  	});
		}

		public CmsContentItem Retrieve(string contentItemName)
		{
			return ConnectAnd(c =>
			                  	{
			                  		var items = c.Query<CmsContentItem>("select * from CmsContentItem where Name = @Name",
			                  		                                    new {Name = contentItemName});
			                  		return items.FirstOrDefault();
			                  	});
		}

		public void SaveOrUpdate(CmsContentItem item)
		{
			ConnectAnd(c =>
			           	{
			           		var existingItem = c.Query<CmsContentItem>("select * from CmsContentItem where Name = @Name",
			           		                                           new {Name = item.Name}).FirstOrDefault();

			           		if (existingItem != null)
			           		{
			           			c.Execute(@"update CmsContentItem set Name = @Name, Content = @Content
											where Name = @Name",
			           			          new {Name = item.Name, Content = item.Content});
			           		}
			           		else
			           		{
			           			c.Execute(@"insert CmsContentItem(Name, Content) values (@Name, @Content)",
			           			          new {Name = item.Name, Content = item.Content});
			           		}
			           	});
		}

		public void Delete(string contentItemName)
		{
			throw new NotImplementedException();
		}

		private void VerifySchema()
		{
			ConnectAnd(c =>
			{
				var rows = c.Query(@"SELECT name FROM sys.tables WHERE (name = @TableName)",
			           				new {TableName = "CmsContentItem"});
				if (rows.Count() == 0)
				{
					CreateSchema(c);
				}

			});
		}

		private static void CreateSchema(IDbConnection connection)
		{
			connection.Execute(
				@"CREATE TABLE [dbo].[CmsContentItem](
					[Id] [int] IDENTITY(1,1) NOT NULL,
					[Name] [nvarchar] (max) NULL,
					[Content] [nvarchar] (max) NULL,	
				 ) ON [PRIMARY]");
		}

		public void ConnectAnd(Action<SqlConnection> action)
		{
			using (var dbConnection = new SqlConnection(_connectionString))
			{
				try
				{
					dbConnection.Open();
					action(dbConnection);
				}
				finally
				{
					dbConnection.Close();
				}
			}
		}

		public TReturnType ConnectAnd<TReturnType>(Func<SqlConnection, TReturnType> action)
		{
			using (var dbConnection = new SqlConnection(_connectionString))
			{
				try
				{
					dbConnection.Open();
					return action(dbConnection);
				}
				finally
				{
					dbConnection.Close();
				}
			}
		}
	}
}
