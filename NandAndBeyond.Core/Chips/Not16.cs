using System.Text;

namespace NandAndBeyond.Core.Chips;

public class Not16 : Chip
{
    // Fields
    private Nand? _result;
    
    private string _outA;
    
    private string _inA;

    private string _inB;
    
    // Properties
    private string InAs => Ins[0];
    
    public string OutAs => IsResult 
        ? Outs[0] 
        : _outA ??= Result.Out;

    public override string InParams => $"\tIN {Ins[0]};";
    
    public override string OutParams => $"\tOUT {Outs[0]};";
    
    private Nand Result => _result ??= new Nand(
        inA: _inA, 
        inB: _inA, 
        outA: _outA, 
        arraySize: ArraySize,
        isArray: IsArray,
        isResult: IsResult, 
        debug: Debug);

    // Constructors
    public Not16(
        string inAs = "in[16]",
        string inA = "in",
        string outAs = "out[16]",
        string outA = "out",
        bool isResult = false,
        bool debug = false)
    {
        Name = "Not16";
        Ins = [inAs];
        Outs = [outAs];
        _inA = inA;
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
        if (Debug) sb.AppendLine($"\t// Not(A[]: {InAs}) => {OutAs}");
        sb.AppendLine($"\t{Result.Print()}");

        return sb.ToString();
    }

    public static void Make()
    {
        new Not16().Make(true);
        new Not16().Make(false);
    }

    private void Make(bool debug) => 
        base.Make(debug, new Not16(isResult: true, debug: debug));
}