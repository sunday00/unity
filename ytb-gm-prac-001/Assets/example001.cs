using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewMonoBehaviourScript : MonoBehaviour
{
    int health = 30;

    void Start()
    {
        Debug.Log("Hello unity!");

        int lv = 4;
        float str = 11.1f;
        string name = "Solder";
        bool isFullLv = false;

        Debug.Log((lv, str, name, isFullLv));

        // array
        string[] monsters = {"a", "b", "c"};
        Debug.Log(monsters[0]);

        int[] levels = new int[3];
        levels[0] = 1;
        levels[1] = 3;
        levels[2] = 11;
        Debug.Log(levels[0]);

        // list
        List<string> items = new List<string>();
        items.Add("life potion");
        items.Add("mana potion");

        for(int i = 0; i < items.Count; i++) {
            Debug.Log(items[i]);
        }

        foreach(string item in items) {
            Debug.Log(item);
        }

        items.RemoveAt(1);

        int exp = 1500;
        exp += 100;

        Debug.Log(exp);

        string first = "Walter";
        string last = "White";
        Debug.Log(first + " " + last);

        Debug.Log(this.Heal(10));

        this.HealSelf(10);
        Debug.Log(this.health);

        Actor actor = new Actor("Bob Odenkirk");
        Debug.Log(actor.SayName());

        Player p = new Player("Jessi");
        Debug.Log(p.Move());
    }

    int Heal(int health) {
        return health + 10;
    }

    void HealSelf(int health) {
        this.health += health;
    }
}
