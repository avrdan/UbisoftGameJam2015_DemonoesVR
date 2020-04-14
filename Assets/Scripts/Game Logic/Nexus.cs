using UnityEngine;
using System.Collections;

public class Nexus : MonoBehaviour {

    [SerializeField]
    float health = 100;
    [SerializeField]
    float dominoDamage = 10;

    public float m_currHealth;
    bool m_destroyed;

	// Use this for initialization
	void Start () {
        m_currHealth = health;
        m_destroyed = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (m_destroyed)
            return;

        if (other.gameObject.tag == "Enemy")
        {
            m_currHealth -= dominoDamage;

            other.gameObject.GetComponent<Enemy>().IDied();

            // destroy domino
            if (m_currHealth <= 0)
            {
                m_destroyed = true;
                // game over
                GameObject playerObj = null;

                if (playerObj == null)
                    playerObj = GameObject.FindWithTag("Player");
                playerObj.GetComponent<PlayerControl>().GameOver();
            }
        }
    }


}
