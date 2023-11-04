using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class BagConrtoller : MonoBehaviour
{
    [SerializeField] private Transform bag;
    public List<GameObject> ProductList;
    private Vector3 _productSize;

    public void AddProductToBag(GameObject productObje)
    {
        GameObject box = Instantiate(productObje, Vector3.zero, Quaternion.identity);
       box.transform.SetParent(bag,true);

       CalculateObjectSize(box);
       float yPosition = CalculateNewYPositionOfBox();
           
       box.transform.localRotation=Quaternion.identity;
       box.transform.localPosition = Vector3.zero;
       box.transform.localPosition = new Vector3(0, yPosition, 0);
       ProductList.Add(productObje);
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
}
