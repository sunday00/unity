using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Invoke("Disable", 2f);
    }

    public void StartExplosion(Vector3 pos, float size)
    {
        _animator.SetTrigger(Constants.EffectExplosionTrigger);
        transform.position = pos;
        transform.localScale = Vector3.one * size;
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}