using UnityEngine;

namespace Ducks.Object
{
    public class Bullet : MonoBehaviour
    {
        public float damage;
        public int per;

        public void Init(float damageProp, int perProp)
        {
            damage = damageProp;
            per = perProp;
        }
    }
}