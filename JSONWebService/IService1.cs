using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using System.IO;

namespace JSONWebService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IService1
    {

        // TODO: agregue aquí sus operaciones de servicio

        //BodyStyle = WebMessageBodyStyle.Wrapped
        /*
         * Este es el Formato que te bota de poner eso BodyStyle = WebMessageBodyStyle.Wrapped sino quiero que se muestro solo lo quito
                "GetAllCustomersResult": [ 
                                            {
                                            "City": "Berlin",
                                            "CompanyName": "Alfreds Futterkiste",
                                            "CustomerID": "ALFKI"
                                            },
                                            {
                                            "City": "México D.F.",
                                            "CompanyName": "Ana Trujillo Emparedados y helados",
                                            "CustomerID": "ANATR"
                                            }
                ]
         * 
         * Te tiene que quedar asi sin el BodyStyle = WebMessageBodyStyle.Wrapped
           {
              {
                "City": "Berlin",
                "CompanyName": "Alfreds Futterkiste",
                "CustomerID": "ALFKI"
              },
              {
                "City": "México D.F.",
                "CompanyName": "Ana Trujillo Emparedados y helados",
                "CustomerID": "ANATR"
              }
            }
         */

        //Esto es un metodo GET
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "getAllCustomers")]
        List<wsCustomer> GetAllCustomers();

        //Esto es un metodo GET
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "getOrdersForCustomer/{customerID}")]
        List<wsOrder> GetOrdersForCustomer(string customerID);

        /*
         * Cuando Usamos ( "Int" orderID) como parametro no va a funcionar en estos casos tenemos que pasarlo como string y adentro cambiarlo a Int
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getOrderDetails/{orderID}")]
        List<wsOrder> GetOrderDetails(int orderID);
        */
        //Esto sería su solucion
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "getOrderDetails/{orderID}")]
        wsOrder GetOrderDetails(string orderID);
        // List<wsOrder> GetOrderDetails(string orderID);

        //Es Ejemplo de metodo GET con storedProcedure
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "getCustomerOrderHistory/{customerID}")]
        List<CustomerOrderHistory> GetCustomerOrderHistory(string customerID);
        
        //Un Ejemplo de metodo Post
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "updateOrderAddress")]
        int UpdateOrderAddress(Stream JSONdataStream);

        //Ejemplos de Web sdervices al crear esta clase por defecto
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getData/{value}")]
        string GetData(string value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

    }


    // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
