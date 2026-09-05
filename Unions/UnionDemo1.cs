
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
        Console.WriteLine($"Pet union cases: {string.Join(", ", Pet.CaseTypes.Select(type => type.Name))}");

        var somePets = new Pet[]{
            new Dog("Rex"),
            new Cat(7, "Whiskers"),
            new Parrot(true, "Polly")
        };

        foreach (var pet in somePets)
        {
            Console.WriteLine(pet.Description);    
        }

        Console.WriteLine();
        Console.WriteLine("Listing all pet union case types:");
        foreach (var caseType in UnionExtensions.GetCaseTypes<Pet>())
        {
            Console.Write(caseType.FullName);
            Console.WriteLine($" with props: {string.Join(", ", caseType.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public).Select(p => p.Name))}");
        }

    }

}


public record Dog(string Name);
public record Cat(int NumberOfLives, string? Name = default);
public record Parrot(bool WantsCrackers, string? Name = default);


union Pet(Dog, Cat, Parrot)
{
    public static IReadOnlyList<Type> CaseTypes { get; } = UnionExtensions.GetCaseTypes<Pet>();

    public string Description => this switch
    {
        Cat cat => $"Meow! I got {cat.NumberOfLives} lives left",
        Dog dog => $"Bark Bark! My name is {dog.Name}",
        Parrot parrot => $"Squawk! {(parrot.WantsCrackers ? "I want crackers!" : "Give us a kiss!")}"
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



