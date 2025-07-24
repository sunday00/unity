using UnityEngine;
using UnityEngine.UI;

namespace Ducks
{
    public class Hud : MonoBehaviour
    {
        public enum InfoType
        {
            Exp,
            Level,
            Kill,
            Time,
            Health
        }

        public InfoType type;
        private Slider _mySlider;

        private Text _myText;

        private void Awake()
        {
            _myText = GetComponent<Text>();
            _mySlider = GetComponent<Slider>();
        }

        private void LateUpdate()
        {
            switch (type)
            {
                case InfoType.Exp:
                    float cueExp = GameManager.Instance.exp;
                    float maxExp = GameManager.Instance.nextExp[GameManager.Instance.level];
                    _mySlider.value = cueExp / maxExp;

                    break;
                case InfoType.Level: break;
                case InfoType.Kill: break;
                case InfoType.Time: break;
                case InfoType.Health: break;
            }
        }
    }
}