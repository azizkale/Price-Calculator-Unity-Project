using System;
using UnityEngine;
using UnityEngine.UI;

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
    Product product;

    void Start()
    {
       
       
    }
  
   public void calculateThePrice()
    {
        product = new Product();
        product.pName = productName.text;

        //this shit is needed to convert the data to decimal with "." (dot)
        System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
        customCulture.NumberFormat.NumberDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

        product.supplyingPrice = Decimal.Parse(String.Format("{0:0.##}",productSupplyingPrice.text,customCulture));
        product.trendyolComissionRate = Decimal.Parse(String.Format("{0:0.00}", productTYComissionRate.text, customCulture));
        product.KDV = Decimal.Parse(String.Format("{0:0.00}",productKdvRate.text, customCulture));
        product.cargoExpense = Decimal.Parse(String.Format("{0:0.00}", productCargoExpence.text, customCulture));
        product.profitRate = Decimal.Parse(String.Format("{0:0.00}", productProfitRate.text, customCulture));

        product.calculateSellingPrice();

        showSellingPrice(product);

       
    }

    void showSellingPrice(Product product)
    {
        sellingPriceSet = GameObject.FindGameObjectWithTag("sellingPriceSet");

        txtSellingPriceLabel = sellingPriceSet.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        txtSellingPriceLabel.text = "Satış Fiyatı:";

        txtSellingPriceAmount = sellingPriceSet.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();

        txtSellingPriceAmount.text = "";
        txtSellingPriceAmount.text = String.Format("{0:0.00}", product.sellingingPrice) + " TL";
    }
}
