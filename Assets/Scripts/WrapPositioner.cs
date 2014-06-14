using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class WrapPositioner : MonoBehaviour {

    public Transform group;

    private float width, height;

    public enum Position {
        TopLeft, TopMid, TopRight,
        MidLeft, MidMid, MidRight,
        BotLeft, BotMid, BotRight
    }

    public Position position;

	void Start () {
	    var cam = Camera.main;
	    height = cam.orthographicSize * 2;
	    width = height * cam.aspect;

	    switch( position ) {
	        case Position.TopLeft:
	            transform.name = "TopLeft";
                transform.position = new Vector3(-width, height);
	            break;
            case Position.TopMid:
	            transform.name = "TopMid";
                transform.position = new Vector3(0, height);
	            break;
            case Position.TopRight:
	            transform.name = "TopRight";
                transform.position = new Vector3(width, height);
	            break;
            case Position.MidLeft:
	            transform.name = "MidLeft";
                transform.position = new Vector3(-width, 0);
	            break;
            case Position.MidMid:
	            transform.name = "MidMid";
                transform.position = new Vector3(0, 0);
	            break;
            case Position.MidRight:
	            transform.name = "MidRight";
                transform.position = new Vector3(width, 0);
	            break;
            case Position.BotLeft:
	            transform.name = "BotLeft";
                transform.position = new Vector3(-width, -height);
	            break;
            case Position.BotMid:
	            transform.name = "BotMid";
                transform.position = new Vector3(0, -height);
	            break;
            case Position.BotRight:
	            transform.name = "BotRight";
                transform.position = new Vector3(width, -height);
	            break;
	    }
	}

    void Update() {
        var pos = group.position;
        if( pos.x > width / 2 ) pos.x -= width;
        if( pos.x < -width / 2 ) pos.x += width;
        if( pos.y > height / 2 ) pos.y -= height;
        if( pos.y < -height / 2 ) pos.y += height;
        group.position = pos;
    }
}
