using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    [Header("Balon Ayarları")]
    public GameObject[] balloonPrefabs;

    [Header("Zaman Ayarları")]
    public float spawnInterval = 1.5f;

    [Header("Pozisyon Ayarları")]
    public float spawnXMin = -7f;
    public float spawnXMax = 7f;
    private float spawnY;

    void Start()
    {
        // Kamera alt sınırına göre spawn pozisyonunu hesapla
        if (Camera.main != null)
        {
            spawnY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 10f)).y - 1f; // 10f -> kamera uzaklığı
        }
        else
        {
            Debug.LogError("Ana kamera bulunamadı.");
            spawnY = -6f; // yedek değer
        }

        // Prefab kontrolü
        if (balloonPrefabs == null || balloonPrefabs.Length == 0)
        {
            Debug.LogWarning("BalloonSpawner: balloonPrefabs dizisi boş.");
            return;
        }

        // Sürekli balon üretmeye başla
        InvokeRepeating(nameof(SpawnBalloon), 1f, spawnInterval);
    }

    void SpawnBalloon()
    {
        // Prefab listesinden rastgele birini seç
        int index = Random.Range(0, balloonPrefabs.Length);
        GameObject balloonPrefab = balloonPrefabs[index];

        if (balloonPrefab == null)
        {
            Debug.LogWarning($"BalloonSpawner: Element {index} null, spawn atlanıyor.");
            return;
        }

        // Instantiate et
        float randomX = Random.Range(spawnXMin, spawnXMax);
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0f);
        GameObject newBalloon = Instantiate(balloonPrefab, spawnPosition, Quaternion.identity);

        // BalloonMovement varsa hızını yazdır
        var movement = newBalloon.GetComponent<BalloonMovement>();
        if (movement != null)
        {
            Debug.Log($"BalloonSpawner ➤ {newBalloon.name} üretildi. Hız: {movement.GetSpeed():0.00}");
        }
    }
}
