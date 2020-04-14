
using UnityEngine;
using System.Collections.Generic;
[ExecuteInEditMode]
public class UIStretch : MonoBehaviour
{
    public Camera m_camera;

    void Awake()
    {
        if (!m_camera)
        {
            return;
        }
        SetScale();

    }
    //public float z = 3.5f;
    void SetScale()
    {
        float z = transform.localPosition.z;
        Vector3 topLeft = m_camera.ViewportToWorldPoint(new Vector3(0, 1, z));
        Vector3 topRight = m_camera.ViewportToWorldPoint(new Vector3(1, 1, z));
        Vector3 newScale = transform.localScale;
        newScale.x = (topLeft - topRight).magnitude;
        Vector3 botLeft = m_camera.ViewportToWorldPoint(new Vector3(0, 0, z));

        newScale.y = (botLeft - topLeft).magnitude;

        transform.localScale = newScale;
    }




#if UNITY_EDITOR
    //public bool rearrange = false;
    void Update()
    {
        if (!m_camera)
        {
            return;
        }
        SetScale();

    }
#endif
}