using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager _instance;
    public static GameManager Instance {
        get {
            if( _instance == null ) _instance = FindObjectOfType<GameManager>();
            return _instance;
        }
    }

    private int _score = 0;
    public int score {
        get { return _score; }
        set {
            _score = value;
            highscore = Mathf.Max( _score, highscore );
        }
    }
    public int highscore;
    public int oldHighscore;

    public bool isGameOver = false;

    void Awake() {
        // Ignore collisions between player and player bullets.
        Physics2D.IgnoreLayerCollision( 8, 9 );
        oldHighscore = highscore = PlayerPrefs.GetInt( "highscore", 0 );
    }

    public void GameOver() {
        if( score > oldHighscore ) PlayerPrefs.SetInt("highscore", score);
        isGameOver = true;
    }
}
