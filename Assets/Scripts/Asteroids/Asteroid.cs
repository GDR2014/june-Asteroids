using UnityEngine;

[RequireComponent(typeof(Wrapper))]
public class Asteroid : MonoBehaviour {

    public int health = 5;
    public int remainingSplits = 2;

    public float baseMass = 10f;
    public float splitNudgePower = 1;

    public Vector2 scale {
        get { return wrapper.ghostScale; }
        set { wrapper.ghostScale = value; }
    }

    private Wrapper wrapper;
    private Rigidbody2D rb;

    void Awake() {
        wrapper = GetComponent<Wrapper>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        rb.mass = baseMass * scale.magnitude * scale.magnitude;
    }

    void Hurt() {
        health -= 1;
    }

    void Split() {
        var pos = transform.position;
        var fragments = new[] { PrefabManager.Instance.Asteroid.Spawn(pos), PrefabManager.Instance.Asteroid.Spawn(pos) };
        foreach( var fragment in fragments ) {
            fragment.scale = scale / fragments.Length;
            fragment.remainingSplits = remainingSplits - 1;
        }
        Vector2 fragPos = fragments[0].transform.position;
        fragPos += Random.insideUnitCircle * splitNudgePower * .001f;
        fragments[0].transform.position = fragPos;
    }

    void Die() {
        if( remainingSplits > 0 ) Split();
        this.Recycle();
        WaveManager.Instance.CheckForAsteroids();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        var other = collision.gameObject;
        if( other.layer != 9 ) return; // Layer 9 is player bullets
        Hurt();
        if( health <= 0 ) Die();
    }
}
