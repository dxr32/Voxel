using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();
    List<Vector2> UVs = new List<Vector2>();

    Mesh CubeMesh;

    void Start()
    {
        GenerateCube();
    }

    void GenerateCube()
    {
        CubeMesh = new Mesh();

        CreateCubeSide("front");
        CreateCubeSide("back");
        CreateCubeSide("top");
        CreateCubeSide("bottom");
        CreateCubeSide("left");
        CreateCubeSide("right");

        GeneratePhysicalCube();
    }

    void CreateCubeSide(string side)
    {
        triangles.Add(0 + vertices.Count);
        triangles.Add(1 + vertices.Count);
        triangles.Add(2 + vertices.Count);

        triangles.Add(0 + vertices.Count);
        triangles.Add(2 + vertices.Count);
        triangles.Add(3 + vertices.Count);

        UVs.Add(new Vector2(1, 1));
        UVs.Add(new Vector2(0, 1));
        UVs.Add(new Vector2(0, 0));
        UVs.Add(new Vector2(1, 0));

        Vector3 V0 = new Vector3(0.5f, -0.5f, 0.5f);
        Vector3 V1 = new Vector3(0.5f, 0.5f, 0.5f);
        Vector3 V2 = new Vector3(-0.5f, 0.5f, 0.5f);
        Vector3 V3 = new Vector3(-0.5f, -0.5f, 0.5f);
        Vector3 V4 = new Vector3(-0.5f, -0.5f, -0.5f);
        Vector3 V5 = new Vector3(-0.5f, 0.5f, -0.5f);
        Vector3 V6 = new Vector3(0.5f, 0.5f, -0.5f);
        Vector3 V7 = new Vector3(0.5f, -0.5f, -0.5f);

        switch (side)
        {
            case "front":
                vertices.Add(V0);
                vertices.Add(V1);
                vertices.Add(V2);
                vertices.Add(V3);
            break;

            case "back":
                vertices.Add(V4);
                vertices.Add(V5);
                vertices.Add(V6);
                vertices.Add(V7);
            break;

            case "top":
                vertices.Add(V1);
                vertices.Add(V6);
                vertices.Add(V5);
                vertices.Add(V2);
                break;

            case "bottom":
                vertices.Add(V7);
                vertices.Add(V0);
                vertices.Add(V3);
                vertices.Add(V4);
            break;

            case "left":
                vertices.Add(V7);
                vertices.Add(V6);
                vertices.Add(V1);
                vertices.Add(V0);
            break;

            case "right":
                vertices.Add(V3);
                vertices.Add(V2);
                vertices.Add(V5);
                vertices.Add(V4);
            break;
        }
    }

    void GeneratePhysicalCube()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        MeshRenderer mr = GetComponent<MeshRenderer>();

        CubeMesh.vertices = vertices.ToArray();
        CubeMesh.triangles = triangles.ToArray();
        CubeMesh.uv = UVs.ToArray();

        CubeMesh.RecalculateBounds();
        CubeMesh.RecalculateNormals();

        mf.mesh = CubeMesh;
    }
}
