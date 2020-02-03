using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LenteScript : Enemy
{
    public DetectionScript detectionScript;
    bool alpacaHit;
    public bool alerta;
    public Animator animator;
    public GameObject cameraLight;
    public Transform generalCamera;
    public Transform player;
    //public float cameraAngle = Mathf.Clamp(0, -90, 90);
    float timer = 0;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(timer);
        ActivarAlerta();
        AlertaAlpaca();
        //alpacaHit = detectionScript.alpacaHit;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Escupitajo"))
        {
            animator.SetBool("StopCamera", true);
            Destroy(cameraLight);
        }
    }

    void ActivarAlerta()
    {
        if (alpacaHit)
        {
            alerta = true;

            if (timer >= 5)
            {
                Debug.Log("Moriste");
            }
        }
        else
        {
            if (timer >= 5)
            {
                alerta = false;
            }
        }
    }

    void AlertaAlpaca()
    {
        if (alerta)
        {
            timer += Time.deltaTime;
            animator.enabled = false;
            generalCamera.LookAt(player.position, Vector3.up);
        }
        else
        {
            animator.enabled = true;
            timer = 0;
        }
    }

    public void SetAlpacaHit(bool hit)
    {
        alpacaHit = hit;
        cameraLight.GetComponent<Light>().color = Color.red;
    }

    public override void SetPause(bool state)
    {
        base.SetPause(state);

    }
}
