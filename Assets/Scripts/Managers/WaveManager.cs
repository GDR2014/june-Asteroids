using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour {

    public int wave = 0;
    public float interWaveDelay = 1f;

    private static WaveManager _instance;
    public static WaveManager Instance {
        get {
            if( _instance == null ) _instance = FindObjectOfType<WaveManager>();
            return _instance;
        }
    }

    void Start() {
        StartCoroutine( LoadNextWave() );
    }

    public void CheckForAsteroids() {
        bool asteroidsLeft = FindObjectOfType<Asteroid>() != null;
        Debug.Log( "Asteroids left: " + asteroidsLeft );
        if( asteroidsLeft ) return;
        StartCoroutine( LoadNextWave() );
    }

    IEnumerator LoadNextWave() {
        wave++;
        yield return new WaitForSeconds( interWaveDelay );
        SpawnAsteroids();
    }

    void SpawnAsteroids() {
        foreach( var spawnPoint in GetAsteroidSpawnPoints() ) {
            var asteroid = PrefabManager.Instance.Wrapsteroid.Spawn( spawnPoint );
            asteroid.RemainingSplits = asteroid.baseSplits;
        }
    }

    IEnumerable<Vector2> GetAsteroidSpawnPoints() {
        Vector2[] points = new Vector2[wave];
        var cam = Camera.main;
        var height = cam.orthographicSize;
        var width = cam.aspect * height;
        var player = FindObjectOfType<Player>();
        var pos = player.transform.position;
        var x = width / 4 * 3;
        var y = height / 4 * 3;
        var xSign = pos.x < 0 ? 1 : -1;
        var ySign = pos.y < 0 ? 1 : -1;
        for( int i = 0; i < points.Length; i++ ) {
            points[i] = new Vector2(Random.value * x * xSign, Random.value * y * ySign);
        }
        return points;
    }

}
