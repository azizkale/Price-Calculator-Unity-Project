using System;
using UnityEngine;
using UnityEngine.UI;
using NativeWebSocket;

public class mainControl : MonoBehaviour
{

    public InputField productName;
    public InputField productSupplyingPrice;
    public InputField productKdvRate;
    public InputField productCargoExpence;
    public InputField productTYComissionRate;
    public InputField productProfitRate;

    GameObject sellingPriceSet;
    Text txtSellingPriceLabel;
    Text txtSellingPriceAmount;
    Text tyComissionAmount;
    Text KDVAmount;
    Text cargoExpenceAmount;
    Text profitAmount;
    Text txtInvoiceAmount;


    Product product;
    ProductValidator validate;

    WebSocket websocket;

    public GameObject menu;
    public GameObject subMenu;

    void Start()
    {
        validate = new ProductValidator();
        this.defafultValuesOfInputFields();
       webSocketConnection();
    }
  
   public void calculateThePrice()
    {
        product = new Product();
        product.pName = productName.text;

        

        //this shit is needed to convert the data to decimal with "." (dot)
        System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
        customCulture.NumberFormat.NumberDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

        product.supplyingPrice = float.Parse(String.Format("{0:0.##}",productSupplyingPrice.text, customCulture));
        product.trendyolComissionRate = float.Parse(String.Format("{0:0.00}", productTYComissionRate.text, customCulture));
        product.KDV = float.Parse(String.Format("{0:0.00}",productKdvRate.text, customCulture));
        product.cargoExpense = float.Parse(String.Format("{0:0.00}", productCargoExpence.text, customCulture));
        product.profitRate = float.Parse(String.Format("{0:0.00}", productProfitRate.text, customCulture));

        if (validate.Validate(product))
        {
            product.calculateSellingPrice();
        }

        showAmounts(product);

    }

   void showAmounts(Product product)
    {
       
        //this shit is needed to convert the data to decimal with "." (dot)
        System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
        customCulture.NumberFormat.NumberDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;


        sellingPriceSet = GameObject.FindGameObjectWithTag("sellingPriceSet");

        txtSellingPriceLabel = sellingPriceSet.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        txtSellingPriceLabel.text = "Satış Fiyatı:";

        txtSellingPriceAmount = sellingPriceSet.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
        //txtSellingPriceAmount.text = "";
        txtSellingPriceAmount.text = String.Format("{0:0.00}", product.sellingingPrice, customCulture) + " TL";

        
        //Trendyol Comission Amount Text
        tyComissionAmount = GameObject.FindGameObjectWithTag("comissionAmount").GetComponent<Text>();
        tyComissionAmount.text = String.Format("{0:0.00}", product.calculateTrendyolComisssionExpenseAmount(), customCulture) + " TL";

        //KDV Amount Text
        KDVAmount = GameObject.FindGameObjectWithTag("KDV_Amount").GetComponent<Text>();
        KDVAmount.text = String.Format("{0:0.00}", product.calculateKDVExpenseAmount(), customCulture) + " TL";

        //Cargo Expence Amount Text
        cargoExpenceAmount = GameObject.FindGameObjectWithTag("CargoExpenceAmount").GetComponent<Text>();
        cargoExpenceAmount.text = String.Format("{0:0.00}", product.cargoExpense, customCulture) + " TL";

        //Profit Amount Text
        profitAmount = GameObject.FindGameObjectWithTag("ProfitAmount").GetComponent<Text>();
        profitAmount.text = String.Format("{0:0.00}", product.calculateprofitAmount(), customCulture) + " TL";
        
        //Invoice Amount Text
        txtInvoiceAmount = GameObject.FindGameObjectWithTag("InvoiceAmount").GetComponent<Text>();
        txtInvoiceAmount.text = String.Format("{0:0.00}", product.calculateInvoice(), customCulture) + " TL";


    }


   public void clearTheForm()
    {
        productName.text = "";
        productSupplyingPrice.text = "";
        productKdvRate.text = "";
        productCargoExpence.text = "";
        productTYComissionRate.text = "";
        productProfitRate.text = "";

        txtSellingPriceAmount.text = "";
        tyComissionAmount.text = "";
        KDVAmount.text = "";
        cargoExpenceAmount.text = "";
        profitAmount.text = "";
    }

   public void closeApp()
    {
        Application.Quit();
    }
   
    private void defafultValuesOfInputFields()
    {
        productSupplyingPrice.text = "0";
        productKdvRate.text = "0";
        productCargoExpence.text = "0";
        productTYComissionRate.text = "0";
        productProfitRate.text = "0";
    }

    // Websocket funct,ons
    private async void webSocketConnection()
    {
        //websocket = new WebSocket("ws://localhost:5000");
        websocket = new WebSocket("ws://frozen-earth-60865.herokuapp.com");

        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
        };

        websocket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        websocket.OnClose += (e) =>
        {
            //Debug.Log("Connection closed!");
        };

        websocket.OnMessage += (bytes) =>
        {
            // Reading a plain text message
            //var message = System.Text.Encoding.UTF8.GetString(bytes);
           
        };

        // Keep sending messages at every 0.3s
        // InvokeRepeating("SendWebSocketMessage", 0.0f, 0.3f);

        await websocket.Connect();
    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        websocket.DispatchMessageQueue();
#endif
    }

    public async void SendWebSocketMessage()
    {
        //serializes the data to json format and sends to websocket server
        product.requestInfo = "appReq";
        string productsData = JsonUtility.ToJson(product);
        //string dataToWebSocket = "data:{data1:fromApp,data2:"+productsData+"}";
        if (websocket.State == WebSocketState.Open)
        {
            await websocket.SendText(productsData);            
        }
    }
   
    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }

    //Menu functions
    public void showMenu()
    {
        subMenu.SetActive(true);
        menu.SetActive(false);
    }
    
    public void closeMenu()
    {
        subMenu.SetActive(false);
        menu.SetActive(true);
    }   
}

