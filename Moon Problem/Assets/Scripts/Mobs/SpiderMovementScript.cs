using UnityEngine;

public class SpiderMovementScript : Character
{
    public override void Start()
    {
        base.Start();
        gameObject.SetActive(false);
    }
}
