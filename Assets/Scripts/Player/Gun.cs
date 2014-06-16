using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour {

    public Transform bulletSpawnpoint;

    public float cooldown = .2f;
    private bool canFire = true;

    void OnEnable() {
        canFire = true;
    }

    void Update() {
        if( !canFire || !Input.GetButton( "Fire1" ) ) return;
        Fire();
    }

    private void Fire() {
        canFire = false;
        PrefabManager.Instance.Bullet.Spawn( bulletSpawnpoint.position, transform.localRotation);
        StartCoroutine( cooldownRoutine() );
    }

    IEnumerator cooldownRoutine() {
        yield return new WaitForSeconds( cooldown );
        canFire = true;
    }

}
