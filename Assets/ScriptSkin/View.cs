using UnityEngine;

public class View : MonoBehaviour
{
    public bool isActiveInHierarchy => gameObject.activeInHierarchy;

    public virtual void Open() =>
        gameObject.SetActive(true);
       
    public virtual void Close() =>
        gameObject.SetActive(false);

    public void SetActive(bool isOn)
    {
        gameObject.SetActive(isOn);
    }
}
