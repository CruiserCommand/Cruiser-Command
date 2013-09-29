using UnityEngine;
using System.Collections;

public class WeaponsConsole : MonoBehaviour {
	public Player player;
	// FireButton : Left Mouse Button
	public const int FireButton = 0;
	public const float MissileSpeed = 50.0f;
	public GameObject MissilePrefab;
	public LayerMask Ground;
	private GameObject Missile;

	void Update() {
		if (Input.GetMouseButtonDown(FireButton)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit info;
			Physics.Raycast(ray, out info, Mathf.Infinity, Ground);
			Vector3 target = info.point;
			StartCoroutine(FireMissile(target));
		}
	}
	IEnumerator FireMissile(Vector3 target) {
		if (Missile == null) {
			// Should define where the missiles come from better than simply the cruiser's position
			Vector3 cruiserPos = new Vector3(0, 0, 0);//player.getShip().transform.position;
			Missile = GameObject.Instantiate(MissilePrefab, cruiserPos, Quaternion.identity) as GameObject;
			float t = 0.0f;
			while (t < 1) {
				t += Time.deltaTime / (Vector3.Distance(cruiserPos, target) / MissileSpeed);
				Missile.transform.position = Vector3.Lerp(cruiserPos, target, t);
				yield return null;
			}
			GameObject.DestroyObject(Missile);
			Missile = null;
		}
	}

	// EnterConsole is called when a unit enters this console. obj is the unit entering the console.
	void EnterConsole(GameObject obj) {
		Debug.Log(Player.getTeamBattlecruiser(1));
		//player = obj.GetComponent<Unit>().owner;
		//player.setShip(Player.getTeamBattlecruiser(1));
	}

	// DisconnectConsole is called when a unit exits this console.

	void DisconnectConsole() {
		Debug.Log("Disconnected from Weapons Console");
		player.setShip(null);
		player = null;
	}
}
