/**
 * MIT License
 *
 * Copyright (c) 2017 Mo Kakwan, Uwe Gruenefeld, Helena Deus, Michal Leszczynski, Alisha Haris
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 **/
using UnityEngine;
using System;

namespace MagicTool
{
	public class Pointer : MonoBehaviour 
	{
		[HeaderAttribute("Reference to Controller")]
		public GameObject controller;

		[HeaderAttribute("Deformable Object")]
		public GameObject sphere;

		private Deformer deformer;

		/// <summary>
		/// Start is called once at startup
		/// </summary>
		void Start()
		{
			this.deformer = this.sphere.GetComponent<Deformer> ();
		}

		/// <summary>
		/// Update is called once per frame by Unity
		/// </summary>
		void Update() 
		{
			if (!deformer)
				return;

			// Calculate the distance
			float distance = Vector3.Distance (sphere.transform.position, gameObject.transform.GetChild(0).position);

			// Based on distance create force feedback
			if (distance < sphere.GetComponent<CubeSphere> ().radius * sphere.transform.localScale.x) {
				((NetworkController)controller.GetComponent<NetworkController> ()).Request (100, 100, 100);
				//Debug.Log ("100");
			} else {
				((NetworkController)controller.GetComponent<NetworkController> ()).Request (0, 0, 0);
				//Debug.Log ("0");
			}

			//Debug.Log (distance);

			// NEW APPROACH: Add force from the tip of the tool
			deformer.AddDeformingForce(gameObject.transform.GetChild(0).position, 15f);

			// OLD APPROACH: Finding intersection over RayCast (working but unefficient)
			/*Vector3 forward = -transform.TransformDirection (sphere.transform.position);
			Debug.DrawRay (this.gameObject.transform.position, forward, Color.red);
			Ray ray = new Ray(this.gameObject.transform.position, forward);
			RaycastHit hit;

			SphereCollider collider = this.sphere.GetComponent<SphereCollider> ();

			if (!collider)
				return;
	
			if(collider.Raycast(ray, out hit, Int32.MaxValue))
			{
				MeshDeformer deformer = sphere.GetComponent<MeshDeformer> ();
				if (deformer) {
					Vector3 point = hit.point * 2f;
					point += hit.normal * 0.1f;

					Debug.Log (point);

					deformer.AddDeformingForce(point, 15f);
				}
			}
			*/
		}
	}
}
	