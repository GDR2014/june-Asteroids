using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Wrapper))]
public class Player : MonoBehaviour {
    
    public float thrusterForce = 300;
    public float turnSpeed = 7.5f;

    private Rigidbody2D rb;
    private Wrapper wrapper;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        wrapper = GetComponent<Wrapper>();
    }

    void Update () {
	    var turn = Input.GetAxis( "Horizontal" ) * turnSpeed * -1;
        foreach( var ghost in wrapper.Ghosts ) {
            ghost.transform.Rotate( new Vector3(0,0,turn) );
        }
	}

    void FixedUpdate() {
        var throttle = Input.GetAxis("Vertical") * thrusterForce;
        rb.AddRelativeForce(wrapper.Ghosts[0].transform.up * throttle);
    }
}
