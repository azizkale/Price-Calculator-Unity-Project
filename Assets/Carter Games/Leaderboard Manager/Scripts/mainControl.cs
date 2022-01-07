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
        product.supplyingPrice = Decimal.Parse(String.Format("{0:0.##}",productSupplyingPrice.text));
        product.trendyolComissionRate = Decimal.Parse(String.Format("{0:0.##}", productTYComissionRate.text));
        product.KDV = Decimal.Parse(String.Format("{0:0.##}", productKdvRate.text));
        product.cargoExpense = Decimal.Parse(String.Format("{0:0.##}", productCargoExpence.text));
        product.profitRate = Decimal.Parse(String.Format("{0:0.##}", productProfitRate.text));

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
        txtSellingPriceAmount.text = String.Format("{0:0.##}", product.sellingingPrice.ToString());
    }
}
