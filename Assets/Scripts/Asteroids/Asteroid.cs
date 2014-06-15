using UnityEngine;

[RequireComponent(typeof(Wrapper))]
public class Asteroid : MonoBehaviour {

    public Asteroid vanillaPrefab;

    public int health = 5;
    public int remainingSplits = 2;

    public Vector2 scale {
        get { return wrapper.ghostScale; }
        set { wrapper.ghostScale = value; }
    }

    private Wrapper wrapper;

    void Awake() {
        wrapper = GetComponent<Wrapper>();
    }

    void Start() {
        
    }

    void Hurt() {
        health -= 1;
    }

    void Split() {
        var pos = transform.position;
        var fragments = new[] {vanillaPrefab.Spawn( pos ), vanillaPrefab.Spawn( pos )};
        foreach( var fragment in fragments ) {
            fragment.scale = scale / fragments.Length;
            fragment.remainingSplits = remainingSplits - 1;
        }
    }

    void Die() {
        if( remainingSplits > 0 ) Split();
        this.Recycle();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        var other = collision.gameObject;
        Debug.Log("Asteroid collided with " + other + ". Layer = " + other.layer  );
        // If not a collision with player bullets, return
        if( other.layer != 9 ) return;
        // Else
        Hurt();
        if( health <= 0 ) Die();
    }
}
