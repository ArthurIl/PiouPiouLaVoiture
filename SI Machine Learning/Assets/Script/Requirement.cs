using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Requirement : MonoBehaviour, IComparable<Requirement>
{
    public List<int> listOfInt;
    public int[] arrayOfInt;

    public int[][] jaggedArray2dOfInt;
    public int[][][] jaggedArray3dOfInt;

    public int value;

    public int CompareTo(Requirement other)
    {
        if(value < other.value)
        {
            return 1;
        }

        if(value > other.value)
        {
            return -1;
        }

        return 0;
    }

    void Start()
    {
        TestList();
        TestArray();
        TestJaggedArray2D();
        TestJaggedArray3D();
        TestRaycast();
    }
    public LayerMask layerMask;

    void TestRaycast()
    {
        Vector3 origin = Vector3.zero;
        Vector3 direction = Vector3.up;
        float lenght = 2;

        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, lenght, layerMask))
        {
            Debug.DrawRay(origin, direction*hit.distance, Color.green);
        }
        else
        {
            Debug.DrawRay(origin, direction * lenght, Color.red);
        }
    }


    void TestJaggedArray3D()
    {

            jaggedArray3dOfInt = new int[4][][];

            for (int x = 0; x < jaggedArray3dOfInt.Length; x++)
            {
                jaggedArray3dOfInt[x] = new int[4][];

                for (int y = 0; y < jaggedArray3dOfInt[x].Length; y++)
                {
                    jaggedArray3dOfInt[x][y] = new int[4];

                    for (int z = 0; z < jaggedArray3dOfInt[x][y].Length; z++)
                    {
                        jaggedArray3dOfInt[x][y][z] = 3;

                        Debug.Log(jaggedArray3dOfInt[x][y][z]);
                    }
                }
            }
    }

    void TestList()
    {
        listOfInt = new List<int>();

        listOfInt.Add(99);
        listOfInt.Add(95);

        int myInt = 123;

        listOfInt.Add(myInt);

        listOfInt.RemoveAt(1);
        listOfInt = new List<int>();

        listOfInt.Add(4);
        listOfInt.Add(2);
        listOfInt.Add(3);
        listOfInt.Add(1);

        listOfInt.Sort();

        for (int x = 0; x < listOfInt.Count; x++)
        {
            Debug.Log(listOfInt[x]);
        }

        Debug.Log(listOfInt[0]);
        Debug.Log(listOfInt[listOfInt.Count - 1]);
    }

    void TestArray()
    {
        arrayOfInt = new int[4];
        arrayOfInt[0] = 3;
        arrayOfInt[1] = 0;
        arrayOfInt[2] = 2;
        arrayOfInt[3] = 1;

        for (int x = 0; x < arrayOfInt.Length; x++)
        {
            Debug.Log(arrayOfInt[x]);
        }
    }

    void TestJaggedArray2D()
    {
        int count = 1;
        jaggedArray2dOfInt = new int[4][]; //nombre de colone

        /*jaggedArray2dOfInt[0] = new int[4]; // nombre de cases dans ma première colone
        jaggedArray2dOfInt[1] = new int[2];
        jaggedArray2dOfInt[2] = new int[4];
        jaggedArray2dOfInt[3] = new int[2];*/

        for (int x = 0; x < jaggedArray2dOfInt.Length; x++)
        {
            jaggedArray2dOfInt[x] = new int[x + 1];
            Debug.Log(jaggedArray2dOfInt[x].Length);

            for (int y = 0; y < jaggedArray2dOfInt[x].Length; y++)
            {
                jaggedArray2dOfInt[x][y] = count;
                count++;

            }
        }
    }

}
