using System;
//Tiene que incluir las referencias del crm para que estos namespace funcionen
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.ServiceModel.Description;

namespace ProxyCrm.Models
{
    /// <summary>
    /// Clase que hace la conexion al CRM. 
    /// <warning>
    /// En caso de que no encuentre 'Microsoft.IdentityModel, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35' o algunas de sus dependencias. 
    /// necesitara instalar Windows Identity Foundation.
    /// </warning>
    /// </summary>
    public class CrmProxy
    {
        //Almacena el proxy de la organizacion
        private IOrganizationService _service;
        //Usuario que se va conectar al crm.
        private string _user;
        //Url del CRM que se desea conectar. 
        private Uri _url;
        //Dominio donde esta el crm.
        private string _domain;
        //contrasena
        private string _pass;
        public string Pass
        {
            get { return _pass; }
        }
        public string User
        {
            get { return _user; }
        }
        public string Domain
        {
            get { return _domain; }
        }
        public Uri Url
        {
            get
            {
                return _url;
            }
        }

        public IOrganizationService Service
        {
            get
            {
                OrganizationServiceProxy servicio;
                ClientCredentials cliente = new ClientCredentials();
                cliente.Windows.ClientCredential = new System.Net.NetworkCredential(_user, _pass, _domain);
                ClientCredentials cliente1 = new ClientCredentials();
                using (servicio = new OrganizationServiceProxy(_url, null, cliente, cliente1))
                {
                    servicio.ServiceConfiguration.CurrentServiceEndpoint.Behaviors.Add(new ProxyTypesBehavior());
                    _service = (IOrganizationService)servicio;
                }
                return _service;
            }

        }

        /// <summary>
        /// Contrcutor del Proxy, crea la conexion con el crm.
        /// </summary>
        public CrmProxy()
        {  
                _user = "dga";
                _pass = "@mb13nt32015";
                _domain = "semarena";
                _url = new Uri("http://crmdesarrollo/MinisteriodeMedioAmbienteyRecu/XRMServices/2011/Organization.svc ");
        }
    }
}