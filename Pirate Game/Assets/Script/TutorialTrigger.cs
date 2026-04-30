using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public bool isCloseTrigger;
    public int tutorialIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (isCloseTrigger)
            TutorialUI.Instance.CloseUI();
        else
            TutorialUI.Instance.TriggerUI(tutorialIndex);

        Destroy(this.gameObject);
    }
}