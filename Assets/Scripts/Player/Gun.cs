using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour {

    public Transform bulletSpawnpoint;

    public AudioClip fireSound;
    public float fireVolume = .03f;

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
        AudioSource.PlayClipAtPoint(fireSound, transform.position, fireVolume);
        PrefabManager.Instance.Bullet.Spawn( bulletSpawnpoint.position, transform.localRotation);
        StartCoroutine( cooldownRoutine() );
    }

    IEnumerator cooldownRoutine() {
        yield return new WaitForSeconds( cooldown );
        canFire = true;
    }

}
