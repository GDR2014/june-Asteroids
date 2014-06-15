using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour {

    public Transform bulletSpawnpoint;
    public Bullet bulletPrefab;

    public float cooldown = .2f;
    private bool canFire = true;

    void Awake() {
        bulletPrefab.CreatePool();
    }

    void Update() {
        if( !canFire || !Input.GetButton( "Fire1" ) ) return;
        Fire();
    }

    private void Fire() {
        canFire = false;
        var bullet = bulletPrefab.Spawn( bulletSpawnpoint.position, transform.localRotation);
        StartCoroutine( cooldownRoutine() );
    }

    IEnumerator cooldownRoutine() {
        yield return new WaitForSeconds( cooldown );
        canFire = true;
    }

}
