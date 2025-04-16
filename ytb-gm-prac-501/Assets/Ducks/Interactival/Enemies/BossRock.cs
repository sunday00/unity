using System.Collections;
using Ducks.Interactival.Items;
using UnityEngine;

namespace Ducks.Interactival.Enemies
{
    public class BossRock : BulletAction
    {
        private float _angularPower = 2f;
        private bool _isShot;
        private Rigidbody _rb;
        private float _scalarValue = 0.1f;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            StartCoroutine(GainPowerTimer());
            StartCoroutine(GainPower());
        }

        private IEnumerator GainPowerTimer()
        {
            yield return new WaitForSeconds(2.2f);
            _isShot = true;
        }

        private IEnumerator GainPower()
        {
            while (!_isShot)
            {
                _angularPower += 0.2f;
                _scalarValue += 0.005f;

                transform.localScale = Vector3.one * _scalarValue;
                _rb.AddTorque(transform.right * _angularPower, ForceMode.Acceleration);
                yield return null;
            }
        }
    }
}