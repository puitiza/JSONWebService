using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace JSONWebService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IService1
    {
        public List<wsCustomer> GetAllCustomers()
        {
            try
            {
                NorthwindDataContext dc = new NorthwindDataContext();
                List<wsCustomer> results = new List<wsCustomer>();
               //Revisar porque asi debería ser
               /* foreach (Customer cust in dc.Customers)
                {
                    results.Add(new wsCustomer()
                    {
                        CustomerID = cust.CustomerID,
                        CompanyName = cust.CompanyName,
                        City = cust.City
                    });
                } */
                foreach (Customers cust in dc.Customers)
                {
                    results.Add(new wsCustomer()
                    {
                        CustomerID = cust.CustomerID,
                        CompanyName = cust.CompanyName,
                        City = cust.City
                    });
                }
              return results;
            }
            catch (Exception ex)
            {
                //  Return any exception messages back to the Response header
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.StatusDescription = ex.Message.Replace("\r\n", "");
                return null;
            }
        }

        public string GetData(string value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
