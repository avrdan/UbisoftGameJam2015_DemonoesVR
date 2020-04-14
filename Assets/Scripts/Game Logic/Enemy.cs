using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
    static float lastEnemySoundDie = 0;
	public static int Count = 0;
	protected Rigidbody rb;
	protected bool dead = false;

    public AudioClip clipDie;

	Transform xForm;
	
	[SerializeField]
	GameObject prefabExplosion;
	protected float sleepTimer = 0;
	protected float sleepTillDeath = 0.2f + Random.Range (-0.1f, 0.1f);

	public static float speedMultiplier = 1.0f;

    // fade stuff
    public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.
    private bool sceneStarting = true;      // Whether or not the scene is still fading in.

    void Awake()
	{
		xForm = transform;
		rb = GetComponent<Rigidbody> ();
		Count++;
	}
	void Update () 
	{
		if (Mathf.Abs(transform.position.y - 0.5f) < 0.001f)
		{
			rb.MovePosition (transform.position - transform.forward * Time.deltaTime*speedMultiplier);
			Vector3 pos = Camera.main.transform.position;
			pos.y = transform.position.y;
			//transform.position = transform.position - transform.forward * Time.deltaTime*speedMultiplier;
		}
		if(transform.position.y<0.15f)
		{
            IDied();
        }
	}

	void FixedUpdate()
	{

	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerControl>().GameOver();

        }
    
    }

    public void IDied()
    {
        if (lastEnemySoundDie + 0.3F < Time.time)
        {
            Audio.PlaySound(clipDie, xForm.position);
            lastEnemySoundDie = Time.time;
        }
        GameObject.Destroy(gameObject);
        Count--;
        GameObject explosion = Instantiate(prefabExplosion, xForm.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        Destroy(explosion, 2);
    }
}
