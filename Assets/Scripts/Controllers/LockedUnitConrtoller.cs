using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class LockedUnitConrtoller : MonoBehaviour
    {
        [SerializeField] private string _id;
        
        [SerializeField] private int _price;
        [SerializeField] private TextMeshPro _priceText;
        [SerializeField] private GameObject _lockedUnit;
        [SerializeField] private GameObject _unluckedUnit;

        private bool isPurchased;
        private void Awake()
        {
            _priceText.text = _price.ToString();
        }

        private void Start()
        {
            LoadUnit();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")&&!isPurchased)
            {
                UnlockedUnit();
            }
        }

        private void UnlockedUnit()
        {
            if (CashManager.Instance.TryBuyThisUnit(_price))
            {
                AudioManager.Instance.PlayAudio(AudioClipType.shopClip);
                Unlocked();
                SaveUnit();
            }
        }
        private void Unlocked()
        {
            isPurchased = true;
            _lockedUnit.SetActive(false);
            _unluckedUnit.SetActive(true);
        }

        private void SaveUnit()
        {
            PlayerPrefs.SetString(_id,"saved");
        }

        private void LoadUnit()
        {
         string status=PlayerPrefs.GetString(_id);
         if (status.Equals("saved"))
         {
             Unlocked();
         }
        }
    }
}