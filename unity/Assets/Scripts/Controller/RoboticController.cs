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
using System.Collections;
using System.Collections.Generic;

namespace MagicTool
{
	public class RoboticController : MonoBehaviour 
	{
		[Header("Arm Controller")]
		public float angle1;
		public float angle2;
		public float angle3;

		// GameObjects for parts
		private GameObject armA, armB;
		private GameObject armA_anchor, armB_anchor;

		/// <summary>
		/// Start is called once at startup
		/// </summary>
		void Start () 
		{
			this.armA = GameObject.Find ("ArmA");
			this.armB = GameObject.Find ("ArmB");

			this.armA_anchor = GameObject.Find ("ArmA_Anchor");
			this.armB_anchor = GameObject.Find ("ArmB_Anchor");
		}

		/// <summary>
		/// Update is called once per frame by Unity
		/// </summary>
		void Update () 
		{
			this.armA_anchor.transform.rotation = Quaternion.Euler (angle2, angle1, 0);
			this.armB_anchor.transform.rotation = Quaternion.Euler (angle3, 0, 0);
		}
	}
}
