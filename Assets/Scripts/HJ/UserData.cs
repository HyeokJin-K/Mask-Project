using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public string userName;
    public Color[] userColors;

    public UserData(string name)
    {
        userName = name;
    }
    public UserData(string name, Color[] colors)
    {
        userName = name;
        userColors = colors;
    }
}
