using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class BagConrtoller : MonoBehaviour
{
    [SerializeField] private Transform bag;
    [SerializeField] private TextMeshPro _maxText;
    public List<ProductData> ProductList;
    private Vector3 _productSize;
    private int maxBagCapacity;
    
    
    private void Awake()
    {
     
    maxBagCapacity = LoadBagCapacity();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ShopPoint"))
        {
            if (ProductList.Count>0)
                AudioManager.Instance.PlayAudio(AudioClipType.shopClip);
            for (int i =ProductList.Count -1;i>=0;i--)
            {
                SellProductsToShop(ProductList[i]);
                Destroy(bag.transform.GetChild(i).gameObject);
                ProductList.RemoveAt(i);
            }
            ControlBagCapacity();
            CashManager.Instance.Display();
        }

        if (other.CompareTag("UnlockBakeryUnit"))
        {
            UnlockBakeryUnitController bakeryUnit = other.GetComponent<UnlockBakeryUnitController>();
            ProductType neededType = bakeryUnit.getNeededProductType();
            for (int i =ProductList.Count -1;i>=0;i--)
            {
                if (ProductList[i].ProductType==neededType)
                {
                    if (bakeryUnit.StoreProduct()==true)
                    {
                        Destroy(bag.transform.GetChild(i).gameObject);
                        ProductList.RemoveAt(i);
                    }
                }

                StartCoroutine(PutProductsInOrder());
                ControlBagCapacity();
            }
        }
    }

    private void SellProductsToShop(ProductData productData)
    {
        CashManager.Instance.ExChangeProduct(productData);
    }

    public void AddProductToBag(ProductData productData)
    {
    
        GameObject box = Instantiate(productData.ProductPrefab, Vector3.zero, Quaternion.identity);
       box.transform.SetParent(bag,true);

       CalculateObjectSize(box);
       float yPosition = CalculateNewYPositionOfBox();
           
       box.transform.localRotation=Quaternion.identity;
       box.transform.localPosition = Vector3.zero;
       box.transform.localPosition = new Vector3(0, yPosition, 0);
       ProductList.Add(productData);
       ControlBagCapacity();
    }

    private float CalculateNewYPositionOfBox()
    {
        float newYPos;
        return newYPos = _productSize.y * ProductList.Count;
     
    }
    private void CalculateObjectSize(GameObject gameObject)
    {
        if (_productSize==Vector3.zero)
        { 
            MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
            _productSize = renderer.bounds.size;
        }
    }

    private void ControlBagCapacity()
    {
        if (ProductList.Count==maxBagCapacity)
        {
            SetMaxTextOn();
        }
        else
        {
            SetMaxTextOff();
        }
    }
    private void SetMaxTextOn()
    {
        if (!_maxText.isActiveAndEnabled)
        {
            _maxText.gameObject.SetActive(true);
        }
    }
    private void SetMaxTextOff()
    {
        if (_maxText.isActiveAndEnabled)
        {
            _maxText.gameObject.SetActive(false);
        }
    }

    public bool IsEmptySpace()
    {
        if (ProductList.Count<maxBagCapacity)
        {
            return true;
        }

        return false;
    }

    private IEnumerator PutProductsInOrder()
    {
        yield return new WaitForSeconds(0.15f);
        for (int i = 0; i < bag.childCount; i++)
        {
            float newYPos = _productSize.y * i;
            bag.GetChild(i).transform.localPosition = new Vector3(0, newYPos, 0);
        }
        
    }

    public void BagCapacityUp(int boostCount)
    {
        maxBagCapacity += boostCount;
        PlayerPrefs.SetInt("BagCapacity",maxBagCapacity);
        ControlBagCapacity();
    }

    private int LoadBagCapacity()
    {
        int maxBag = PlayerPrefs.GetInt("BagCapacity",5);
        return maxBag;
    }
}
