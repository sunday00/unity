using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Ducks.Player
{
    public class PlayerItem : MonoBehaviour
    {
        private Dictionary<string, int> _inventory;
        private GameObject _nearObject;
        private PlayerManager _playerManager;

        private void Awake()
        {
            _playerManager = gameObject.GetComponent<PlayerManager>();
            _inventory = new Dictionary<string, int>();
        }

        private void Update()
        {
            Interact(Input.GetButtonDown("Interact"));

            // foreach (var item in _inventory) print(item.Key + ":" + item.Value);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Weapon")) _nearObject = null;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Weapon")) _nearObject = other.gameObject;
        }


        private void Interact(bool interact)
        {
            if (_playerManager.PlayerMove.IsActing) return;

            if (interact && !_nearObject.IsUnityNull())
            {
                if (_nearObject.CompareTag("Weapon"))
                {
                    if (_inventory.ContainsKey(_nearObject.name) && _inventory[_nearObject.name] > 0)
                        _inventory[_nearObject.name] += 1;
                    else
                        _inventory.Add(_nearObject.name, 1);
                }

                Destroy(_nearObject);
            }
        }
    }
}