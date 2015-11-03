using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProxyCrm.Models.CrmCRUD
{
    /// <summary>
    /// Esta Clase contiene el guid del usuario y del OwningUnit de crm.
    /// </summary>
    public class BaseConfiguration
    {
        public static Guid userId {
            get { return new Guid("251E0342-0E53-E511-A3F2-005056AB0F09"); }} 
        public  static Guid OwningUnit
        { get { return new Guid("28CD4A8C-35A4-E111-B30E-00155D323506");}}
    }
}