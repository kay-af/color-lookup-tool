# -*- coding: utf-8 -*-
"""
Created on Sat Jul 25 01:37:03 2020
Color lookup tool implementation in python.
@author: AFRIDI KAYAL
"""

import csv
import os
import kdtree

COLORS_CSV_PATH = os.path.join(os.path.dirname(__file__), "..", "resources", "colors.csv")

class color_info(object):
    def __init__(self, red : int, green : int, blue : int, name : str):
        self.color = (red, green, blue)
        self.name = name

    def __len__(self):
        return len(self.color)

    def __getitem__(self, i):
        return self.color[i]

    def __repr__(self):
        return 'Color({}, {}, {}) => {}'.format(self.color[0], self.color[1], self.color[2], self.name)
    
    def to_hex(self):
        red = self.color[0]
        green = self.color[1]
        blue = self.color[2]
        h = (red << 16) | (green << 8) | blue
        hexa = hex(h)
        padded = '0x' + hexa[2:].zfill(6)
        return padded
    
    def to_dict(self):
        return {"color": {"red": self.color[0], "green": self.color[1], "blue": self.color[2]}, "hex": self.to_hex(), "name": self.name }
    

class color_lookup:
    
    def __init__(self):
        colors_arr = []
        with open(COLORS_CSV_PATH) as colors_csv:
            reader = csv.reader(colors_csv, delimiter=',')
            line_count = 0
            for row in reader:
                if line_count == 0:
                    line_count += 1
                    continue
                else:
                    colors_arr.append(row)
        self.kd = kdtree.create(list(map(self._transform, colors_arr)))
        
    def _transform(self, color_data):
        red = int(color_data[2])
        green = int(color_data[3])
        blue = int(color_data[4])
        name = color_data[0]
        return color_info(red, green, blue, name)
    
    def match(self, color : iter, num : int = 1):
        assert len(color) == 3, "Color iterable must be of length 3 (Red, Green, Blue)"
        matches = self.kd.search_knn(color, k = num)
        colors = []
        for match in matches:
            colors.append(match[0].data)
        return colors