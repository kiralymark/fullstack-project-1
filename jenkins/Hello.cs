using System;

class Hello {

  public static void Main(String[] args)
  {

    Hello myObj = new Hello();

    foreach (var arg in args)
    {
      myObj.PrintWelcomeMessage(arg);
    }
    
  }

  public void PrintWelcomeMessage(string name="World")
  {
      Console.WriteLine("Hello " + name + "!");
  
  }

}
