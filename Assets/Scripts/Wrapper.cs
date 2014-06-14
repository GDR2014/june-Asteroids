using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Wrapper : MonoBehaviour {

    public Transform prefab;

    private List<Transform> _ghosts;
    public ReadOnlyCollection<Transform> Ghosts {
        get { return _ghosts.AsReadOnly(); }
    }

    private float width, height;

    void Awake() {
        prefab.CreatePool();
        _ghosts = new List<Transform>(9);
    }

	void Start () {
	    var cam = Camera.main;
	    height = cam.orthographicSize * 2;
	    width = height * cam.aspect;

	    var ghost = prefab.Spawn();
	    ghost.transform.parent = transform;
	    ghost.transform.name = "TopLeft";
	    ghost.transform.position = new Vector3( -width, height );
        _ghosts.Add( ghost );

        ghost = prefab.Spawn();
        ghost.transform.parent = transform;
	    ghost.transform.name = "TopMid";
	    ghost.transform.position = new Vector3( 0, height );
        _ghosts.Add(ghost);

        ghost = prefab.Spawn();
        ghost.transform.parent = transform;
	    ghost.transform.name = "TopRight";
	    ghost.transform.position = new Vector3( width, height );
        _ghosts.Add(ghost);

        ghost = prefab.Spawn();
        ghost.transform.parent = transform;
	    ghost.transform.name = "MidLeft";
	    ghost.transform.position = new Vector3( -width, 0 );
        _ghosts.Add(ghost);

        ghost = prefab.Spawn();
        ghost.transform.parent = transform;
	    ghost.transform.name = "MidMid";
	    ghost.transform.position = new Vector3( 0, 0 );
        _ghosts.Add(ghost);

        ghost = prefab.Spawn();
        ghost.transform.parent = transform;
	    ghost.transform.name = "MidRight";
	    ghost.transform.position = new Vector3( width, 0 );
        _ghosts.Add(ghost);

        ghost = prefab.Spawn();
        ghost.transform.parent = transform;
	    ghost.transform.name = "BotLeft";
	    ghost.transform.position = new Vector3( -width, -height );
        _ghosts.Add(ghost);

        ghost = prefab.Spawn();
        ghost.transform.parent = transform;
	    ghost.transform.name = "BotMid";
	    ghost.transform.position = new Vector3( 0, -height );
        _ghosts.Add(ghost);

        ghost = prefab.Spawn();
        ghost.transform.parent = transform;
        ghost.transform.name = "BotRight";
        ghost.transform.position = new Vector3(width, -height);
        _ghosts.Add(ghost);

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
