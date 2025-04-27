using System.Collections;

namespace Ducks.Player
{
    public interface IPlayerWeapon
    {
        public void StartRoutine();

        public Constants.WeaponType GeWeaponType();

        public IEnumerator Action();

        public int GetCurrentAmmo();
    }
}