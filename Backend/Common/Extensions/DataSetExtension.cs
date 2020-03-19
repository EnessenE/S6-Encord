using System.Data;

namespace Encord.Common.Extensions
{
    public static class DataSetExtension
    {
        public static bool HasData(this DataSet data)
        {
            bool result = false;

            if (data.Tables.Count > 0)
            {
                foreach (DataTable table in data.Tables)
                {
                    if (table.Rows.Count > 0)
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }
    }
}
