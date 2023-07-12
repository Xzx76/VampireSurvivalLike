using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireSLike
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed;
        public Animator anim;
        public float pickupRange = 1.5f;
        public Transform WeaponHolder;
        //public Weapon activeWeapon;
        public List<Weapon> unassignedWeapons, assignedWeapons;
        public int maxWeapons = 3;
        [HideInInspector]
        public List<Weapon> fullyLevelledWeapons = new List<Weapon>();

        // Start is called before the first frame update
        void Start()
        {
            if (assignedWeapons.Count == 0)
            {
                AddWeapon(Random.Range(0, unassignedWeapons.Count));
            }

            moveSpeed = PlayerStatController.instance.moveSpeed[0].value;
            pickupRange = PlayerStatController.instance.pickupRange[0].value;
            maxWeapons = Mathf.RoundToInt(PlayerStatController.instance.maxWeapons[0].value);
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 moveInput = new Vector3(0f, 0f, 0f);
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");

            //Debug.Log(moveInput);

            moveInput.Normalize();

            //Debug.Log(moveInput);

            transform.position += moveInput * moveSpeed * Time.deltaTime;

            if (moveInput != Vector3.zero)
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
        }

        public void AddWeapon(int weaponNumber)
        {
            if (weaponNumber < unassignedWeapons.Count)
            {
                AssetManager.Instance.InstantiateAsync(unassignedWeapons[weaponNumber].WeaponName, obj =>
                {
                    obj.transform.SetParent(WeaponHolder);
                    obj.transform.position = Vector3.zero;
                    assignedWeapons.Add(obj.GetComponent<Weapon>());
                    obj.SetActive(true);
                    unassignedWeapons.RemoveAt(weaponNumber);
                });
            }
        }

        public void AddWeapon(Weapon weaponToAdd)
        {
            AssetManager.Instance.InstantiateAsync(weaponToAdd.WeaponName, obj =>
            {
                obj.transform.SetParent(WeaponHolder);
                obj.transform.position = Vector3.zero;
                assignedWeapons.Add(obj.GetComponent<Weapon>());
                obj.SetActive(true);
                unassignedWeapons.Remove(weaponToAdd);
            });
        }
    }

}
