using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject enemyBulletA;
    public GameObject enemyBulletB;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject enemyL;
    public GameObject enemyM;
    public GameObject enemyS;

    public GameObject itemBomb;
    public GameObject itemCoin;
    public GameObject itemLife;
    public GameObject itemPower;

    public GameObject playerBulletA;
    public GameObject playerBulletB;
    public GameObject followerBulletA;


    private GameObject[] enemyBulletAs;
    private GameObject[] enemyBulletBs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject[] enemyLs;
    private GameObject[] enemyMs;
    private GameObject[] enemySs;
    private GameObject[] followerBulletAs;

    private GameObject[] itemBombs;
    private GameObject[] itemCoins;
    private GameObject[] itemLifes;
    private GameObject[] itemPowers;

    private Dictionary<string, (GameObject, GameObject[])> objectLists;

    private GameObject[] playerBulletAs;
    private GameObject[] playerBulletBs;

    private void Awake()
    {
        enemyBulletAs = new GameObject[100];
        enemyBulletBs = new GameObject[100];
        playerBulletAs = new GameObject[100];
        playerBulletBs = new GameObject[100];
        followerBulletAs = new GameObject[100];

        enemyLs = new GameObject[10];
        enemyMs = new GameObject[10];
        enemySs = new GameObject[20];

        itemPowers = new GameObject[20];
        itemCoins = new GameObject[20];
        itemBombs = new GameObject[20];
        itemLifes = new GameObject[20];

        objectLists = new Dictionary<string, (GameObject, GameObject[])>
        {
            { "enemyBulletA", (enemyBulletA, enemyBulletAs) },
            { "enemyBulletB", (enemyBulletB, enemyBulletBs) },
            { "PlayerBulletA", (playerBulletA, playerBulletAs) },
            { "PlayerBulletB", (playerBulletB, playerBulletBs) },
            { "FollowerBulletA", (followerBulletA, followerBulletAs) },
            { "enemyL", (enemyL, enemyLs) },
            { "enemyM", (enemyM, enemyMs) },
            { "enemyS", (enemyS, enemySs) },
            { "itemPower", (itemPower, itemPowers) },
            { "itemCoin", (itemCoin, itemCoins) },
            { "itemBomb", (itemBomb, itemBombs) },
            { "itemLife", (itemLife, itemLifes) }
        };

        Generate();
    }

    private void Generate()
    {
        foreach (var objectTuple in objectLists) GenerateEach(objectTuple.Value.Item1, objectTuple.Value.Item2);
    }

    private void GenerateEach(GameObject prefab, GameObject[] objectParent)
    {
        for (var i = 0; i < objectParent.Length; i++)
        {
            var obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            objectParent[i] = obj;
        }
    }

    public GameObject MakeObject(string objectName)
    {
        var targetList = objectLists[objectName].Item2;

        foreach (var target in targetList)
            if (!target.activeSelf)
            {
                target.SetActive(true);
                return target;
            }

        return null;
    }

    public GameObject[][] GetEnemiesObjects()
    {
        return new[] { enemyLs, enemyMs, enemySs, enemyBulletAs, enemyBulletBs };
    }
}