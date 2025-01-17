using System.Text;

namespace NandAndBeyond.Core.Chips;

public class Not : Chip
{
    // Fields
    private Nand? _result;
    
    // Properties
    private string In => Ins[0];
    
    public string Out => Result.Out;

    public override string InParams => $"\tIN {Ins[0]};";
    
    public override string OutParams => $"\tOUT {Outs[0]};";
    
    private Nand Result => _result ??= new Nand(
        inA: Ins[0], 
        inB: Ins[0], 
        outA: Outs[0], 
        arraySize: ArraySize,
        isArray: IsArray,
        isResult: IsResult, 
        debug: Debug);

    // Constructors
    public Not(
        string @in = "in",
        string @out = "out",
        int arraySize = 0,
        bool isArray = false,
        bool isResult = false,
        bool debug = false)
    {
        Name = "Not";
        Ins = [@in];
        Outs = [@out];
        ArraySize = arraySize;
        IsArray = isArray;
        IsResult = isResult;
        Debug = debug;
    }
    
    // Methods
    public override string Print()
    {
        var sb = new StringBuilder();
        if (Debug) sb.AppendLine($"\t// Not(A: {In}) => {Out}");
        sb.AppendLine($"\t{Result.Print()}");

        return sb.ToString();
    }

    public static void Make()
    {
        new Not().Make(true);
        new Not().Make(false);
    }

    private void Make(bool debug) => 
        base.Make(debug, new Not(isResult: true, debug: debug));
}