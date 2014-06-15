using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Wrapper : MonoBehaviour {

    public Vector2 initialScale = Vector2.one;

    public Vector2 ghostScale {
        get { return _ghosts[0].localScale; }
        set {
            foreach( var ghost in _ghosts ) {
                ghost.localScale = value;
            }
        }
    }

    public Transform ghostPrefab;

    private List<Transform> _ghosts;
    public ReadOnlyCollection<Transform> Ghosts {
        get { return _ghosts.AsReadOnly(); }
    }

    private float width, height;

    void Awake() {
        _ghosts = new List<Transform>(9);

	    var cam = Camera.main;
	    height = cam.orthographicSize * 2;
	    width = height * cam.aspect;

	    var ghost = ghostPrefab.Spawn();
	    ghost.transform.parent = transform;
	    ghost.transform.name = "TopLeft";
	    ghost.transform.localPosition = new Vector3( -width, height );
        _ghosts.Add( ghost );

        ghost = ghostPrefab.Spawn();
        ghost.transform.parent = transform;
	    ghost.transform.name = "TopMid";
	    ghost.transform.localPosition = new Vector3( 0, height );
        _ghosts.Add(ghost);

        ghost = ghostPrefab.Spawn();
        ghost.transform.parent = transform;
	    ghost.transform.name = "TopRight";
	    ghost.transform.localPosition = new Vector3( width, height );
        _ghosts.Add(ghost);

        ghost = ghostPrefab.Spawn();
        ghost.transform.parent = transform;
	    ghost.transform.name = "MidLeft";
	    ghost.transform.localPosition = new Vector3( -width, 0 );
        _ghosts.Add(ghost);

        ghost = ghostPrefab.Spawn();
        ghost.transform.parent = transform;
	    ghost.transform.name = "MidMid";
	    ghost.transform.localPosition = new Vector3( 0, 0 );
        _ghosts.Add(ghost);

        ghost = ghostPrefab.Spawn();
        ghost.transform.parent = transform;
	    ghost.transform.name = "MidRight";
	    ghost.transform.localPosition = new Vector3( width, 0 );
        _ghosts.Add(ghost);

        ghost = ghostPrefab.Spawn();
        ghost.transform.parent = transform;
	    ghost.transform.name = "BotLeft";
	    ghost.transform.localPosition = new Vector3( -width, -height );
        _ghosts.Add(ghost);

        ghost = ghostPrefab.Spawn();
        ghost.transform.parent = transform;
	    ghost.transform.name = "BotMid";
	    ghost.transform.localPosition = new Vector3( 0, -height );
        _ghosts.Add(ghost);

        ghost = ghostPrefab.Spawn();
        ghost.transform.parent = transform;
        ghost.transform.name = "BotRight";
        ghost.transform.localPosition = new Vector3(width, -height);
        _ghosts.Add(ghost);

        ghostScale = initialScale;
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
