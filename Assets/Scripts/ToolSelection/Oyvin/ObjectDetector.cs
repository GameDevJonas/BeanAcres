using UnityEngine;
using DG.Tweening;

public class ObjectDetector : MonoBehaviour
{
	[HideInInspector] public Transform lastFocused;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Tweener t = collision.transform.DOScale(2f, 0.2f);
		t.SetEase(Ease.OutElastic);
		lastFocused = collision.transform;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		Tweener t = collision.transform.DOScale(1f, 0.2f);
		t.SetEase(Ease.OutElastic);
	}
}