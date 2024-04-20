using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoronoiDiagram : MonoBehaviour
{
    [SerializeField] private Color[] PossibleColors; // Array to store possible colors
    private int imgSize; // Size of the image
    public Canvas canvasToDelete; 
    private int gridSize = 5; // Size of the grid
    private int pixelsPerCell; // Number of pixels per grid cell
    private RawImage image; // Reference to the RawImage component

    private Vector2Int[,] pointPositions; // Array to store positions of grid points
    private Color[,] colors; // Array to store colors for each grid point

    private void Awake()
    {
        image = GetComponent<RawImage>(); // Get the RawImage component
        imgSize = Mathf.RoundToInt(image.GetComponent<RectTransform>().sizeDelta.x); // Calculate the image size
        PossibleColors = new Color[] { Color.red, Color.blue, Color.green, Color.yellow }; // Initialize possible colors array
        GenerateDiagram(); // Generate the Voronoi diagram
    }
    void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0)) // 0 represents the left mouse button, 1 for right, and 2 for middle
        {
            // Log vertices of each cell
            LogCellVertices();
        }
    }
    private void GenerateDiagram()
{
    Texture2D texture = new Texture2D(imgSize, imgSize); // Create a new texture
    texture.filterMode = FilterMode.Point; // Set texture filter mode to Point for pixelated look

    pixelsPerCell = imgSize / gridSize; // Calculate the number of pixels per grid cell
    GeneratePoints(); // Generate random points and colors for the grid

    // Loop through each pixel in the image
    for (int i = 0; i < imgSize; i++)
    {
        for (int j = 0; j < imgSize; j++)
        {
            float nearestDistance = Mathf.Infinity; // Initialize nearest distance to positive infinity
            Vector2Int nearestPoint = new Vector2Int(); // Initialize nearest point

            // Iterate through neighboring grid cells
            for (int a = -1; a < 2; a++)
            {
                for (int b = -1; b < 2; b++)
                {
                    int gridX = i / pixelsPerCell; // Calculate X index of the current grid cell
                    int gridY = j / pixelsPerCell; // Calculate Y index of the current grid cell

                    int X = gridX + a; // Calculate X index of the neighboring grid cell
                    int Y = gridY + b; // Calculate Y index of the neighboring grid cell

                    // Skip if the neighboring grid cell is out of bounds
                    if (X < 0 || Y < 0 || X >= gridSize || Y >= gridSize) continue;

                    float distance = Vector2Int.Distance(new Vector2Int(i, j), pointPositions[X, Y]); // Calculate distance to the grid point
                    // Update nearest point if the distance is smaller
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance; // Update nearest distance
                        nearestPoint = new Vector2Int(X, Y); // Update nearest point
                    }
                }
            }

            // Set the pixel color to the color of the nearest grid point
            texture.SetPixel(i, j, colors[nearestPoint.x, nearestPoint.y]);
        }
    }

    // Rest of the method remains unchanged...

    texture.Apply(); // Apply changes to the texture
    image.texture = texture; // Assign the texture to the RawImage component


}


    // Generate random points and colors for the grid
    private void GeneratePoints()
    {
        pointPositions = new Vector2Int[gridSize, gridSize]; // Initialize array to store grid point positions
        colors = new Color[gridSize, gridSize]; // Initialize array to store colors for each grid point

        int colorCount = PossibleColors.Length; // Get the total number of colors available

        // Loop through each grid cell
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                // Generate a random position within the current grid cell
                pointPositions[i, j] = new Vector2Int(i * pixelsPerCell + Random.Range(0, pixelsPerCell),
                                                      j * pixelsPerCell + Random.Range(0, pixelsPerCell));

                // Assign a random color from the PossibleColors array to the current grid cell
                colors[i, j] = new Color(Random.value, Random.value, Random.value);            }
        }
    }
    private void LogCellVertices()
    {
        // Loop through each grid cell
        for (int i = 0; i < gridSize -1 ; i++)
        {
            for (int j = 0; j < gridSize -1; j++)
            {
                // Calculate vertices of the current Voronoi cell
                Vector2Int bottomLeft = pointPositions[i, j];
                Vector2Int bottomRight = pointPositions[i + 1, j];
                Vector2Int topRight = pointPositions[i + 1, j + 1];
                Vector2Int topLeft = pointPositions[i, j + 1];
                DrawSquare(bottomLeft,bottomRight,topRight,topLeft);
                // Log vertices of the cell
                Debug.Log("Cell " + i + ", " + j + " Vertices: " + bottomLeft + ", " + bottomRight + ", " + topRight + ", " + topLeft);
            }
        }
    }
void DrawSquare(Vector2Int bottomLeft, Vector2Int bottomRight, Vector2Int topRight, Vector2Int topLeft)
{
    // Create a new Mesh
    Mesh mesh = new Mesh();

    // Vertices
    Vector3[] vertices = new Vector3[4];
    float shift=-80;
    float shiftDown=40;
    // Convert Vector2Int points to Vector3 vertices (assuming z-coordinate is 0)
    vertices[0] = new Vector3(bottomLeft.x + shift, bottomLeft.y -shiftDown, 0); // bottomLeft
    vertices[1] = new Vector3(bottomRight.x + shift, bottomRight.y-shiftDown, 0); // bottomRight
    vertices[2] = new Vector3(topRight.x + shift, topRight.y-shiftDown, 0); // topRight
    vertices[3] = new Vector3(topLeft.x + shift, topLeft.y-shiftDown, 0); // topLeft

    // Scale up the vertices by a factor of 100
    for (int i = 0; i < vertices.Length; i++)
    {
        vertices[i] *= 100;
    }

    // Triangles
    int[] triangles = new int[6] { 0, 2, 1, 0, 3, 2 }; // Corrected triangle order

    // UVs
    Vector2[] uv = new Vector2[4];
    uv[0] = new Vector2(0, 0);
    uv[1] = new Vector2(1, 0);
    uv[2] = new Vector2(1, 1);
    uv[3] = new Vector2(0, 1);

    // Assign vertices, triangles, and UVs to the mesh
    mesh.vertices = vertices;
    mesh.triangles = triangles;
    mesh.uv = uv;

    // Create a new GameObject and MeshRenderer
    GameObject obj = new GameObject("Square");
    MeshRenderer renderer = obj.AddComponent<MeshRenderer>();
    MeshFilter filter = obj.AddComponent<MeshFilter>();
    filter.mesh = mesh;
    Rigidbody2D rb = obj.AddComponent<Rigidbody2D>();
    rb.gravityScale = 0; // Disable gravity for the Rigidbody2D

    RandomMovement movementScript = obj.AddComponent<RandomMovement>();


    // Set the GameObject's parent to the canvas
   // obj.transform.SetParent(canvasToDelete.transform, false);

    // Generate random material
    Material material = new Material(Shader.Find("Standard"));
    material.color = Random.ColorHSV(); // Random color
    renderer.material = material;
    
    string objectNameToDelete = "Canvas"; // Specify the name of the GameObject to delete
    // Find the GameObject with the specified name
    GameObject objToDelete = GameObject.Find(objectNameToDelete);
    Destroy(objToDelete);
}


}
