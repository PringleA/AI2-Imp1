using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHandler : MonoBehaviour
{
    public float damage = 2.0f;
	public float range = 999.0f;
    public float fireRate = 0.5f;
    float shootStart;
    bool canFire = true;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;

    void Update()
    {
        if (shootStart < fireRate)
            shootStart += Time.fixedDeltaTime;

        else if (shootStart >= fireRate)
            canFire = true;

        if (Input.GetButton("Fire1"))
        {
			muzzleFlash.Play();
            if (canFire)
            {
                Shoot();
                canFire = false;
                shootStart = 0;
            }
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            EnemyClass enemy = hit.transform.GetComponent<EnemyClass>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
