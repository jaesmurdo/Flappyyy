using UnityEngine;

public class Saut : MonoBehaviour
{
    public float forceSaut = 5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * forceSaut, ForceMode.Impulse);
        }
    }
}
