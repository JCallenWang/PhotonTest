using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class ButtonNavigationLobbyScene : MonoBehaviour
{
    public InputField inputPlyerName; //0
    public InputField inputRoomName;  //1
    public Button createRoom;         //2
    public Button joinRoom;           //3
 
    int selectedButtonIndex;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            selectedButtonIndex++;
            if (selectedButtonIndex > 3) selectedButtonIndex = 0;
            SwithButtonWithTab();
        }

        CheckUISelected();
    }

    void SwithButtonWithTab()
    {
        switch (selectedButtonIndex)
        {
            case 0:
                inputPlyerName.Select();
                break;
            case 1:
                inputRoomName.Select();
                break;
            case 2: 
                createRoom.Select();
                break;
            case 3: 
                joinRoom.Select();
                break;
        }
    }

    void CheckUISelected()
    {
        if (inputPlyerName.isFocused)
        {
            selectedButtonIndex = 0;
        }
        if (inputRoomName.isFocused)
        {
            selectedButtonIndex = 1;
        }
    }

    //如果用滑鼠選取要改變selectedButtonIndex的數字
    //public void InputPlayerName() => selectedButtonIndex = 0;
    //public void InputRoomName() => selectedButtonIndex = 1;
    //public void CreateRoom() => selectedButtonIndex = 2;
    //public void JoinRoom() => selectedButtonIndex = 3;
}
