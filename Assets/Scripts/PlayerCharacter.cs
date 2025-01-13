using UnityEngine;
using UnityEngine.UI;

public class Saut : MonoBehaviour
{
    public float forceSaut = 5f;
    public float tempsRecharge = 0.5f;
    public GameObject objetAActiver;  // Game Over � activer apr�s la mort
    public Button boutonStart;        // Bouton pour d�marrer le jeu
    public Button boutonRestart;      // Bouton pour recommencer apr�s la mort
    public Transform pointDeDepart;  // Le point de d�part o� le joueur sera t�l�port� apr�s le clic sur Restart

    private Rigidbody rb;
    private bool peutSauter = true;
    private float dernierSautTime = 0f;
    private bool jeuLance = false;   // Indique si le jeu a commenc�

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Assurez-vous que les boutons et l'objet Game Over sont dans l'�tat initial correct
        if (boutonStart != null)
        {
            boutonStart.gameObject.SetActive(true); // Le bouton "Start" est visible au d�but
            boutonStart.onClick.AddListener(OnStartButtonClick); // Ajout de l'�couteur pour d�marrer le jeu
        }

        if (boutonRestart != null)
        {
            boutonRestart.gameObject.SetActive(false); // Le bouton "Restart" est d�sactiv� au d�but
            boutonRestart.onClick.AddListener(OnRestartButtonClick); // Ajout de l'�couteur pour recommencer
        }

        if (objetAActiver != null)
        {
            objetAActiver.SetActive(false); // Assurez-vous que l'�cran Game Over est cach� au d�but
        }
    }

    void Update()
    {
        // Si le jeu est lanc� et que la touche espace est press�e
        if (jeuLance && Input.GetKeyDown(KeyCode.Space) && peutSauter)
        {
            rb.velocity = new Vector3(rb.velocity.x, forceSaut, rb.velocity.z);
            peutSauter = false;
            dernierSautTime = Time.time;
        }

        // V�rifier si le temps de recharge est �coul� pour permettre un nouveau saut
        if (!peutSauter && Time.time - dernierSautTime >= tempsRecharge)
        {
            peutSauter = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (jeuLance)
        {
            gameObject.SetActive(false); // D�sactive le joueur en cas de collision (mort)

            // Affiche l'�cran Game Over
            if (objetAActiver != null)
            {
                objetAActiver.SetActive(true); // Affiche l'�cran Game Over
            }

            // Affiche le bouton Restart
            if (boutonRestart != null)
            {
                boutonRestart.gameObject.SetActive(true);
            }
        }

        // Supprimer l'objet en collision, si n�cessaire
        Destroy(collision.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        ScoreManager.IncrementerScore(1); // Incr�mente le score si n�cessaire
    }

    // Cette m�thode est appel�e lorsque le bouton Start est cliqu�
    void OnStartButtonClick()
    {
        // D�sactive le bouton Start
        if (boutonStart != null)
        {
            boutonStart.gameObject.SetActive(false);
        }

        // Commence le jeu (le personnage peut maintenant sauter)
        jeuLance = true;

        // Le joueur commence dans la position actuelle, pas de changement ici
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
            objetAActiver.SetActive(false); // Cache l'�cran Game Over
        }

        if (boutonRestart != null)
        {
            boutonRestart.gameObject.SetActive(false); // Cache le bouton Restart
        }

        // R�initialise l'�tat du jeu (s'il y a besoin)
        jeuLance = true;  // Recommence le jeu
        peutSauter = true;  // Permet au joueur de sauter � nouveau
    }
}
