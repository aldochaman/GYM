using QRCoder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;

namespace Control_Gimmnacio
{
     public class QRCodeGenerator
     {
          private byte[] qrCodeBinary;

          public void GenerateQrCode(string data, string directory, string fileName)
          {
               // Configurar opciones de codificación
               EncodingOptions options = new EncodingOptions
               {
                    Width = 300,
                    Height = 300,
                    Margin = 0
               };

               // Crear el escritor de código QR
               BarcodeWriter writer = new BarcodeWriter();
               writer.Format = BarcodeFormat.QR_CODE;
               writer.Options = options;

               // Generar el código QR como un bitmap
               Bitmap qrCodeImage = writer.Write(data);

               // Guardar la imagen en el directorio especificado
               string filePath = Path.Combine(directory, fileName + ".png");
               qrCodeImage.Save(filePath);

               // Convertir la imagen a bytes
               using (MemoryStream ms = new MemoryStream())
               {
                    qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    qrCodeBinary = ms.ToArray();
               }
               //GUARDAR EN LA BASE
               conexionDatos dts = new conexionDatos();
               // Construir el query SQL
               string query = "UPDATE socio SET QrCodePath = @QrCodePath, QrCodeBinary = @QrCodeBinary WHERE idsocio = @IdSocio";

               // Crear los parámetros
               SqlParameter[] parameters =
               {
                new SqlParameter("@QrCodePath", filePath),
                new SqlParameter("@QrCodeBinary", qrCodeBinary),
                new SqlParameter("@IdSocio", data)
               };
               dts.update(query.ToString(),parameters);
          }
     }
}
