using System.Collections;
using UnityEngine;

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
        
    }

}
