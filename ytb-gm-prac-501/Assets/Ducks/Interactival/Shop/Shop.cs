using Ducks.Player;
using UnityEngine;

namespace Ducks.Interactival.Shop
{
    public class Shop : MonoBehaviour
    {
        public Animator animator;
        public RectTransform ui;
        private PlayerManager _player;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public void Enter(PlayerManager player)
        {
            _player = player;
            ui.anchoredPosition = Vector3.zero;
        }

        // Update is called once per frame
        public void Exit()
        {
            animator.SetTrigger("DoHello");
            ui.anchoredPosition = Vector3.down * 1000;
        }
    }
}