using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Ingridient
/// </summary>

public class Ingridient
{
    string ingredientName;
    int quantity;
    string unit;
    
    
    public Ingridient( string ingred,int quant, string uom)
    {
        ingredientName = ingred;
        quantity = quant;
        unit = uom;
    }
    public string IngredientName
    {
        get { return ingredientName; }
        set { ingredientName = value; }
    }

    public int Quantity
    {
        get { return quantity; }
        set { quantity = value; }
    }

    public string UnitofMeasures
    {
        get { return unit; }
        set { unit = value; }
    }

    //public List<Ingridient> makeIngredient()
    //{
    //    List<Ingridient> myList = new List<Ingridient>();
    //    myList.Add(new Ingridient("Carrot", 1.5, "KG"));
    //    myList.Add(new Ingridient("Sugar", 1.0, "temp"));
    //    myList.Add(new Ingridient("Carrot", 1.5, "KG"));
    //    myList.Add(new Ingridient("Sugar", 1.0, "temp"));
    //    myList.Add(new Ingridient("Carrot", 1.5, "KG"));
    //    myList.Add(new Ingridient("Sugar", 1.0, "temp"));
    //    return myList;

    //    //}
    //    //
    //    // TODO: Add constructor logic here
    //    //
}
