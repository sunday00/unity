using UnityEngine;

namespace Ducks.Player
{
    public class PlayerManager : MonoBehaviour
    {
        public PlayerItem PlayerItem;
        public PlayerMove PlayerMove;
        public PlayerState PlayerState;
        public PlayerWeapon PlayerWeapon;

        public void Awake()
        {
            PlayerItem = GetComponent<PlayerItem>();
            PlayerMove = GetComponent<PlayerMove>();
            PlayerState = GetComponent<PlayerState>();
            PlayerWeapon = GetComponent<PlayerWeapon>();
        }
    }
}