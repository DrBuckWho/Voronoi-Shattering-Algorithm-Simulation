
Payne Clark

This is a usage of a complex algorithm from this research Paper (https://www.researchgate.net/publication/318208318_EXAMINING_THE_USE_OF_VORONOI_DIAGRAMS_IN_ARCHITECTURE_ON_A_STUDENT_PROJECT)
I mostly taught myself the research paper is just to show you what it is.

This Lab meets the requirements for the research alg and the glass breaking simulation

The code isnt too long but this took me well over 40 hours to do as the full fledge alg is insanley complex and I had to keep doing simplifications 
as my math knowledge and mesh knowledge was not up to snuff

I wanted to do this in 3d however the proffessor said a 2d implementation would be enough

Voronoi Diagram Implementation in Unity:
This project demonstrates a Voronoi diagram generator implemented in Unity using C#.
The Voronoi diagram is created on a grid, where each cell is assigned a random color from a predefined set.

Cell Separation:
The Voronoi diagram separates the grid into regions based on the proximity to a set of randomly generated points.
Each cell represents a region of influence around its corresponding point.

Interactive Glass Breaking:
Left-clicking on the diagram simulates glass breaking by destroying the Voronoi cells.
Upon destruction, the plane breaks apart creating an effect similar to shattered glass.

Implementation Details:
The project utilizes Unity's RawImage component to render the Voronoi diagram on a canvas.
Voronoi cells are represented as Customized meshes.
Random movement is applied to the shattered pieces, adding dynamism to the simulation.

Usage:

Left-click on the diagram to trigger the glass-breaking effect.
