﻿using UnityEngine;

[System.Serializable]
public class Home
{
    public static Home Instance = null;

    public Cell[,] cells;
    public HomeInfo info;

    public void Init()
    {
    }

    public Room RoomAt(Vector2Int coord)
    {
        return cells[coord.x, coord.y].room;
    }

    public Vector2Int GetRoomCenter(Vector3 coord)
    {
        return GetRoomCenter(new Vector2Int(Mathf.RoundToInt(coord.x), Mathf.RoundToInt(coord.y)));
    }

    public Vector2Int GetRoomCenter(Vector2Int coord)
    {
        Cell cell = cells[coord.x, coord.y];
        if (cell == null)
            return new Vector2Int(-1, -1);
        Room room = cell.room;
        if (cell == room.GetCell(0, 0))
            return coord;
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                if (cell == room.GetCell(x, y))
                    return new Vector2Int(coord.x - x, coord.y - y);
            }
        }
        return coord;
    }

    public void MoveRoom(RoomBehaviour src, Room dest, Vector2Int destPos)
    {
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                var c = GetCell(src.room.x + x, src.room.y + y);
                if (c != null && c.room == src)
                    cells[src.room.x + x, src.room.y + y] = null;
            }
        }
        src.room.CopyFrom(dest);
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                if (src.room.HasCell(x,y))
                    cells[src.room.x + x, src.room.y + y] = src.room.GetCell(x, y);
            }
        }
        src.transform.position = new Vector3(destPos.x, destPos.y, 0f);
        src.room.SetCoord(destPos.x, destPos.y);
        src.ApplyRotation();
    }

    public void TryToMove(Vector3 v)
    {
        var x = v.x;
        var y = v.y;

        int gridX = Mathf.RoundToInt(x);
        int gridY = Mathf.RoundToInt(y);


        float xDiff = x - gridX;
        float yDiff = y - gridY;


        if (Mathf.Abs(xDiff) < Mathf.Abs(yDiff))
        {
            //Y movement
            int offset = yDiff < 0 ? -1 : 1;
            int newY = gridY + offset;

            if (newY >= 0 && newY < cells.GetLength(1) && CellsExist(gridX, newY))
                info.doorPassEvent.Raise(new Vector2Int(gridX, gridY), new Vector2Int(gridX, gridY + offset));
        }
        else
        {
            int offset = xDiff < 0 ? -1 : 1;
            int newX = gridX + offset;

            if (newX >= 0 && newX < cells.GetLength(0) && CellsExist(newX, gridY))
                info.doorPassEvent.Raise(new Vector2Int(gridX, gridY), new Vector2Int(gridX + offset, gridY));
        }
    }

    public Vector2Int GetAdjacent(Vector3 v)
    {
        var x = v.x;
        var y = v.y;

        int gridX = Mathf.RoundToInt(x);
        int gridY = Mathf.RoundToInt(y);


        float xDiff = x - gridX;
        float yDiff = y - gridY;


        if (Mathf.Abs(xDiff) < Mathf.Abs(yDiff))
        {
            //Y movement
            int offset = yDiff < 0 ? -1 : 1;
            int newY = gridY + offset;

            if (newY < 0 || newY > cells.GetLength(1) || !CellsExist(gridX, newY))
                return new Vector2Int(-1, -1);
        
            return new Vector2Int(gridX, gridY + offset);
        }
        else
        {
            int offset = xDiff < 0 ? -1 : 1;
            int newX = gridX + offset;

            if (newX < 0 || newX > cells.GetLength(0) || !CellsExist(newX, gridY))
                return new Vector2Int(-1, -1);

            return new Vector2Int(gridX + offset, gridY);
        }

    }

    public bool CanBePlaced(Room r, int x, int y)
    {

        int doorCount = 0;

        for(int i=-1; i<2; i++)
        {
            for(int j=-1; j<2; j++)
            {
                if (r.HasCell(i, j))
                {
                    if (x + i < 0 || x + i >= cells.GetLength(0))
                        return false;
                    if (y + j < 0 || y + j >= cells.GetLength(1))
                        return false;

                    if (r.Overlap(i, j, cells[x + i, y + j]))
                        return false;
                    else
                    {

                        //UP case
                        if (r.Overlap(i, j, GetCell(x + i, y + j + 1)))
                        {
                            WallType w = r.GetWallType(i, j, Cell.UP);

                            if (w != cells[x + i, y + j + 1].GetWallType(Cell.DOWN))
                                return false;

                            if (w == WallType.Door)
                                doorCount++;
                        }
                        //RIGHT case
                        if (r.Overlap(i, j, GetCell(x + i + 1, y + j)))
                        {
                            WallType w = r.GetWallType(i, j, Cell.RIGHT);

                            if (w != cells[x + i + 1, y + j].GetWallType(Cell.LEFT))
                                return false;

                            if (w == WallType.Door)
                                doorCount++;
                        }

                        //DOWN case
                        if (r.Overlap(i, j, GetCell(x + i, y + j - 1)))
                        {
                            WallType w = r.GetWallType(i, j, Cell.DOWN);

                            if (w != cells[x + i, y + j - 1].GetWallType(Cell.UP))
                                return false;

                            if (w == WallType.Door)
                                doorCount++;
                        }


                        //LEFT case
                        if (r.Overlap(i, j, GetCell(x + i - 1, y + j)))
                        {
                            WallType w = r.GetWallType(i, j, Cell.LEFT);

                            if (w != cells[x + i - 1, y + j].GetWallType(Cell.RIGHT))
                                return false;

                            if (w == WallType.Door)
                                doorCount++;
                        }



                    }
                }
            }
        }

        return doorCount > 0;
    }


    private bool CellsExist(int x, int y)
    {
        return cells[x, y] != null;
    }

    public Cell GetCell(int x, int y)
    {
        if (x < 0 || x >= cells.GetLength(0) || y < 0 || y >= cells.GetLength(1))
            return null;
        else
            return cells[x, y];
    }


    private void UpdateDoors()
    {
        int xLim = cells.GetLength(0);
        int yLim = cells.GetLength(1);

        for(int x = 0; x < xLim; x++)
            for(int y = 0; y < yLim; y++)
                cells[x, y].UpdateDoorsState(this, x, y);
        }
    }



}
