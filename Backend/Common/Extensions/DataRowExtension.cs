using System;
using System.Data;
using Encord.Common.Models;

namespace Encord.Common.Extensions
{
    public static class DataRowExtension
    {
        public static object GetValue(this DataRow row, string column)
        {
            return row.Table.Columns.Contains(column) ? row[column] : null;
        }

        public static int ToInt(this DataRow reader)
        {
            return Convert.ToInt32(reader.ItemArray[0]);
        }

        public static Guild ToGuild(this DataRow reader)
        {

            int id = Convert.ToInt32(reader["Id"]);
            string name = reader["Name"].ToString();
            DateTime creationDate = Convert.ToDateTime(reader["CreationDate"]);

            Guild guild = new Guild()
            {
                Id = id,
                Name = name,
                CreationDate = creationDate
            };

            return guild;
        }
    }
}
