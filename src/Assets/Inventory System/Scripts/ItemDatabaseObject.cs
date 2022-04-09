using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/ItemObjects/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
   public List<ItemObject> ItemObjects;
   public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();

    [ContextMenu("Update IDs")]
    public void UpdateID()
    {
        foreach (ItemObject io in ItemObjects)
        {
           if(io.data.Id < 0){
            io.data.Id = ItemObjects.IndexOf(io);
           }
        }
    }
    
    public void OnAfterDeserialize()
    {
        UpdateID();
    }

    public void OnBeforeSerialize()
    {
    }
}
