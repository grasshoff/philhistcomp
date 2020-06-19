# -*- coding: utf-8 -*-
"""
Spyder Editor

This is a temporary script file.
"""
#Necessary packages
import osmnx as ox
import networkx as nx
import geopandas 
import matplotlib.pyplot as plt
import pandas as pd

#Import data frame, adding 'New York City' to every field in Address-column
df = pd.read_excel('citybenchlocationsv03.xlsx')
df['Address'] = df['Address'] + ', New York City'
df.head(10)

#Subset original data set
addressborough = df[['Address', 'Borough']]
addressborough.head(10)

#Proof of Concept: ten city benches in Manhattan
addressmanhattan = addressborough[addressborough['Borough'] == 'Manhattan']
addressmanhattanh = addressmanhattan.head(10) #Limited to 10 entries

#Adding a column with Geocodes to df
addressmanhattang = []
for i in addressmanhattanh['Address']:
    addressmanhattang.append(ox.geocode(i))

addressmanhattanh.insert(2, 'Geocode', addressmanhattang, True)

#Choosing a graph that contains all ten addresses (suboptimal solution)
G = ox.graph_from_point((40.74619633333333, -73.983977), network_type = 'walk', distance = 5000, simplify = False)

#Adding a column with Nodes to df
addressmanhattann = []
for i in addressmanhattanh['Geocode']:
    addressmanhattann.append(ox.get_nearest_node(G, i))    
    
addressmanhattanh.insert(3, 'Node', addressmanhattann, True)

#Printing the extended df 
addressmanhattanh

#Loop for distance calculation (suboptimal)
distance = []
for i in addressmanhattanh['Node']:
    for j in addressmanhattanh['Node']:
        distance.append(nx.shortest_path_length(G, i, j, weight = 'length'))

distancewd = dict.fromkeys(distance)
print("The shortest distance [in m] between two benches:")
print(sorted(distancewd)[1])
distancewd

example = {'key1': [1,2,3]}


