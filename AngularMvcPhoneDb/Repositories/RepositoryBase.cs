using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace AngularMvcPhoneDb.Core.Repositories
{
    public abstract class RepositoryBase
    {
        protected List<T> BuildSqlBody<T>(String command, Dictionary<string, object> parameters, Func<SqlDataReader, T> objectBuilder, List<T> data)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["AddPhoneDb"]))
            {
                SqlCommand sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = command;

                foreach (KeyValuePair<string, object> pair in parameters)
                    sqlCommand.Parameters.AddWithValue(pair.Key, pair.Value);
                
                connection.Open();
                
                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                        data.Add(objectBuilder(reader));
                }
            }

            return data;
        }
    }
}