using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEditorInternal;

public class LoadingSceneManager : MonoBehaviour
{

    public List<GameObject> loadingAnimationsPrefabs;
    public GameObject currentLoadingAnimation;

    public Animator alpacaMenuAnimator;

    private float nextChangeAlpaca;
    private float timerChangeAlpaca;

    Coroutine alpacaChanging;

    public void LoadLoadingAnimation()
    {
        int i = Random.Range((int)0, loadingAnimationsPrefabs.Count);
        if(currentLoadingAnimation != null)
        {
            UnloadLoadingAnimation();
        }

        currentLoadingAnimation = Instantiate(loadingAnimationsPrefabs[i],this.transform);
    }

    public void UnloadLoadingAnimation()
    {
        Destroy(currentLoadingAnimation);
    }

    public void LoadAlpaca()
    {
        alpacaMenuAnimator.SetBool("Active",true);
        alpacaChanging = StartCoroutine(AnimateAlpaca());
    }

    public void UnloadAlpaca()
    {
        alpacaMenuAnimator.SetBool("Active", false);
        StopCoroutine(alpacaChanging);
    }

    IEnumerator AnimateAlpaca()
    {
        nextChangeAlpaca = 6;
        timerChangeAlpaca = 0;
        
        while (true)
        {
            if(nextChangeAlpaca < timerChangeAlpaca)
            {
                timerChangeAlpaca = 0;
                alpacaMenuAnimator.SetInteger("NextAnimation", Random.Range(1, 7));
                alpacaMenuAnimator.SetTrigger("ChangeAnimation");
                nextChangeAlpaca = Random.Range(8, 15);
            }

            timerChangeAlpaca += Time.deltaTime;
            yield return null;
        }
    }
}
