using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActivationScript : MonoBehaviour
{
    [Header("Param�tres")]
    public Button activationButton; // Le bouton qui active l'action
    public GameObject prefabToSpawn; // Le prefab � faire appara�tre (par exemple le joueur)
    public Vector3 spawnPosition; // L'endroit o� le prefab sera spawn�
    public Image imageToHide; // L'image UI (avec Image component) � cacher

    [Header("Pr�fabriqu�s suppl�mentaires")]
    public GameObject secondPrefabToSpawn; // Deuxi�me prefab � faire appara�tre
    public Vector3 secondSpawnPosition; // Position du deuxi�me prefab � faire appara�tre

    public GameObject thirdPrefabToSpawn; // Troisi�me prefab � faire appara�tre
    public Vector3 thirdSpawnPosition; // Position du troisi�me prefab � faire appara�tre

    private bool hasActivated = false; // Bool�en pour v�rifier si l'action a �t� ex�cut�e

    void Start()
    {
        // Ajouter un listener au bouton pour activer l'action
        if (activationButton != null)
        {
            activationButton.onClick.AddListener(ActivateAction);
        }
    }

    void Update()
    {
        // Si l'action n'a pas encore �t� ex�cut�e et la touche espace est press�e
        if (!hasActivated && Input.GetKeyDown(KeyCode.Space))
        {
            ActivateAction();
        }
    }

    void ActivateAction()
    {
        // Si l'action a d�j� �t� activ�e, ne rien faire
        if (hasActivated) return;

        // Marquer l'action comme ex�cut�e
        hasActivated = true;

        // D�sactiver le bouton
        if (activationButton != null)
        {
            activationButton.gameObject.SetActive(false);
        }

        // Commencer l'interpolation pour cacher l'image avec un fondu
        if (imageToHide != null)
        {
            StartCoroutine(FadeOut(imageToHide));
        }

        // Faire appara�tre le premier prefab au bon endroit
        if (prefabToSpawn != null)
        {
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }

        // Faire appara�tre le deuxi�me prefab au bon endroit
        if (secondPrefabToSpawn != null)
        {
            Instantiate(secondPrefabToSpawn, secondSpawnPosition, Quaternion.identity);
        }

        // Faire appara�tre le troisi�me prefab au bon endroit
        if (thirdPrefabToSpawn != null)
        {
            Instantiate(thirdPrefabToSpawn, thirdSpawnPosition, Quaternion.identity);
        }
    }

    // Coroutine pour faire dispara�tre l'image avec un fondu
    IEnumerator FadeOut(Image image)
    {
        float fadeTime = 1f; // Dur�e de l'interpolation de disparition
        float startAlpha = image.color.a; // L'alpha initial de l'image
        float endAlpha = 0f; // L'alpha final (transparent)
        float timeElapsed = 0f;

        // On v�rifie que l'image est bien visible au d�but
        if (startAlpha <= 0f)
        {
            Debug.LogWarning("L'image est d�j� transparente au d�part!");
            yield break;
        }

        // On fait varier l'alpha sur la dur�e sp�cifi�e
        while (timeElapsed < fadeTime)
        {
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, timeElapsed / fadeTime);
            image.color = new Color(image.color.r, image.color.g, image.color.b, newAlpha);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // On s'assure que l'alpha est � z�ro � la fin
        image.color = new Color(image.color.r, image.color.g, image.color.b, endAlpha);

        // D�sactiver l'image apr�s le fondu
        image.gameObject.SetActive(false);
    }
}