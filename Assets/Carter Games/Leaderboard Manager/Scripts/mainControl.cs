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
    public void dasdasd()
    {

    }
   public void calculateThePrice()
    {
        product = new Product();
        product.pName = productName.text;
        product.supplyingPrice = Decimal.Parse(productSupplyingPrice.text);
        //product.trendyolComissionRate = Decimal.Parse(productTYComissionRate.text);
        product.trendyolComissionRate = 10;
        product.KDV = Decimal.Parse(productKdvRate.text);
        product.cargoExpense = Decimal.Parse(productCargoExpence.text);
        product.profitRate = Decimal.Parse(productProfitRate.text);
        product.calculateSellingPrice();
        showSellingPrice(product);

       
    }

    void showSellingPrice(Product product)
    {
        sellingPriceSet = GameObject.FindGameObjectWithTag("sellingPriceSet");

        txtSellingPriceLabel = sellingPriceSet.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        txtSellingPriceLabel.text = "Satış Fiyatı:";

        txtSellingPriceAmount = sellingPriceSet.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
        txtSellingPriceAmount.text = product.sellingingPrice.ToString();
    }
}
