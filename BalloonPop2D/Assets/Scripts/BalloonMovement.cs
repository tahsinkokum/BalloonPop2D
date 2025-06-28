using UnityEngine;

public class BalloonMovement : MonoBehaviour
{
    public GameObject explosionEffect; // Patlama efekti prefab
    public AudioClip popSound; // Ses efekti
    private float speed;

    void Start()
    {
        speed = Random.Range(1f, 3f);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        float upperLimit = Camera.main.transform.position.y + 10f;
        if (transform.position.y > upperLimit)
        {
            Destroy(gameObject);
        }
    }

    void OnMouseDown()
    {
        // Skoru balon adına göre güncelle
        switch (gameObject.name)
        {
            case string name when name.Contains("PixelBalloonLong1"):
                GameManager.Instance.PuanEkle(1);
                break;
            case string name when name.Contains("PixelBalloonLong2"):
                GameManager.Instance.PuanEkle(5);
                break;
            case string name when name.Contains("PixelBalloonLong3"):
                GameManager.Instance.PuanEkle(-2);
                break;
        }

        // 🎆 Patlama efekti
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // 🔊 Ses efekti
        if (popSound != null)
        {
            AudioSource.PlayClipAtPoint(popSound, Camera.main.transform.position);
        }

        Destroy(gameObject);
    }

    public float GetSpeed() => speed;
}
