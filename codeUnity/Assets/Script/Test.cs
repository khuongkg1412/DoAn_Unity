using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct ObjectTest
{
    public int a;
    public ObjectTest(int a)
    {
        this.a = a;
    }
    public void add()
    {
        a += 1;
    }
    public int returnA()
    {
        return a;
    }

}

public class ObjectTest2
{
    public int a;
    public ObjectTest2(int a)
    {
        this.a = a;
    }
    public int retrun()
    {
        return a;
    }
}
public class Test : MonoBehaviour
{
    public Text text;
    public Button reset, damage;
    ObjectTest2 a;
    ObjectTest b;

    ObjectTest c;
    private void Start()
    {
        a = new ObjectTest2(1);

        b = new ObjectTest(a.retrun());
        c = new ObjectTest(b.returnA());


    }
    void resetNumber()
    {

    }

    public void damageBTN()
    {
        Debug.Log("Goooo");
        c.a += 1;
        Debug.Log("c" + c.returnA());
        Debug.Log("B" + b.returnA());
    }
}
