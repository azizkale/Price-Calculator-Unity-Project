using System;
using UnityEngine;
using UnityEngine.UI;
using NativeWebSocket;

public class mainControl : MonoBehaviour
{

    public InputField inputProductName;
    public InputField inputProductSupplyingPrice;
    public InputField inputProductKdvRate;
    public InputField inputProductCargoExpence;
    public InputField inputProductTYComissionRate;
    public InputField inputProductProfitRate;

    GameObject sellingPriceSet;
    Text txtSellingPriceLabel;
    Text txtSellingPriceAmount;
    Text tyComissionAmount;
    Text KDVAmount;
    Text cargoExpenceAmount;
    Text profitAmount;
    Text txtInvoiceAmount;
    Text txtAlert;


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
        //
        txtAlert = GameObject.FindGameObjectWithTag("alertText").GetComponent<Text>();

    }

    public void calculateThePrice()
    {
        // Inputs texts can not be null or zero (except product name)
        if (validate.Validate(
              inputProductSupplyingPrice,
              inputProductKdvRate,
              inputProductCargoExpence,
              inputProductTYComissionRate,
              inputProductProfitRate))
        {

            product = new Product();
            product.pName = inputProductName.text;

        

            //this shit is needed to convert the data to decimal with "." (dot)
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            product.supplyingPrice = float.Parse(String.Format("{0:0.00}",inputProductSupplyingPrice.text, customCulture));
            product.trendyolComissionRate = float.Parse(String.Format("{0:0.00}", inputProductTYComissionRate.text, customCulture));
            product.KDV = float.Parse(String.Format("{0:0.00}",inputProductKdvRate.text, customCulture));
            product.cargoExpense = float.Parse(String.Format("{0:0.00}", inputProductCargoExpence.text, customCulture));
            product.profitRate = float.Parse(String.Format("{0:0.00}", inputProductProfitRate.text, customCulture));

       
                                                     
            product.calculateSellingPrice();             
            showAmounts(product);                        
            txtAlert.text = "";
        }

       

    }

   void showAmounts(Product product)
    {
       
        //this shit is needed to convert the data to decimal with "." (dot)
        System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
        customCulture.NumberFormat.NumberDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
        
        //Selling Price Amount and Label Name
        txtSellingPriceLabel = GameObject.FindGameObjectWithTag("sellingPriceAmountLabel").GetComponent<Text>();
        txtSellingPriceLabel.text = "Satış Fiyatı:";
        txtSellingPriceAmount = GameObject.FindGameObjectWithTag("sellingPriceAmount").GetComponent<Text>();
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

    // On value change event
    public void makeInputFieldsBackgroundWhite(InputField input)
    {
        input.GetComponent<Image>().color = Color.white;
    }

   public void clearTheForm()
    {
        inputProductName.text = "";
        inputProductSupplyingPrice.text = "0";
        inputProductKdvRate.text = "0";
        inputProductCargoExpence.text = "0";
        inputProductTYComissionRate.text = "0";
        inputProductProfitRate.text = "0";

        txtSellingPriceAmount.text = "";
        tyComissionAmount.text = "";
        KDVAmount.text = "";
        cargoExpenceAmount.text = "";
        profitAmount.text = "";

        txtAlert.text = "";
        txtInvoiceAmount.text = "";
    }

   public void closeApp()
    {
        Application.Quit();
    }
   
    private void defafultValuesOfInputFields()
    {
        inputProductSupplyingPrice.text = "0";
        inputProductKdvRate.text = "0";
        inputProductCargoExpence.text = "0";
        inputProductTYComissionRate.text = "0";
        inputProductProfitRate.text = "0";
    }

    // Websocket funct,ons
    private async void webSocketConnection()
    {
        websocket = new WebSocket("ws://localhost:5000");
      
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
        txtSellingPriceAmount = GameObject.FindGameObjectWithTag("sellingPriceAmount").GetComponent<Text>();      

        if (websocket.State == WebSocketState.Open)
        {
            if (txtSellingPriceAmount.text != "")
            {                //serializes the data to json format and sends to websocket server
                product.requestInfo = "appReq";
                string productsData = JsonUtility.ToJson(product);
                await websocket.SendText(productsData);
                txtAlert.text = "Ürün gönderildi.";
                txtAlert.GetComponent<Text>().color = Color.blue;
            }
            else if (txtSellingPriceAmount.text =="")
            {
                txtAlert.text = "İlk önce fiyat hesaplayınız!";
                txtAlert.GetComponent<Text>().color = Color.red;
            }
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

