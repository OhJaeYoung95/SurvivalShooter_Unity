using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum State
    {
        Ready,
        Empty,
        Reloading
    }

    public State state;

    public Transform firePos;
    public ParticleSystem fireEffect;
    public AudioClip fireClip;

    private AudioSource gunAudio;
    private LineRenderer line;
    public float damage = 1f;
    public float fireDistance = 20f;
    public float timeBetFire = 0.12f;
    public float lastFireTime;

    public int ammoRemain = 100;
    public int magCapacity = 25;
    public int currentMagAmmo;

    private void Awake()
    {
        gunAudio = GetComponent<AudioSource>();
        line = GetComponent<LineRenderer>();

        line.positionCount = 2;
        line.enabled = false;
    }

    private void OnEnable()
    {
        state = State.Ready;
        currentMagAmmo = magCapacity;

        lastFireTime = 0f;
    }

    public void Fire()
    {
        if(state == State.Ready && lastFireTime + timeBetFire < Time.time)
        {
            lastFireTime = Time.time;
            Shot();
        }
    }

    void Shot()
    {
        var hitPosition = firePos.position + firePos.forward * fireDistance;
        Ray ray = new Ray(firePos.position, firePos.forward);

        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo, fireDistance))
        {
            var target = hitInfo.collider.GetComponent<IDamageable>();
            if(target != null)
                hitInfo.transform.GetComponent<IDamageable>().OnDamage(damage, hitInfo.point, hitInfo.normal);
            hitPosition = hitInfo.point;
        }

        StartCoroutine(ShotCoroutine(hitPosition));

        //--currentMagAmmo;
        if (currentMagAmmo <= 0)
            state = State.Empty;
    }
    IEnumerator ShotCoroutine(Vector3 hitPosition)
    {
        fireEffect.Play();
        gunAudio.PlayOneShot(fireClip);

        line.SetPosition(0, firePos.position);
        line.SetPosition(1, hitPosition);
        line.enabled = true;

        yield return new WaitForSeconds(timeBetFire + 0.1f);

        line.enabled = false;
    }


    public bool Reload()
    {
        return true;
    }

    IEnumerator ReloadCoroutine()
    {
        yield return null;
    }
}
