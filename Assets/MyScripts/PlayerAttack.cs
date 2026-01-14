using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform firePoint;     

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // 1. Verificăm dacă ai tras obiectele în Inspector
        if (bulletPrefab == null) { Debug.LogError("Lipsește BULLET PREFAB în Inspector!"); return; }
        if (firePoint == null) { Debug.LogError("Lipsește FIRE POINT în Inspector!"); return; }

        // 2. Verificăm dacă Camera are tag-ul corect
        if (Camera.main == null) { 
            Debug.LogError("CAMERA nu are tag-ul 'MainCamera'! Click pe Camera -> Tag -> MainCamera"); 
            return; 
        }

        // 3. Calculăm poziția mouse-ului
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        // 4. Direcția și Rotația
        Vector2 direction = (Vector2)mousePos - (Vector2)firePoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        // 5. Creăm glonțul
        Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, angle));
        
        Debug.Log("Glonț creat cu succes! Dacă nu îl vezi, verifică ierarhia în timpul jocului.");
    }
}