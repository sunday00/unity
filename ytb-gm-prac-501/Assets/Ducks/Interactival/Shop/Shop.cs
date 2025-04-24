using System.Collections;
using Ducks.Interactival.Items;
using Ducks.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Ducks.Interactival.Shop
{
    public class Shop : MonoBehaviour
    {
        public Animator animator;
        public RectTransform ui;

        public Transform spawnPoint;
        public GameObject[] itemObjects;
        public Text moneyText;
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

        public void Buy(int idx)
        {
            var item = itemObjects[idx].GetComponent<ItemReact>();

            if (_player.PlayerState.curCoin < item.price)
            {
                StopCoroutine(NotEnoughMoney());
                StartCoroutine(NotEnoughMoney());
                return;
            }

            _player.PlayerState.curCoin -= item.price;

            Instantiate(item,
                spawnPoint.position
                + (
                    Vector3.right * Random.Range(-1, 1)
                    + Vector3.forward * Random.Range(-1, 1)
                ), spawnPoint.rotation);
        }

        private IEnumerator NotEnoughMoney()
        {
            moneyText.color = Color.red;

            yield return new WaitForSeconds(0.5f);

            moneyText.color = Color.white;

            yield return null;
        }
    }
}