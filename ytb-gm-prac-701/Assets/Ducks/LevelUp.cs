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
            rect.localScale = Vector3.one;
            GameManager.Instance.Stop();
        }

        public void Hide()
        {
            rect.localScale = Vector3.zero;
            GameManager.Instance.Resume();
        }

        public void Select(int index)
        {
            items[index].OnClick();
        }
    }
}