using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeCell: MonoBehaviour
{   
    public bool up;
    public bool right;
    public bool down;
    public bool left;

    public MazeCell()
    {
        up = false;
        right = false;
        down = false;
        left = false;
    }

    public bool wasNotVisited()
    {
        return (!up && !right && !down && !left);
    }

    public Tile getTile()
    {
        GameObject mazeTilesPrefab = Instantiate<GameObject>((GameObject) Resources.Load("Prefabs/MazeTilesPrefab"));
        MazeTilesReference tilesReference = mazeTilesPrefab.GetComponent<MazeTilesReference>();

        if (up && right && down && left) return tilesReference.crossTile;
        if (up && right && left) return tilesReference.horizontalUpTile;
        if (down && right && left) return tilesReference.horizontalDownTile;
        if (up && down && left) return tilesReference.verticalLeftTile;
        if (up && right && down) return tilesReference.verticalRightTile;
        if (up && right) return tilesReference.upRightCornerTile;
        if (up && left) return tilesReference.upLeftCornerTile;
        if (down && right) return tilesReference.downRightCornerTile;
        if (down && left) return tilesReference.downLeftCornerTile;
        if (left && right) return tilesReference.horizontalTile;
        if (up && down) return tilesReference.verticalTile;
        if (up) return tilesReference.upTile;
        if (down) return tilesReference.downTile;
        if (left) return tilesReference.leftTile;
        if (right) return tilesReference.rightTile;

        return new Tile();
    } 
}
