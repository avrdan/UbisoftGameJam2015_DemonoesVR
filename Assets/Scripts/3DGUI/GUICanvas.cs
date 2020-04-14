using UnityEngine;
using System.Collections;

public class GUICanvas : MonoBehaviour {

    [SerializeField]
    Transform startText;
    [SerializeField]
    float yStartTextOffset = 10;
    [SerializeField]
    float timeToTurnStartTextOff = 3; // in seconds
    [SerializeField]
    Transform tutorialText;
    [SerializeField]
    float yTutorialTextOffset = -10;
    [SerializeField]
    Transform centerCam;
    [SerializeField]
    Transform restartText;

    bool m_tutorialText;
    bool m_tutorialState;
    bool m_restartText;
    bool m_restartState;
    Transform obj;
    Transform obj2;
    Transform obj3;

    public bool TutorialText
    {
        get { return m_tutorialText; }
        set { m_tutorialText = value; }
    }

    public bool RestartText
    {
        get { return m_restartText; }
        set { m_restartText = value; }
    }

    float m_restartTime;

    // Use this for initialization
    void Start () {
        obj = (Transform)Instantiate(startText, transform.position, transform.rotation);
        obj.transform.SetParent(transform, false);
        obj.transform.Translate(new Vector3(0, yStartTextOffset, 0));

        obj2 = (Transform)Instantiate(tutorialText, transform.position, transform.rotation);
        obj2.transform.SetParent(transform, false);
        obj2.transform.Translate( new Vector3(0, yTutorialTextOffset, 0));

        obj3 = (Transform)Instantiate(restartText, transform.position, transform.rotation);
        obj3.transform.SetParent(transform, false);
        obj3.gameObject.SetActive(false);
        //obj3.transform.Translate(new Vector3(0, yTutorialTextOffset, 0));

        m_tutorialText = true;
        m_tutorialState = true;
        m_restartText = false;
        m_restartState = false;
    }
	
	// Update is called once per frame
	void Update () {

       if (!m_tutorialText && m_tutorialState)
        {
            m_tutorialState = false;
            obj2.gameObject.SetActive(false);
        }

       if (!m_restartText && m_restartState)
        {
            m_restartState = false;
            obj3.gameObject.SetActive(true);
        }

        m_restartTime += Time.deltaTime;
        if (m_restartTime > timeToTurnStartTextOff)
        {
            obj.gameObject.SetActive(false);
        }
    }

}
