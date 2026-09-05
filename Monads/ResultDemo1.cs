public static class ResultDemo1
{
    public static void RunDemo()
    {

        Queue<Result<int>> results = new Queue<Result<int>>();

        results.Enqueue(new Success<int>(42));
        results.Enqueue(new Success<int>(118));
        results.Enqueue(new Error("Sorry, something went terribly wrong"));

        while (results.TryDequeue(out var result))
        {
            int data = result switch
            {
                Success<int> s => s.data,
                Error(var m) => throw new InvalidOperationException(m)
            };
            Console.WriteLine(data);           
        }  
    }

    public record Success<T>(T data);

    public record Error(string errorMessage);

    public union Result<T>(Success<T>, Error);


}