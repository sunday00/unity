namespace Ducks.Interactival.Enemies
{
    public interface IEnemyMove
    {
        public bool GetIsTest();

        public void SetChase(bool isChase);

        public void SetNav(bool active);
    }
}