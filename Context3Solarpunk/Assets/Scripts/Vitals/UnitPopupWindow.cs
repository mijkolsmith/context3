using UnityEngine;
using UnityEngine.UI;

public class UnitPopupWindow : PopupWindow
{
	[SerializeField] private GameObject popupWindow;
	[SerializeField] private GameObject temperatureIndicator;
	[SerializeField] private Slider temperatureSlider;
	[SerializeField] private int fixHappinessValue = 25;
	private bool open;

	public override PopupWindowType GetPopupWindowType() => PopupWindowType.Unit;

	private void Update()
	{
		if (temperatureSlider.value > temperatureIndicator.transform.localPosition.x - 2 && temperatureSlider.value < temperatureIndicator.transform.localPosition.x + 2 && open)
		{
			popupWindow.SetActive(false);
			GameManager.Instance.ChangePassengerHappiness(fixHappinessValue);
			open = false;
		}
	}

	public override void Open()
	{
		popupWindow.SetActive(true);
		int x = System.Convert.ToBoolean(Random.Range(0, 2)) ? Random.Range(-10, -50) : Random.Range(10, 50);
		temperatureIndicator.transform.localPosition = new Vector3(x, temperatureIndicator.transform.localPosition.y, temperatureIndicator.transform.localPosition.z);
		temperatureSlider.value = -x;
		open = true;
	}
}