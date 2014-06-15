using UnityEngine;

public class PrefabManager : MonoBehaviour {

    public Transform SingleAsteroid;
    public Asteroid Asteroid;
    public Bullet Bullet;

    private static PrefabManager _instance;
    public static PrefabManager Instance {
        get {
            if( _instance == null ) _instance = FindObjectOfType<PrefabManager>();
            return _instance;
        }
    }

    void Awake() {
        SingleAsteroid.CreatePool();
        Asteroid.CreatePool();
        Bullet.CreatePool();
    }
}
