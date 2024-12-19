using UnityEngine;

public class Saut : MonoBehaviour
{
    public float forceSaut = 5f; // Force de base du saut
    public float tempsRecharge = 0.5f; // Temps de recharge entre deux sauts (en secondes)
   

    private Rigidbody rb;
    private bool peutSauter = true; // Si le joueur peut sauter ou non
    private float dernierSautTime = 0f; // Temps du dernier saut

    // Références aux objets que vous souhaitez manipuler après la collision
    public GameObject objetAActiver; // L'objet dont le SpriteRenderer sera activé
    public GameObject objetASupprimer; // L'objet à supprimer après la collision

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Si le joueur peut sauter et appuie sur la barre d'espace
        if (Input.GetKeyDown(KeyCode.Space) && peutSauter)
        {
            // Appliquer une vitesse verticale constante pour le saut
            rb.velocity = new Vector3(rb.velocity.x, forceSaut, rb.velocity.z);

            // Désactiver la possibilité de sauter et démarrer le temps de recharge
            peutSauter = false;
            dernierSautTime = Time.time;
        }

        // Vérifier si le temps de recharge est écoulé
        if (!peutSauter && Time.time - dernierSautTime >= tempsRecharge)
        {
            peutSauter = true; // Le joueur peut à nouveau sauter
        }
    }

    // Détection de collision avec un autre objet (non trigger)
    void OnCollisionEnter(Collision collision)
    {
        // Affiche un message dans la console lorsqu'une collision avec un objet (non trigger) est détectée
        Debug.Log("Collision détectée avec " + collision.gameObject.name);

        // Faire disparaître le personnage
        gameObject.SetActive(false);

        // Activer le SpriteRenderer d'un objet choisi
        if (objetAActiver != null)
        {
            SpriteRenderer spriteRenderer = objetAActiver.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = true; // Activer le SpriteRenderer
            }
        }

        // Supprimer un autre GameObject
        if (objetASupprimer != null)
        {
            Destroy(objetASupprimer); // Supprimer le GameObject choisi
            Debug.Log("Objet supprimé : " + objetASupprimer.name); // Ajouter un message pour déboguer
        }

        // Supprimer l'objet en collision
        Destroy(collision.gameObject); // Supprimer l'objet avec lequel le personnage entre en collision
        Debug.Log("Objet en collision supprimé : " + collision.gameObject.name); // Message de débogage
    }

    // Détection de collision avec un trigger
    void OnTriggerEnter(Collider other)
    {
        // Affiche un message dans la console lorsqu'une collision avec un trigger est détectée
        Debug.Log("Trigger détecté avec " + other.gameObject.name);

        // Incrémente le score de 1 chaque fois que le joueur entre dans un trigger
        ScoreManager.IncrementerScore(1);
    }
}
