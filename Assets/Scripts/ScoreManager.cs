using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // Classe statique pour gérer le score
    public static int score = 0; // Le score initialisé à 0

    // Référence au TextMeshPro pour afficher le score
    [SerializeField] private TextMeshProUGUI textScore; // Référence à un TextMeshPro simple dans l'inspecteur

    // Méthode statique pour incrémenter le score
    public static void IncrementerScore(int points)
    {
        score += points; // Incrémente le score de la valeur des points
    }

    // Met à jour l'affichage du score
    void Update()
    {
        // Vérifie si le TextMeshPro est assigné et met à jour son texte
        if (textScore != null)
        {
            textScore.text = "Score : " + score.ToString(); // Met à jour le texte pour afficher le score
        }
    }
}
