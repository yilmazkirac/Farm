using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class CashManager : MonoBehaviour
    {
        public static CashManager Instance;
        private int coins;
        private void Awake()
        {
            if (Instance!=null&&Instance!=this)
            {
                Destroy(gameObject);
            }

            Instance = this;
        }

        private void Start()
        {
            LoadCash();
            Display();
        }

        public void AddCoin(int price)
        {
            coins += price;
            Display();
        }

        private void SpendCoin(int price)
        {
            coins -=price;
            Display();
        }
        public bool TryBuyThisUnit(int price)
        {
            if (GetCoins()>price)
            {
                SpendCoin(price);
                return true;
            }

            return false;
        }
        public int GetCoins()
        {
            return coins;
        }
        public void ExChangeProduct(ProductData productData)
        {
            AddCoin(productData.ProductPrice);
        }

        public void Display()
        {
           UIManager.Instance.ShowCoinCountOnScreen(coins);
           SaveCash();
        }

        private void LoadCash()
        {
            coins=PlayerPrefs.GetInt("Coin");
        }

        private void SaveCash()
        {
            PlayerPrefs.SetInt("Coin",coins);
     
        }
    }
}