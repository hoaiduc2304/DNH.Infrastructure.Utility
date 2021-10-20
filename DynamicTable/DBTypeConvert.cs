using System;
using System.Collections.Generic;
using System.Text;

namespace DNH.Infrastructure.DynamicTable
{
    public class DBTypeConvert
    {
        public static System.Type ConvertType(string DBColumnType)
        {
            switch (DBColumnType.ToLower())
            {
                case "datetime":
                    return typeof(DateTime);
                case "int":
                    return typeof(Int32?);
                case "bigint":
                    return typeof(Int64?);
                case "decimal":
                    return typeof(decimal?);
                default:
                    return typeof(String);
            }
        }
    }
}
