using UnityEngine;

public abstract class Item<TData> : MonoBehaviour where TData : class {
	public abstract void SetData(TData data/*, bool isTexture*/);
}
