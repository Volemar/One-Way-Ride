using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera fpCamera;
    [SerializeField] float range = 100f;
    [SerializeField] int damage = 10;
    [SerializeField] ParticleSystem shootVFX;
    [SerializeField] GameObject defaultHitVfx;
    // [SerializeField] Ammo ammoSlot;
    // [SerializeField] AmmoType ammoType;
    [SerializeField] float timeBetweenShots = 1f;
    [SerializeField] TextMeshProUGUI ammoText;

    bool canShoot = true;
    private PlayerControls _playerControls;

    private void OnEnable()
    {
        StartCoroutine(CoolWeapon());
    }

    private void Start()
    {
        _playerControls = GetComponent<PlayerControls>();
    }

    private IEnumerator CoolWeapon()
    {
        if (canShoot)
        {
            yield break;
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    void Update()
    {
        DisplayAmmo();
        if (_playerControls.GetPlayerAttackedThisFrame && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    private void DisplayAmmo()
    {
        //ammoText.text = ammoSlot.GetCurrentAmmo(ammoType).ToString();
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        // if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        // {
        //     ProccessEffects();
        //     ProccessRaycasting();
        //     ammoSlot.ReduceAmmo(ammoType);
        // }
        ProccessEffects();
        ProccessRaycasting();
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void ProccessEffects()
    {
        shootVFX.Play();
    }

    private void ProccessRaycasting()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpCamera.transform.position, fpCamera.transform.forward, out hit, range))
        {
            // EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            // if (target == null)
            // {
            //     CreateHitImpact(defaultHitVfx, hit);
            //     return;
            // }
            // CreateHitImpact(target.hitVfx, hit);
            // target.TakeDamage(damage);
            CreateHitImpact(defaultHitVfx, hit);
            return;
        }
    }

    private void CreateHitImpact(GameObject hitVfx, RaycastHit hit)
    {
        GameObject effect = Instantiate(hitVfx, hit.point, Quaternion.LookRotation(hit.normal));
        float time = effect.GetComponentInChildren<ParticleSystem>().main.duration;
        Destroy(effect, time);
    }
}
