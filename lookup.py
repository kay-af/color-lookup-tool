# -*- coding: utf-8 -*-
"""
Created on Sat Jul 25 10:13:52 2020

@author: AFRIDI KAYAL
"""

import json
import argparse
from scripts.color_lookup import color_lookup

class result_container:
    def __init__(self, status: str, result = None):
        self.status = status
        self.result = result
        
    def to_dict(self):
        return {"status": self.status, "result": self.result}

def convert_to_json(obj):
    return json.dumps(obj, default = lambda o: o.to_dict())

if __name__ == "__main__":
    
    parser = argparse.ArgumentParser(description = "Get nearest color matches from a given color")
    parser.add_argument("--k", help="Number of matches required", type=int, default=1)
    parser.add_argument("red", type=int, help="The red value 0-255")
    parser.add_argument("green", type=int, help="The green value 0-255")
    parser.add_argument("blue", type=int, help="The blue value 0-255")
    
    try:
        args = parser.parse_args()
        
        if args.red < 0 or args.green < 0 or args.blue < 0 or args.red > 255 or args.green > 255 or args.blue > 255 or args.k < 1:
            raise Exception()

        tool = color_lookup()
        color = (args.red, args.green, args.blue)
        k = args.k
        
        result = tool.match(color, num=k)
        print(convert_to_json(result_container("success", result = result)))
    except:
        print(convert_to_json(result_container("fail", result = "invalid arguments")))