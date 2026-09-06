
using System.Runtime.CompilerServices;

/// <summary>
/// Demonstrates discovering the case types of a C# union.
/// </summary>
public class Union1Demo
{

    /// <summary>
    /// Runs the union case discovery and pattern-matching examples.
    /// </summary>
    public static void RunDemo()
    {
        Console.WriteLine($"Pet union cases: {string.Join(", ", UnionExtensions.GetCaseTypes<Pet>().Select(type => type.Name))}");

        var somePets = new Pet[]{
            new Dog("Rex"),
            new Cat(7, "Whiskers"),
            new Parrot(true, "Polly"),
            new GoldFish(true, "Timmy")
        };

        foreach (var pet in somePets)
        {
            Console.WriteLine(pet.Description);    
        }

        Console.WriteLine();
        Console.WriteLine("Listing all pet union case types:\n-------------------------------------------");
        foreach (var caseType in typeof(Pet).GetUnionCaseTypes())
        {
            Console.Write($"* {caseType.FullName}");
            Console.WriteLine($" with props: {string.Join(", ", caseType.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public).Select(p => p.Name))}");
        }

    }

}


public record Dog(string Name);
public record Cat(int NumberOfLives, string? Name = default);
public record Parrot(bool WantsCrackers, string? Name = default);
public record GoldFish(bool MakesBubbles, string? Name = default);


union Pet(Dog, Cat, Parrot, GoldFish)
{

    public string Description => this switch
    {
        Cat cat => $"{cat.Name} says: Meow! I got {cat.NumberOfLives} lives left 🐈",
        Dog dog => $"Bark Bark! {dog.Name} says! 🦴 🐕",
        Parrot parrot => $"🦜Squawk! {parrot.Name} says: {(parrot.WantsCrackers ? "I want crackers!" : "Give us a kiss!")}",
        GoldFish goldFish => $"🐠 {goldFish.Name} says: Blub Blub! {(goldFish.MakesBubbles ? "I make bubbles!" : "I don't make bubbles!")}"
    };
}



/// <summary>
/// Provides reflection-based helpers for C# unions.
/// </summary>
public static class UnionExtensions
{
    /// <summary>
    /// Retrieves the union case types from generic type reference (compile-time checked)
    /// </summary>
    /// <typeparam name="TUnion">The union type to inspect.</typeparam>
    /// <returns>The distinct case types exposed by the union constructors.</returns>
    public static IReadOnlyList<Type> GetCaseTypes<TUnion>() where TUnion : IUnion
    {
        return GetUnionCaseTypes(typeof(TUnion));
    }

    /// <summary>
    /// Retrieves the union case types from a runtime type reference.
    /// </summary>
    /// <param name="t">The union type to inspect.</param>
    /// <returns>The distinct case types exposed by the union constructors.</returns>
    public static IReadOnlyList<Type> GetUnionCaseTypes(this Type t){

            if (t == null || !typeof(IUnion).IsAssignableFrom(t)){
                return Array.Empty<Type>(); //guard against null or non-union types, just return an empty result in this case
            }
  
            return t
                .GetConstructors(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                .Where(constructor => constructor.GetParameters().Length == 1)
                .Select(constructor => constructor.GetParameters()[0].ParameterType)
                .Distinct()
                .ToArray();
    }
   
}



