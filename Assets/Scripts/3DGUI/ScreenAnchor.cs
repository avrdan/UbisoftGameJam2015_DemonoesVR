
using UnityEngine;
using System.Collections.Generic;
[ExecuteInEditMode]
public class ScreenAnchor : MonoBehaviour
{

    public Camera m_camera;

    void Awake()
    {
        if (!m_camera)
        {
            m_camera = Camera.main;
            
        }
        SetPosition();
    }

    public Vector3 viewportPos;


    void OnDrawGizmosSelected()
    {
        if (!m_camera)
        {
            return;
        }
        Vector3 p = m_camera.ViewportToWorldPoint(viewportPos);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(p, 0.1F);
    }
    void SetPosition()
    {

        Vector3 worldPos = m_camera.ViewportToWorldPoint(new Vector3(viewportPos.x, viewportPos.y, viewportPos.z));
        

        transform.position = worldPos;
    }
#if UNITY_EDITOR

    void Update()
    {
        if (!m_camera)
        {
            return;
        }
        SetPosition();
    }
#endif
}