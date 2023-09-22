using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; private set; }
    public float MoveZ { get; private set; }
    public float MoveX { get; private set; }
    public bool Fire { get; private set; }
    public bool Reload { get; private set; }
    public Ray Look { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        MoveZ = Input.GetAxis("Vertical");    // »óÇÏ
        MoveX = Input.GetAxis("Horizontal");  // ÁÂ¿ì
        Fire = Input.GetButton("Fire1");
        Reload = Input.GetKeyDown(KeyCode.R);
        Look = Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
