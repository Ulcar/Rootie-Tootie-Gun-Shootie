using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
[ExecuteInEditMode]
[CustomPropertyDrawer(typeof(EditModeTestScript))]
public class EditModeTestScript : MonoBehaviour
{
    public bool Wall;
    public bool AlwaysOntop;
    public static bool StaticWall;
    public static bool StaticAlwaysOntop;
    public static Tilemap StaticTilemap;
    public Tilemap tilemap;
    public FloorInfo floorInfo;
    public static Sprite[] sprites;
    [Range(0, 81)]
    public int RangeNumber;
    public static int StaticRangeNumber;
    public Sprite White;
    public Image sprite0;
    public Image sprite1;
    public Image sprite2;
    public Image sprite3;
    public Image sprite4;
    public static EditModeTestScript instance;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("StartOfzo");
    }

    void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        StaticTilemap = tilemap;
        StaticRangeNumber = RangeNumber;
        StaticWall = Wall;
        StaticAlwaysOntop = AlwaysOntop;
        
        sprites = Resources.LoadAll<Sprite>(floorInfo.Floor1SpriteSheet.name);
        if (RangeNumber - 2 < 0 == false && RangeNumber - 2 > sprites.Length == false)
        {
            sprite0.sprite = sprites[RangeNumber - 2];
        }
        else
        {
            sprite0.sprite = White;
        }
        if (RangeNumber - 1 < 0 == false && RangeNumber - 1 > sprites.Length == false)
        {
            sprite1.sprite = sprites[RangeNumber - 1];
        }
        else
        {
            sprite1.sprite = White;
        }
        if (RangeNumber < 0 == false && RangeNumber > sprites.Length == false)
        {
            sprite2.sprite = sprites[RangeNumber];
        }
        else
        {
            sprite2.sprite = White;
        }
        if (RangeNumber + 1 < 0 == false && RangeNumber + 1 > sprites.Length == false)
        {
            sprite3.sprite = sprites[RangeNumber + 1];
        }
        else
        {
            sprite3.sprite = White;
        }
        if (RangeNumber + 2 < 0 == false && RangeNumber + 2 > sprites.Length == false)
        {
            sprite4.sprite = sprites[RangeNumber + 2];
        }
        else
        {
            sprite4.sprite = White;
        }

        //EditorGUILayout.ObjectField(sprite, typeof(Sprite)); 
        Debug.Log("UpdateOfzo, Number: " + RangeNumber);
    }

}
