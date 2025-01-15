using UnityEngine;

public class DestructionTuiles : MonoBehaviour
{
    // Booléen pour savoir si la destruction des tuiles doit être activée
    private bool gameOver = false;

    // Référence au script PlayerCharacter pour activer et désactiver la destruction
    [SerializeField] private Saut playerCharacterScript; // Référence au script du PlayerCharacter (à assigner dans l'inspecteur)

    void Update()
    {
        if (gameOver)
        {
            // Si le gameOver est activé, on désactive en boucle les objets "tuile"
            DestroyTuileObjects();
        }
    }

    // Méthode pour détruire tous les objets avec le tag "tuile"
    private void DestroyTuileObjects()
    {
        GameObject[] tuiles = GameObject.FindGameObjectsWithTag("tuile");

        foreach (GameObject tuile in tuiles)
        {
            Destroy(tuile); // Détruit la tuile
        }
    }

    // Méthode appelée pour activer la destruction des tuiles (GameOver)
    public void ActivateGameOver()
    {
        gameOver = true; // Active la destruction en boucle des tuiles
        Debug.Log("Game Over - Destruction des tuiles activée");
    }

    // Méthode appelée pour désactiver la destruction des tuiles (Restart)
    public void DeactivateGameOver()
    {
        gameOver = false; // Arrête la destruction des tuiles
        Debug.Log("Jeu redémarré - Destruction des tuiles désactivée");
    }
}
