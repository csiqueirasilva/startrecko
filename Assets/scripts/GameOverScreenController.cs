using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreenController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void OnEnable()
    {
        StartCoroutine(telaInicio());
    }

    IEnumerator telaInicio()
    {
        yield return new WaitForSeconds(3.7f); // devia pegar o tamanho da musica
        MainController mainController = transform.parent.GetComponent<MainController>();
        mainController.telaInicial();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
