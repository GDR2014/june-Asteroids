using System.Collections;
using System.Collections.ObjectModel;
using UnityEngine;

[RequireComponent(typeof(Wrapper))]
[RequireComponent(typeof(Rigidbody2D))]
public class AsteroidMovement : MonoBehaviour {

    public float maxRandomRotationSpeed;
    public float rotationSpeed;

    public float maxMoveSpeed = 5;

    private ReadOnlyCollection<Transform> ghosts;

    void Awake() {
        ghosts = GetComponent<Wrapper>().Ghosts;
    }

    void OnEnable() {
        rigidbody2D.velocity = GetRandomVelocity();
        StartRotation();
    }

    void OnDisable() {
        StopCoroutine( rotationRoutine() );
    }

    Vector2 GetRandomVelocity() {
        var dir = Random.insideUnitCircle;
        var moveSpeed = Random.value * maxMoveSpeed * 2 - maxMoveSpeed;
        return dir * moveSpeed;
    }

    void StartRotation() {
        rotationSpeed = Random.value * maxRandomRotationSpeed * 2 - maxRandomRotationSpeed;
        StartCoroutine(rotationRoutine());
    }

    IEnumerator rotationRoutine() {
        while( true ) {
            if( ghosts == null ) yield return null;
            foreach( var ghost in ghosts ) {
                ghost.Rotate( new Vector3(0,0,rotationSpeed) );
            }
            yield return null;
        }
    }
}
