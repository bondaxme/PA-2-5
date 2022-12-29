import os
import psutil as psutil
import func_timeout
from pyamaze import maze,agent,textLabel
from queue import PriorityQueue
from time import time

def h(cell_a: tuple, cell_b: tuple) -> int: #manhattan distance
    return abs(cell_a[0] - cell_b[0]) + abs(cell_a[1] - cell_b[1])

def call_algorithm(maze, algorithm_name): # with time limit
    if algorithm_name == "BFS":
        return func_timeout.func_timeout(60 * 30, BFS, args=[maze])
    else:
        return func_timeout.func_timeout(60 * 30, Astar, args=[maze])

def neighbour_check(direction, current): #find neighbour
    if direction == 'E':
        children = (current[0], current[1] + 1)
    if direction == 'W':
        children = (current[0], current[1] - 1)
    if direction == 'N':
        children = (current[0] - 1, current[1])
    if direction == 'S':
        children = (current[0] + 1, current[1])
    return children

def Astar(m):
    iterations_counter = 0
    states_amount = 0
    states = []
    final_path = {}
    a_path = {}

    start_cell = (m.rows, m.cols)

    g_score = {cell: float('inf') for cell in m.grid}
    g_score[start_cell] = 0

    f_score = {cell: float('inf') for cell in m.grid}
    f_score[start_cell] = h(start_cell, (1, 1))

    queue = PriorityQueue()
    queue.put((h(start_cell, (1, 1)), h(start_cell, (1, 1)), start_cell))

    while not queue.empty():
        if psutil.Process(os.getpid()).memory_info().rss > 1024 ** 3:
            raise MemoryError("1 Gb memory exceeded")
        iterations_counter += 1
        current = queue.get()[2]
        if current == (1, 1):
            states_amount = len(states)
            break

        if current not in states:
            states.append(current)

        for direction in 'ESNW':
            if m.maze_map[current][direction]:
                children = neighbour_check(direction, current)

                temp_g_score = g_score[current] + 1
                temp_f_score = temp_g_score + h(children, (1, 1))

                if temp_f_score < f_score[children]:
                    g_score[children] = temp_g_score
                    f_score[children] = temp_f_score
                    queue.put((temp_f_score, h(children, (1, 1)), children))
                    a_path[children] = current
    cell = (1, 1)
    while cell != start_cell:
        final_path[a_path[cell]] = cell
        cell = a_path[cell]
    return final_path, iterations_counter, states_amount

def BFS(m):
    iterations_counter = 0
    states = []
    states_amount = 0
    final_path = {}
    bfs_path = {}

    start_cell = (m.rows, m.cols)
    front_cells = [start_cell]
    passed_list = [start_cell]

    while len(front_cells) != 0:
        if psutil.Process(os.getpid()).memory_info().rss > 1024 ** 3:
            raise MemoryError("1 Gb memory exceeded")
        iterations_counter += 1
        current = front_cells.pop(0)
        if current == (1, 1):
            states_amount = len(states)
            break
        if current not in states:
            states.append(current)
        for direction in 'ESNW':
            if m.maze_map[current][direction]:
                children = neighbour_check(direction, current)
                if children in passed_list:
                    continue
                front_cells.append(children)
                passed_list.append(children)
                bfs_path[children] = current
    cell = (1, 1)
    while cell != start_cell:
        final_path[bfs_path[cell]] = cell
        cell = bfs_path[cell]
    return final_path, iterations_counter, states_amount