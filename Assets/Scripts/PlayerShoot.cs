using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Gun gun;
    //public Animator gunAnim;
    
    private void OnEnable()
    {
        gun.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        gun.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInput.Instance.Fire)
            gun.Fire();
    }
}
