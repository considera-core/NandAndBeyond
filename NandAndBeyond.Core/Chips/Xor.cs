using System.Text;

namespace NandAndBeyond.Core.Chips;

public class Xor : Chip
{
    // Fields
    private Not? _notA;
    
    private Not? _notB;
    
    private And? _andLeft;
    
    private And? _andRight;
    
    private Or? _result;
    
    // Properties
    private string InA => Ins[0];

    private string InB => Ins[1];
    
    public string Out => Result.Out;

    public override string InParams => $"\tIN {InA}, {InB};";
    
    public override string OutParams => $"\tOUT {Outs[0]};";
    
    private Not NotA => _notA ??= new Not(
        @in: InA);

    private Not NotB => _notB ??= new Not(
        @in: InB);
    
    private And AndLeft => _andLeft ??= new And(
        inA: InA, 
        inB: NotB.Out, 
        debug: Debug);
    
    private And AndRight => _andRight ??= new And(
        inA: NotA.Out, 
        inB: InB, 
        debug: Debug);
    
    private Or Result => _result ??= new Or(
        inA: AndLeft.Out, 
        inB: AndRight.Out, 
        isResult: IsResult, 
        debug: Debug);
    
    // Constructors
    public Xor(
        string inA = "a",
        string inB = "b",
        string outA = "out",
        bool isResult = false,
        bool debug = false)
    {
        Name = "Xor";
        Ins = [inA, inB];
        Outs = [outA];
        IsResult = isResult;
        Debug = debug;
    }

    // Methods
    public override string Print()
    {
        var sb = new StringBuilder();

        if (Debug) sb.AppendLine($"\t// Xor(A: {InA}, B: {InB}) => {Out}");
        sb.AppendLine($"\t{NotA.Print()}");
        sb.AppendLine($"\t{NotB.Print()}");
        sb.AppendLine($"\t{AndLeft.Print()}");
        sb.AppendLine($"\t{AndRight.Print()}");
        sb.AppendLine($"\t{Result.Print()}");

        return sb.ToString();
    }

    public static void Make()
    {
        new Xor().Make(true);
        new Xor().Make(false);
    }

    private void Make(bool debug) => 
        base.Make(debug, new Xor(isResult: true, debug: debug));
}