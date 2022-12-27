from pyamaze import maze,agent,textLabel
from queue import PriorityQueue

def h(cell_a: tuple, cell_b: tuple) -> int:
    return abs(cell_a[0] - cell_b[0]) + abs(cell_a[1] - cell_b[1])\

def aStar(m):
    start_cell=(m.rows,m.cols)
    g_score={cell:float('inf') for cell in m.grid}
    g_score[start_cell]=0
    f_score={cell:float('inf') for cell in m.grid}
    f_score[start_cell]=h(start_cell,(1,1))

    open=PriorityQueue()
    open.put((h(start_cell,(1,1)),h(start_cell,(1,1)),start_cell))
    aPath={}
    while not open.empty():
        current=open.get()[2]
        if current==(1,1):
            break
        for d in 'ESNW':
            if m.maze_map[current][d]==True:
                if d=='E':
                    children=(current[0],current[1]+1)
                if d=='W':
                    children=(current[0],current[1]-1)
                if d=='N':
                    children=(current[0]-1,current[1])
                if d=='S':
                    children=(current[0]+1,current[1])

                temp_g_score=g_score[current]+1
                temp_f_score=temp_g_score+h(children,(1,1))

                if temp_f_score < f_score[children]:
                    g_score[children]= temp_g_score
                    f_score[children]= temp_f_score
                    open.put((temp_f_score,h(children,(1,1)),children))
                    aPath[children]=current
    fwdPath={}
    cell=(1,1)
    while cell!=start_cell:
        fwdPath[aPath[cell]]=cell
        cell=aPath[cell]
    return fwdPath