using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // Classe statique pour g�rer le score
    public static int score = 0; // Le score est maintenant public
    public static int highScore = 0; // Ajout du highscore

    // R�f�rence au TextMeshPro pour afficher le score
    [SerializeField] private TextMeshProUGUI textScore; // R�f�rence au TextMeshPro pour le score
    [SerializeField] private TextMeshProUGUI textHighScore; // R�f�rence au TextMeshPro pour le highscore

    // M�thode statique pour incr�menter le score
    public static void IncrementerScore(int points)
    {
        score += points; // Incr�mente le score de la valeur des points
    }

    // M�thode pour r�initialiser le score (si n�cessaire)
    public static void ResetScore()
    {
        score = 0; // R�initialise le score � 0
    }

    // Met � jour l'affichage du score et du highscore
    void Update()
    {
        if (textScore != null)
        {
            textScore.text = score.ToString(); // Met � jour le texte pour afficher le score
        }

        // Mise � jour du highscore
        if (textHighScore != null)
        {
            textHighScore.text = "Highscore : " + highScore.ToString(); // Met � jour le texte pour afficher le highscore
        }
    }

    // M�thode pour v�rifier et mettre � jour le highscore
    public static void UpdateHighScore()
    {
        if (score > highScore)
        {
            highScore = score; // Met � jour le highscore
        }
    }

    // M�thode pour afficher ou masquer l'affichage du score
    public void SetScoreVisibility(bool visible)
    {
        if (textScore != null)
        {
            textScore.gameObject.SetActive(visible); // Active ou d�sactive l'objet contenant le score
        }
    }

    // M�thode pour afficher ou masquer l'affichage du highscore
    public void SetHighScoreVisibility(bool visible)
    {
        if (textHighScore != null)
        {
            textHighScore.gameObject.SetActive(visible); // Active ou d�sactive l'objet contenant le highscore
        }
    }
}
