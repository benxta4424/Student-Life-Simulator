using UnityEngine;

public class PersistentaObiect : MonoBehaviour
{
    private static PersistentaObiect instance;

    void Awake()
    {
        // Verificăm dacă mai există deja un astfel de obiect în scenă
        if (instance == null)
        {
            instance = this;
            // Spunem Unity-ului să nu distrugă acest obiect la schimbarea scenei
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Dacă intrăm într-o cameră nouă și găsim un alt Jucător/Canvas, îl ștergem pe cel nou
            // pentru a-l păstra pe cel vechi care are viața actualizată.
            Destroy(gameObject);
        }
    }
}