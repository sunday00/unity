using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Ducks.Player
{
    public class PlayerItem : MonoBehaviour
    {
        public List<GameObject> equippables;
        public GameObject equipped;
        private Animator _animator;
        private GameObject _nearObject;
        private PlayerManager _playerManager;

        public Dictionary<string, int> Inventory { get; private set; }

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _playerManager = gameObject.GetComponent<PlayerManager>();
            Inventory = new Dictionary<string, int>();
        }

        private void Update()
        {
            Interact(Input.GetButtonDown("Interact"));

            var equip = GetEquipInput();

            Equip(equip);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Weapon")) _nearObject = null;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Weapon")) _nearObject = other.gameObject;
        }

        private void Equip(int idx)
        {
            if (idx.Equals(0) || idx > equippables.Count - 1) return;

            var candidate = equippables[idx];
            if (!equipped.IsUnityNull() && equipped.name.Equals(candidate.name)) return;

            if (!Inventory.ContainsKey(equippables[idx].name) || Inventory[equippables[idx].name] <= 0) return;

            foreach (var equip in equippables) equip.SetActive(false);

            _playerManager.PlayerMove.BlinkEquipState();

            equipped = candidate;
            _animator.SetTrigger(Constants.DoEquip);
            equippables[idx].SetActive(true);
        }

        private int GetEquipInput()
        {
            for (var i = 0; i <= 9; i++)
                if (Input.GetButtonDown("Equip" + i))
                    return i;

            return 0;
        }


        private void Interact(bool interact)
        {
            if (_playerManager.PlayerMove.IsActing) return;

            if (interact && !_nearObject.IsUnityNull())
            {
                if (_nearObject.CompareTag("Weapon"))
                {
                    if (Inventory.ContainsKey(_nearObject.name) && Inventory[_nearObject.name] > 0)
                        Inventory[_nearObject.name] += 1;
                    else
                        Inventory.Add(_nearObject.name, 1);
                }

                Destroy(_nearObject);
            }
        }
    }
}