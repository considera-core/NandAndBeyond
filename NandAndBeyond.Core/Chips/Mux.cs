using System.Text;

namespace NandAndBeyond.Core.Chips;

public class Mux : Chip
{
    // Fields
    private Not? _notSel;
    
    private And? _left;
    
    private And? _right;
    
    private Or? _result;
    
    // Properties
    private string InA => Ins[0];

    private string InB => Ins[1];

    private string Sel => Ins[2];
    
    public string Out => Result.Out;

    public override string InParams => $"\tIN {InA}, {InB}, {Sel};";
    
    public override string OutParams => $"\tOUT {Outs[0]};";
    
    private Not NotSel => _notSel ??= new Not(
        @in: Sel, 
        debug: Debug);
    
    private And Left => _left ??= new And(
        inA: InA, 
        inB: NotSel.Out, 
        debug: Debug);

    private And Right => _right ??= new And(
        inA: InB, 
        inB: Sel, 
        debug: Debug);

    private Or Result => _result ??= new Or(
        inA: Left.Out, 
        inB: Right.Out, 
        isResult: IsResult, 
        debug: Debug);
    
    // Constructors
    public Mux(
        string inA = "a",
        string inB = "b",
        string inSel = "sel",
        string outA = "out",
        int arraySize = 0,
        bool isArray = false,
        bool isResult = false,
        bool debug = false)
    {
        Name = "Mux";
        Ins = [inA, inB, inSel];
        Outs = [outA];
        IsResult = isResult;
        Debug = debug;
    }

    // Methods
    public override string Print()
    {
        var sb = new StringBuilder();
        if (Debug) sb.AppendLine($"\t// And(A: {InA}, B: {InB}) => {Out}");
        sb.AppendLine($"\t{NotSel.Print()}");
        sb.AppendLine($"\t{Left.Print()}");
        sb.AppendLine($"\t{Right.Print()}");
        sb.AppendLine($"\t{Result.Print()}");
        return sb.ToString();
    }

    public static void Make()
    {
        new Mux().Make(true);
        new Mux().Make(false);
    }

    private void Make(bool debug) => 
        base.Make(debug, new Mux(isResult: true, debug: debug));
}