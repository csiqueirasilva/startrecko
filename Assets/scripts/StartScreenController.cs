using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenController : MonoBehaviour {

    private float lastUpdate;
    private SpriteRenderer text;
    private MainController mainController;

	// Use this for initialization
	void Start () {
        mainController = transform.parent.gameObject.GetComponent<MainController>();
	}

    void OnEnable()
    {
        lastUpdate = Time.time;
        text = transform.Find("pressspace").GetComponent<SpriteRenderer>();
    }

	// Update is called once per frame
	void Update () {
        float t = Time.time;

        if(t - lastUpdate >= 0.5f)
        {
            text.enabled = !text.enabled;
            lastUpdate = t;    
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            mainController.telaJogo();
        }
	}
}
