using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        [SerializeField] private TextMeshProUGUI _coinCountText;

        private void Awake()
        {
            if (Instance!=null&&Instance!=this)
            {
               Destroy(gameObject);
            }

            Instance = this;
        }


        public void ShowCoinCountOnScreen(int coins)
        {
            _coinCountText.text = coins.ToString();
        }
    }
}