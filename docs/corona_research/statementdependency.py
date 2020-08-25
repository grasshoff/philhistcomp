from dataclasses import dataclass, field
from typing import List, Dict

from tabulate import tabulate
from graphviz import Digraph

@dataclass(unsafe_hash=True)
class Factor:
    '''
    Factors are vertices in a causal graph with edges for a relevance relationship. Edges connect factors to their relevant causes 
    or they are root factors which only can be changed or invoked by interventions i.e. causally relevant events.
    
    The fields of Factors

    - name
        - is an integer as a unique name for that factor, negative integer for negative factors
    - factortype
        - cat: categorial factors (2 states change when effected)
        - negative factors: the string of a name may be negative to express "not Factor"
    - junction: List
        - A list of and-causes representing an and-or bundle of relevant causes for this factor
        - Root factors have an empty junction
        - [[a1,a2,a3],[b1,b2]] is a bundle of two and-causes
    - state: str 
        - the state of the factor
    '''
    name: int
    junction: List = field(default_factory=list)
    factortype: str = "cat"
    state: str = "off"


@dataclass(unsafe_hash=True)
class StatementGraph:
    '''
    A directed graph of relevances (and-or junctions). 
    It is represented as a dictionary of relevances named by numbers for factors

    - junction: list of lists
        - A disjunction (list) of conjunctions (list) for a statement
        - Root statement may have an empty junction
        - example: [[a1,a2,a3],[b1,b2]] 
        - negative statements are represented by negative factor names
    '''
    name: str
    minimalTheory: Dict = None
    factorspace: List = field(default_factory=list)
    state: Dict = None

    def __post_init__(self):
        relevance = self.minimalTheory
        d = {}
        des = {}
        for j in relevance.keys():
            disjunct = relevance.get(j)
            d[j] = disjunct
            for conj in disjunct:
                for fact in conj:
                    des[fact] = Factor(fact).state
                    if fact in relevance:
                        d[fact] = relevance[fact]
                    else:
                        d[abs(fact)] = []
        self.minimalTheory = d
        self.factorspace = [Factor(f, junction=d[f]) for f in d]
        self.len = len(self.factorspace)
        self.state = des

# Identify an object of class Factor by name


def Fac(y, causG):
    l = [x for x in causG.factorspace if x.name == abs(y)][0]
    return(l)
# set the state of a Factor


def statefac(o, causG):
    for i in range(causG.len):
        Fac(i, causG).state = o



def truthval(f,causG):
    if Fac(f,causG).state == "on":
        sf = True
    else:
        sf = False
    if f < 0:
        sf = (not sf)
    return(sf)


def sufficientC(f, causG):
    '''
    sufficientC is True if the states of a causal graph causG are sufficient for a  
    factors f. The condition is derived from the minimal theory MT of a factor f 
    
    (1) Categorial condition: 
        It is sufficient, if for a minimal theory MT of f at least one disjunct D is True, i.e. all positiv cofact are "on" and negated cofacts "off",
    '''
    truthdisj = False
    MT = causG.minimalTheory[f]
    if MT == []:
        truthdisj = False
    else:
        for disjunct in MT:
            #            print(f"\n==== for factor {f}\nDisjunct={disjunct}")
            truthconj = True
            for conjunct in disjunct:
                truthconj = truthconj and truthval(conjunct,causG)
#                print(f"conjunct {conjunct} state= {Fac(f).state} is {truthval(conjunct)} mit truthconj={truthconj}")
            truthdisj = truthdisj or truthconj
#            print(f"Disjunct von {f} ergibt: {truthdisj}")
    return(truthdisj)

def causalProcess(events, causG):
    '''
    events is a list of factors for change
    - (1) event from events is changed to "on" and the effect propagates through the causal graph. 
    - (2) all factors of MT are checked whether the current state of the graph is sufficient for the factor to be changed and put on event list 
    - (3) if part of if suffient for fact
    - (4) then promote  state of fact
    '''

    MT = list(causG.minimalTheory.keys())
    MT = [abs(x) for x in MT]
    while events != []:
        while events != []:  # second loop to invoke all events of the list at once
            event = abs(events.pop(0))  # (1)
            Fac(event,causG).state = "on"
            MT.remove(event)
        for potF in MT:  # (2)
            scon = sufficientC(potF, causG)
            if scon:  # (3)
                events.append(potF)
                events = list(set(events))


def dotCausG(causG,argdict=[]):
    '''
    returns a dot graph object g
    
    print(g.source)
    
    lists the source for a Markdown
    '''
    graph_label = 'Argument visualizer'

    graph_attr = {
        'rankdir': 'LR',
        'labelloc': 't',
        'fontsize': '20',
        'label': graph_label
    }
    node_attr = {"shape": "box"}
    g = Digraph(comment='Rule based links visualization',
                node_attr=node_attr, graph_attr=graph_attr)
    g.attr(size='10')
    mt = causG.minimalTheory
  #  mtt = mt[f]
    lk = list(mt.keys())
    lttt = [l for l in lk if mt[l] != []]
    for f in lttt:
        zdisj = 0
        mtt = mt[f]
        for disj in mtt:
            zdisj += 1
            ldisj = len(disj)
            for conj in disj:
                g.node(str(abs(conj)), str(conj))
                if ldisj == 1:
                    g.edge(str(abs(conj)), str(abs(f)))
                else:
                    g.edge(str(abs(conj)), str(abs(f)), samehead=str(zdisj))

    return(g)

def showCausG(g):
    tablist = []
    for (k, v) in g.minimalTheory.items():
        s = Fac(k,g).state
        tablist.append([k, s, v])
    print(tabulate(tablist, headers=[
          "Factor", "State", "MT"], tablefmt="simple"))
