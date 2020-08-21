using Cinemachine;
using System.Collections;
using UnityEngine;

public class AlpacaCinematics : MonoBehaviour
{

    public RuntimeAnimatorController playingAnimationController;
    public RuntimeAnimatorController cinematicAnimationController;

    public float tiempoCarga = 3f;

    Animator animator;
    AlpacaMovement alpaca;

    CinemachineStoryboard storyboard;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        alpaca = GetComponent<AlpacaMovement>();
        storyboard = GameObject.Find("CM Cam 1").GetComponent<CinemachineStoryboard>();
    }

    void CambiarController(int numero)
    {
        if(numero == 0)
        {
            animator.runtimeAnimatorController = playingAnimationController;
        }
        else
        {
            animator.runtimeAnimatorController = cinematicAnimationController;
        }

        animator.Rebind();
    }

    public IEnumerator EntradaNivel(Vector3 alpacaObjective)
    {
        bool done = false;
        float velocity = Vector3.Distance(transform.position, alpacaObjective) / tiempoCarga;

        storyboard.m_Alpha = 1;
        CambiarController(1);

        transform.LookAt(new Vector3(alpacaObjective.x, transform.position.y, alpacaObjective.z));

        animator.SetTrigger("Caminar");
        while (!done)
        {
            if (Vector3.Distance(transform.position, alpacaObjective) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, alpacaObjective, velocity * Time.deltaTime);
                storyboard.m_Alpha = Mathf.Clamp(storyboard.m_Alpha - 2 / tiempoCarga * Time.deltaTime, 0, 1);
            }
            else
            {
                done = true;
            }
            yield return null;
        }

        CambiarController(0);

        alpaca.RetomarControl();
    }

    public IEnumerator SalidaNivel(Vector3 alpacaObjective)
    {
        bool done = false;
        float velocity = Vector3.Distance(transform.position, alpacaObjective) / tiempoCarga;

        storyboard.m_Alpha = 0;
        CambiarController(1);

        animator.SetTrigger("Caminar");
        while (!done)
        {
            if (Vector3.Distance(transform.position, alpacaObjective) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, alpacaObjective, velocity * Time.deltaTime);
                storyboard.m_Alpha = Mathf.Clamp(storyboard.m_Alpha + 2 / tiempoCarga * Time.deltaTime, 0, 1);
            }
            else
            {
                done = true;
            }
            yield return null;
        }

        CambiarController(0);

        alpaca.TerminarNivel();
    }
}
