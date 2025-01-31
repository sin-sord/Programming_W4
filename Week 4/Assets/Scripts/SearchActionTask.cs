using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.Collections.LowLevel.Unsafe;

namespace NodeCanvas.Tasks.Actions{

	public class SearchActionTask : ActionTask{

		private NavMeshAgent navAgent;
		public float searchRange;
		float distance;
		float maxDistance;
		

		protected override string OnInit(){

			navAgent = agent.GetComponent<NavMeshAgent>();
			return null;
		}

		protected override void OnExecute() {
			//Choose a random destination on the navmesh

			//  finds a random spot on the NavMesh
			Vector3 randomPosition = Random.insideUnitSphere * searchRange + agent.transform.position;

			//  will find the closest point and label it as hit
			NavMeshHit hit;

            //  will preform the search starting at: origin, when hit, the range, on all masks
            if (!NavMesh.SamplePosition(randomPosition, out hit, searchRange, NavMesh.AllAreas))  //  if not point is found then stop the action
            {
				EndAction(false);
				return;
			}


            //Set the path to that position
            navAgent.SetDestination(hit.position);

		}

		protected override void OnUpdate(){
            //When they have arrived then end the task

            // once we've gotten closer to the target and a path is planned..
            if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
            {
				//  target is found, end action 
				EndAction(true);
			}
		}

		//Called when the task is disabled.
		protected override void OnStop(){
			
		}

		//Called when the task is paused.
		protected override void OnPause(){
			
		}
	}
}