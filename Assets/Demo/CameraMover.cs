using UnityEngine;

namespace JobsUtils.Demo
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float patternChangeTime;

        private float actualTime;
        private bool patternFlag;
        
        private void Update()
        {
            Vector3 dir = patternFlag ?  transform.right : -transform.right;
            transform.position += dir * moveSpeed * Time.deltaTime;
        }


        private void FixedUpdate()
        {
            if (actualTime <= 0)
            {
                patternFlag = !patternFlag;
                actualTime = patternChangeTime;
            }
            
            actualTime -= Time.fixedDeltaTime;
        }
    }
}
