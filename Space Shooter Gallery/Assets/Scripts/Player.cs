
using UnityEngine;
using UnityEditor;

public class Player : MonoBehaviour
{
    Enemy enemy;

    AudioSource audioSource;

    GameController gameController;

    int energy;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        enemy = null;
        energy = 6;
        gameController = FindObjectOfType<GameController>();
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPosition;
        if (Input.GetMouseButtonDown(0))
        {
            audioSource.Play();
            if (enemy != null)
            {
                gameController.UpdateTarget(enemy.gameObject);
                enemy.PlayHitAnimation();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        enemy = collision.gameObject.GetComponent("Enemy") as Enemy;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        enemy = null;
    }

    public int GetEnergy () 
    {
        return this.energy;
    }

    public void SetEnergy(int energy)
    {
        this.energy = energy;
    }

}
