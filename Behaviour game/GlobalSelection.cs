using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSelection : MonoBehaviour
{
    SelectedDic SelectedTable;
    RaycastHit Hit;

    bool DragSelect;


    [SerializeField] private LayerMask Terrain;
    [SerializeField] private LayerMask Units;
    //collider Vars
    private MeshCollider SelectionBox;
    private Mesh SelectionMesh;

    private Vector3 p1;
    private Vector3 p2;

    Vector2[] Corners;


    Vector3[] Vertices;
    Vector3[] Vecs;
    void Start()
    {
        SelectedTable = GetComponent<SelectedDic>();
        DragSelect = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            p1 = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            if ((p1 - Input.mousePosition).magnitude > 40)
            {
                DragSelect = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (DragSelect == false)
            {
                Ray ray = Camera.main.ScreenPointToRay(p1);
                if (Physics.Raycast(ray,out Hit,100000, Units))
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        SelectedTable.AddSelected(Hit.transform.gameObject);
                    }
                    else
                    {
                        SelectedTable.DeselectAll();
                        SelectedTable.AddSelected(Hit.transform.gameObject);
                    }
                }
                else
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {

                    }
                    else
                    {
                        SelectedTable.DeselectAll();
                    }
                }
            }
            else
            {
                Vertices = new Vector3[4];
                Vecs = new Vector3[4];

                int i = 0;
                p2 = Input.mousePosition;
                Corners = getBoundingBox(p1, p2);

                foreach (Vector2 corner in Corners)
                {
                    Ray ray = Camera.main.ScreenPointToRay(corner);

                    if (Physics.Raycast(ray,out Hit,5000f, Terrain))
                    {
                        Vertices[i] = new Vector3(Hit.point.x, 0, Hit.point.z);
                        Vecs[i] = ray.origin - Hit.point;

                        Debug.DrawLine(Camera.main.ScreenToWorldPoint(corner), Hit.point, Color.red, 1.0f);
                    }
                    i++;

                }


                SelectionMesh = GenerateSelectionMesh(Vertices, Vecs);

                SelectionBox = gameObject.AddComponent<MeshCollider>();
                SelectionBox.sharedMesh = SelectionMesh;
                SelectionBox.convex = true;
                SelectionBox.isTrigger = true;
                //Debug.Log("hit");

                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    SelectedTable.DeselectAll();
                }

                Destroy(SelectionBox, 0.02f);
            }
            DragSelect = false;

        }
    }

    private void OnGUI()
    {
        if (DragSelect == true)
        {
            var rect = utils.GetScreenRect(p1, Input.mousePosition);
            utils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            utils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }

    Vector2[] getBoundingBox(Vector2 p1, Vector2 p2)
    {
        Vector2 newP1;
        Vector2 newP2;
        Vector2 newP3;
        Vector2 newP4;

        if (p1.x < p2.x) //if p1 is to the left of p2
        {
            if (p1.y > p2.y) // if p1 is above p2
            {
                newP1 = p1;
                newP2 = new Vector2(p2.x, p1.y);
                newP3 = new Vector2(p1.x, p2.y);
                newP4 = p2;
            }
            else //if p1 is below p2
            {
                newP1 = new Vector2(p1.x, p2.y);
                newP2 = p2;
                newP3 = p1;
                newP4 = new Vector2(p2.x, p1.y);
            }
        }
        else //if p1 is to the right of p2
        {
            if (p1.y > p2.y) // if p1 is above p2
            {
                newP1 = new Vector2(p2.x, p1.y);
                newP2 = p1;
                newP3 = p2;
                newP4 = new Vector2(p1.x, p2.y);
            }
            else //if p1 is below p2
            {
                newP1 = p2;
                newP2 = new Vector2(p1.x, p2.y);
                newP3 = new Vector2(p2.x, p1.y);
                newP4 = p1;
            }

        }

        Vector2[] corners = { newP1, newP2, newP3, newP4 };
        return corners;

    }

    private Mesh GenerateSelectionMesh(Vector3[] _corners, Vector3[] _vecs)
    {
        Vector3[] verts = new Vector3[8];

        int[] tris = { 0, 1, 2, 2, 1, 3, 4, 6, 0, 0, 6, 2, 6, 7, 2, 2, 7, 3, 7, 5, 3, 3, 5, 1, 5, 0, 1, 1, 4, 0, 4, 5, 6, 6, 5, 7 }; //map the tris of our cube

        for (int i = 0; i < 4; i++)
        {
            verts[i] = _corners[i];
        }
        for (int i = 4; i < 8; i++)
        {
            verts[i] = _corners[i - 4] + _vecs[i - 4];
        }

        Mesh selectionMesh = new Mesh();
        selectionMesh.vertices = verts;
        selectionMesh.triangles = tris;

        return selectionMesh;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Units)
        {
            
        }
        Debug.Log("hit");
        SelectedTable.AddSelected(other.gameObject);
    }
}
