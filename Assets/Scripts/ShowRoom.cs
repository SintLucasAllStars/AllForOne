using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRoom : MonoBehaviour
{
    #region Properties
    public Transform Center { get { return center; } }
    #endregion

    #region Fields
    public static ShowRoom instance;

    [SerializeField] private Transform center;
    #endregion

    #region Methods
    private void Update()
    {
        center.Rotate(0, 1, 0);
    }
    #endregion
}
