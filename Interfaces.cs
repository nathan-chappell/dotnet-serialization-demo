interface IBase
{
    string X { get; set; }
}

class Base
{
    public string X { get; set; }
}

interface IDerived : IBase
{
    string Y { get; set; }
}

class Derived : Base, IDerived
{
    public string Y { get; set; }
    public Derived(string x, string y)
    {
        X = x;
        Y = y;
    }
}