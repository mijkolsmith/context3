using UnityEngine;

public class QuestAdvancer : MonoBehaviour, IInteractable
{
    public void Highlight(Color color)
    {
        //throw new System.NotImplementedException();
    }

    public void Interact()
    {
        GameManager.Instance.QuestManager.AdvanceTasks(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == PlayerControllerPointClick.Player)
        {
            Interact();
        }
    }
}