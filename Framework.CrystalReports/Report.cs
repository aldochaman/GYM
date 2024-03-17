using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Configuration;

namespace Framework.CrystalReports
{
     public class Report
     {
          #region "Variables"                    

          private ReportDocument _Report;
          private ConnectionInfo _ConnectionInfo;
          private string _ServerName;
          private string _DatabaseName;
          private string _UserID;
          private string _Password;
          private List<Tables> _Tables;
          private Dictionary<string, string> _Formula;
          private string _ReportName;
          private string _ReportPath;
          private ExportFormatType _Format;
          private struct Tables
          {
               public string Name;
               public string Value;
               public IEnumerable Data;
          }
          #endregion

          #region "Properties"
          public int TotalColumns { get; set; }
          public List<Columns> Columns { get; set; }
          /// <summary>
          /// Gets or sets the name of the output file.
          /// </summary>
          /// <value>
          /// The name of the output file.
          /// </value>
          public string OutputFileName { get; set; }

          /// <summary>
          /// Gets or sets the selection formula.
          /// </summary>
          /// <value>
          /// The selection formula.
          /// </value>
          public string SelectionFormula { get; set; }

          /// <summary>
          /// Gets or sets the path temporary.
          /// </summary>
          /// <value>
          /// The path temporary.
          /// </value>
          public string PathTmp { get; set; }

          /// <summary>
          /// Gets or sets the type of the format.
          /// </summary>
          /// <value>
          /// The type of the format.
          /// </value>
          public FormatType FormatType { get; set; }

          /// <summary>
          /// Gets or sets a value indicating whether [refresh data].
          /// </summary>
          /// <value>
          ///   <c>true</c> if [refresh data]; otherwise, <c>false</c>.
          /// </value>
          public bool RefreshData { get; set; }
          /// <summary>
          /// Gets or sets a value indicating whether [verifice data base].
          /// </summary>
          /// <value>
          ///   <c>true</c> if [verifice data base]; otherwise, <c>false</c>.
          /// </value>
          public bool VerifyDataBase { get; set; }
          #endregion

          #region "Builders"

          /// <summary>
          /// Initializes a new instance of the <see cref="Report"/> class.
          /// </summary>
          /// <param name="serverName">Name of the server.</param>
          /// <param name="databaseName">Name of the database.</param>
          /// <param name="userID">The user identifier.</param>
          /// <param name="password">The password.</param>
          /// <param name="reportPath">The report path.</param>
          /// <param name="reportName">Name of the report.</param>
          /// 
          
          
          public Report(string reportPath, string reportName)
          {
               
               InternalBuilder(reportPath, reportName);
          }

          #endregion

          #region "Metodos"
          private void InternalBuilder(string reportPath, string reportName)
          {
               string cadenaConexion = ConfigurationManager.ConnectionStrings["Control_Gimmnacio.Properties.Settings.controlGymConnectionString"].ConnectionString;
               // Separamos la cadena por ;
               string[] componentes = cadenaConexion.Split(';');

               // Inicializamos variables para cada componente
               string serverName = "";
               string databaseName = "";
               string userID = "";
               string password = "";

               // Iteramos sobre los componentes para extraer la información relevante
               foreach (string componente in componentes)
               {
                    if (componente.Contains("Data Source"))
                    {
                         serverName = componente.Split('=')[1];
                    }
                    else if (componente.Contains("Initial Catalog"))
                    {
                         databaseName = componente.Split('=')[1];
                    }
                    else if (componente.Contains("User Id"))
                    {
                         userID = componente.Split('=')[1];
                    }
                    else if (componente.Contains("Password"))
                    {
                         password = componente.Split('=')[1];
                    }
               }

               _Formula = new Dictionary<string, string>();
               _Tables = new List<Tables>();
               _ReportName = reportName;
               _ReportPath = reportPath;
               FormatType = FormatType.PDF;
               _ConnectionInfo = new ConnectionInfo();
               _ServerName = serverName;
               _DatabaseName = databaseName;
               _UserID = userID;
               _Password = password;
          }

          private void CreateConecctionInfo()
          {
               _ConnectionInfo.ServerName = _ServerName;
               _ConnectionInfo.DatabaseName = _DatabaseName;
               _ConnectionInfo.Type = ConnectionInfoType.SQL;
               if (!string.IsNullOrEmpty(_UserID))
               {
                    _ConnectionInfo.UserID = _UserID;
                    _ConnectionInfo.Password = _Password;
               }
               else
               {
                    _ConnectionInfo.IntegratedSecurity = true;
               }
          }

          private void GenerateOutputReportName()
          {
               OutputFileName = string.Format("tmp{0}{1}{2}.pdf", _ReportName.Substring(0, _ReportName.LastIndexOf(".")), DateTime.Now.ToUniversalTime().ToString("yyyyMMddmmss"), (new Random()).Next(1000, 9999));
          }

          private void SetFormulas()
          {
               foreach (KeyValuePair<string, string> formula in _Formula)
               {
                    try
                    {
                         if (FormulaExist(formula.Key))
                         {
                              if (_Report.DataDefinition.FormulaFields[formula.Key].ValueType == FieldValueType.StringField && !formula.Value.Contains("{") && !formula.Value.Contains("}"))
                                   _Report.DataDefinition.FormulaFields[formula.Key].Text = $"\"{formula.Value}\"";
                              else
                                   _Report.DataDefinition.FormulaFields[formula.Key].Text = formula.Value;
                         }
                    }
                    catch
                    {
                         //No quiten estos exception porque estan pensados para que si el nombre
                         //o el valor de la fórmula son incorrectos no truene la aplicación                         
                    }
               }
          }

          private bool FormulaExist(string name)
          {
               foreach (FormulaFieldDefinition formula in _Report.DataDefinition.FormulaFields)
               {
                    if (formula.Name == name)
                    {
                         return true;
                    }
               }
               return false;
          }

          private void SetTables()
          {
               ConnectTables(_Report.Database.Tables);
               foreach (Section section in _Report.ReportDefinition.Sections)
               {
                    foreach (ReportObject reportObject in section.ReportObjects)
                    {
                         if (reportObject.Kind == ReportObjectKind.SubreportObject)
                         {
                              SubreportObject subreport = (reportObject as SubreportObject);
                              ReportDocument report = subreport.OpenSubreport(subreport.SubreportName);
                              ConnectTables(report.Database.Tables);
                         }
                    }
               }
          }

          private void ConnectTables(CrystalDecisions.CrystalReports.Engine.Tables tables)
          {
               bool isExists = false;
               foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
               {
                    foreach (Tables map in _Tables)
                    {
                         if (!string.Equals(table.Name, map.Name, StringComparison.OrdinalIgnoreCase))
                         {
                              continue;
                         }
                         isExists = true;
                         if (!String.IsNullOrEmpty(map.Value))
                         {
                              table.LogOnInfo.ConnectionInfo = _ConnectionInfo;
                              table.ApplyLogOnInfo(table.LogOnInfo);
                              table.Location = map.Value;
                         }
                         else
                         {
                              table.SetDataSource(map.Data);
                         }
                         break;
                    }
                    if (!isExists)
                    {
                         table.LogOnInfo.ConnectionInfo = _ConnectionInfo;
                         table.ApplyLogOnInfo(table.LogOnInfo);
                         table.Location = table.Name;

                    }
                    isExists = false;
               }
          }
          private Stream ExportToStream()
          {
               if (!String.IsNullOrEmpty(SelectionFormula))
                    _Report.RecordSelectionFormula = SelectionFormula;
               if (VerifyDataBase)
                    _Report.VerifyDatabase();
               if (RefreshData)
                    _Report.Refresh();
               return _Report.ExportToStream(_Format);
          }

          private void SetExtension()
          {
               string lsExtension;
               if (OutputFileName.IndexOf(".") != -1)
                    OutputFileName = OutputFileName.Substring(0, OutputFileName.IndexOf("."));
               switch (FormatType)
               {
                    case FormatType.XLSX:
                         _Format = ExportFormatType.Excel;
                         lsExtension = ".xls";
                         break;
                    case FormatType.PDF:
                         _Format = ExportFormatType.PortableDocFormat;
                         lsExtension = ".pdf";
                         break;
                    case FormatType.DOCX:
                         _Format = ExportFormatType.WordForWindows;
                         lsExtension = ".doc";
                         break;
                    default:
                         _Format = ExportFormatType.PortableDocFormat;
                         lsExtension = ".pdf";
                         break;
               }
               OutputFileName = string.Format("{0}{1}", OutputFileName, lsExtension);
          }
          private FieldObject Campo(string Ps_Seccion, string Ps_Campo)
          {
               Section section = _Report.ReportDefinition.Sections[Ps_Seccion];
               return section.ReportObjects[Ps_Campo] as FieldObject; 
          }
          private Section Seccion(string Ps_Seccion)
          {
               return _Report.ReportDefinition.Sections[Ps_Seccion];
          }
          private void FormatSections()
          {
               int posX;
               int posY;
               int posH;
               int posW;
               FieldObject field;
               int count;

               if (TotalColumns == 0 || Columns == null || Columns.Count == 0)
                    return;
               posX = 0;
               posY = 15;
               posH = 0;

               for (count = 1; count <= 45; count++)
               {
                    Campo("Detalle", "fCol" + count).Top = 0;
                    Campo("Detalle", "fColA" + count).Top = 0;
                    if (count > 2)
                    {
                         Campo("Encabezado", "fNCol" + count).Top = 0;
                         Campo("TotalGrupo", "fTGCol" + count).Top = 0;
                         Campo("TotalGral", "fTCol" + count).Top = 0;
                    }
               }
               if (TotalColumns >= 46)
               {
                    TotalColumns = 45;
               }

               for (count = 1; count < TotalColumns; count++)
               {
                    //Modifica la sección de detalle
                    switch (count)
                    {
                         case 1:
                         case 2: //ASIGNA EL NUMERO DEL EMPLEADO
                              field = Campo("Detalle", "fCol" + count);
                              posW = Campo("Detalle", "fCol" + count).Width;
                              break;
                         default:
                              //-1 lao columnas
                              if (Columns[count].IsNumeric)
                              { //VALORES NUMERICOS
                                   field = Campo("Detalle", "fCol" + count);
                                   posW = Columns[count].Length * 90;
                              }
                              else
                              { //VALORES ALFANUMERICOS
                                   field = Campo("Detalle", "fColA" + count);
                                   posW = Columns[count].Length * 120;
                              }
                              break;
                    }
                    posH = 200;
                    if (posX + posW > 11200)
                    {
                         posY += posH + 30;
                         posX = 0;
                    }
                    field.Top = posY;
                    field.Left = posX;
                    field.Height = posH;
                    field.Width = posW;
                    //Si la columna es mayor a 2
                    //Modifica la sección de Encabezado
                    if (count > 2)
                    {
                         Campo("Encabezado", "fNCol" + count).Top = posY;
                         Campo("Encabezado", "fNCol" + count).Left = posX;
                         Campo("Encabezado", "fNCol" + count).Height = posH;
                         Campo("Encabezado", "fNCol" + count).Width = posW;
                         if (Columns[count].IsNumeric)
                         { // VALORES NUMERICOS
                              //Modifica la sección de Totales del Grupo
                              Campo("TotalGrupo", "fTGCol" + count).ObjectFormat.HorizontalAlignment = Alignment.RightAlign;
                              Campo("TotalGrupo", "fTGCol" + count).Top = posY;
                              Campo("TotalGrupo", "fTGCol" + count).Left = posX;
                              Campo("TotalGrupo", "fTGCol" + count).Height = posH;
                              Campo("TotalGrupo", "fTGCol" + count).Width = posW;

                              //Modifica la sección de Totales Generales
                              Campo("TotalGral", "fTCol" + count).ObjectFormat.HorizontalAlignment = Alignment.RightAlign;
                              Campo("TotalGral", "fTCol" + count).Top = posY;
                              Campo("TotalGral", "fTCol" + count).Left = posX;
                              Campo("TotalGral", "fTCol" + count).Height = posH;
                              Campo("TotalGral", "fTCol" + count).Width = posW;
                         }
                    }
                    posX += posW + 70; //130
               }
               Seccion("Detalle").Height = posY + posH + 80;
               Seccion("Encabezado").Height = Seccion("Detalle").Height;
               Seccion("TotalGrupo").Height = Seccion("Detalle").Height;
               Seccion("TotalGral").Height = Seccion("Detalle").Height;
          }
          #endregion

          #region "Funciones"          
          /// <summary>
          /// Tables the exist.
          /// </summary>
          /// <param name="tableName">Name of the table.</param>
          /// <returns></returns>
          /// <exception cref="System.Exception"></exception>
          public bool TableExist(string tableName)
          {
               string file;
               CrystalDecisions.CrystalReports.Engine.Tables tables;
               SubreportObject subreport;
               Sections sections;
               using (_Report = new ReportDocument())
               {
                    file = Path.Combine(_ReportPath, _ReportName);
                    if (!File.Exists(file))
                         throw new Exception(String.Format("No file exist: {0}", file));
                    _Report.Load(file, OpenReportMethod.OpenReportByTempCopy);
                    tables = _Report.Database.Tables;
                    foreach (Table locrTable in tables)
                    {
                         if (string.Equals(locrTable.Name, tableName, StringComparison.OrdinalIgnoreCase))
                         {
                              return true;
                         }
                    }
                    sections = _Report.ReportDefinition.Sections;
                    foreach (Section section in sections)
                    {
                         foreach (ReportObject reportObject in section.ReportObjects)
                         {
                              if (reportObject.Kind == ReportObjectKind.SubreportObject)
                              {
                                   subreport = ((SubreportObject)reportObject);
                                   foreach (Table locrTable in subreport.OpenSubreport(subreport.SubreportName).Database.Tables)
                                   {
                                        if (string.Equals(locrTable.Name, tableName, StringComparison.OrdinalIgnoreCase))
                                        {
                                             return true;
                                        }
                                   }
                              }
                         }
                    }
                    return false;
               }
          }

          /// <summary>
          /// Adds the formula.
          /// </summary>
          /// <param name="name">The name.</param>
          /// <param name="value">The value.</param>
          public void AddFormula(string name, string value)
          {
               if (!_Formula.ContainsKey(name))
               {
                    _Formula.Add(name, value);
               }
          }

          /// <summary>
          /// Adds the table.
          /// </summary>
          /// <param name="name">The name.</param>
          /// <param name="value">The value.</param>
          public void AddTable(string name, string value)
          {
               Tables lstTablaMapeo;
               lstTablaMapeo.Name = name;
               lstTablaMapeo.Value = value;
               lstTablaMapeo.Data = null;
               _Tables.Add(lstTablaMapeo);
          }

          /// <summary>
          /// Adds the table.
          /// </summary>
          /// <param name="name">The name.</param>
          /// <param name="data">The data.</param>
          public void AddTable(string name, DataSet data)
          {
               Tables lstTablaMapeo;
               lstTablaMapeo.Name = name.ToLower();
               lstTablaMapeo.Value = "";
               lstTablaMapeo.Data = data.Tables;
               _Tables.Add(lstTablaMapeo);
          }

          /// <summary>
          /// Adds the table.
          /// </summary>
          /// <param name="name">The name.</param>
          /// <param name="data">The data.</param>
          public void AddTable(string name, DataView data)
          {
               Tables lstTablaMapeo;
               lstTablaMapeo.Name = name.ToLower();
               lstTablaMapeo.Value = "";
               lstTablaMapeo.Data = data;
               _Tables.Add(lstTablaMapeo);
          }

          /// <summary>
          /// Adds the table.
          /// </summary>
          /// <param name="name">The name.</param>
          /// <param name="data">The data.</param>
          public void AddTable(string name, DataTable data)
          {
               Tables lstTablaMapeo;
               lstTablaMapeo.Name = name.ToLower();
               lstTablaMapeo.Value = "";
               lstTablaMapeo.Data = new DataView(data);
               _Tables.Add(lstTablaMapeo);
          }

          /// <summary>
          /// Adds the table.
          /// </summary>
          /// <param name="name">The name.</param>
          /// <param name="data">The data.</param>
          public void AddTable(string name, ArrayList data)
          {
               Tables lstTablaMapeo;
               lstTablaMapeo.Name = name.ToLower();
               lstTablaMapeo.Value = "";
               lstTablaMapeo.Data = data;
               _Tables.Add(lstTablaMapeo);
          }

          
          /// <summary>
          /// Starts this instance.
          /// </summary>
          /// <returns></returns>
          /// <exception cref="System.Exception"></exception>
          public byte[] Start()
          {
               string file;
               using (_Report = new ReportDocument())
               {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                         file = Path.Combine(_ReportPath, _ReportName);
                         if (!File.Exists(file))
                              throw new Exception(String.Format("The report {0} not exists", file));
                         CreateConecctionInfo();
                         GenerateOutputReportName();
                         SetExtension();
                         _Report.Load(file, OpenReportMethod.OpenReportByTempCopy);
                         FormatSections();
                         SetFormulas();
                         SetTables();
                         ExportToStream().CopyTo(memoryStream);
                         return memoryStream.ToArray();
                    }
               }
          }
          #endregion
     }
}