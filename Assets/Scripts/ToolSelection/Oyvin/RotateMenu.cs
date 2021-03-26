using UnityEngine;
using UnityEngine.EventSystems;

public class RotateMenu : MonoBehaviour, IPointerDownHandler
{
	[SerializeField] private ObjectDetector detector;

	private Vector3 deltaAngle, initEuler;
	private bool drag;
	public void OnPointerDown(PointerEventData eventData)
	{
		deltaAngle = Input.mousePosition - transform.position;
		initEuler = transform.localEulerAngles;
		drag = true;
	}

	private void Update()
	{
		if (drag)
			transform.localEulerAngles = initEuler - new Vector3(0f, 0f, Vector3.SignedAngle(Input.mousePosition - transform.position, deltaAngle, Vector3.forward));

		if (!Input.GetMouseButtonUp(0))
			return;

		drag = false;
		transform.localEulerAngles += new Vector3(0f, 0f, Vector3.SignedAngle(detector.lastFocused.position - transform.position, Vector3.up, Vector3.forward));    //P� den framen vi l�fter musen roterer vi s� den peker opp igjen
	}
}
