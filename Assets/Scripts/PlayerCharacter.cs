using UnityEngine;

public class Saut : MonoBehaviour
{
    public float forceSaut = 5f; // Force de base du saut
    public float tempsRecharge = 0.5f; // Temps de recharge entre deux sauts (en secondes)
   

    private Rigidbody rb;
    private bool peutSauter = true; // Si le joueur peut sauter ou non
    private float dernierSautTime = 0f; // Temps du dernier saut

    // R�f�rences aux objets que vous souhaitez manipuler apr�s la collision
    public GameObject objetAActiver; // L'objet dont le SpriteRenderer sera activ�
    public GameObject objetASupprimer; // L'objet � supprimer apr�s la collision

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

            // D�sactiver la possibilit� de sauter et d�marrer le temps de recharge
            peutSauter = false;
            dernierSautTime = Time.time;
        }

        // V�rifier si le temps de recharge est �coul�
        if (!peutSauter && Time.time - dernierSautTime >= tempsRecharge)
        {
            peutSauter = true; // Le joueur peut � nouveau sauter
        }
    }

    // D�tection de collision avec un autre objet (non trigger)
    void OnCollisionEnter(Collision collision)
    {
        // Affiche un message dans la console lorsqu'une collision avec un objet (non trigger) est d�tect�e
        Debug.Log("Collision d�tect�e avec " + collision.gameObject.name);

        // Faire dispara�tre le personnage
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
            Debug.Log("Objet supprim� : " + objetASupprimer.name); // Ajouter un message pour d�boguer
        }

        // Supprimer l'objet en collision
        Destroy(collision.gameObject); // Supprimer l'objet avec lequel le personnage entre en collision
        Debug.Log("Objet en collision supprim� : " + collision.gameObject.name); // Message de d�bogage
    }

    // D�tection de collision avec un trigger
    void OnTriggerEnter(Collider other)
    {
        // Affiche un message dans la console lorsqu'une collision avec un trigger est d�tect�e
        Debug.Log("Trigger d�tect� avec " + other.gameObject.name);

        // Incr�mente le score de 1 chaque fois que le joueur entre dans un trigger
        ScoreManager.IncrementerScore(1);
    }
}
