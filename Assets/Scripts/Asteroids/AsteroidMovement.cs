using System.Collections;
using System.Collections.ObjectModel;
using UnityEngine;

[RequireComponent(typeof(Wrapper))]
public class AsteroidMovement : MonoBehaviour {

    public float maxRandomRotationSpeed;
    public float rotationSpeed;
    private ReadOnlyCollection<Transform> ghosts;

    void OnEnable() {
        ghosts = GetComponent<Wrapper>().Ghosts;
        rotationSpeed = Random.value * maxRandomRotationSpeed - maxRandomRotationSpeed / 2;
        StartCoroutine( rotationRoutine() );
    }

    void OnDisable() {
        StopCoroutine( rotationRoutine() );
    }

    IEnumerator rotationRoutine() {
        while( true ) {
            foreach( var ghost in ghosts ) {
                ghost.Rotate( new Vector3(0,0,rotationSpeed) );
            }
            yield return null;
        }
    }
}
