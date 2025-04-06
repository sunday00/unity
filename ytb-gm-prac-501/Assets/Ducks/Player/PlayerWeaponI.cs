using System.Collections;

namespace Ducks.Player
{
    public interface IPlayerWeapon
    {
        public void StartRoutine();

        public IEnumerator Action();
    }
}