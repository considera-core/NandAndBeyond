using System.Text;

namespace NandAndBeyond.Core.Chips;

public class Mux16 : Chip
{
    // Fields
    private Not? _notSel;
    
    private And? _left;
    
    private And? _right;
    
    private Or? _result;
    
    private string _outA;
    
    private string? _outB;

    private string _inA;

    private string _inB;

    private string _inSel;
    
    // Properties
    private string InAs => Ins[0];

    private string InBs => Ins[1];

    private string InSel => Ins[2];
    
    public string OutAs => IsResult 
        ? Outs[0] 
        : _outA ??= Result.Out;
    
    public override string InParams => $"\tIN {InAs}, {InBs}, {InSel};";

    public override string OutParams => $"\tOUT {OutAs};";
    
    private Not NotSel => _notSel ??= new Not(
        @in: _inSel, 
        arraySize: ArraySize,
        isArray: IsArray,
        debug: Debug);
    
    private And Left => _left ??= new And(
        inA: _inA, 
        inB: NotSel.Out, 
        arraySize: ArraySize,
        isArray: IsArray,
        debug: Debug);

    private And Right => _right ??= new And(
        inA: _inB, 
        inB: _inSel, 
        arraySize: ArraySize,
        isArray: IsArray,
        debug: Debug);

    private Or Result => _result ??= new Or(
        inA: Left.Out, 
        inB: Right.Out, 
        outA: _outA,
        arraySize: ArraySize,
        isArray: IsArray,
        isResult: IsResult, 
        debug: Debug);
    
    // Constructors
    public Mux16(
        string inAs = "a[16]",
        string inBs = "b[16]",
        string inA = "a",
        string inB = "b",
        string inSel = "sel",
        string outAs = "out[16]",
        string outA = "out",
        bool isResult = false,
        bool debug = false)
    {
        Name = "Mux16";
        Ins = [inAs, inBs, inSel];
        Outs = [outAs];
        _inA = inA;
        _inB = inB;
        _inSel = inSel;
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
        if (Debug) sb.AppendLine($"\t// Mux(A[]: {InAs}, B[]: {InBs}, Sel: {InSel}) => {OutAs}");
        sb.AppendLine($"\t{NotSel.Print()}");
        sb.AppendLine($"\t{Left.Print()}");
        sb.AppendLine($"\t{Right.Print()}");
        sb.AppendLine($"\t{Result.Print()}");
        return sb.ToString();
    }

    public static void Make()
    {
        new Mux16().Make(true);
        new Mux16().Make(false);
    }

    private void Make(bool debug) => 
        base.Make(debug, new Mux16(isResult: true, debug: debug));
}