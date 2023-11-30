

using UnityEngine;

public class ReaperLocatorPointer : MonoBehaviour
{
    private GameObject targetObject;
    private Color startColor = Color.green;
    private Color endColor = Color.red;
    public float maxDistance = 100;
    private SpriteRenderer triangleSprite;
    
    
    private void Start()
    {
        targetObject = GameObject.Find("Grim(Clone)");
        triangleSprite = transform.Find("Triangle").GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        var direction = targetObject.transform.position - transform.position;
        var distance = direction.magnitude;
        var normalizedDistance = 1-Mathf.Clamp01(distance / maxDistance);
        var lerpColor = Color.Lerp(startColor, endColor, normalizedDistance);
        triangleSprite.color = lerpColor;
        
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90));
    }
}