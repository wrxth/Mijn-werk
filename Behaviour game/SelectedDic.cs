using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedDic : MonoBehaviour
{

    public Dictionary<int, GameObject> SelectedTable = new Dictionary<int, GameObject>();

    public void AddSelected(GameObject _obj)
    {
        int id = _obj.GetInstanceID();

        if (!(SelectedTable.ContainsKey(id)))
        {
            SelectedTable.Add(id, _obj);
            _obj.AddComponent<SelectionObj>();
        }
    }

    public void Deselect(int _id)
    {
        Destroy(SelectedTable[_id].GetComponent<SelectionObj>());
        SelectedTable.Remove(_id);
    }

    public void DeselectAll()
    {
        foreach (KeyValuePair<int,GameObject> pair in SelectedTable)
        {
            if (pair.Value != null)
            {
                Destroy(SelectedTable[pair.Key].GetComponent<SelectionObj>());
            }
        }
        SelectedTable.Clear();
    }
}
