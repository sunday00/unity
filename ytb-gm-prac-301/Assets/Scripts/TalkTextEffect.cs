using UnityEngine;
using UnityEngine.UI;

public class TalkTextEffect : MonoBehaviour
{
    public int CharPerSecond;
    public GameObject endCursor;
    public AudioSource audioSource;
    public bool isEffecting;

    private int _index;
    private string _targetMsg;

    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    public void SetMessage(string msg)
    {
        if (isEffecting)
        {
            CancelInvoke("Effecting");
            _text.text = _targetMsg;
            EffectEnd();
            return;
        }

        _targetMsg = msg;
        EffectStart();
    }


    public void EffectStart()
    {
        isEffecting = true;
        endCursor.SetActive(false);
        _text.text = "";
        _index = 0;

        Invoke("Effecting", 1.0f / CharPerSecond);
    }

    public void Effecting()
    {
        if (_text.text.Equals(_targetMsg))
        {
            EffectEnd();
            return;
        }

        if (!_targetMsg[_index].Equals(" ") || !_targetMsg[_index].Equals(".")) audioSource.Play();
        _text.text += _targetMsg[_index];
        _index++;

        Invoke("Effecting", 1.0f / CharPerSecond);
    }

    public void EffectEnd()
    {
        isEffecting = false;
        endCursor.SetActive(true);
    }
}