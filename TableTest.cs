using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace TestProject2;

public class TableTest: Page
{
    
    public TableTest(WebDriver driver)
    {
        Driver = driver;

    }
    
    private String name;
    private String position;
    private String office;
    private int age;
    private DateTime startDate;
    private int salary;

    private TableTest(String _name,  String _position,String _office,int _age, DateTime _startDate, int _salary)
    {
        this.name = _name;
        this.position = _position;
        this.office = _office;
        this.age = _age;
        this.startDate = _startDate;
        this.salary = _salary;
    }
   
}