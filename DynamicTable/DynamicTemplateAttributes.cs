
using System;
using System.Collections.Generic;
using System.Text;

namespace DNH.Infrastructure.DynamicTable
{
    public class DynamicTemplateAttributes
    {
        public string TypeName { get; set; }
     
        public string ColumName { get; set; }

        
    }

    public class DynamicTemplateAttributesOutput 
    {
        public string DisplayName { get; set; }
    }

    public class TableName 
    {
        public string nametable { get; set; }
    }
}
