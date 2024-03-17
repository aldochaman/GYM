using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Control_Gimmnacio
{
     class Email
     {
          SmtpClient Smtp;
          MailMessage mm;
          String usuario;
          String contraseña;
          String servidor;
          int puerto;
          const String cuerpoHtml1 = "Hola ";
          const String cuerpoHtml2 = "Gracias por elegirnos para tu acondicionamiento físico! Nos alegra tenerte como miembro.";
          const String cuerpoHtml3 = "Estamos aquí para ayudarte a alcanzar tus objetivos. No dudes en acercarte si necesitas algo.";
          const String cuerpoHtml4 = "¡Nos vemos en el gimnasio!";
          const String cuerpoHtml5 = "Saludos";
          const String cuerpoHtml6 = "Omar Garcia Pastrana - EXE GYM";
          
          conexionDatos Parametro = new conexionDatos();
          public Email()
          {         
               usuario = Parametro.GetParametro("usuario");
               contraseña = Parametro.GetParametro("pass");
               servidor = Parametro.GetParametro("host");
               puerto = Convert.ToInt32(Parametro.GetParametro("puerto"));
               Smtp = new SmtpClient();
               Smtp.Port = puerto;
               Smtp.Host = servidor;
               Smtp.EnableSsl = true;
               Smtp.Timeout = 10000;
               Smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
               Smtp.UseDefaultCredentials = false;
               Smtp.Credentials = new System.Net.NetworkCredential(usuario, contraseña);
               
          }
          public Boolean Enviar(String destinatario, String asunto, String nombre, String rutafile)
          {
               String cuerpoCompleto = "";

               cuerpoCompleto = cuerpoHtml1 + " " + nombre + "<br/><br/><p>" + cuerpoHtml2 + "<br/><br/><p>" + cuerpoHtml3 + "<br/><br/><p>" + cuerpoHtml4 + "<br/><br/><p>" + cuerpoHtml5 + "<br/><br/><p>" + cuerpoHtml6;
               mm = new MailMessage(usuario, destinatario, asunto, cuerpoCompleto);
               mm.IsBodyHtml = true;

               //if (rutafile != null)
               //{
               //     //agregado de archivo
               //     //foreach (string archivo in rutafile)
               //     //{
               //     //     //comprobamos si existe el archivo y lo agregamos a los adjuntos
               //     //     if (System.IO.File.Exists(archivo))
               //     //          mm.Attachments.Add(new Attachment(archivo));
               //     //}
               //}
               mm.Attachments.Add(new Attachment(rutafile));            
               mm.BodyEncoding = UTF8Encoding.UTF8;
               mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
               Smtp.Send(mm);
               return true;
          }
     }
}
