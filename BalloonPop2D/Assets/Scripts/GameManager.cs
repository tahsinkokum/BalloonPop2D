using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // TextMeshPro desteği

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Skor Ayarları")]
    public int puan = 0;
    public TextMeshProUGUI puanText; // TMP kullanıldı

    [Header("Oyun Sonu Paneli")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText; // TMP kullanıldı

    [Header("Butonlar")]
    public Button retryButton;
    public Button exitButton;

    private bool oyunBitti = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        SkorGuncelle();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (retryButton != null)
            retryButton.onClick.AddListener(OyunuYenidenBaslat);

        if (exitButton != null)
            exitButton.onClick.AddListener(OyundanCik);
    }

    public void PuanEkle(int miktar)
    {
        if (oyunBitti) return;

        puan += miktar;
        Debug.Log($"[GameManager] Puan Eklendi: {miktar} ➤ Yeni Skor: {puan}");
        SkorGuncelle();

        if (puan >= 50)
        {
            Debug.Log("[GameManager] Oyun Bitti - Kazanıldı.");
            OyunBitti(true);
        }
        else if (puan < 0)
        {
            Debug.Log("[GameManager] Oyun Bitti - Kaybedildi.");
            OyunBitti(false);
        }
    }

    void SkorGuncelle()
    {
        if (puanText != null)
            puanText.text = "Score : " + puan;
        else
            Debug.LogWarning("[GameManager] Puan text UI bileşeni atanmadı!");
    }

    void OyunBitti(bool kazandiMi)
    {
        Debug.Log("Oyun bitti sebep: " + (kazandiMi ? "Kazanma" : "Kaybetme") + " | Skor: " + puan);

        oyunBitti = true;
        Time.timeScale = 0f;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (gameOverText != null)
            gameOverText.text = kazandiMi ? "You win !" : "You Lose !";
    }

    public void OyunuYenidenBaslat()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OyundanCik()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
