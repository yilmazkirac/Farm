using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class UnlockBakeryUnitController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _bakeryText;
        [SerializeField] private int _maxStoredPorductCount;
        [SerializeField] private ProductType _productType;
        [SerializeField] private int _usePorductInSeconds = 10;
        [SerializeField] private Transform _coinTransform;
        [SerializeField] private GameObject _coinGO;
        [SerializeField] private ParticleSystem _smokeParticle;

        private float time;
        private int storedProductCount;

        private void Awake()
        {
            DisplayProductCount();
        }

        private void DisplayProductCount()
        {
            _bakeryText.text = storedProductCount.ToString() + "/" + _maxStoredPorductCount.ToString();
            ControlSmokeEfect();
        }
        private void Update()
        {
            if (storedProductCount>0)
            {
                time += Time.deltaTime;
                if (time>=_usePorductInSeconds)
                {
                    time = 0f;
                    UseProduct();
                }
            }
        }

     

        public ProductType getNeededProductType()
        {
            return _productType;
        }
        public bool StoreProduct()
        {
            if (_maxStoredPorductCount==storedProductCount)
            {
                return false;
            }

            storedProductCount++;
            DisplayProductCount();
            return true;
        }

        private void UseProduct()
        {
            storedProductCount--;
            DisplayProductCount();
            CreateCoin();
        }

        private void CreateCoin()
        {
            Vector3 position = Random.insideUnitSphere * 1f;
            Vector3 InstantiatePos = _coinTransform.position + position;

            Instantiate(_coinGO, InstantiatePos, Quaternion.identity);
        }

        private void ControlSmokeEfect()
        {
            if (storedProductCount==0)
            {
                if (_smokeParticle.isPlaying)
                {
                    _smokeParticle.Stop();
                }
            }
         
            else
            {
                if (_smokeParticle.isStopped)
                {
                    _smokeParticle.Play();
                }
            }
        }
    }
    
}