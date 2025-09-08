using Ducks.Object;
using UnityEngine;

namespace Ducks
{
    public class LevelUp : MonoBehaviour
    {
        private Item[] items;
        private RectTransform rect;

        private void Awake()
        {
            rect = GetComponent<RectTransform>();
            items = GetComponentsInChildren<Item>(true);
        }

        public void Show()
        {
            Next();
            rect.localScale = Vector3.one;
            GameManager.Instance.Stop();
            AudioManager.Instance.PlaySfx(AudioManager.Sfx.LevelUp);
        }

        public void Hide()
        {
            rect.localScale = Vector3.zero;
            GameManager.Instance.Resume();
            AudioManager.Instance.PlaySfx(AudioManager.Sfx.Select);
        }

        public void Select(int index)
        {
            items[index].OnClick();
        }

        private void Next()
        {
            foreach (var item in items) item.gameObject.SetActive(false);

            var rand = new int[3];
            while (true)
            {
                rand[0] = Random.Range(0, items.Length);
                rand[1] = Random.Range(0, items.Length);
                rand[2] = Random.Range(0, items.Length);

                if (rand[0] != rand[1] && rand[1] != rand[2] && rand[0] != rand[2]) break;
            }

            for (var i = 0; i < rand.Length; i++)
            {
                var item = items[rand[i]];

                if (item.level == item.data.damages.Length) items[4].gameObject.SetActive(true);
                else item.gameObject.SetActive(true);
            }
        }
    }
}