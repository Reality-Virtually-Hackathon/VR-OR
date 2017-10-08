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
	[RequireComponent(typeof(MeshFilter))]
	public class Deformer : MonoBehaviour 
	{
		public float springForce = 20f;
		public float damping = 5f;

		private Mesh deformingMesh;
		private Vector3[] originalVertices, displacedVertices, vertexVelocities;
		float uniformScale = 1f;

		private void Start () 
		{
			deformingMesh = GetComponent<MeshFilter>().mesh;
			originalVertices = deformingMesh.vertices;
			displacedVertices = new Vector3[originalVertices.Length];
			for (int i = 0; i < originalVertices.Length; i++) {
				displacedVertices[i] = originalVertices[i];
			}
			vertexVelocities = new Vector3[originalVertices.Length];
		}

		private void Update () 
		{
			uniformScale = transform.localScale.x;
			for (int i = 0; i < displacedVertices.Length; i++) {
				UpdateVertex(i);
			}
			deformingMesh.vertices = displacedVertices;
			deformingMesh.RecalculateNormals();
		}

		private void UpdateVertex (int i) 
		{
			Vector3 velocity = vertexVelocities[i];
			Vector3 displacement = displacedVertices[i] - originalVertices[i];
			displacement *= uniformScale;
			velocity -= displacement * springForce * Time.deltaTime;
			velocity *= 1f - damping * Time.deltaTime;
			vertexVelocities[i] = velocity;
			displacedVertices[i] += velocity * (Time.deltaTime / uniformScale);
		}

		public void AddDeformingForce (Vector3 point, float force) 
		{
			point = transform.InverseTransformPoint(point);

			for (int i = 0; i < displacedVertices.Length; i++) 
			{
				AddForceToVertex(i, point, force);
			}
		}

		private void AddForceToVertex (int i, Vector3 point, float force) 
		{
			Vector3 pointToVertex = displacedVertices[i] - point;
			pointToVertex *= uniformScale;

			float attenuatedForce = force / (1f + pointToVertex.sqrMagnitude);
			float velocity = attenuatedForce * Time.deltaTime;
			vertexVelocities[i] += pointToVertex.normalized * velocity;
		}
	}
}