using UnityEngine;

[RequireComponent(typeof(Wrapper))]
public class Asteroid : MonoBehaviour {

    public int baseScore = 2;
    public int baseSplits = 2;
    public int baseHealth = 1;
    public float baseMass = 1f;
    public Vector2 baseScale = Vector2.one / 3;

    public float splitNudgePower = 1;
    
    private int _health;
    
    public int ScoreAmount { get; private set; }

    private int _remainingSplits;
    public int RemainingSplits {
        get { return _remainingSplits; }
        set {
            _remainingSplits = value;
            updateScore();
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

    private void updateScore() {
        ScoreAmount = (int) Mathf.Pow( baseScore, RemainingSplits );
    }

    void updateHealth() {
        _health = RemainingSplits * baseHealth + 1;
    }

    void updateMass() {
        rb.mass = baseMass * (RemainingSplits + 1);
    }

    void updateScale() {
        scale = baseScale * (RemainingSplits + 1);
    }

    void Hurt() {
        _health -= 1;
    }

    void Split() {
        var pos = transform.position;
        var fragments = new[] { PrefabManager.Instance.Wrapsteroid.Spawn(pos), PrefabManager.Instance.Wrapsteroid.Spawn(pos) };
        foreach( var fragment in fragments ) {
            fragment.scale = scale / fragments.Length;
            fragment.RemainingSplits = RemainingSplits - 1;
        }
        Vector2 fragPos = fragments[0].transform.position;
        fragPos += Random.insideUnitCircle * splitNudgePower * .001f;
        fragments[0].transform.position = fragPos;
    }

    void Die() {
        GameManager.Instance.score += ScoreAmount;
        if( RemainingSplits > 0 ) Split();
        this.Recycle();
        WaveManager.Instance.CheckForAsteroids();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        var other = collision.gameObject;
        if( other.layer != 9 ) return; // Layer 9 is player bullets
        Hurt();
        if( _health <= 0 ) Die();
    }
}
