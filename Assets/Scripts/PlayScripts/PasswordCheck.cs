using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordCheck : MonoBehaviour
{
	public Text password_key;
	public Text password_value;

	public GameObject trigger;

	private int password_temp;

	private void Awake() {
	}

	private void Start() {
		int temp_password = Random.Range(0, 1000);
		password_key.text = temp_password.ToString("0 0 0");
		password_value.text = password_temp.ToString("0 0 0");
	}

	public void Change_units_up() {
		int temp = ((password_temp % 100) % 10);
		if ( temp == 9 ) password_temp -= 9;
		else password_temp++;

		UpdatePassword_value();
	}

	public void Change_units_down() {
		int temp = ((password_temp % 100) % 10);
		if ( temp == 0 ) password_temp += 9;
		else password_temp--;

		UpdatePassword_value();
	}

	public void Change_tens_up() {
		int temp = (password_temp % 100);
		if ( temp >= 90 ) password_temp -= 90;
		else password_temp += 10;

		UpdatePassword_value();
	}

	public void Change_tens_down() {
		int temp = (password_temp % 100);
		if ( temp < 10 ) password_temp += 90;
		else password_temp -= 10;

		UpdatePassword_value();
	}

	public void Change_hundreds_up() {
		int temp = (password_temp / 100);
		if ( temp == 9 ) password_temp -= 900;
		else password_temp += 100;

		UpdatePassword_value();
	}

	public void Change_hundreds_down() {
		int temp = (password_temp / 100);
		if ( temp == 0 ) password_temp += 900;
		else password_temp -= 100;

		UpdatePassword_value();
	}

	public void ComparePassword() {
		if(password_key.text == password_value.text ) {
			Debug.Log("compare");
			transform.parent.gameObject.SetActive(false);
			trigger.GetComponent<IsTrigger>().TriggerPlay();

		}
		else {
			Debug.Log("not compare");
		}
	}

	private void UpdatePassword_value() {
		password_value.text = password_temp.ToString("0 0 0");
	}
}
