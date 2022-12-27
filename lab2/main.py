from pyamaze import COLOR

from algorithms import *



def main():
    size = int(input("Maze size [2 - 20]: "))
    while size < 2 or size > 20:
        size = int(input("Maze size [2 - 20]: "))

    alg_choose = input("Enter the algorithm you want to use [BFS or Astar]: ")
    while not (alg_choose == "BFS" or alg_choose == "Astar"):
        alg_choose = input("Enter the correct value [BFS or Astar]: ")

    maze_ = maze(size, size)
    maze_.CreateMaze(loopPercent=40)

    if alg_choose == "BFS":
        path = aStar(maze_)
    else:
        path = BFS_alg(maze_)

    a = agent(maze_, shape="arrow", footprints=True, color=COLOR.red)
    maze_.tracePath({a: path}, delay=50)
    maze_.run()
    print(f"[{alg_choose}] The length of path {len(path)}")

if __name__ == '__main__':
    main()
