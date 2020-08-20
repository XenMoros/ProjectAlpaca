using UnityEngine;
using System.Collections.Generic;

public class LoadingSceneManager : MonoBehaviour
{

    public List<GameObject> loadingAnimationsPrefabs;
    public GameObject currentLoadingAnimation;

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
}
