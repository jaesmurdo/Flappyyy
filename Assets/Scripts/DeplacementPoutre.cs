using UnityEngine;

public class DeplacementPoutre : MonoBehaviour
{
    [SerializeField] public float vitesseDeplacement = 2f; // Vitesse de d�placement r�glable dans l'inspecteur
    [SerializeField] public float tempsAvantDisparition = 5f; // Temps avant la disparition de l'objet, r�glable dans l'inspecteur

    private float tempsEcoule = 0f; // Temps qui a pass� depuis le d�but du mouvement

    void Update()
    {
        // D�place la poutre vers la gauche (direction n�gative de l'axe X)
        transform.Translate(Vector3.left * vitesseDeplacement * Time.deltaTime);

        // Ajouter le temps �coul� depuis le d�but
        tempsEcoule += Time.deltaTime;

        // V�rifier si le temps �coul� est sup�rieur ou �gal au temps avant disparition
        if (tempsEcoule >= tempsAvantDisparition)
        {
            // D�sactiver l'objet ou le d�truire
            Destroy(gameObject); // D�truire l'objet apr�s le d�lai
        }
    }
}
