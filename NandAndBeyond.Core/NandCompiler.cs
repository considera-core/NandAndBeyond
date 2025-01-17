using NandAndBeyond.Core.Chips;

namespace NandAndBeyond.Core;

public abstract class NandCompiler
{
    public static string GetOutputFile(string name, bool debug) =>
        Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName 
        + $"/out/{name}.{(debug ? "debug." : "")}hdl";
    
    private static void Main()
    {
        And.Make();
        And16.Make();
        DMux.Make();
        Mux.Make();
        Mux16.Make();
        Nand.Make();
        Not.Make();
        Not16.Make();
        Or.Make();
        Or16.Make();
        Xor.Make();
    }
}