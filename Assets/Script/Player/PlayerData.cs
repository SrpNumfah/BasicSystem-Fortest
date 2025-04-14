using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int diamond;
    public int heart;
   
    #region Public
    public PlayerData( int diamond, int heart)
    {
        this.diamond = diamond;
        this.heart = heart;
    }


    #endregion
}
