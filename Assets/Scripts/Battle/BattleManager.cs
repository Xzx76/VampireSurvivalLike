using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireSLike
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance;
        public PlayerController PlayerCtrl;
        public Transform BattleControl;
        void Awake()
        {
            Instance = this;
            BattleControl.gameObject.SetActive(false);
            AssetManager.Instance.InstantiateAsync("Player01", playerObj =>
            {
                playerObj.transform.SetParent(this.transform.parent);
                playerObj.transform.position = Vector3.zero;
                PlayerCtrl = playerObj.GetComponent<PlayerController>();
                BattleControl.gameObject.SetActive(true);
            });

        }
    }
}

