using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShooting : NetworkBehaviour
{
    [SerializeField] float shotCooldown = .3f;
    [SerializeField] Transform firePosition;
    [SerializeField] ShotEffectsmanager shotEffects;

    float elapsedTime;
    bool canShoot;

    void Start()
    {
        shotEffects.Initialize();

        if (isLocalPlayer)
        {
            canShoot = true;
        }

    }
    void Update()
    {
        if (!canShoot)
        {
            return;
        }

        elapsedTime += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && elapsedTime > shotCooldown)
        {
            elapsedTime = 0;

            //command
            CmdFireshot(firePosition.position, firePosition.forward);
        }
        
    }
    [Command]
    void CmdFireshot(Vector3 origin, Vector3 direction)
    {
        RaycastHit hit;

        Ray ray = new Ray(origin, direction);
        Debug.DrawRay(ray.origin, ray.direction * 3f, Color.green, 1f);

        //50 represents distance, its arbitrary
        bool result = Physics.Raycast(ray, out hit, 50f);

        if (result)
        {
            //health Magic Later
        }
        RpcProcessShotEffects(result, hit.point);

    }
    [ClientRpc]
    void RpcProcessShotEffects(bool result, Vector3 point)
    {
        shotEffects.PlayShotEffects();
        if (result)
        {
            shotEffects.PlayImpactEffect(point);
        }
    }

}
