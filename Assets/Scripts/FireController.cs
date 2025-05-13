using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento del objeto
    public GameObject proyectilePrefab;
    public float HorizontalInput;
    public float fireCooldown = 1.0f; // Tiempo de enfriamiento en segundos
    private float fireTimer = 0.0f; // Tiempo transcurrido desde el último lanzamiento
    public int currentHealth = 6;
    private bool isCooldown = false; // Indica si está en tiempo de espera (cooldown)
    public int GetHealth()
    {
        return currentHealth;
    }


    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isCooldown)
        {
            if (currentHealth > 0)
            {
                currentHealth++;
                Debug.Log("El jugador ha sido golpeado por un enemigo. Vida restante: " + currentHealth);

                StartCoroutine(StartCooldown(1f)); // Inicia el tiempo de espera (cooldown) de un segundo
            }
            else
            {
                Debug.Log("El jugador está fuera de vida. Fin del juego.");
                GameOverManager.gameOverManager.CallGameOver();
            }
        }
    }

    private IEnumerator StartCooldown(float cooldownTime)
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }
      

    // Update is called once per frame
    void Update()
    {
        // Verificar si el temporizador ha alcanzado el tiempo de enfriamiento
        if (fireTimer >= fireCooldown)
        {
            // Lanzar proyectil
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(proyectilePrefab, transform.position, proyectilePrefab.transform.rotation);
                fireTimer = 0.0f; // Reiniciar el temporizador
            }
            
        }

        // Actualizar el temporizador
        fireTimer += Time.deltaTime; 

        HorizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right*Time.deltaTime*speed*HorizontalInput);
        
    }
}
