using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController player;
    private float sensitivity = 400f;
    private float clampangle = 85f;

    private float verticalRotation;
    private float horizontalRotation;    
    // Start is called before the first frame update
    void Start()
    {
        this.verticalRotation = this.transform.localEulerAngles.x;
        this.horizontalRotation = this.transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        Look();
        Debug.DrawRay(this.transform.position, this.transform.forward * 2, Color.red);
    }

    public void Look()
    {
        float mouseVerical = -Input.GetAxis("Mouse Y");
        float mouseHorizontal = Input.GetAxis("Mouse X");

        this.verticalRotation += mouseVerical * this.sensitivity * Time.deltaTime;
        this.horizontalRotation += mouseHorizontal * this.sensitivity * Time.deltaTime;

        this.verticalRotation = Mathf.Clamp(this.verticalRotation, -this.clampangle, this.clampangle);

        this.transform.localRotation = Quaternion.Euler(this.verticalRotation, 0f, 0f);
        this.player.transform.rotation = Quaternion.Euler(0f, horizontalRotation, 0f);
    }
}
