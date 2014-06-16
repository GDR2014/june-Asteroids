using UnityEngine;

public class PrefabManager : MonoBehaviour {

    public Transform Asteroid;
    public Asteroid Wrapsteroid;
    public Bullet Bullet;

    private static PrefabManager _instance;
    public static PrefabManager Instance {
        get {
            if( _instance == null ) _instance = FindObjectOfType<PrefabManager>();
            return _instance;
        }
    }

    void Awake() {
        Asteroid.CreatePool();
        Wrapsteroid.CreatePool();
        Bullet.CreatePool();
    }
}
