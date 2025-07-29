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
                case InfoType.Level:
                    _myText.text = string.Format("Level.{0:F0}", GameManager.Instance.level);
                    break;
                case InfoType.Kill:
                    _myText.text = string.Format("{0:F0}", GameManager.Instance.kill);
                    break;
                case InfoType.Time:
                    var remainTime = GameManager.Instance.maxGameTime - GameManager.Instance.gameTime;
                    var hours = Mathf.FloorToInt(remainTime / 3600);
                    var minutes = Mathf.FloorToInt(remainTime / 60);
                    var seconds = Mathf.FloorToInt(remainTime % 60);
                    _myText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
                    break;
                case InfoType.Health:
                    float cueHealth = GameManager.Instance.health;
                    float maxHealth = GameManager.Instance.maxHealth;
                    _mySlider.value = cueHealth / maxHealth;
                    break;
            }
        }
    }
}