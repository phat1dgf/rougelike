using UnityEngine;

public class test1 : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        GameRoot.Instance.GameEventBus.Publish(new GameEvents.SceneBegin("dmm tu"));
    }
}
