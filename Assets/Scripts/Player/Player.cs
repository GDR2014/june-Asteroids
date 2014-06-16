using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Wrapper))]
public class Player : MonoBehaviour {

    public int remainingLives = 2;

    public float thrusterForce = 300;
    public float turnSpeed = 7.5f;

    public float respawnDelay = 1f;
    public float respawnInvincibilityTime = 1.5f;

    public AudioClip deathSound;
    private AudioSource thrusterSource;
    private float originalThrusterPitch;

    private Rigidbody2D rb;
    private Wrapper wrapper;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        wrapper = GetComponent<Wrapper>();
        thrusterSource = GetComponent<AudioSource>();
        originalThrusterPitch = thrusterSource.pitch;
    }

    void Update () {
	    var turn = Input.GetAxis( "Horizontal" ) * turnSpeed * -1;
        foreach( var ghost in wrapper.Ghosts ) {
            ghost.transform.Rotate( new Vector3(0,0,turn) );
        }
	}

    void FixedUpdate() {
        var throttle = Input.GetAxis("Vertical") * thrusterForce;
        if( throttle < 0 ) throttle = 0; // Can't move backwards
        rb.AddRelativeForce(wrapper.Ghosts[0].transform.up * throttle);
        thrusterSource.enabled = throttle > 0;
        thrusterSource.pitch = originalThrusterPitch + throttle * .1f;
    }

    void OnCollisionEnter2D( Collision2D collision ) {
        Die();
        remainingLives--;
        if( remainingLives < 0 ) {
            GameManager.Instance.GameOver();
            return;
        }
        GameManager.Instance.StartCoroutine(Respawn()); // Hack for running coroutine of a deactivated gameObject
    }

    private void Die() {
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        gameObject.SetActive( false ); // Boom!
    }

    IEnumerator Respawn() {
        yield return new WaitForSeconds( respawnDelay );
        this.transform.position = Vector2.zero;
        gameObject.SetActive(true);
        StartCoroutine( Invincibility( respawnInvincibilityTime ) );
    }

    public IEnumerator Invincibility(float duration) {
        var ghosts = wrapper.Ghosts;
        foreach( var ghost in ghosts ) {
            var sprite = ghost.FindChild( "Sprite" );
            sprite.collider2D.enabled = false;
            var color = sprite.renderer.material.color;
            color.a = .4f;
            sprite.renderer.material.color = color;
        }
        yield return new WaitForSeconds( duration );
        foreach (var ghost in ghosts) {
            var sprite = ghost.FindChild("Sprite");
            sprite.collider2D.enabled = true;
            var color = sprite.renderer.material.color;
            color.a = 1f;
            sprite.renderer.material.color = color;
        }
    }
}
