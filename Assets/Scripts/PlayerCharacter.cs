using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class Saut : MonoBehaviour
{
    public float forceSaut = 5f;
    public float tempsRecharge = 0.5f;
    public GameObject objetAActiver;  // Game Over à activer après la mort
    public Button boutonStart;        // Bouton pour démarrer le jeu
    public Button boutonRestart;      // Bouton pour recommencer après la mort
    public Transform pointDeDepart;  // Le point de départ où le joueur sera téléporté après le clic sur Restart

    // Référence au script de destruction des tuiles
    [SerializeField] private DestructionTuiles destructionTuilesScript;  // Référence à DestructionTuiles
    [SerializeField] private ScoreManager scoreManager; // Référence à ScoreManager pour activer/désactiver le score et highscore

    // Ajouter une référence au son de saut
    [SerializeField] private AudioClip sonSaut;  // Référence au clip audio de saut (à assigner dans l'inspecteur)
    [SerializeField] private AudioSource audioSource;  // Référence à l'AudioSource (à assigner dans l'inspecteur)

    private Rigidbody rb;
    private bool peutSauter = true;
    private float dernierSautTime = 0f;
    private bool jeuLance = false;   // Indique si le jeu a commencé

    private HashSet<Collider> obstaclesTraverses = new HashSet<Collider>();

    private bool gameOver = false; // Indicateur pour savoir si le jeu est terminé

    private Collider playerCollider;  // Référence au collider du joueur

    private SpriteRenderer spriteRenderer; // Référence au SpriteRenderer de l'écran Game Over

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>(); // Initialisation du collider du joueur
        spriteRenderer = objetAActiver.GetComponent<SpriteRenderer>(); // Initialisation du SpriteRenderer pour l'écran Game Over

        // Assurez-vous que les boutons sont dans l'état initial correct
        boutonStart.gameObject.SetActive(true); // Le bouton "Start" est visible au début
        boutonStart.onClick.AddListener(OnStartButtonClick); // Ajout de l'écouteur pour démarrer le jeu

        boutonRestart.gameObject.SetActive(false); // Le bouton "Restart" est désactivé au début
        boutonRestart.onClick.AddListener(OnRestartButtonClick); // Ajout de l'écouteur pour recommencer

        if (objetAActiver != null)
        {
            spriteRenderer.enabled = false; // Assurez-vous que l'écran Game Over est caché au début
        }

        if (scoreManager != null)
        {
            scoreManager.SetScoreVisibility(true); // Le score est visible au début
            scoreManager.SetHighScoreVisibility(false); // Le highscore est masqué au début
        }
    }

    void Update()
    {
        // Si le jeu est lancé et que la touche espace est pressée
        if (jeuLance && !gameOver && Input.GetKeyDown(KeyCode.Space) && peutSauter)
        {
            rb.velocity = new Vector3(rb.velocity.x, forceSaut, rb.velocity.z);
            peutSauter = false;
            dernierSautTime = Time.time;

            // Vérifier si le son de saut peut être joué
            if (audioSource != null && sonSaut != null && !audioSource.isPlaying)
            {
                audioSource.PlayOneShot(sonSaut);  // Joue le son de saut
            }
        }

        // Vérifier si le temps de recharge est écoulé pour permettre un nouveau saut
        if (!peutSauter && Time.time - dernierSautTime >= tempsRecharge)
        {
            peutSauter = true;
        }
    }

    // Cette méthode est appelée lorsque le joueur entre en collision avec un objet (ex : mort)
    void OnCollisionEnter(Collision collision)
    {
        if (jeuLance)
        {
            gameOver = true; // Le jeu est terminé
            Debug.Log("Collision détectée - Game Over");  // Vérification si la collision est bien détectée

            gameObject.SetActive(false); // Désactive le joueur en cas de collision (mort)

            // Affiche l'écran Game Over
            if (objetAActiver != null)
            {
                spriteRenderer.enabled = true; // Affiche l'écran Game Over
            }

            // Affiche le bouton Restart
            boutonRestart.gameObject.SetActive(true); // Affiche le bouton Restart

            // Mise à jour du highscore si nécessaire
            ScoreManager.UpdateHighScore();

            // Désactive l'affichage du score pendant le Game Over
            if (scoreManager != null)
            {
                scoreManager.SetScoreVisibility(false); // Masque le score pendant le Game Over
                scoreManager.SetHighScoreVisibility(true); // Affiche le highscore pendant le Game Over
            }

            // Réinitialise le score
            ScoreManager.ResetScore();  // Réinitialise le score à zéro
            Debug.Log("Score réinitialisé à : " + ScoreManager.score); // Affiche le score réinitialisé

            // Désactive la destruction des tuiles via DestructionTuiles
            destructionTuilesScript.ActivateGameOver();  // Démarre la destruction des tuiles
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Vérifier si l'objet a le tag "Point" et si ce point n'a pas encore été comptabilisé
        if (other.CompareTag("Point") && !obstaclesTraverses.Contains(other))
        {
            ScoreManager.IncrementerScore(1); // Incrémente le score
            obstaclesTraverses.Add(other); // Marque cet obstacle comme traversé

            // Affiche ou met à jour le score dans l'UI
            Debug.Log("Score : " + ScoreManager.score); // Affiche le score dans la console
        }
    }

    // Cette méthode est appelée lorsque le bouton Start est cliqué
    void OnStartButtonClick()
    {
        // Cache le bouton Start
        boutonStart.gameObject.SetActive(false);

        // Commence le jeu (le personnage peut maintenant sauter)
        jeuLance = true;
    }

    // Cette méthode est appelée lorsque le bouton Restart est cliqué
    void OnRestartButtonClick()
    {
        // Réactive le joueur avant de le téléporter
        gameObject.SetActive(true);

        // Réinitialise la position du joueur au point de départ
        if (pointDeDepart != null)
        {
            transform.position = pointDeDepart.position; // Le joueur revient à la position de départ
        }

        // Cache l'écran Game Over et le bouton Restart
        if (objetAActiver != null)
        {
            spriteRenderer.enabled = false; // Cache l'écran Game Over
        }

        // Cache le bouton Restart
        boutonRestart.gameObject.SetActive(false);

        // Réinitialise l'état du jeu
        jeuLance = true;
        peutSauter = true;
        gameOver = false; // Le jeu n'est plus terminé

        // Réinitialise les obstacles traversés pour pouvoir prendre des points à nouveau
        obstaclesTraverses.Clear();

        // Désactive la destruction des tuiles via DestructionTuiles
        destructionTuilesScript.DeactivateGameOver();  // Désactive la destruction des tuiles

        // Réactive l'affichage du score
        if (scoreManager != null)
        {
            scoreManager.SetScoreVisibility(true); // Réaffiche le score après le restart
            scoreManager.SetHighScoreVisibility(false); // Cache le highscore lors du restart
        }

        // Lance la coroutine pour bloquer les collisions pendant X secondes
        StartCoroutine(TemporaireCollisionBlocker(2f));  // Par exemple, bloque les collisions pendant 2 secondes
    }

    // Coroutine pour bloquer les collisions pendant X secondes
    private IEnumerator TemporaireCollisionBlocker(float seconds)
    {
        // Désactive le collider du joueur
        playerCollider.enabled = false;
        Debug.Log("Collisions désactivées pendant " + seconds + " secondes.");

        // Attendre pendant le temps spécifié
        yield return new WaitForSeconds(seconds);

        // Réactive le collider du joueur
        playerCollider.enabled = true;
        Debug.Log("Collisions réactivées.");
    }
}
