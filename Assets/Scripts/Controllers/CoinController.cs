using UnityEngine;

namespace DefaultNamespace
{
    public class CoinController : MonoBehaviour
    {
        [SerializeField] private int _coinPrice;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                CashManager.Instance.AddCoin(_coinPrice);
                Destroy(gameObject);
            }
        }
    }
}