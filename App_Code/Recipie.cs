using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RecipieFetcher
/// </summary>
public class Recipie
{

    public Recipie()
    {  

       // TODO: Add constructor logic here
       
    }
    public string recipieName;
    public string RecipieName
    {
        get { return recipieName; }
        set { recipieName = value; }
    }

    public string submittedBy;
    public string SubmittedBy
    {
        get { return submittedBy; }
        set { submittedBy = value; }
    }
  
    public string cookingTime;
    public string CookingTime
    {
        get { return cookingTime; }
        set { cookingTime = value; }
    }
   



}