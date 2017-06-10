using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{

    private GameObject GameScreen;
    private GameObject StartScreen;
    private GameObject EndScreen;

    // Use this for initialization
    void Start()
    {
        StartScreen = transform.Find("StartScreen").gameObject;
        GameScreen = transform.Find("MainGame").gameObject;
        EndScreen = transform.Find("GameOverScreen").gameObject;

        telaInicial();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void telaInicial()
    {
        StartScreen.SetActive(true);
        GameScreen.SetActive(false);
        EndScreen.SetActive(false);
    }

    public void telaJogo()
    {
        StartScreen.SetActive(false);
        GameScreen.SetActive(true);
        EndScreen.SetActive(false);
    }

    public void telaFim()
    {
        GameObject[] asteroides = GameObject.FindGameObjectsWithTag("asteroid"); // destroi asteroides antes de reiniciar
        for(int i = 0; i < asteroides.Length; i++)
        {
            Destroy(asteroides[i]);
        }
        StartScreen.SetActive(false);
        GameScreen.SetActive(false);
        EndScreen.SetActive(true);
    }

    void Awake ()
    {
        /* LIMITAR FPS ATE 30 */
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
    }
}