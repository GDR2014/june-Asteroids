using UnityEngine;

public class GUIManager : MonoBehaviour {

    public GUIStyle gameOverTextStyle;
    public GUIStyle livesRemainingTextStyle;

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
        if( gameManager.isGameOver ) return;
        GUI.DrawTexture( new Rect(20, 20, 40, 40), livesRemainingIcon, ScaleMode.ScaleToFit  );
        GUI.Label(new Rect(65, 20, 0, 0), "x " + player.remainingLives, livesRemainingTextStyle );
    }

    private void DrawGameOverScreen() {
        if( !gameManager.isGameOver ) return;
        GUI.Label( new Rect( w, h - 100, 0, 0 ), "Game over!", gameOverTextStyle );
        if( GUI.Button( new Rect( w - 40, h + 25, 80, 30 ), "Restart" ) ) {
            Application.LoadLevel( 0 );
        }
    }
}
