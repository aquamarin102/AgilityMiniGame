using UnityEngine;

public class MiniGameDebugStart : MonoBehaviour
{
    public DexMiniGameController controller;
    public KeyCode key = KeyCode.Space;

    private void Update()
    {
        if (Input.GetKeyDown(key))
            controller.StartMiniGame();
    }
}
