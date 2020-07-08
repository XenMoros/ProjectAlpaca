using UnityEngine;
using System.Collections;

public class PuertaAscensor : Puerta
{
    public Ascensor ascensor;
    public Transform indicador;
    public bool posicionArriba, estaParpadeando;
    float mult = 2f;

    public Renderer objectRenderer;
    Material material;

    private void Start()
    {
        material = objectRenderer.material;
        material.SetFloat("_Opacity", 0f);
        estaParpadeando = false;
    }

    private void OnEnable()
    {
        if (ascensor != null)
        {
            if (posicionArriba)
            {
                ascensor.OnReachUp += AbrirPuerta;
            }
            else
            {
                ascensor.OnReachDown += AbrirPuerta;
            }
            ascensor.OnReachUp += TerminarParpadeo;
            ascensor.OnReachDown += TerminarParpadeo;
            ascensor.OnLeave += CerrarPuerta;
            ascensor.OnLeave += EmpezarParpadeo;
        }
    }

    private void OnDisable()
    {
        if (ascensor != null)
        {
            if (posicionArriba)
            {
                ascensor.OnReachUp -= AbrirPuerta;
            }
            else
            {
                ascensor.OnReachDown -= AbrirPuerta;
            }
            ascensor.OnReachUp -= TerminarParpadeo;
            ascensor.OnReachDown -= TerminarParpadeo;
            ascensor.OnLeave -= CerrarPuerta;
            ascensor.OnLeave -= EmpezarParpadeo;
        }
    }

    public void EmpezarParpadeo()
    {
        estaParpadeando = true;
        StartCoroutine(Parpadeando());
    }

    public void TerminarParpadeo()
    {
        estaParpadeando = false;
        material.SetFloat("_Opacity", 0f);
        indicador.Rotate(new Vector3(0, 0, 180));
    }

    public IEnumerator Parpadeando()
    {
        float opacidad;

        opacidad = material.GetFloat("_Opacity");

        while (estaParpadeando)
        {
            opacidad = Mathf.Clamp(opacidad + mult * Time.deltaTime, 0, 1);
            if(opacidad == 1 || opacidad == 0)
            {
                mult *= -1;
            }

            material.SetFloat("_Opacity", opacidad);
            yield return null;
        }
    }
}
