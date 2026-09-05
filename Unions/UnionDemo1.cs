
public class Union1Demo
{

    public static void RunDemo()
    {

        var somePets = new Pet[]{
            new Dog("Rex"),
            new Cat(7),
            new Parrot(true)
        };

        foreach (var pet in somePets)
        {
            Console.WriteLine(pet.Description);
        }
    }

}


public record Dog(string Name);
public record Cat(int NumberOfLives);
public record Parrot(bool WantsCrackers);


union Pet(Dog, Cat, Parrot)
{
    public string Description => this switch
    {
        Cat cat => $"Meow! I got {cat.NumberOfLives} lives left",
        Dog dog => $"Bark Bark! My name is {dog.Name}",
        Parrot parrot => $"Squawk! {(parrot.WantsCrackers ? "I want crackers!" : "Give us a kiss!")}"
    };
}



