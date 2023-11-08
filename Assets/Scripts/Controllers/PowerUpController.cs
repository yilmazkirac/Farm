using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PowerUpController : MonoBehaviour
    {
        [SerializeField] private PowerUpData _powerUpData;
        [SerializeField] private int _LockedUnitId;
        private bool isPowerUpUsed;

        private void Awake()
        {
            isPowerUpUsed = GetPowerUpStatus();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (_powerUpData.PowerUpType==PowerUpType.BagBooster&&!isPowerUpUsed)
                {
                    isPowerUpUsed = true;
                    BagConrtoller bagConrtoller = other.GetComponent<BagConrtoller>();
                     bagConrtoller.BagCapacityUp(_powerUpData.BoostCount);
                     AudioManager.Instance.PlayAudio(AudioClipType.grabClip);
                     PlayerPrefs.SetString("PowerUpStatus", "used");
                }
            }
        }

        private bool GetPowerUpStatus()
        {
            string status = PlayerPrefs.GetString("PowerUpStatus", "Ready");
            if (status.Equals("Ready"))
            {
                return false;
            }

            return true;
        }
    }
}