using UnityEngine;

namespace Ducks.Player
{
    public class Character : MonoBehaviour
    {
        public static float Speed => GameManager.Instance.playerId == 0 ? 1.1f : 1f;

        public static float WeaponSpeed => GameManager.Instance.playerId == 1 ? 1.1f : 1f;
        public static float WeaponRate => GameManager.Instance.playerId == 1 ? 0.9f : 1f;

        public static float Damage => GameManager.Instance.playerId == 2 ? 1.2f : 1f;

        // count....
    }
}