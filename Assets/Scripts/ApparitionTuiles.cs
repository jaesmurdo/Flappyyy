using UnityEngine;

public class ApparitionTuiles : MonoBehaviour
{
    [Header("Références")]
    public Transform pointApparition; // L'emplacement où les tuiles apparaîtront (vide)
    public GameObject[] tuiles; // Tableau des 5 tuiles à choisir

    [Header("Réglages")]
    public float intervalleApparition = 2f; // Intervalle de temps entre chaque apparition de tuile (en secondes)

    private void Start()
    {
        // Lancer la fonction pour faire apparaître les tuiles après un délai
        InvokeRepeating("FaireApparaitreTuile", 0f, intervalleApparition);
    }

    void FaireApparaitreTuile()
    {
        // Vérifier si le tableau de tuiles n'est pas vide
        if (tuiles.Length > 0)
        {
            // Choisir une tuile au hasard
            int indexAleatoire = Random.Range(0, tuiles.Length);
            GameObject tuileAApparaitre = tuiles[indexAleatoire];

            // Instancier la tuile au point d'apparition
            Instantiate(tuileAApparaitre, pointApparition.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Aucune tuile assignée dans le tableau des tuiles.");
        }
    }
}
