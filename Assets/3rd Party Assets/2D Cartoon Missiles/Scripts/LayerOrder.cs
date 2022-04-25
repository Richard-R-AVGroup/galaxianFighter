using UnityEngine;

public class LayerOrder : MonoBehaviour
{
    public int Order = 0;

    private void Start()
    {
        GetComponent<Renderer>().sortingOrder = Order;
    }
}