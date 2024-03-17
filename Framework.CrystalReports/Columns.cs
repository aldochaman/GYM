using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.CrystalReports
{
     /// <summary>
     /// Columns
     /// </summary>
     public class Columns
     {
          /// <summary>
          /// Gets or sets a value indicating whether this instance is numeric.
          /// </summary>
          /// <value>
          ///   <c>true</c> if this instance is numeric; otherwise, <c>false</c>.
          /// </value>
          public bool IsNumeric { get; set; }
          /// <summary>
          /// Gets or sets the length.
          /// </summary>
          /// <value>
          /// The length.
          /// </value>
          public int Length { get; set; }
     }
}
