using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{

	public Transform player;
	public GameController gc;
	public int itemId;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(0) && Time.timeScale != 0f)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit) && (raycastHit.transform.gameObject & Vector3.Distance(player.position, base.transform.position) < 5f))
			{
				gc.CollectItem(itemId);
				this.gameObject.SetActive(false);
			}
		}
	}
}
