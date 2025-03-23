using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemySpwanPoints;
    private float _curEnemySpawnDelay;

    private Manager _manager;
    private float _nextEnemySpawnDelay;
    private Player _player;
    private int _spawnIndex;

    private List<Spawn> _spawns;

    private void Awake()
    {
        _manager = GetComponent<Manager>();
        _player = _manager.player;
        _spawns = new List<Spawn>();

        ReadSpawnFile();
    }

    private void Update()
    {
        _curEnemySpawnDelay += Time.deltaTime;
        if (_curEnemySpawnDelay >= _nextEnemySpawnDelay && _spawns.Count > _spawnIndex) SpawnEnemy();
    }

    private void ReadSpawnFile()
    {
        _spawns.Clear();
        var textAsset = Resources.Load<TextAsset>("Stage0");
        var reader = new StringReader(textAsset.text);

        while (!reader.IsUnityNull())
        {
            var line = reader.ReadLine();

            if (line == null) break;

            var properties = line.Split(',');
            var spawn = new Spawn();
            spawn.delay = float.Parse(properties[0]);
            spawn.type = properties[1];
            spawn.point = int.Parse(properties[2]);

            _spawns.Add(spawn);
        }

        reader.Close();
        _spawnIndex = 0;
        _nextEnemySpawnDelay = _spawns[_spawnIndex].delay;
    }

    private void SpawnEnemy()
    {
        _curEnemySpawnDelay = 0;
        var spawn = _spawns[_spawnIndex];
        var enemyPoint = enemySpwanPoints[spawn.point];
        var enemyObj = _manager.objectManager.MakeObject("enemy" + spawn.type);

        if (enemyObj.IsUnityNull()) return;
        enemyObj.transform.position = enemyPoint.transform.position;
        enemyObj.transform.rotation = enemyPoint.transform.rotation;

        enemyObj.GetComponent<Enemy>().manager = _manager;
        enemyObj.GetComponent<Enemy>().SetVelocity(enemyPoint.name);
        enemyObj.GetComponent<Enemy>().GetComponent<EnemyFiring>().player = _player;
        enemyObj.GetComponent<Enemy>().GetComponent<EnemyFiring>().objectManager = _manager.objectManager;

        _spawnIndex++;
        if (_spawns.Count > _spawnIndex) _nextEnemySpawnDelay = _spawns[_spawnIndex].delay;
    }
}