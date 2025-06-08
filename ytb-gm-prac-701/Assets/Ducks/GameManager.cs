using UnityEngine;

namespace Ducks
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public Player.Player player;

        private void Awake()
        {
            Instance = this;
        }
    }
}