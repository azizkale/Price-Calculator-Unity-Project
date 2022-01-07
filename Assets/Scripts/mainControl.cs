using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    Product product = new Product();
    void Start()
    {
        productName.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
