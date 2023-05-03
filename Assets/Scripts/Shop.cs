using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] List<Text> costText = new List<Text>();
    [SerializeField] GameObject shopUI;
    [SerializeField] GameObject currentSkin;
    [SerializeField] private int money;
    [SerializeField] private Text moneyText;
    public Skin[] skin;
    bool isOpen;

    void Start()
    {
        for (var i = 0; i < skin.Length; i++)
        {
            costText[i].text = skin[i].price.ToString() + " Gold";
        }
    }

    public void OpenShop()
    {
        if (!isOpen)
        {
            shopUI.SetActive(true);
            isOpen = true;
            Time.timeScale = 0;
        }
        else
        {
            shopUI.SetActive(false);
            isOpen = false;
            Time.timeScale = 1;
        }
    }

    public void AddMoney(int count)
    {
        money += count;
        moneyText.text = "Money: " + money.ToString();
    }

    public void BuySkin(int count)
    {
        if (money >= skin[count].price && !skin[count].isBuy)
        {
            costText[count].text = "Sold";
            currentSkin.SetActive(false);
            skin[count].skinToBuy.SetActive(true);
            currentSkin = skin[count].skinToBuy;
            skin[count].isBuy = true;
            AddMoney(-skin[count].price);
        }
        if (skin[count].isBuy)
        {
            currentSkin.SetActive(false);
            skin[count].skinToBuy.SetActive(true);
            currentSkin = skin[count].skinToBuy;
        }
    }
}
[System.Serializable]
public class Skin
{
    public GameObject skinToBuy;
    public int price;
    public bool isBuy;
}

