Problem Statement    	
You are given two line segments on the plane. Each segment is parallel to either the X axis or the Y axis. Your task is to figure out how the segments intersect and return one of the following strings:
"NO", if the segments do not intersect
"POINT", if the segments' intersection forms a point
"SEGMENT", if the segments' intersection forms a line segment
The segments will be given as two int[]s s1 and s2. Each of them will contain four integers x1, y1, x2, y2 (in that order) where (x1, y1), (x2, y2) are segment endpoints. 

Definition
    	
Method signature:	String intersection(int[] s1, int[] s2)

Constraints
-	 Each of s1 and s2 will contain exactly four elements
-	 All integers in s1 and s2 will be between -1000 and 1000, inclusive
-	 Each segment will be parallel to either the X axis or the Y axis
 
Examples
1)    	
{0, 0, 0, 1}	
{1, 0, 1, 1}
Returns: "NO"
The segments are parallel and there is no intersection.

2)    	
{0, 0, 0, 1}
{0, 1, 0, 2}
Returns: "POINT"
The segments are located on the same line and have only one common point (0,1).

3)    	
{0, -1, 0, 1}
{-1, 0, 1, 0}
Returns: "POINT"
The segments intersect at point (0,0).

4)    	
{0, 0, 2, 0}
{1, 0, 10, 0}
Returns: "SEGMENT"
The segments have a common line segment from (1,0) to (2,0).

5)    	
{5, 5, 5, 5}
{6, 6, 6, 6}
Returns: "NO"
These are two different points.

6)    	
{10, 0, -10, 0}
{5, 0, -5, 0}
Returns: "SEGMENT"
The segments have a common line segment from (-5,0) to (5,0).
