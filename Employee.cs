using System;
using HtmlAgilityPack;

namespace TestProject2;

public class Employee
{
    public string Name { get; }
        
    public string Position { get; }
        
    public string Office { get; }
    
    public string Age { get; }

    public string Salary { get; }

   
    public Employee(string name, string position,string office, string age,  string salary)
    {
        Name = name;
        Position = position;
        Office = office;
        Age = age;
        Salary = salary;
    }
}