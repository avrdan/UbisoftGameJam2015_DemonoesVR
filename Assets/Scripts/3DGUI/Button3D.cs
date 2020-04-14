using UnityEngine;

public class Button3D : AbstractButton
{
    #region Fields Arranged Nicely :)

    
    Color originalColor;
    //public Color mouseOverColor = Color.red;
    [SerializeField]
    Color colorWhenPressing = Color.magenta;

    [SerializeField]
    Renderer targetGraphic;

    [SerializeField]
    Collider targetCollider;

    #endregion Fields Arranged Nicely :)

    #region Methods

    public override bool TouchInsideButtonBounds(TouchGUI aTouch)
    {
        Ray ray = m_camera.ScreenPointToRay(new Vector3(aTouch.position.x, aTouch.position.y));
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        
        return (hit.collider != null && hit.collider == targetCollider);
    }

    void Awake()
    {
        if (targetGraphic == null)
        {
            targetGraphic = GetComponent<Renderer>();
        }
        if (targetCollider == null)
        {
            targetCollider = GetComponent<Collider>();
        }
        if (m_camera == null)
        {
            m_camera = Camera.main;
        }
        originalColor = targetGraphic.material.color;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        targetGraphic.material.color = originalColor;
    }
    protected override void OnBeginPress()
    {
        base.OnBeginPress();
        targetGraphic.material.color = colorWhenPressing;
    }
    protected override void OnOutOfBounds()
    {
        base.OnOutOfBounds();
        targetGraphic.material.color = originalColor;
    }
    protected override void OnReleaseInBounds()
    {
        base.OnReleaseInBounds();
        targetGraphic.material.color = originalColor;
    }
  
    #endregion Methods
}