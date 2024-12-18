using UnityEngine;

public class DeplacementPoutre : MonoBehaviour
{
    [SerializeField] public float vitesseDeplacement = 2f; // Vitesse de déplacement réglable dans l'inspecteur
    [SerializeField] public float tempsAvantDisparition = 5f; // Temps avant la disparition de l'objet, réglable dans l'inspecteur

    private float tempsEcoule = 0f; // Temps qui a passé depuis le début du mouvement

    void Update()
    {
        // Déplace la poutre vers la gauche (direction négative de l'axe X)
        transform.Translate(Vector3.left * vitesseDeplacement * Time.deltaTime);

        // Ajouter le temps écoulé depuis le début
        tempsEcoule += Time.deltaTime;

        // Vérifier si le temps écoulé est supérieur ou égal au temps avant disparition
        if (tempsEcoule >= tempsAvantDisparition)
        {
            // Désactiver l'objet ou le détruire
            Destroy(gameObject); // Détruire l'objet après le délai
        }
    }
}
