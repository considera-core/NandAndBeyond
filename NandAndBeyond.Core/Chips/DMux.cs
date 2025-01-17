using System.Text;

namespace NandAndBeyond.Core.Chips;

public class DMux : Chip
{
    // Fields
    private Not? _notSel;
    
    private And? _resultA;
    
    private And? _resultB;
    
    private string? _outA;
    
    private string? _outB;
    
    // Properties
    private string InIn => Ins[0];

    private string InSel => Ins[1];
    
    public string OutA => IsResult 
        ? Outs[0] 
        : _outA ??= ResultA.Out;
    
    public string OutB => IsResult 
        ? Outs[1] 
        : _outB ??= ResultB.Out;
    
    public override string InParams => $"\tIN {InIn}, {InSel};";

    public override string OutParams => $"\tOUT {OutA}, {OutB};";
    
    private Not NotSel => _notSel ??= new(
        @in: InSel, 
        debug: Debug);
    
    private And ResultA => _resultA ??= new(
        inA: NotSel.Out, 
        inB: InIn, 
        outA: OutA, 
        isResult: IsResult, 
        debug: Debug);
    
    private And ResultB => _resultB ??= new(
        inA: InSel, 
        inB: InIn, 
        outA: OutB, 
        isResult: IsResult, 
        debug: Debug);
    
    // Constructors
    public DMux(
        string inA = "in",
        string inB = "sel",
        string outA = "a",
        string outB = "b",
        int arraySize = 0,
        bool isArray = false,
        bool isResult = false,
        bool debug = false)
    {
        Name = "DMux";
        Ins = [inA, inB];
        Outs = [outA, outB];
        IsResult = isResult;
        Debug = debug;
    }

    // Methods
    public override string Print()
    {
        var sb = new StringBuilder();
        if (Debug) sb.AppendLine($"\t// Dmux(In: {InIn}, Sel: {InSel}) => [{OutA}, {OutB}]");
        sb.AppendLine($"\t{NotSel.Print()}");
        sb.AppendLine($"\t{ResultA.Print()}");
        sb.AppendLine($"\t{ResultB.Print()}");
        return sb.ToString();
    }

    public static void Make()
    {
        new DMux().Make(true);
        new DMux().Make(false);
    }

    private void Make(bool debug) => 
        base.Make(debug, new DMux(isResult: true, debug: debug));

}