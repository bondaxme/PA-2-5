from pyamaze import COLOR

from algorithms import *



def main():
    size = int(input("Maze size [2 - 20]: "))
    while size < 2 or size > 20:
        size = int(input("Maze size [2 - 20]: "))
    BFS_maze = maze(size, size)
    BFS_maze.CreateMaze(loopPercent=40)
    path = aStar(BFS_maze)
    a = agent(BFS_maze, shape="arrow", footprints=True, color=COLOR.red)
    BFS_maze.tracePath({a: path}, delay=50)
    BFS_maze.run()
    print(f"[ASTAR] The length of path {len(path)}")

    Astar_maze = maze(size, size)
    Astar_maze.CreateMaze(loopPercent=40)
    path1 = BFS_alg(Astar_maze)
    a1 = agent(Astar_maze, footprints=True, filled=True, color=COLOR.yellow)
    Astar_maze.tracePath({a1: path1}, delay=50)
    Astar_maze.run()
    print(f"[BFS] The length of path {len(path)}")


if __name__ == '__main__':
    main()
