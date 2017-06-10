using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Limites
{
    public float xMin, xMax, yMin, yMax;
}

public class NaveController : MonoBehaviour
{

    private Rigidbody2D rgdbNave;

    public Limites limites;
    public GameObject tiro;

    private float myTime = 0.0F;
    public float fireDelta = 0.5F;
    private float nextFire = 0.5F;

    private static Object explosionPrefab;

    private Sprite[] spritesNave;

    private AudioClip somTiro;

    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        rgdbNave = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        audioSource.volume = 0.3f;

        explosionPrefab = Resources.Load("Animations/ExplosionPrefab");
        somTiro = (AudioClip) Resources.Load("Sounds/Tiro");
        spritesNave = Resources.LoadAll<Sprite>("Nave");
    }

    private void criarTiro()
    {
        Vector3 posInicialTiro = new Vector3(2.25f, -0.45f, 0f);
        Instantiate(tiro, this.transform.position + posInicialTiro, new Quaternion(), this.transform.parent);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Instantiate(explosionPrefab, this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        myTime = myTime + Time.deltaTime;

        if (Input.GetButton("Fire1") && myTime > nextFire)
        {
            audioSource.PlayOneShot(somTiro);
            nextFire = myTime + fireDelta;
            criarTiro();
            nextFire = nextFire - myTime;
            myTime = 0.0F;
        }

        float moveLeft = Input.GetAxis("Horizontal");
        float moveTop = Input.GetAxis("Vertical");

        float velocidade = 0.15f;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (moveTop > 0)
        {
            sr.sprite = spritesNave[0];
        }
        else if (moveTop < 0)
        {
            sr.sprite = spritesNave[2];
        }
        else {
            sr.sprite = spritesNave[1];
        }

        //movimenta a nave
        rgdbNave.position = rgdbNave.position + new Vector2(moveLeft * velocidade, moveTop * velocidade);

        //garante que ela não saia dos limites da tela
        rgdbNave.position = new Vector2(
            Mathf.Clamp(rgdbNave.position.x, limites.xMin, limites.xMax),
            Mathf.Clamp(rgdbNave.position.y, limites.yMin, limites.yMax)
        );


    }
}
