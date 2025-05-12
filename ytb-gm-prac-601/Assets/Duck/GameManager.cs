using System.Collections;
using Duck.Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Duck
{
    public class GameManager : MonoBehaviour
    {
        public Dongle lastDongle;
        public GameObject donglePrefab;
        public Transform dongleGroup;

        private void Start()
        {
            NextDongle();
        }

        private Dongle GetDongle()
        {
            return Instantiate(donglePrefab, dongleGroup).GetComponent<Dongle>();
        }

        private void NextDongle()
        {
            lastDongle = GetDongle();
        }

        private IEnumerator WaitNext()
        {
            yield return null;
        }

        public void TouchDown()
        {
            if (lastDongle.IsUnityNull()) return;

            lastDongle.Drag();
        }

        public void TouchUp()
        {
            if (lastDongle.IsUnityNull()) return;

            lastDongle.Drop();
            lastDongle = null;
        }
    }
}