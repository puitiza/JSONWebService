namespace JSONWebService
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.ServiceModel.Web;


    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IService1
    {

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

        //Esto es un metodo GET "Sin parametro" ---  CRUD(READ)
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "getAllCustomers")]
        List<wsCustomer> GetAllCustomers();

        //Esto es un metodo GET "Con parametro" ---  CRUD(READ)
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "getOrdersForCustomer/{customerID}")]
        List<wsOrder> GetOrdersForCustomer(string customerID);

        //Esto es un metodo GET "Con parametro" ---  CRUD(DELETE)
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "deleteCustomer/{customerID}")]
        wsSQLResult DeleteCustomer(string customerID);
        // int DeleteCustomer(string customerID); -- -Esta es la forma no recomendable


        /*
         * Cuando Usamos ( "Int" orderID) como parametro no va a funcionar en estos casos tenemos que pasarlo como string y adentro cambiarlo a Int
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getOrderDetails/{orderID}")]
        ---- List<wsOrder> GetOrderDetails(int orderID); ----
        */
        //Esto sería su solucion
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "getOrderDetails/{orderID}")]
        wsOrder GetOrderDetails(string orderID);
        // List<wsOrder> GetOrderDetails(string orderID);

        //Es Ejemplo de metodo GET con storedProcedure "Con parametro" ---  CRUD(READ)
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "getCustomerOrderHistory/{customerID}")]
        List<CustomerOrderHistory> GetCustomerOrderHistory(string customerID);
        
        //Un Ejemplo de metodo Post -- CRUD(UPDATE)
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "updateOrderAddress")]
        int UpdateOrderAddress(wsOrder JSONdataStream);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "createCustomer")]
        wsSQLResult CreateCustomer(wsCustomer JSONdataStream);
        
    }

}
