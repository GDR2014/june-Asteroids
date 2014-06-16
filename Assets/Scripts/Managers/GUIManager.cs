using UnityEngine;

public class GUIManager : MonoBehaviour {

    public GUIStyle gameOverTextStyle;
    public GUIStyle remainingLivesTextStyle;
    public GUIStyle scoreTextStyle;
    public GUIStyle highscoreTextStyle;
    public GUIStyle newHighscoreTextStyle;

    public Texture livesRemainingIcon;

    private GUIManager _instance;
    public GUIManager Instance {
        get {
            if( _instance == null ) _instance = FindObjectOfType<GUIManager>();
            return _instance;
        }
    }

    private GameManager gameManager;
    private Player player;

    // Easy references to middle of screen
    private static int w { get { return Screen.width / 2; } }
    private static int h { get { return Screen.height / 2; } }

    void Start() {
        gameManager = GameManager.Instance;
        player = FindObjectOfType<Player>();
    }

    void OnGUI() {
        DrawGameOverScreen();
        DrawLivesRemaining();
        DrawScore();
    }

    private void DrawScore() {
        if( gameManager.isGameOver ) return;
        GUI.Label( new Rect( w, 20, 0, 0 ), "Score: " + gameManager.score, scoreTextStyle );
        if( gameManager.score > gameManager.oldHighscore ) highscoreTextStyle.normal.textColor = Color.yellow;
        GUI.Label( new Rect( w * 2 - 20, 20, 0, 0 ), "Highscore: " + gameManager.highscore, highscoreTextStyle );
    }

    private void DrawLivesRemaining() {
        if( gameManager.isGameOver ) return;
        GUI.DrawTexture( new Rect( 20, 20, 40, 40 ), livesRemainingIcon, ScaleMode.ScaleToFit );
        GUI.Label( new Rect( 70, 20, 0, 0 ), "x " + player.remainingLives, remainingLivesTextStyle );
    }

    private void DrawGameOverScreen() {
        if( !gameManager.isGameOver ) return;
        GUI.Label( new Rect( w, h - 175, 0, 0 ), "Game over!", gameOverTextStyle );
        // If new highscore
        if( gameManager.score > gameManager.oldHighscore ) {
            GUI.Label( new Rect(w, h - 60, 0, 0), "New highscore! :D", newHighscoreTextStyle);
        }
        GUI.Label(new Rect(w, h-40, 0, 0), "You scored " + gameManager.score + " points!", scoreTextStyle);
        // Reset button
        if( GUI.Button( new Rect( w - 40, h + 25, 80, 30 ), "Restart" ) ) {
            Application.LoadLevel( 0 );
        }
    }
}
