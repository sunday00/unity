using UnityEngine;

namespace Ducks.Player
{
    public class PlayerManager : MonoBehaviour
    {
        public PlayerItem PlayerItem;
        public PlayerMove PlayerMove;

        public void Awake()
        {
            PlayerItem = GetComponent<PlayerItem>();
            PlayerMove = GetComponent<PlayerMove>();
        }
    }
}