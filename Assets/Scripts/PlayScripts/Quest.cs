using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
	private struct QuestList {
		public string questName;
		public int currCount;
		public int goalCount;

		public string mix {
			get {
				string temp;
				temp = questName + " " + currCount + " / " + goalCount;

				return temp;
			}
		}
	}

	private Animator _anim;
	private readonly int hash_nextQuest = Animator.StringToHash("nextQuest");

	private string questName = "Quest00";

	private int questIdx = 0;
	public int QuestIdx {
		get { return questIdx; }
	}

	private Text questText;
	private QuestList questList;

	private void Awake() {
		if ( init == null ) init = this;
		questText = GetComponent<Text>();
		_anim = GetComponent<Animator>();
	}

	private void Start() {
		CurrQuest();
	}

	private void CurrQuest() {
		questName = questName.Substring(0, questName.Length - 2);
		questName += questIdx.ToString("00");

		gameObject.SendMessage(questName);
	}

	private IEnumerator NextQuest() {
		questText.color = Color.gray;
		_anim.SetTrigger(hash_nextQuest);
		++questIdx;

		yield return new WaitForSeconds(1.0f);
		questText.color = Color.white;

		questName = questName.Substring(0, questName.Length - 2);
		questName += questIdx.ToString("00");

		gameObject.SendMessage(questName);
	}

	public void QuestUpdate() {
		questList.currCount++;
		TextUpdate();

		if ( questList.currCount >= questList.goalCount ) StartCoroutine(NextQuest());


	}

	private void Quest00() {
		questList.questName = "에일리언 처치";
		questList.currCount = 0;
		questList.goalCount = 5;

		TextUpdate();
	}

	private void Quest01() {
		questList.questName = "문 열기";
		questList.currCount = 0;
		questList.goalCount = 1;

		TextUpdate();
	}

	private void Quest02() {
		questList.questName = "거대 에일리언 처치";
		questList.currCount = 0;
		questList.goalCount = 1;

		TextUpdate();
	}

	private void TextUpdate() {
		questText.text = questList.mix;
	}

	static public Quest init;

	public void nextQuestcontrol() {
		StartCoroutine( NextQuest());
	}
}
