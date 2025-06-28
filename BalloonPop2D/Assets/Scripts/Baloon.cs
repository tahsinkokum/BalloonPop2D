using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balon : MonoBehaviour
{
    public float hareketHizi = 2f;
    public int puanDegeri = 1;

    void Update()
    {
        // Balon yukarı hareket eder
        transform.Translate(Vector3.up * hareketHizi * Time.deltaTime);

        // Ekran dışına çıkarsa yok edilir (Y > 6)
        if (transform.position.y > 6f)
        {
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        // GameManager varsa puanı ekle
        GameManager.Instance.PuanEkle(puanDegeri);

        // Ses ve patlama efekti eklenecekse buraya
        Debug.Log("Balon Patladı! Puan: " + puanDegeri);

        Destroy(gameObject);
    }
}
