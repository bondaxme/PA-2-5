from pyamaze import COLOR
from algorithm import *

def main():
    size = int(input("Maze size [2 - 20]: "))
    while size < 2 or size > 20:
        size = int(input("Maze size [2 - 20]: "))

    alg_choose = input("Enter the algorithm you want to use [BFS or Astar]: ")
    while not (alg_choose == "BFS" or alg_choose == "Astar"):
        alg_choose = input("Enter the correct value [BFS or Astar]: ")

    maze_ = maze(size, size)
    maze_.CreateMaze(loopPercent=40)

    path, iterations_counter, states_amount = call_algorithm(maze_, alg_choose)

    a = agent(maze_, shape="arrow", footprints=True, color=COLOR.yellow)
    maze_.tracePath({a: path}, delay=40)
    maze_.run()
    print(f"[{alg_choose}] The length of path {len(path)}, the number of iterations: {iterations_counter}, the amount of unique states: {states_amount}")

if __name__ == '__main__':
    main()
