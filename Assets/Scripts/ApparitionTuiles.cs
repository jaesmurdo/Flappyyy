using UnityEngine;

public class ApparitionTuiles : MonoBehaviour
{
    [Header("R�f�rences")]
    public Transform pointApparition; // L'emplacement o� les tuiles appara�tront (vide)
    public GameObject[] tuiles; // Tableau des 5 tuiles � choisir

    [Header("R�glages")]
    public float intervalleApparition = 2f; // Intervalle de temps entre chaque apparition de tuile (en secondes)

    private void Start()
    {
        // Lancer la fonction pour faire appara�tre les tuiles apr�s un d�lai
        InvokeRepeating("FaireApparaitreTuile", 0f, intervalleApparition);
    }

    void FaireApparaitreTuile()
    {
        // V�rifier si le tableau de tuiles n'est pas vide
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
            Debug.LogWarning("Aucune tuile assign�e dans le tableau des tuiles.");
        }
    }
}
