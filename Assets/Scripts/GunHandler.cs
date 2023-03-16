using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHandler : MonoBehaviour
{
    public float damage = 10.0f;
	public float range = 200.0f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;

	private void Awake()
    {
        
	}

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();

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
