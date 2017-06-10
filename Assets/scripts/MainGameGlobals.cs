using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameGlobals : MonoBehaviour
{
    private int startTime;
    private int lastFrameCount = 0;
    private int CADA_N_FRAME = 5;
    private int MAX_GAMEPLAY_TIME = 121;

    private Object asteroidPrefab;
    private Transform planeta;

    private const float SPEED_PLANETA_X = 0.01f;
    private const float POS_PLANETA_Y_MIN = -2f;
    private const float POS_PLANETA_Y_MAX = 2f;
    private const float POS_PLANETA_SCALE_MIN = 0.25f;
    private const float POS_PLANETA_SCALE_MAX = 1f;

    private const float POS_X_PLANETA_RESPAWN_END = -13f;
    private const float POS_X_PLANETA_RESPAWN_BEGIN = 13f;

    private const float PARALLAX_X_DIFF_SPAWN = 39.68f;
    private const float PARALLAX_Y = 0.04f;

    private const float PARALLAX_X_SPEED = -0.005f;

    public Transform nave;

    private Object navePrefab;

    private Transform parallax;
    private Object parallaxPrefab;

    // Use this for initialization
    void Start()
    {
        parallax = this.transform.Find("Fundo");
        parallaxPrefab = Resources.Load("ParallaxPrefab");
        navePrefab = Resources.Load("NavePrefab");
        asteroidPrefab = Resources.Load("Animations/AsteroidPrefab");
        planeta = this.transform.Find("planeta");
    }

    void OnEnable()
    {
        startTime = (int)Time.time;
        if (navePrefab == null)
        {
            navePrefab = Resources.Load("NavePrefab");
        }
        nave = ((GameObject)Instantiate(navePrefab, this.transform)).transform;
    }

    // Update is called once per frame
    void Update()
    {

        int t = (int)Time.time - startTime + 1;

        int nFrames = CADA_N_FRAME;

        if (t > MAX_GAMEPLAY_TIME)
        {
            nFrames = (int)(nFrames * 0.1f);
        }
        else
        {
            nFrames = (int)(nFrames * (Mathf.Max((float)(MAX_GAMEPLAY_TIME / t), 0.9f) + 0.1f));
        }

        if (Time.frameCount - lastFrameCount >= CADA_N_FRAME)
        {
            Instantiate(asteroidPrefab, this.transform);
            lastFrameCount = Time.frameCount;
        }

        // mover planeta

        if (planeta.position.x <= POS_X_PLANETA_RESPAWN_END)
        {
            RespawnPlaneta();
        }
        else
        {
            planeta.position += new Vector3(-0.05f, 0f, 0f);
        }

        // cycle parallax
        cycleParallax();

    }

    void cycleParallax()
    {
        Transform tile0 = parallax.Find("Tile0");
        Transform tile1 = parallax.Find("Tile1");

        tile0.transform.position += new Vector3(PARALLAX_X_SPEED, 0f, 0f);
        tile1.transform.position += new Vector3(PARALLAX_X_SPEED, 0f, 0f);

        if (tile0.transform.position.x <= -PARALLAX_X_DIFF_SPAWN)
        {
            tile0.gameObject.name = "old tile0";
            tile1.gameObject.name = "Tile0";
            Destroy(tile0.gameObject);
            tile0 = tile1;
            // tile da frente vira o detras e cria um novo
            tile1 = ((GameObject)Instantiate(parallaxPrefab, parallax)).transform;
            tile1.gameObject.name = "Tile1";
            tile1.position = new Vector3(tile0.position.x + PARALLAX_X_DIFF_SPAWN, tile0.position.y, tile0.position.z);
        }
    }

    void RespawnPlaneta()
    {
        planeta.position = new Vector3(POS_X_PLANETA_RESPAWN_BEGIN, Random.Range(POS_PLANETA_Y_MIN, POS_PLANETA_Y_MAX), planeta.position.z);
        planeta.localScale.Set(1f, 1f, 1f);
        float escalaPlaneta = Random.Range(POS_PLANETA_SCALE_MIN, POS_PLANETA_SCALE_MAX);
        planeta.localScale = new Vector3(escalaPlaneta, escalaPlaneta, 1f);
        Color color = Random.ColorHSV(0.0f, 1.0f, 0.7f, 0.9f, 0.9f, 1.0f);
        planeta.GetComponent<SpriteRenderer>().color = color;
    }

}
