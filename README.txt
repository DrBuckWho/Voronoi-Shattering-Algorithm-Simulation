# Voronoi Algorithm Overview

The Voronoi algorithm is used to partition a space into regions based on proximity to a set of input points. Its purpose is to efficiently divide space into distinct areas, where each area encompasses all the points that are closer to a specific input point than to any other point in the set. This algorithm finds applications in various fields, including computer graphics, geographical analysis, pattern recognition, and robotics, where spatial organization or proximity-based analysis is required. It's particularly useful in problems involving spatial clustering, nearest neighbor searches, and spatial interpolation.

## Algorithm Steps

- **Input:** Set of points P = {p1, p2, ..., pn}

1. **Initialization:** Initialize an empty list of Voronoi cells or regions.

2. **Edge Calculation:**
   - For each point pi in P:
       - Initialize an empty list of edges.
       - For each point pj in P where j ≠ i:
           - Calculate the perpendicular bisector of the line segment connecting pi and pj.
           - Add the bisector to the list of edges.

3. **Vertex Calculation:**
   - For each pair of adjacent edges:
       - Find their intersection point.
       - Add this point to the list of vertices of the corresponding Voronoi cell.

4. **Cell Creation:**
   - For each point pi in P:
       - Create a Voronoi cell using the vertices obtained in step 3.
       - Add this cell to the list of Voronoi cells.

5. **Output:** Output the list of Voronoi cells.

# Voronoi Diagram Implementation in Unity

This project demonstrates a Voronoi diagram generator implemented in Unity using C#.

## Cell Separation

The Voronoi diagram separates the grid into regions based on the proximity to a set of randomly generated points. Each cell represents a region of influence around its corresponding point.

## Interactive Glass Breaking

- Left-clicking on the diagram simulates glass breaking by destroying the Voronoi cells. Upon destruction, the plane breaks apart creating an effect similar to shattered glass.

## Implementation Details

- **Rendering:** The project utilizes Unity's RawImage component to render the Voronoi diagram on a canvas.
- **Representation:** Voronoi cells are represented as customized meshes.
- **Dynamism:** Random movement is applied to the shattered pieces, adding dynamism to the simulation.

## Usage

- Left-click on the diagram to trigger the glass-breaking effect.
