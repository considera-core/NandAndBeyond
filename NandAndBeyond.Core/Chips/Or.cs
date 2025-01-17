using System.Text;

namespace NandAndBeyond.Core.Chips;

public class Or : Chip
{
    // Fields
    private Nand? _left;
    
    private Nand? _right;
    
    private Nand? _result;
    
    // Properties
    private string InA => Ins[0];

    private string InB => Ins[1];
    
    public string Out => Result.Out;

    public override string InParams => $"\tIN {InA}, {InB};";
    
    public override string OutParams => $"\tOUT {Outs[0]};";
    
    private Nand Left => _left ??= new Nand(
        inA: InA, 
        inB: InA, 
        debug: Debug);

    private Nand Right => _right ??= new Nand(
        inA: InB, 
        inB: InB, 
        debug: Debug);

    private Nand Result => _result ??= new(
        inA: Left.Out, 
        inB: Right.Out, 
        outA: Outs[0],
        isResult: IsResult, 
        debug: Debug);
    
    // Constructors
    public Or(
        string inA = "a",
        string inB = "b",
        string outA = "out",
        int arraySize = 0,
        bool isArray = false,
        bool isResult = false,
        bool debug = false)
    {
        Name = "Or";
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
        if (Debug) sb.AppendLine($"\t// Or(A: {InA}, B: {InB}) => {Out}");
        sb.AppendLine($"\t{Left.Print()}");
        sb.AppendLine($"\t{Right.Print()}");
        sb.AppendLine($"\t{Result.Print()}");

        return sb.ToString();
    }

    public static void Make()
    {
        new Or().Make(true);
        new Or().Make(false);
    }

    private void Make(bool debug) => 
        base.Make(debug, new Or(isResult: true, debug: debug));
}