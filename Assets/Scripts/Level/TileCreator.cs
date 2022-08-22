using JASUtils;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileCreator : MonoBehaviour
{
	public static TileCreator instance;

	[SerializeField] private Tile tile;

	[HideInInspector] public Vector3Int cellPosition;

	private Tilemap tilemap;
	private TilemapRenderer itilemap;

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
	}

	private void Start()
	{
		tilemap = GetComponentInChildren<Tilemap>();
		itilemap = GetComponentInChildren<TilemapRenderer>();

		RoadTile roadTile = ScriptableObject.CreateInstance<RoadTile>();
		roadTile.sprite = tile.sprite;
		tilemap.SetTile(Vector3Int.zero, roadTile);
	}

	private void Update()
	{
		/*if (InputManager.GetButton("LMB"))
			SetTile(cellPosition);*/
	}

	public void SetTile(Vector3Int pos)
	{
		tilemap.SetTile(pos, tile);
		TileSaveData data = new TileSaveData(0, Utils.Vector3ToFloatArray(pos));
		LevelCreator.tilesSaveData.Add(data);
	}
}

public class RoadTile : Tile
{
    public Sprite[] m_Sprites;
    public Sprite m_Preview;
    // This determines which sprite is used based on the RoadTiles that are adjacent to it and rotates it to fit the other tiles.
    // As the rotation is determined by the RoadTile, the TileFlags.OverrideTransform is set for the tile.
    public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = sprite;
        tileData.color = Color.white;
        var m = tileData.transform;
        m.SetTRS(new Vector3(0.5f, 0.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 45.0f), Vector3.one);
        tileData.transform = m;
        tileData.flags = TileFlags.LockTransform;
        tileData.colliderType = ColliderType.None;
    }
}
