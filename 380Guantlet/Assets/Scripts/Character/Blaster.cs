using System.Collections;
using UnityEngine;

namespace Character
{
    public class Blaster : MonoBehaviour
    {
        private void Update()
        {
            BlasterActive();
        }

        public void BlasterActive()
        {
            transform.Translate(new Vector3(0, -5 * Time.deltaTime, 0));
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Destroy(collision.gameObject);
                StartCoroutine(ResetBlaster());
                Destroy(this.gameObject);
            }
        }

        IEnumerator ResetBlaster()
        {
            yield return new WaitForSeconds(2);
        }
    }
}