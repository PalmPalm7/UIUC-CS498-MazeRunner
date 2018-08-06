using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour {

	// Use this for initialization
	bool start;
	void Start () {
		start = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!start){
			start= true;
			StartCoroutine(this.FadeScreen());
		}
		
	}
	
	public IEnumerator FadeScreen()
    {
		yield return new WaitForSeconds(2.0f);
		
        Image background = GameObject.Find("Image").GetComponent<Image>();
		Text text = GameObject.Find("Win").GetComponent<Text>();
        background.color = new Color(0.0f, 0f, 0f, 0.75f);
		text.color = new Color(1f, 1f, 1f, 1f);

        float time = 0f;
        while (background.color.a > 0)
        {
            time += Time.deltaTime * Time.timeScale;
            background.color = new Color(0.0f, 0f, 0f, 0.75f * (1f - (time / 3.5f)));
			text.color = new Color(1f, 1f, 1f, 1f * (1f - (time / 3.5f)));
			print(text.color.a);
            yield return new WaitForEndOfFrame();
            
        }

        GameObject.Find("Canvas").SetActive(false);

    }

}
