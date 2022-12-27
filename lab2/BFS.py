from pyamaze import maze,agent,COLOR,textLabel
def BFS_alg(m):
    start_cell = (m.rows,m.cols)
    frontier = [start_cell]
    explored = [start_cell]
    bfsPath = {}
    while len(frontier) > 0:
        current = frontier.pop(0)
        if current == (1, 1):
            break
        for d in 'ESNW':
            if m.maze_map[current][d]==True:
                if d == 'E':
                    children = (current[0], current[1]+1)
                elif d == 'W':
                    children = (current[0], current[1]-1)
                elif d == 'N':
                    children = (current[0]-1, current[1])
                elif d == 'S':
                    children = (current[0]+1, current[1])
                if children in explored:
                    continue
                frontier.append(children)
                explored.append(children)
                bfsPath[children] = current
    fwdPath = {}
    cell=(1, 1)
    while cell != start_cell:
        fwdPath[bfsPath[cell]] = cell
        cell = bfsPath[cell]
    return fwdPath