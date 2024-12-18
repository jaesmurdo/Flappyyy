using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // Classe statique pour g�rer le score
    public static int score = 0; // Le score initialis� � 0

    // R�f�rence au TextMeshPro pour afficher le score
    [SerializeField] private TextMeshProUGUI textScore; // R�f�rence � un TextMeshPro simple dans l'inspecteur

    // M�thode statique pour incr�menter le score
    public static void IncrementerScore(int points)
    {
        score += points; // Incr�mente le score de la valeur des points
    }

    // Met � jour l'affichage du score
    void Update()
    {
        // V�rifie si le TextMeshPro est assign� et met � jour son texte
        if (textScore != null)
        {
            textScore.text = "Score : " + score.ToString(); // Met � jour le texte pour afficher le score
        }
    }
}
