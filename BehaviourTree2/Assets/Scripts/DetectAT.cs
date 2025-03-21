using JetBrains.Annotations;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections.Generic;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class DetectAT : ActionTask {

		
		public float detectRadius;
		public LayerMask detectionMask;

		public BBParameter<Transform> target;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
            detection();
            //EndAction(true);
        }

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			

        }

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}

		private void detection()
		{
            

            Transform bestTarget = null;
            float closestDistance = detectRadius;
			
			Collider[] detections = Physics.OverlapSphere(agent.transform.position, detectRadius, detectionMask);
			
			foreach (Collider detection in detections)
			{
                float currentDistance = Vector3.Distance(agent.transform.position, detection.transform.position);
                //Transform enemyLocation = detections.transform.position;
                //enemyList.Add(detections.transform);
				if( currentDistance <closestDistance)
				{
					bestTarget = detection.transform;
					closestDistance = currentDistance;
				}

			}
			if(bestTarget !=null)
			{
				target.value = bestTarget;
                EndAction(true);
                Debug.DrawLine(agent.transform.position, bestTarget.position, Color.red);
            }
			else
			{
				EndAction(false);
			}
			
		}

	}
}