using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // Classe statique pour gérer le score
    public static int score = 0; // Le score est maintenant public
    public static int highScore = 0; // Ajout du highscore

    // Référence au TextMeshPro pour afficher le score
    [SerializeField] private TextMeshProUGUI textScore; // Référence au TextMeshPro pour le score
    [SerializeField] private TextMeshProUGUI textHighScore; // Référence au TextMeshPro pour le highscore

    // Méthode statique pour incrémenter le score
    public static void IncrementerScore(int points)
    {
        score += points; // Incrémente le score de la valeur des points
    }

    // Méthode pour réinitialiser le score (si nécessaire)
    public static void ResetScore()
    {
        score = 0; // Réinitialise le score à 0
    }

    // Met à jour l'affichage du score et du highscore
    void Update()
    {
        if (textScore != null)
        {
            textScore.text = score.ToString(); // Met à jour le texte pour afficher le score
        }

        // Mise à jour du highscore
        if (textHighScore != null)
        {
            textHighScore.text = "Highscore : " + highScore.ToString(); // Met à jour le texte pour afficher le highscore
        }
    }

    // Méthode pour vérifier et mettre à jour le highscore
    public static void UpdateHighScore()
    {
        if (score > highScore)
        {
            highScore = score; // Met à jour le highscore
        }
    }

    // Méthode pour afficher ou masquer l'affichage du score
    public void SetScoreVisibility(bool visible)
    {
        if (textScore != null)
        {
            textScore.gameObject.SetActive(visible); // Active ou désactive l'objet contenant le score
        }
    }

    // Méthode pour afficher ou masquer l'affichage du highscore
    public void SetHighScoreVisibility(bool visible)
    {
        if (textHighScore != null)
        {
            textHighScore.gameObject.SetActive(visible); // Active ou désactive l'objet contenant le highscore
        }
    }
}
