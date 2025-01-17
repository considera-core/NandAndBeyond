using System.Text;

namespace NandAndBeyond.Core.Chips;

public class Nand : Chip
{
    // Fields
    private string? _out;
    
    // Properties
    private string InA => Ins[0];

    private string InB => Ins[1];
    
    public string Out => IsResult 
        ? Outs[0] 
        : _out ??= $"{InA}{InB}{Id}";

    public override string InParams => $"\tIN {InA}, {InB};";
    
    public override string OutParams => $"\tOUT {Out};";

    // Constructors
    public Nand(
        string inA = "a",
        string inB = "b",
        string outA = "out",
        int arraySize = 0,
        bool isArray = false,
        bool isResult = false,
        bool debug = false)
    {
        Name = "Nand";
        Ins = [inA, inB];
        Outs = [outA];
        ArraySize = arraySize;
        IsArray = isArray;
        IsResult = isResult;
        Debug = debug;
    }
    
    // Methods
    public override string Print()
    {
        var sb = new StringBuilder();
        var suffix = IsArray ? ArraySize.ToString() : "";
        if (Debug) sb.AppendLine($"\t// Nand{suffix}(A: {InA}, B: {InB}) => {Out}");
        sb.AppendLine($"\tNand{suffix}(a={InA}, b={InB},out={Out});");
        return sb.ToString();
    }

    public static void Make()
    {
        new Nand().Make(true);
        new Nand().Make(false);
    }

    private void Make(bool debug) => 
        base.Make(debug, new Nand(isResult: true, debug: debug));
}