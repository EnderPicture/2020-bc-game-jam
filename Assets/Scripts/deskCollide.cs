using UnityEngine;
public class deskCollide : MonoBehaviour {
    void update() {

    }
    
    void OnTriggerStay(Collider other) {
        if( other.gameObject.layer == LayerMask.NameToLayer("Bullet") ) {
            other.gameObject.GetComponent<BulletController>().die();
        }
    } // OnCollisionEnter
} // deskCollide