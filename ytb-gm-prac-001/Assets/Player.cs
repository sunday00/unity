public class Player : Actor
{
    public Player(string name) : base(name)
    {
        this.name = name;
    }

    public string Move () {
        return this.name + " Player.Move";
    }
}
