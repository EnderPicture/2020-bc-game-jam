using UnityEngine;
public class deskCollide : MonoBehaviour {
    void update() {

    }
    
    void OnTriggerStay(Collision other) {
        if( other.gameObject.layer == LayerMask.NameToLayer("Bullet") ) {
            Debug.Log("Bullet should be dead now");
            other.gameObject.GetComponent<BulletController>().die();
        }
    } // OnCollisionEnter
} // deskCollide