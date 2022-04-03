using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToTarget : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject arrow;
    [SerializeField] private float borderSize;

    private Vector3 dir;
    private float angle;
    private Vector3 viewportPos;
    private Vector3 cappedPos;

    void Update()
    {
        //transform.position = player.transform.position;

        viewportPos = Camera.main.WorldToViewportPoint(target.position);
        if ((viewportPos.x < 0 || viewportPos.x > 1) || (viewportPos.y < 0 || viewportPos.y > 1))
        {
            arrow.SetActive(true);
        } else
        {
            arrow.SetActive(false);
        }

        dir = target.position - transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        cappedPos = viewportPos;
        cappedPos.x = Mathf.Clamp(cappedPos.x, borderSize, 1 - borderSize);
        cappedPos.y = Mathf.Clamp(cappedPos.y, borderSize, 1 - borderSize);
        transform.position = Camera.main.ViewportToWorldPoint(cappedPos);
    }
}
