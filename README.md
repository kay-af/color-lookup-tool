
# Color Lookup Tool

A simple but efficient library for detecting color name from given **RGB** values using **KdTree**.

## Usage:

	using ColorLookupTool;
	
	// Call once in the beginning
	ColorLookup.Initialize();
  
	string colorName1 = ColorLookup.Match("#ff5624");
	string colorName2 = ColorLookup.Match(88, 123, 44); // RGB
	string colorName3 = ColorLookup.Match(0xffffff); // Integer form

## Thanks to:

 - An efficient KD-Tree implementation for .Net can be found here:
   [eregina92/KdTree](https://github.com/eregina92/Supercluster.KDTree)
    
 - Dataset for colors with their names obtained from:
   [Dataset by Dilumr](https://data.world/dilumr/color-names)
