using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class Saut : MonoBehaviour
{
    public float forceSaut = 5f;
    public float tempsRecharge = 0.5f;
    public GameObject objetAActiver;  // Game Over � activer apr�s la mort
    public Button boutonStart;        // Bouton pour d�marrer le jeu
    public Button boutonRestart;      // Bouton pour recommencer apr�s la mort
    public Transform pointDeDepart;  // Le point de d�part o� le joueur sera t�l�port� apr�s le clic sur Restart

    // R�f�rence au script de destruction des tuiles
    [SerializeField] private DestructionTuiles destructionTuilesScript;  // R�f�rence � DestructionTuiles
    [SerializeField] private ScoreManager scoreManager; // R�f�rence � ScoreManager pour activer/d�sactiver le score et highscore

    // Ajouter une r�f�rence au son de saut
    [SerializeField] private AudioClip sonSaut;  // R�f�rence au clip audio de saut (� assigner dans l'inspecteur)
    [SerializeField] private AudioSource audioSource;  // R�f�rence � l'AudioSource (� assigner dans l'inspecteur)

    private Rigidbody rb;
    private bool peutSauter = true;
    private float dernierSautTime = 0f;
    private bool jeuLance = false;   // Indique si le jeu a commenc�

    private HashSet<Collider> obstaclesTraverses = new HashSet<Collider>();

    private bool gameOver = false; // Indicateur pour savoir si le jeu est termin�

    private Collider playerCollider;  // R�f�rence au collider du joueur

    private SpriteRenderer spriteRenderer; // R�f�rence au SpriteRenderer de l'�cran Game Over

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>(); // Initialisation du collider du joueur
        spriteRenderer = objetAActiver.GetComponent<SpriteRenderer>(); // Initialisation du SpriteRenderer pour l'�cran Game Over

        // Assurez-vous que les boutons sont dans l'�tat initial correct
        boutonStart.gameObject.SetActive(true); // Le bouton "Start" est visible au d�but
        boutonStart.onClick.AddListener(OnStartButtonClick); // Ajout de l'�couteur pour d�marrer le jeu

        boutonRestart.gameObject.SetActive(false); // Le bouton "Restart" est d�sactiv� au d�but
        boutonRestart.onClick.AddListener(OnRestartButtonClick); // Ajout de l'�couteur pour recommencer

        if (objetAActiver != null)
        {
            spriteRenderer.enabled = false; // Assurez-vous que l'�cran Game Over est cach� au d�but
        }

        if (scoreManager != null)
        {
            scoreManager.SetScoreVisibility(true); // Le score est visible au d�but
            scoreManager.SetHighScoreVisibility(false); // Le highscore est masqu� au d�but
        }
    }

    void Update()
    {
        // Si le jeu est lanc� et que la touche espace est press�e
        if (jeuLance && !gameOver && Input.GetKeyDown(KeyCode.Space) && peutSauter)
        {
            rb.velocity = new Vector3(rb.velocity.x, forceSaut, rb.velocity.z);
            peutSauter = false;
            dernierSautTime = Time.time;

            // V�rifier si le son de saut peut �tre jou�
            if (audioSource != null && sonSaut != null && !audioSource.isPlaying)
            {
                audioSource.PlayOneShot(sonSaut);  // Joue le son de saut
            }
        }

        // V�rifier si le temps de recharge est �coul� pour permettre un nouveau saut
        if (!peutSauter && Time.time - dernierSautTime >= tempsRecharge)
        {
            peutSauter = true;
        }
    }

    // Cette m�thode est appel�e lorsque le joueur entre en collision avec un objet (ex : mort)
    void OnCollisionEnter(Collision collision)
    {
        if (jeuLance)
        {
            gameOver = true; // Le jeu est termin�
            Debug.Log("Collision d�tect�e - Game Over");  // V�rification si la collision est bien d�tect�e

            gameObject.SetActive(false); // D�sactive le joueur en cas de collision (mort)

            // Affiche l'�cran Game Over
            if (objetAActiver != null)
            {
                spriteRenderer.enabled = true; // Affiche l'�cran Game Over
            }

            // Affiche le bouton Restart
            boutonRestart.gameObject.SetActive(true); // Affiche le bouton Restart

            // Mise � jour du highscore si n�cessaire
            ScoreManager.UpdateHighScore();

            // D�sactive l'affichage du score pendant le Game Over
            if (scoreManager != null)
            {
                scoreManager.SetScoreVisibility(false); // Masque le score pendant le Game Over
                scoreManager.SetHighScoreVisibility(true); // Affiche le highscore pendant le Game Over
            }

            // R�initialise le score
            ScoreManager.ResetScore();  // R�initialise le score � z�ro
            Debug.Log("Score r�initialis� � : " + ScoreManager.score); // Affiche le score r�initialis�

            // D�sactive la destruction des tuiles via DestructionTuiles
            destructionTuilesScript.ActivateGameOver();  // D�marre la destruction des tuiles
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // V�rifier si l'objet a le tag "Point" et si ce point n'a pas encore �t� comptabilis�
        if (other.CompareTag("Point") && !obstaclesTraverses.Contains(other))
        {
            ScoreManager.IncrementerScore(1); // Incr�mente le score
            obstaclesTraverses.Add(other); // Marque cet obstacle comme travers�

            // Affiche ou met � jour le score dans l'UI
            Debug.Log("Score : " + ScoreManager.score); // Affiche le score dans la console
        }
    }

    // Cette m�thode est appel�e lorsque le bouton Start est cliqu�
    void OnStartButtonClick()
    {
        // Cache le bouton Start
        boutonStart.gameObject.SetActive(false);

        // Commence le jeu (le personnage peut maintenant sauter)
        jeuLance = true;
    }

    // Cette m�thode est appel�e lorsque le bouton Restart est cliqu�
    void OnRestartButtonClick()
    {
        // R�active le joueur avant de le t�l�porter
        gameObject.SetActive(true);

        // R�initialise la position du joueur au point de d�part
        if (pointDeDepart != null)
        {
            transform.position = pointDeDepart.position; // Le joueur revient � la position de d�part
        }

        // Cache l'�cran Game Over et le bouton Restart
        if (objetAActiver != null)
        {
            spriteRenderer.enabled = false; // Cache l'�cran Game Over
        }

        // Cache le bouton Restart
        boutonRestart.gameObject.SetActive(false);

        // R�initialise l'�tat du jeu
        jeuLance = true;
        peutSauter = true;
        gameOver = false; // Le jeu n'est plus termin�

        // R�initialise les obstacles travers�s pour pouvoir prendre des points � nouveau
        obstaclesTraverses.Clear();

        // D�sactive la destruction des tuiles via DestructionTuiles
        destructionTuilesScript.DeactivateGameOver();  // D�sactive la destruction des tuiles

        // R�active l'affichage du score
        if (scoreManager != null)
        {
            scoreManager.SetScoreVisibility(true); // R�affiche le score apr�s le restart
            scoreManager.SetHighScoreVisibility(false); // Cache le highscore lors du restart
        }

        // Lance la coroutine pour bloquer les collisions pendant X secondes
        StartCoroutine(TemporaireCollisionBlocker(2f));  // Par exemple, bloque les collisions pendant 2 secondes
    }

    // Coroutine pour bloquer les collisions pendant X secondes
    private IEnumerator TemporaireCollisionBlocker(float seconds)
    {
        // D�sactive le collider du joueur
        playerCollider.enabled = false;
        Debug.Log("Collisions d�sactiv�es pendant " + seconds + " secondes.");

        // Attendre pendant le temps sp�cifi�
        yield return new WaitForSeconds(seconds);

        // R�active le collider du joueur
        playerCollider.enabled = true;
        Debug.Log("Collisions r�activ�es.");
    }
}
