using UnityEngine;

namespace DefaultNamespace
{
    public enum PowerUpType{BagBooster}
    [CreateAssetMenu(fileName = "CD_PowerUpData Data", menuName = "Scriptable Object/CD_PowerUpData", order = 1)]
    public class PowerUpData : ScriptableObject
    {
        public PowerUpType PowerUpType;
        public int BoostCount;

    }
}