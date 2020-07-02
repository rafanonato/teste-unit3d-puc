using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float distanciaDoCentro;
    public float speed = .05f;
    GameObject player;
    Camera cam;
    float angulo = 0;
    float posX, posY, posZ, facingDirection = 90;
    float camPosX, camPosY, camPosZ, camDistanciaDoCentro, camFacingDirection;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        cam = FindObjectOfType<Camera>();
        posX = player.transform.position.x;
        posY = player.transform.position.y;
        posZ = player.transform.position.z;
        camPosX = cam.transform.position.x;
        camPosY = cam.transform.position.y;
        camPosZ = cam.transform.position.z;
        camDistanciaDoCentro = camPosZ;

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            angulo += speed * Time.deltaTime * 60;
            if (angulo >= 360)
                angulo -= 360;
            facingDirection = angulo - 90;
            camFacingDirection = angulo;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            angulo -= speed * Time.deltaTime * 60;
            if (angulo < 0)
                angulo += 360;
            facingDirection = angulo + 90;
            camFacingDirection = angulo;
        }

        if (Input.GetKey(KeyCode.UpArrow) && distanciaDoCentro >= 1)
        {
            facingDirection = angulo;
            distanciaDoCentro -= speed * Time.deltaTime * 60 / 10;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            facingDirection = angulo - 180;
            distanciaDoCentro += speed * Time.deltaTime * 60 / 10;
        }

        posX = Mathf.Sin(angulo * Mathf.Deg2Rad) * -distanciaDoCentro;
        posZ = Mathf.Cos(angulo * Mathf.Deg2Rad) * -distanciaDoCentro;
        camPosX = Mathf.Sin(angulo * Mathf.Deg2Rad) * camDistanciaDoCentro;
        camPosZ = Mathf.Cos(angulo * Mathf.Deg2Rad) * camDistanciaDoCentro;

        player.transform.rotation = Quaternion.Euler(0, facingDirection, 0);
        player.transform.position = new Vector3(posX, posY, posZ);
        cam.transform.rotation = Quaternion.Euler(0, camFacingDirection, 0);
        cam.transform.position = new Vector3(camPosX, camPosY, camPosZ);

	}
}
