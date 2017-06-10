using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMove : MonoBehaviour {
	private Rigidbody2D rgdbAsteroid;
    private static Object explosionPrefab;

    private const float LIMIT_Y_MAX = 4.3f;
    private const float LIMIT_Y_MIN = -4.3f;

    private const float LIMIT_SPEED_MAX = -0.3f;
    private const float LIMIT_SPEED_MIN = -0.1f;

    private const float LIMIT_SPAWN_X = 11f;
    private const float LIMIT_DESPAWN_X = -11;

    private float speed;

    private Color color;

    private AudioClip somExplosao;

    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
		rgdbAsteroid = GetComponent<Rigidbody2D> ();
        audioSource = GetComponent<AudioSource>();
        somExplosao = (AudioClip)Resources.Load("Sounds/Explosao");
        explosionPrefab = Resources.Load("Animations/ExplosionPrefab");

        audioSource.volume = 0.3f;

        this.transform.position = new Vector3(LIMIT_SPAWN_X, Random.Range(LIMIT_Y_MIN, LIMIT_Y_MAX));
        speed = Random.Range(LIMIT_SPEED_MIN, LIMIT_SPEED_MAX);
        color = Random.ColorHSV(0.0f, 0.2f, 0.5f, 0.6f, 0.6f, 0.7f);
        GetComponent<SpriteRenderer>().color = color;
    }
	
	// Update is called once per frame
	void Update () {
		rgdbAsteroid.position = rgdbAsteroid.position + new Vector2 (speed, 0);	

		if (rgdbAsteroid.position.x < LIMIT_DESPAWN_X && GetComponent<CapsuleCollider2D>().enabled) { // envia novamente o asteroide se ele ainda pode colidir
			rgdbAsteroid.position =	new Vector2 (LIMIT_SPAWN_X, Random.Range(LIMIT_Y_MIN, LIMIT_Y_MAX));	
		}
	}

    private void destroiAsteroide()
    {
        audioSource.PlayOneShot(somExplosao);
        GameObject go = (GameObject) Instantiate(explosionPrefab, this.transform);
        go.GetComponent<SpriteRenderer>().color = color;
        Animator anim = this.GetComponent<Animator>();
        float length = anim.GetCurrentAnimatorStateInfo(0).length;
        this.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(transform.gameObject, length * 0.5f); /* coloquei um multiplicador 2 no animator da explosão. Não sei como obter esse divisor do jeito certo */
    }

    IEnumerator telaFim()
    {
        yield return new WaitForSeconds(1); // devia ser uma fracao da animacao de destruicao
        MainController mainController = transform.parent.parent.GetComponent<MainController>();
        mainController.telaFim();
    }

    void OnCollisionEnter2D(Collision2D coll) 
	{
        // desativa colisão para novos eventos
        GetComponent<CapsuleCollider2D>().enabled = false;
        destroiAsteroide();
        string nome = coll.gameObject.name;
        if (!nome.Contains("AsteroidPreFab")) {

            if (nome.Contains("Nave")) // se foi nave chama o fim do jogo
            {
                StartCoroutine(telaFim());
            }

            Destroy(coll.gameObject); // consume o tiro ou nave
        }
    }
}