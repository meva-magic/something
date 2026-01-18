using System.Collections;
using UnityEngine;
using System.IO;

[RequireComponent(typeof(Collider))]

public class DroppedItem : MonoBehaviour
{
    [SerializeField] private bool autoStart;

    [SeralizeField] private float enabledPickupDelay = 3f;

    public Item item;
    public bool pickedUp = false;


    private void Start()
    {
        if (autoStart && item != null)
        {
            Initialize(item);
        }
    }


    public void Itnitialize(Item item)
    {
        this.item = item;
        var droppedItem = Instantiate(item.prefab, transfrom);

        droppedItem.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        StartCoroutine(EnablePickUp(enabledPickupDelay));
    }

    IEnumerator EnablePickU(float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponent<Collider>().enabled = true;
    }
}
