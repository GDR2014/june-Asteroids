using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {

    private Rigidbody2D rb;
    public float thrusterForce = 300;
    public float turnSpeed = 7.5f;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

	void Start () {
	
	}
	
	void Update () {
	    var turn = Input.GetAxis( "Horizontal" ) * turnSpeed * -1;
        transform.Rotate( new Vector3(0,0,turn) );
	}
}
