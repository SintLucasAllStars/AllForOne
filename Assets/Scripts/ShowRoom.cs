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

    private GameObject shownObject = null;
    #endregion

    private void Awake()
    {
        instance = this;
    }

    public void DisplayObject(GameObject gameObject)
    {
        if(shownObject)
            Destroy(shownObject);

        shownObject = Instantiate(gameObject, center);
    }

    #region Methods
    private void Update()
    {
        center.Rotate(0, Time.deltaTime * 25, 0);
    }
    #endregion
}
