using UnityEngine;
using UnityEngine.AI;

public class TriggerNextScene : MonoBehaviour
{
    [SerializeField] private NextScene nextScene;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            nextScene.Change();
        }
    }

}