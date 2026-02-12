public class Student
{
    private int _id;
    private string _firstName;
    private string _lastName;

    public int ID => _id;
    public string FirstName => _firstName;
    public string LastName => _lastName;

    public Student(int id, string firstName, string lastName)
    {
        _id = id;
        _firstName = firstName;
        _lastName = lastName;
    }

    ~Student()
    {
    }

    public void Update(int id, string name, string lastName)
    {
        _id = id;
        _firstName = name;
        _lastName = lastName;
    }
}

