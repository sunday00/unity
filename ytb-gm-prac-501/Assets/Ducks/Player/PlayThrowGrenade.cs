using UnityEngine;

namespace Ducks.Player
{
    public class PlayThrowGrenade : MonoBehaviour
    {
        public PlayerManager playerManager;
        public GameObject grenade;

        private void Update()
        {
            var throws = Input.GetButtonDown("Fire2");
            Throw(throws);
        }

        private void Throw(bool isFire)
        {
            if (!isFire) return;

            if (playerManager.PlayerState.curGrenades <= 0) return;
            playerManager.PlayerState.curGrenades--;

            for (var i = playerManager.PlayerState.maxGrenades - 1; i >= 0; i--)
                if (playerManager.PlayerState.equippedGrenades[i].activeSelf)
                {
                    playerManager.PlayerState.equippedGrenades[i].SetActive(false);
                    break;
                }

            var g = Instantiate(grenade, playerManager.transform.position, Quaternion.identity);
            var rb = g.GetComponent<Rigidbody>();

            var vec = playerManager.transform.forward * 30f;
            vec.y = 20;

            // rb.linearVelocity = vec;
            rb.AddForce(vec, ForceMode.Impulse);
            // rb.AddTorque(Vector3.back * 1, ForceMode.Impulse);
        }
    }
}