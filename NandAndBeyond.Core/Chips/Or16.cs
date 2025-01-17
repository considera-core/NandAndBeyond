using System.Text;

namespace NandAndBeyond.Core.Chips;

public class Or16 : Chip
{
    // Fields
    private Nand? _left;
    
    private Nand? _right;
    
    private Nand? _result;
    
    private string? _outA;
    
    private string? _outB;

    private string _inA;

    private string _inB;
    
    // Properties
    private string InAs => Ins[0];

    private string InBs => Ins[1];
    
    public string OutAs => IsResult 
        ? Outs[0] 
        : _outA ??= Result.Out;
    
    public override string InParams => $"\tIN {InAs}, {InBs};";

    public override string OutParams => $"\tOUT {OutAs};";
    
    private Nand Left => _left ??= new Nand(
        inA: _inA, 
        inB: _inA, 
        arraySize: ArraySize,
        isArray: IsArray,
        debug: Debug);

    private Nand Right => _right ??= new Nand(
        inA: _inB, 
        inB: _inB, 
        arraySize: ArraySize,
        isArray: IsArray,
        debug: Debug);

    private Nand Result => _result ??= new Nand(
        inA: Left.Out, 
        inB: Right.Out, 
        outA: _outA,
        arraySize: ArraySize,
        isArray: IsArray,
        isResult: IsResult, 
        debug: Debug);
    
    // Constructors
    public Or16(
        string inAs = "a[16]",
        string inBs = "b[16]",
        string inA = "a",
        string inB = "b",
        string outAs = "out[16]",
        string outA = "out",
        bool isResult = false,
        bool debug = false)
    {
        Name = "Or16";
        Ins = [inAs, inBs];
        Outs = [outAs];
        _inA = inA;
        _inB = inB;
        _outA = outA;
        ArraySize = 16;
        IsArray = true;
        IsResult = isResult;
        Debug = debug;
    }

    // Methods
    public override string Print()
    {
        var sb = new StringBuilder();
        if (Debug) sb.AppendLine($"\t// Or16(A[]: {InAs}, B[]: {InBs}) => {OutAs}");
        sb.AppendLine($"\t{Left.Print()}");
        sb.AppendLine($"\t{Right.Print()}");
        sb.AppendLine($"\t{Result.Print()}");
        return sb.ToString();
    }

    public static void Make()
    {
        new Or16().Make(true);
        new Or16().Make(false);
    }

    private void Make(bool debug) => 
        base.Make(debug, new Or16(isResult: true, debug: debug));

}