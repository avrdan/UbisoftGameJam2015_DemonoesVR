


using UnityEngine;

public class FlyingDebree : MonoBehaviour
{
    
    public float flyTime = 3;
    public float speed = 3;
    public float minGravity = 3.2F;
    public float maxGravity = 9.0F;
    public float shrinkSpeed;
    public float launchVectorMagnitude = 1.0F;
    Transform xForm;
    Vector3 originalLocalPosition;
    Vector3 originalLocalScale;
    Vector3 directionToFly = Vector3.one;
    float gravity = 9.7F;
    float timer;
    void Awake()
    {
        
        xForm = transform;
        originalLocalPosition = xForm.localPosition;
        originalLocalScale = xForm.localScale;
    }
    void OnEnable()
    {
        xForm.localPosition = originalLocalPosition;
        xForm.localScale = originalLocalScale;

        timer = flyTime;
        directionToFly = Random.onUnitSphere * launchVectorMagnitude;
        
        gravity = Random.Range(minGravity, maxGravity);
        //directionToFly.y = Mathf.Abs(directionToFly.y);
    }
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            
            xForm.localPosition += directionToFly * Time.deltaTime * speed;
            xForm.localScale = Vector3.Lerp(xForm.localScale, Vector3.zero, Time.deltaTime * shrinkSpeed);

            directionToFly.y -= gravity * Time.deltaTime;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}