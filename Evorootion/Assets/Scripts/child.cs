using UnityEngine;

public class child : parent
{
    protected override void Awake()
    {
        //base.Awake();

        print(name + ": child awake");
    }
}
