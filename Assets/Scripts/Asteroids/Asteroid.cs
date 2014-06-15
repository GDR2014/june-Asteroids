using UnityEngine;

[RequireComponent(typeof(Wrapper))]
public class Asteroid : MonoBehaviour {

    public int baseSplits = 2;
    public int baseHealth = 1;
    public float baseMass = 1f;
    public Vector2 baseScale = Vector2.one / 3;

    public int health;
    public int _remainingSplits;
    public float splitNudgePower = 1;

    public int RemainingSplits {
        get { return _remainingSplits; }
        set {
            _remainingSplits = value;
            updateHealth();
            updateMass();
            updateScale();
        }
    }

    public Vector2 scale {
        get { return wrapper.ghostScale; }
        set { wrapper.ghostScale = value; }
    }

    private Wrapper wrapper;
    private Rigidbody2D rb;

    void Awake() {
        wrapper = GetComponent<Wrapper>();
        rb = GetComponent<Rigidbody2D>();
        RemainingSplits = baseSplits;
    }

    void updateHealth() {
        health = RemainingSplits * baseHealth + 1;
    }

    void updateMass() {
        rb.mass = baseMass * RemainingSplits;
    }

    void updateScale() {
        scale = baseScale * RemainingSplits;
    }

    void Hurt() {
        health -= 1;
    }

    void Split() {
        var pos = transform.position;
        var fragments = new[] { PrefabManager.Instance.Wrapsteroid.Spawn(pos), PrefabManager.Instance.Wrapsteroid.Spawn(pos) };
        foreach( var fragment in fragments ) {
            fragment.scale = scale / fragments.Length;
            fragment._remainingSplits = _remainingSplits - 1;
        }
        Vector2 fragPos = fragments[0].transform.position;
        fragPos += Random.insideUnitCircle * splitNudgePower * .001f;
        fragments[0].transform.position = fragPos;
    }

    void Die() {
        if( _remainingSplits > 0 ) Split();
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
