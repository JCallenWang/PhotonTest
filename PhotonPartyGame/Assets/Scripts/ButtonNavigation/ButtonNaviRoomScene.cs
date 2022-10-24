using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonNaviRoomScene : MonoBehaviour
{
    public Button startGame;
    public Button leaveRoom;
    int selectedIndex;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            selectedIndex++;
            if (selectedIndex > 1) selectedIndex = 0;
            SwitchButtonWithTab();
        }
    }

    void SwitchButtonWithTab()
    {
        switch (selectedIndex)
        {
            case 0:
                startGame.Select();
                break;
            case 1:
                leaveRoom.Select();
                break;
        }
    }
}
