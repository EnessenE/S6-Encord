using System;
using System.Collections.Generic;
using System.Data;
using Encord.Common.Models;

namespace Encord.Common.Extensions
{
    public static class DataTableExtension
    {
        public static List<Guild> ToGuildList(this DataTable table)
        {
            List<Guild> result = new List<Guild>();

            foreach (DataRow row in table.Rows)
            {
                if (!row.HasErrors)
                {
                    result.Add(row.ToGuild());
                }
            }

            return result;
        }
    }
}
