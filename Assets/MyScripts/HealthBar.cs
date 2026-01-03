using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    private Transform playerTransform;

    void Start()
    {
        // Găsim jucătorul (părintele Canvas-ului)
        playerTransform = transform.root;
    }

    void LateUpdate()
    {
        // Forțăm bara să stea dreaptă chiar dacă jucătorul se rotește/oglindește
        transform.rotation = Quaternion.identity;

        // Actualizăm valoarea barei folosind foamea sau o nouă variabilă de viață din GameManager
        GameManager gm = FindAnyObjectByType<GameManager>();
        if (gm != null)
        {
            slider.value = gm.foame / 100f; // Exemplu folosind foamea existentă
        }
    }
}