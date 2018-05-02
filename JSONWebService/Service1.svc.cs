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

        public List<wsOrder> GetOrdersForCustomer(string customerID)
        {
            try
            {
                NorthwindDataContext dc = new NorthwindDataContext();
                List<wsOrder> results = new List<wsOrder>();
                System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.GetCultureInfo("en-US");
                foreach (Orders order in dc.Orders.Where(s => s.CustomerID == customerID))
                {
                    results.Add(new wsOrder()
                    {
                        OrderID = order.OrderID,
                        OrderDate = (order.OrderDate == null ) ? "" : order.OrderDate.Value.ToString("d",ci),
                        ShipAddress = order.ShipAddress,
                        ShipCity = order.ShipCity,
                        ShipName = order.ShipName,
                        ShipPostcode = order.ShipPostalCode,
                        ShippedDate = (order.ShippedDate == null) ? "" : order.ShippedDate.Value.ToString("d", ci)
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


        /*Si fuera una lista seria asi
         * public List<wsOrder> GetOrderDetails(String orderID)*/
        public wsOrder GetOrderDetails(String orderID)
        {
            try
            {
                NorthwindDataContext dc = new NorthwindDataContext();
                int orderIDnumber = int.Parse(orderID);
                wsOrder results = new wsOrder();
                System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.GetCultureInfo("en-US");
            
                foreach (Orders order in dc.Orders.Where(s => s.OrderID == orderIDnumber))
                {
                    results.OrderID = order.OrderID;
                    results.OrderDate = (order.OrderDate == null) ? "" : order.OrderDate.Value.ToString("d", ci);
                    results.ShipAddress = order.ShipAddress;
                    results.ShipCity = order.ShipCity;
                    results.ShipName = order.ShipName;
                    results.ShipPostcode = order.ShipPostalCode;
                    results.ShippedDate = (order.ShippedDate == null) ? "" : order.ShippedDate.Value.ToString("d", ci);     
                }
               /* 
                * Esto es para retornar una lista
                * List<wsOrder> results = new List<wsOrder>();
                System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.GetCultureInfo("en-US");
                foreach (Orders order in dc.Orders.Where(s => s.OrderID == orderIDnumber))
                {
                    results.Add(new wsOrder()
                    {
                        OrderID = order.OrderID,
                        OrderDate = (order.OrderDate == null) ? "" : order.OrderDate.Value.ToString("d", ci),
                        ShipAddress = order.ShipAddress,
                        ShipCity = order.ShipCity,
                        ShipName = order.ShipName,
                        ShipPostcode = order.ShipPostalCode,
                        ShippedDate = (order.ShippedDate == null) ? "" : order.ShippedDate.Value.ToString("d", ci)
                    });
                }*/

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

        public List<CustomerOrderHistory> GetCustomerOrderHistory(string customerID)
        {
            try
            {
                List<CustomerOrderHistory> results = new List<CustomerOrderHistory>();
                NorthwindDataContext dc = new NorthwindDataContext();
                foreach (CustOrderHistResult oneOrder in dc.CustOrderHist(customerID))
                {
                    results.Add(new CustomerOrderHistory()
                    {
                        ProductName = oneOrder.ProductName,
                        Total = oneOrder.Total ?? 0
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
