using UnityEngine;
using UnityEngine.UI;

public class Saut : MonoBehaviour
{
    public float forceSaut = 5f;
    public float tempsRecharge = 0.5f;
    public GameObject objetAActiver;  // Game Over à activer après la mort
    public Button boutonStart;        // Bouton pour démarrer le jeu
    public Button boutonRestart;      // Bouton pour recommencer après la mort
    public Transform pointDeDepart;  // Le point de départ où le joueur sera téléporté après le clic sur Restart

    private Rigidbody rb;
    private bool peutSauter = true;
    private float dernierSautTime = 0f;
    private bool jeuLance = false;   // Indique si le jeu a commencé

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Assurez-vous que les boutons et l'objet Game Over sont dans l'état initial correct
        if (boutonStart != null)
        {
            boutonStart.gameObject.SetActive(true); // Le bouton "Start" est visible au début
            boutonStart.onClick.AddListener(OnStartButtonClick); // Ajout de l'écouteur pour démarrer le jeu
        }

        if (boutonRestart != null)
        {
            boutonRestart.gameObject.SetActive(false); // Le bouton "Restart" est désactivé au début
            boutonRestart.onClick.AddListener(OnRestartButtonClick); // Ajout de l'écouteur pour recommencer
        }

        if (objetAActiver != null)
        {
            objetAActiver.SetActive(false); // Assurez-vous que l'écran Game Over est caché au début
        }
    }

    void Update()
    {
        // Si le jeu est lancé et que la touche espace est pressée
        if (jeuLance && Input.GetKeyDown(KeyCode.Space) && peutSauter)
        {
            rb.velocity = new Vector3(rb.velocity.x, forceSaut, rb.velocity.z);
            peutSauter = false;
            dernierSautTime = Time.time;
        }

        // Vérifier si le temps de recharge est écoulé pour permettre un nouveau saut
        if (!peutSauter && Time.time - dernierSautTime >= tempsRecharge)
        {
            peutSauter = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (jeuLance)
        {
            gameObject.SetActive(false); // Désactive le joueur en cas de collision (mort)

            // Affiche l'écran Game Over
            if (objetAActiver != null)
            {
                objetAActiver.SetActive(true); // Affiche l'écran Game Over
            }

            // Affiche le bouton Restart
            if (boutonRestart != null)
            {
                boutonRestart.gameObject.SetActive(true);
            }
        }

        // Supprimer l'objet en collision, si nécessaire
        Destroy(collision.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        ScoreManager.IncrementerScore(1); // Incrémente le score si nécessaire
    }

    // Cette méthode est appelée lorsque le bouton Start est cliqué
    void OnStartButtonClick()
    {
        // Désactive le bouton Start
        if (boutonStart != null)
        {
            boutonStart.gameObject.SetActive(false);
        }

        // Commence le jeu (le personnage peut maintenant sauter)
        jeuLance = true;

        // Le joueur commence dans la position actuelle, pas de changement ici
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
            objetAActiver.SetActive(false); // Cache l'écran Game Over
        }

        if (boutonRestart != null)
        {
            boutonRestart.gameObject.SetActive(false); // Cache le bouton Restart
        }

        // Réinitialise l'état du jeu (s'il y a besoin)
        jeuLance = true;  // Recommence le jeu
        peutSauter = true;  // Permet au joueur de sauter à nouveau
    }
}
