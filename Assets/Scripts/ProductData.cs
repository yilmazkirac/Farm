using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "CD_Product Data", menuName = "Scriptable Object/CD_Product Data", order = 0)]
    public class ProductData : ScriptableObject
    {
        public GameObject ProductPrefab;
        public ProductType ProductType;
        public int ProductPrice;
    }
    public enum ProductType{tomato,cabbage}
}