// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// [CreateAssetMenu(fileName = "New Quest Database", menuName = "Quest System/Database")]
// public class QuestDatabase : ScriptableObject, IserializationcallbackReceiver
// {
//    public Quest[] Quests;
//    public Dictionary<Quest, int> GetId = new Dictionary<Quest, int>();
//    public Dictionary<int, Quest> GetQuest = new Dictionary<int, Quest>();

//     public void OnAfterDeserialize()
//     {
//         GetId = new Dictionary<Quest, int>();
//         GetItem = new Dictionary<int, BaseItem>();
//         for(int i = 0; i < Items.Length; i++)
//         {
//             GetId.Add(Items[i], i);
//             GetItem.Add(i, Items[i]);
//         }
//     }

//     public void OnBeforeSerialize()
//     {

//     }
// }
