using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ProxyCrm.Models
{
    partial class CrmContext
    {
        public class CRUDArchivo
        {

            /// <summary>
            /// Busca todos los archivos de un record dado.
            /// </summary>
            /// <param name="recordid"></param>
            /// <returns>retorna la lista de archivo de un record</returns>
            public  List<Archivo> Consultar(Guid recordid)
            {
                CRMDataContext db = new CRMDataContext();

                var result = db.AnnotationBases.Where(AnnotationBase => AnnotationBase.ObjectId == recordid);
                if (result == null)
                    return null;
                List<Archivo> returnVal = new List<Archivo>();
                foreach (AnnotationBase i in result)
                {
                    using (FileStream fileStream = new FileStream(i.FileName, FileMode.Create))
                    {
                        byte[] fileContent = Convert.FromBase64String(i.DocumentBody);
                        fileStream.Write(fileContent, 0, fileContent.Length);
                        returnVal.Add(new Archivo
                        {
                            ArchivoId = i.AnnotationId,
                            Contenido = fileContent,
                            NombreArchivo = i.FileName,
                            TipoDeArchivo = i.MimeType,
                            Longitud = i.FileSize.Value
                        });

                    }
                }
                return returnVal;
            }
        }
    }
}