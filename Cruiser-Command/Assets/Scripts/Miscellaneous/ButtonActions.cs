/*
 * Name: RTS Resources
 * Author: James 'Sevion' Nhan and Erik 'Siretu'
 * Date: 05/08/2013
 * Version: 1.0.0.0
 * Description:
 * 		This is a simple function script that handles
 * 		button actions.
 */

using UnityEngine;
using System.Collections;

public class ButtonActions : uLink.MonoBehaviour {
	public string serverAddress = "127.0.0.1";
	public string username;
	public GameObject proxyPrefab = null;
	public GameObject ownerPrefab = null;
	public GameObject serverPrefab = null;
	public UIInput ServerInput;
	public UIInput UsernameInput;
	public AudioSource music;

	public void FadeOutMusic() {
		StartCoroutine(FadeMusic());
	}

	IEnumerator FadeMusic() {
		while (music.volume > 0.1f) {
			music.volume = Mathf.Lerp(music.volume, 0.0f, Time.deltaTime);
			yield return 0;
		}
		music.volume = 0;
	}

	public IEnumerator LoadLevelDelayed(string level, float delay) {
		CameraFade.StartAlphaFade(Color.black, false, delay);
		yield return new WaitForSeconds(delay / 2 - 0.1f);
		Application.LoadLevel(level);
	}

	public void ConnectBtn() {
		serverAddress = ServerInput.text;
        if (serverAddress.Equals("")) {
            serverAddress = "127.0.0.1";
        }
		username = UsernameInput.text;
        if (username.Equals("")) {
            username = "Siretu";
        }
		DontDestroyOnLoad(this);
		FadeOutMusic();
		StartCoroutine(LoadLevelDelayed("Client", 5.0f));
    }

    public void StartServerBtn() {
		Application.LoadLevel("Server");
    }

    public void QuitBtn() {
        Application.Quit();
    }
}
