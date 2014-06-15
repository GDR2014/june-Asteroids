using UnityEngine;

public class GameManager : MonoBehaviour {

    void Awake() {
        // Ignore collisions between player and player bullets.
        Physics2D.IgnoreLayerCollision( 8, 9 );
    }

}
