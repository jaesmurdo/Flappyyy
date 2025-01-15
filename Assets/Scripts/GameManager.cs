using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private bool destructionActive = false; // Contr�le si la destruction est active
    private Coroutine destructionCoroutine; // R�f�rence � la coroutine de destruction

    // M�thode pour commencer la destruction des tuiles
    public void StartTileDestruction()
    {
        if (!destructionActive)
        {
            destructionActive = true;
            destructionCoroutine = StartCoroutine(DetruireTuilesEnContinu());
        }
    }

    // M�thode pour arr�ter la destruction des tuiles
    public void StopTileDestruction()
    {
        destructionActive = false;
        if (destructionCoroutine != null)
        {
            StopCoroutine(destructionCoroutine); // Arr�te la coroutine
        }
    }


    // Coroutine pour d�truire les objets ayant le tag "tuile" en continu
    IEnumerator DetruireTuilesEnContinu()
    {
        while (destructionActive)
        {
            GameObject[] tuiles = GameObject.FindGameObjectsWithTag("tuile");
            foreach (GameObject tuile in tuiles)
            {
                Destroy(tuile); // D�truit chaque objet ayant le tag "tuile"
            }
            yield return null; // Attendre une frame avant de r�p�ter
        }
    }
}
