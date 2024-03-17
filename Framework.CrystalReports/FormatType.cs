using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.CrystalReports
{
     /// <summary>
     /// Supported export formats for Crystal reports.
     /// </summary>
     public enum FormatType
     {
          PDF = 0,
          XLSX = 1,
          DOCX = 2,
          DOC = 3,
          XML = 4,
          CSV = 5,
          HTML = 6
     }
}
