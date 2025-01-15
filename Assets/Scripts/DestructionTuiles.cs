using UnityEngine;

public class DestructionTuiles : MonoBehaviour
{
    // Bool�en pour savoir si la destruction des tuiles doit �tre activ�e
    private bool gameOver = false;

    // R�f�rence au script PlayerCharacter pour activer et d�sactiver la destruction
    [SerializeField] private Saut playerCharacterScript; // R�f�rence au script du PlayerCharacter (� assigner dans l'inspecteur)

    void Update()
    {
        if (gameOver)
        {
            // Si le gameOver est activ�, on d�sactive en boucle les objets "tuile"
            DestroyTuileObjects();
        }
    }

    // M�thode pour d�truire tous les objets avec le tag "tuile"
    private void DestroyTuileObjects()
    {
        GameObject[] tuiles = GameObject.FindGameObjectsWithTag("tuile");

        foreach (GameObject tuile in tuiles)
        {
            Destroy(tuile); // D�truit la tuile
        }
    }

    // M�thode appel�e pour activer la destruction des tuiles (GameOver)
    public void ActivateGameOver()
    {
        gameOver = true; // Active la destruction en boucle des tuiles
        Debug.Log("Game Over - Destruction des tuiles activ�e");
    }

    // M�thode appel�e pour d�sactiver la destruction des tuiles (Restart)
    public void DeactivateGameOver()
    {
        gameOver = false; // Arr�te la destruction des tuiles
        Debug.Log("Jeu red�marr� - Destruction des tuiles d�sactiv�e");
    }
}
