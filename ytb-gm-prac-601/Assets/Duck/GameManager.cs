using Duck.Player;
using UnityEngine;

namespace Duck
{
    public class GameManager : MonoBehaviour
    {
        public Dongle lastDongle;


        private void GetDongle()
        {
        }

        private void NextDongle()
        {
        }

        public void TouchDown()
        {
            lastDongle.Drag();
        }

        public void TouchUp()
        {
            lastDongle.Drop();
        }
    }
}