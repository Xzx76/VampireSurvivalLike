using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireSLike
{
    public class Weapon : MonoBehaviour
    {
        public List<WeaponStats> stats;
        public int weaponLevel;
        public string WeaponName;
        public bool IsEquiped=false;
        [HideInInspector]
        public bool statsUpdated;

        public Sprite icon;

        public void LevelUp()
        {
            if (weaponLevel < stats.Count - 1)
            {
                weaponLevel++;

                statsUpdated = true;

                if (weaponLevel >= stats.Count - 1)
                {
                    BattleManager.Instance.PlayerCtrl.fullyLevelledWeapons.Add(this);
                    BattleManager.Instance.PlayerCtrl.assignedWeapons.Remove(this);
                }
            }
        }
    }

    [System.Serializable]
    public class WeaponStats
    {
        public float speed, damage, range, timeBetweenAttacks, amount, duration;
        public string upgradeText;
    }
}

