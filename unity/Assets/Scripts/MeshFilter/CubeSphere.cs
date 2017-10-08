/**
 * IMPORTANT
 * 
 * BASED ON SOURCECODE FROM 
 * Jasper Flick 
 * http://catlikecoding.com/unity/tutorials/mesh-deformation/
 * 
 * We modified the code to fit our needs.
 * 
 **/
using UnityEngine;

namespace MagicTool 
{
	[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
	public class CubeSphere : MonoBehaviour 
	{
		[Header("Arm Controller")]
		public int grid = 20;
		public float radius = 1f;

		private Mesh mesh;
		private Vector3[] vertices;
		private Vector3[] normals;
		private Color32[] cubeUV;

		private void Awake () 
		{
			this.mesh = new Mesh();
			this.GetComponent<MeshFilter> ().mesh = this.mesh; 
			this.mesh.name = "Cube Sphere (procedural)";

			CreateVertices();
			CreateTriangles();

			gameObject.AddComponent<SphereCollider>();
		}

		private void CreateVertices () 
		{
			int cornerVertices = 8;
			int edgeVertices = (grid + grid + grid - 3) * 4;
			int faceVertices = (
				(grid - 1) * (grid - 1) +
				(grid - 1) * (grid - 1) +
				(grid - 1) * (grid - 1)) * 2;
			vertices = new Vector3[cornerVertices + edgeVertices + faceVertices];
			normals = new Vector3[vertices.Length];
			cubeUV = new Color32[vertices.Length];

			int v = 0;
			for (int y = 0; y <= grid; y++) {
				for (int x = 0; x <= grid; x++) {
					SetVertex(v++, x, y, 0);
				}
				for (int z = 1; z <= grid; z++) {
					SetVertex(v++, grid, y, z);
				}
				for (int x = grid - 1; x >= 0; x--) {
					SetVertex(v++, x, y, grid);
				}
				for (int z = grid - 1; z > 0; z--) {
					SetVertex(v++, 0, y, z);
				}
			}
			for (int z = 1; z < grid; z++) {
				for (int x = 1; x < grid; x++) {
					SetVertex(v++, x, grid, z);
				}
			}
			for (int z = 1; z < grid; z++) {
				for (int x = 1; x < grid; x++) {
					SetVertex(v++, x, 0, z);
				}
			}

			mesh.vertices = vertices;
			mesh.normals = normals;
			mesh.colors32 = cubeUV;
		}

		private void SetVertex (int i, int x, int y, int z) 
		{
			Vector3 v = new Vector3(x, y, z) * 2f / grid - Vector3.one;
			float x2 = v.x * v.x;
			float y2 = v.y * v.y;
			float z2 = v.z * v.z;
			Vector3 s;
			s.x = v.x * Mathf.Sqrt(1f - y2 / 2f - z2 / 2f + y2 * z2 / 3f);
			s.y = v.y * Mathf.Sqrt(1f - x2 / 2f - z2 / 2f + x2 * z2 / 3f);
			s.z = v.z * Mathf.Sqrt(1f - x2 / 2f - y2 / 2f + x2 * y2 / 3f);
			normals[i] = s;
			vertices[i] = normals[i] * radius;
			cubeUV[i] = new Color32((byte)x, (byte)y, (byte)z, 0);
		}

		private void CreateTriangles () 
		{
			int[] trianglesZ = new int[(grid * grid) * 12];
			int[] trianglesX = new int[(grid * grid) * 12];
			int[] trianglesY = new int[(grid * grid) * 12];

			int ring = (grid + grid) * 2;
			int tZ = 0, tX = 0, tY = 0, v = 0;

			for (int y = 0; y < grid; y++, v++) {
				for (int q = 0; q < grid; q++, v++) {
					tZ = SetQuad(trianglesZ, tZ, v, v + 1, v + ring, v + ring + 1);
				}
				for (int q = 0; q < grid; q++, v++) {
					tX = SetQuad(trianglesX, tX, v, v + 1, v + ring, v + ring + 1);
				}
				for (int q = 0; q < grid; q++, v++) {
					tZ = SetQuad(trianglesZ, tZ, v, v + 1, v + ring, v + ring + 1);
				}
				for (int q = 0; q < grid - 1; q++, v++) {
					tX = SetQuad(trianglesX, tX, v, v + 1, v + ring, v + ring + 1);
				}
				tX = SetQuad(trianglesX, tX, v, v - ring + 1, v + ring, v + 1);
			}

			tY = CreateTopFace(trianglesY, tY, ring);
			tY = CreateBottomFace(trianglesY, tY, ring);

			var container = new int[trianglesZ.Length + trianglesX.Length + trianglesY.Length];
			trianglesZ.CopyTo(container, 0);
			trianglesX.CopyTo(container, trianglesZ.Length);
			trianglesY.CopyTo(container, trianglesZ.Length + trianglesX.Length);

			mesh.SetTriangles(container, 0);
		}

		private int CreateTopFace (int[] triangles, int t, int ring) 
		{
			int v = ring * grid;
			for (int x = 0; x < grid - 1; x++, v++) {
				t = SetQuad(triangles, t, v, v + 1, v + ring - 1, v + ring);
			}
			t = SetQuad(triangles, t, v, v + 1, v + ring - 1, v + 2);

			int vMin = ring * (grid + 1) - 1;
			int vMid = vMin + 1;
			int vMax = v + 2;

			for (int z = 1; z < grid - 1; z++, vMin--, vMid++, vMax++) {
				t = SetQuad(triangles, t, vMin, vMid, vMin - 1, vMid + grid - 1);
				for (int x = 1; x < grid - 1; x++, vMid++) {
					t = SetQuad(
						triangles, t,
						vMid, vMid + 1, vMid + grid - 1, vMid + grid);
				}
				t = SetQuad(triangles, t, vMid, vMax, vMid + grid - 1, vMax + 1);
			}

			int vTop = vMin - 2;
			t = SetQuad(triangles, t, vMin, vMid, vTop + 1, vTop);
			for (int x = 1; x < grid - 1; x++, vTop--, vMid++) {
				t = SetQuad(triangles, t, vMid, vMid + 1, vTop, vTop - 1);
			}
			t = SetQuad(triangles, t, vMid, vTop - 2, vTop, vTop - 1);

			return t;
		}

		private int CreateBottomFace (int[] triangles, int t, int ring) 
		{
			int v = 1;
			int vMid = vertices.Length - (grid - 1) * (grid - 1);
			t = SetQuad(triangles, t, ring - 1, vMid, 0, 1);
			for (int x = 1; x < grid - 1; x++, v++, vMid++) {
				t = SetQuad(triangles, t, vMid, vMid + 1, v, v + 1);
			}
			t = SetQuad(triangles, t, vMid, v + 2, v, v + 1);

			int vMin = ring - 2;
			vMid -= grid - 2;
			int vMax = v + 2;

			for (int z = 1; z < grid - 1; z++, vMin--, vMid++, vMax++) {
				t = SetQuad(triangles, t, vMin, vMid + grid - 1, vMin + 1, vMid);
				for (int x = 1; x < grid - 1; x++, vMid++) {
					t = SetQuad(
						triangles, t,
						vMid + grid - 1, vMid + grid, vMid, vMid + 1);
				}
				t = SetQuad(triangles, t, vMid + grid - 1, vMax + 1, vMid, vMax);
			}

			int vTop = vMin - 1;
			t = SetQuad(triangles, t, vTop + 1, vTop, vTop + 2, vMid);
			for (int x = 1; x < grid - 1; x++, vTop--, vMid++) {
				t = SetQuad(triangles, t, vTop, vTop - 1, vMid, vMid + 1);
			}
			t = SetQuad(triangles, t, vTop, vTop - 1, vMid, vTop - 2);

			return t;
		}

		private static int SetQuad (int[] triangles, int i, int v00, int v10, int v01, int v11) 
		{
			triangles[i] = v00;
			triangles[i + 1] = triangles[i + 4] = v01;
			triangles[i + 2] = triangles[i + 3] = v10;
			triangles[i + 5] = v11;
			return i + 6;
		}
	}
}