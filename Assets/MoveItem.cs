using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MoveItem;
using static UnityEditor.Progress;

public class MoveItem : MonoBehaviour
{
    public enum weightType {light, heavy};
    public enum item { Kebab, PosterFiddle, Laptop, Plant, Dog, Cat, CoffeeMug, Chair, Acuarium, Guitar, Pan, Ball, Book, Microwave, Sofa, TV, OfficeTable, LockedSafe, Lamp };

    [SerializeField]
    weightType type;
    [SerializeField]
    item items;
    [SerializeField]
    private int score; //score total, de momento

    private void Start()
    {
        items = (item)Random.Range(0, 18); //de momento
        type = (weightType)Random.Range(0, 1); //de momento
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Truck"))
        {
            Score();
            Destroy(gameObject);
        }
    }

    private void Score( )
    {
        switch(items)
        {
            case item.Cat:
                score += 10;
                break;
            case item.TV:
                score += 50;
                break;
            case item.Chair:
                score += 5;
                break;
            case item.Dog:
                score += 15;
                break;
            case item.Acuarium:
                score += 40;
                break;
            case item.Ball:
                score += 8;
                break;
            case item.Book:
                score += 20;
                break;
            case item.CoffeeMug:
                score += 13;
                break;
            case item.Guitar:
                score += 25;
                break;
            case item.Kebab:
                score += 60;
                break;
            case item.Lamp:
                score += 27;
                break;
            case item.Laptop:
                score += 55;
                break;
            case item.LockedSafe:
                score += 70;
                break;
            case item.Microwave:
                score += 65;
                break;
            case item.OfficeTable:
                score += 70;
                break;
            case item.Pan:
                score += 35;
                break;
            case item.Plant:
                score += 45;
                break;
            case item.PosterFiddle:
                score += 100;
                break;
            case item.Sofa:
                score += 80;
                break;
            default:
                break;          
        }
        if(type == weightType.heavy)
        {
            score += 100;
        }
        else
        {
            score += 50;
        }
    }

}
