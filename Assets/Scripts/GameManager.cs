using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private bool destructionActive = false; // Contrôle si la destruction est active
    private Coroutine destructionCoroutine; // Référence à la coroutine de destruction

    // Méthode pour commencer la destruction des tuiles
    public void StartTileDestruction()
    {
        if (!destructionActive)
        {
            destructionActive = true;
            destructionCoroutine = StartCoroutine(DetruireTuilesEnContinu());
        }
    }

    // Méthode pour arrêter la destruction des tuiles
    public void StopTileDestruction()
    {
        destructionActive = false;
        if (destructionCoroutine != null)
        {
            StopCoroutine(destructionCoroutine); // Arrête la coroutine
        }
    }


    // Coroutine pour détruire les objets ayant le tag "tuile" en continu
    IEnumerator DetruireTuilesEnContinu()
    {
        while (destructionActive)
        {
            GameObject[] tuiles = GameObject.FindGameObjectsWithTag("tuile");
            foreach (GameObject tuile in tuiles)
            {
                Destroy(tuile); // Détruit chaque objet ayant le tag "tuile"
            }
            yield return null; // Attendre une frame avant de répéter
        }
    }
}
