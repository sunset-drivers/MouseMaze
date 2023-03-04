using MouseMaze.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MouseMaze.Controllers
{    
    public class MazeController : MonoBehaviour
    {
        public Tilemap tileMap;       

        [SerializeField]
        private int numberOfVisitedCells = 0;
        [SerializeField]
        private int mazeHeight = 5;
        [SerializeField]
        private int mazeWidth = 5;
        [SerializeField]
        private int pathWidth = 1;

        private MazeCell[,] maze;
        private Stack<Vector2> stack;

        private void Awake()
        {
            InitializeMaze();
            BuildMazeStructure();
            DrawMaze();
            //DebugMaze();
        }

        void DrawMaze()
        {
            tileMap.ClearAllTiles();
            for(int x = 0; x < mazeWidth; x++)
                for(int y = 0; y < mazeHeight; y++)
                {
                    tileMap.SetTile(new Vector3Int(x, y, 0), maze[x,y].getTile());
                }
        }

        void DebugMaze()
        {
            for (int x = 0; x < mazeWidth; x++)
                for (int y = 0; y < mazeHeight; y++)
                    Debug.Log(maze[x, y]);
        }

        void InitializeMaze()
        {
            maze = new MazeCell[mazeWidth, mazeHeight];
            stack = new Stack<Vector2>();

            for (int x = 0; x < mazeWidth; x++)
                for (int y = 0; y < mazeHeight; y++)
                    maze[x, y] = new MazeCell();

            //TODO: sortear a posição inicial;
            maze[0, 0] = new MazeCell();
            stack.Push(new Vector2(0, 0));
            numberOfVisitedCells++;
        }

        void BuildMazeStructure()
        {
            while(numberOfVisitedCells < mazeWidth * mazeHeight)
            {                
                List<DirectionsEnum> neighbours = new List<DirectionsEnum>();
                Vector2 lastCell = stack.Peek();                

                if (lastCell.y < mazeHeight - 1)
                    if (maze[(int)lastCell.x, (int)lastCell.y + 1].wasNotVisited())
                        neighbours.Add(DirectionsEnum.UP);

                if (lastCell.y > 0)
                    if (maze[(int)lastCell.x, (int)lastCell.y - 1].wasNotVisited())
                        neighbours.Add(DirectionsEnum.DOWN);

                if (lastCell.x > 0)
                    if (maze[(int)lastCell.x - 1, (int)lastCell.y].wasNotVisited())
                        neighbours.Add(DirectionsEnum.LEFT);

                if (lastCell.x < mazeWidth - 1)
                    if (maze[(int)lastCell.x + 1, (int)lastCell.y].wasNotVisited())
                        neighbours.Add(DirectionsEnum.RIGHT);

                DirectionsEnum[] avaiableDirections = neighbours.ToArray();
                if (avaiableDirections.Length > 0)
                {
                    DirectionsEnum currentDirection = avaiableDirections[Random.Range(0, avaiableDirections.Length)];
                    switch (currentDirection)
                    {
                        case DirectionsEnum.UP:
                            maze[(int)lastCell.x, (int)lastCell.y + 1].down = true;
                            maze[(int)lastCell.x, (int)lastCell.y].up = true;
                            stack.Push(new Vector2(lastCell.x, lastCell.y + 1));
                            break;

                        case DirectionsEnum.DOWN:
                            maze[(int)lastCell.x, (int)lastCell.y - 1].up = true;
                            maze[(int)lastCell.x, (int)lastCell.y].down = true;
                            stack.Push(new Vector2(lastCell.x, lastCell.y - 1));
                            break;

                        case DirectionsEnum.LEFT:
                            maze[(int)lastCell.x - 1, (int)lastCell.y].right = true;
                            maze[(int)lastCell.x, (int)lastCell.y].left = true;
                            stack.Push(new Vector2(lastCell.x - 1, lastCell.y));
                            break;

                        case DirectionsEnum.RIGHT:
                            maze[(int)lastCell.x + 1, (int)lastCell.y].left = true;
                            maze[(int)lastCell.x, (int)lastCell.y].right = true;
                            stack.Push(new Vector2(lastCell.x + 1, lastCell.y));
                            break;
                    }

                    numberOfVisitedCells++;
                }
                else
                {                                        
                    stack.Pop();
                }                
            }            
        }
    }
}