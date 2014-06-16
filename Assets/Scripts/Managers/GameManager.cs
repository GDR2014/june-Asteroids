using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager _instance;
    public static GameManager Instance {
        get {
            if( _instance == null ) _instance = FindObjectOfType<GameManager>();
            return _instance;
        }
    }

    void Awake() {
        // Ignore collisions between player and player bullets.
        Physics2D.IgnoreLayerCollision( 8, 9 );
    }

    public void GameOver() {
        Debug.Log("Game over! :(");
    }
}
