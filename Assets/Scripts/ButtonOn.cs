using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActivationScript : MonoBehaviour
{
    [Header("Paramètres")]
    public Button activationButton; // Le bouton qui active l'action
    public GameObject prefabToSpawn; // Le prefab à faire apparaître (par exemple le joueur)
    public Vector3 spawnPosition; // L'endroit où le prefab sera spawné
    public Image imageToHide; // L'image UI (avec Image component) à cacher

    [Header("Préfabriqués supplémentaires")]
    public GameObject secondPrefabToSpawn; // Deuxième prefab à faire apparaître
    public Vector3 secondSpawnPosition; // Position du deuxième prefab à faire apparaître

    public GameObject thirdPrefabToSpawn; // Troisième prefab à faire apparaître
    public Vector3 thirdSpawnPosition; // Position du troisième prefab à faire apparaître

    private bool hasActivated = false; // Booléen pour vérifier si l'action a été exécutée

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
        // Si l'action n'a pas encore été exécutée et la touche espace est pressée
        if (!hasActivated && Input.GetKeyDown(KeyCode.Space))
        {
            ActivateAction();
        }
    }

    void ActivateAction()
    {
        // Si l'action a déjà été activée, ne rien faire
        if (hasActivated) return;

        // Marquer l'action comme exécutée
        hasActivated = true;

        // Désactiver le bouton
        if (activationButton != null)
        {
            activationButton.gameObject.SetActive(false);
        }

        // Commencer l'interpolation pour cacher l'image avec un fondu
        if (imageToHide != null)
        {
            StartCoroutine(FadeOut(imageToHide));
        }

        // Faire apparaître le premier prefab au bon endroit
        if (prefabToSpawn != null)
        {
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }

        // Faire apparaître le deuxième prefab au bon endroit
        if (secondPrefabToSpawn != null)
        {
            Instantiate(secondPrefabToSpawn, secondSpawnPosition, Quaternion.identity);
        }

        // Faire apparaître le troisième prefab au bon endroit
        if (thirdPrefabToSpawn != null)
        {
            Instantiate(thirdPrefabToSpawn, thirdSpawnPosition, Quaternion.identity);
        }
    }

    // Coroutine pour faire disparaître l'image avec un fondu
    IEnumerator FadeOut(Image image)
    {
        float fadeTime = 1f; // Durée de l'interpolation de disparition
        float startAlpha = image.color.a; // L'alpha initial de l'image
        float endAlpha = 0f; // L'alpha final (transparent)
        float timeElapsed = 0f;

        // On vérifie que l'image est bien visible au début
        if (startAlpha <= 0f)
        {
            Debug.LogWarning("L'image est déjà transparente au départ!");
            yield break;
        }

        // On fait varier l'alpha sur la durée spécifiée
        while (timeElapsed < fadeTime)
        {
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, timeElapsed / fadeTime);
            image.color = new Color(image.color.r, image.color.g, image.color.b, newAlpha);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // On s'assure que l'alpha est à zéro à la fin
        image.color = new Color(image.color.r, image.color.g, image.color.b, endAlpha);

        // Désactiver l'image après le fondu
        image.gameObject.SetActive(false);
    }
}