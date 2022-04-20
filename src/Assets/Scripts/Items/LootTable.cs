using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour
{
    [SerializeField]
    private Loot[] loot;

    [SerializeField]
    public List<ItemObject> droppedItems = new List<ItemObject>();

    
    public List<ItemObject> RollLoot()
    {
        foreach(Loot item in loot)
        {
            int roll = Random.Range(0,100);
            int roll2 = Random.Range(1, item.MyDropAmount + 1);

            if (roll <= item.MyDropChance)
            {
            for(int i = 0; i < roll2; i++)
                {
                    if(item.MyItem.stackable == false){
                        ItemObject UniqueItem = Object.Instantiate(item.MyItem);
                        UniqueItem.data = new Item(UniqueItem);
                        UniqueItem.data.value = item.MyItem.data.value;
                        Debug.Log(UniqueItem.data.value);
                        
                        droppedItems.Add(UniqueItem);
                    }
                    else{
                        droppedItems.Add(item.MyItem);
                    }
                }
            }
        }
        return droppedItems;
    
    }
    
}
