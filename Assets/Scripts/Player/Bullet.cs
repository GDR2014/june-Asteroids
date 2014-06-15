using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour {

    public float bulletSpeed = 10f;
    private Rigidbody2D rb;

    private float width, height;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();

        var cam = Camera.main;
        height = cam.orthographicSize;
        width = height * cam.aspect;
    }

    void OnEnable() {
        rb.velocity = transform.up * bulletSpeed;
    }

    void Update() {
        bool outOfBounds = transform.position.x < -width
                           || transform.position.x > width
                           || transform.position.y < -height
                           || transform.position.y > height;
        if( outOfBounds ) this.Recycle();
    }

    void OnCollisionEnter2D( Collision2D collision ) {
        var other = collision.gameObject;
        if( other.tag == "Player" ) return;
        this.Recycle();
        Debug.Log( "Bullet hit: " + other );
    }

}
