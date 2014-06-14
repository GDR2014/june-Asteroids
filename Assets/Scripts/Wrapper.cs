using UnityEngine;

public class Wrapper : MonoBehaviour {

    public Transform prefab;

    private float width, height;

    void Awake() {
        prefab.CreatePool();
    }

	void Start () {
	    var cam = Camera.main;
	    height = cam.orthographicSize * 2;
	    width = height * cam.aspect;

	    var part = prefab.Spawn();
	    part.transform.parent = transform;
	    part.transform.name = "TopLeft";
	    part.transform.position = new Vector3( -width, height );

        part = prefab.Spawn();
        part.transform.parent = transform;
	    part.transform.name = "TopMid";
	    part.transform.position = new Vector3( 0, height );

        part = prefab.Spawn();
        part.transform.parent = transform;
	    part.transform.name = "TopRight";
	    part.transform.position = new Vector3( width, height );

        part = prefab.Spawn();
        part.transform.parent = transform;
	    part.transform.name = "MidLeft";
	    part.transform.position = new Vector3( -width, 0 );

        part = prefab.Spawn();
        part.transform.parent = transform;
	    part.transform.name = "MidMid";
	    part.transform.position = new Vector3( 0, 0 );

        part = prefab.Spawn();
        part.transform.parent = transform;
	    part.transform.name = "MidRight";
	    part.transform.position = new Vector3( width, 0 );

        part = prefab.Spawn();
        part.transform.parent = transform;
	    part.transform.name = "BotLeft";
	    part.transform.position = new Vector3( -width, -height );

        part = prefab.Spawn();
        part.transform.parent = transform;
	    part.transform.name = "BotMid";
	    part.transform.position = new Vector3( 0, -height );

        part = prefab.Spawn();
        part.transform.parent = transform;
        part.transform.name = "BotRight";
        part.transform.position = new Vector3(width, -height);

    }

    void Update() {
        var pos = transform.position;
        if( pos.x > width / 2 ) pos.x -= width;
        if( pos.x < -width / 2 ) pos.x += width;
        if( pos.y > height / 2 ) pos.y -= height;
        if( pos.y < -height / 2 ) pos.y += height;
        transform.position = pos;
    }
}
