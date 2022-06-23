using BenchmarkDotNet.Running;

using BenchMarks;


BenchmarkRunner.Run<DirtyWordsInspector>();

//var inspector = new DirtyWordsInspector();
//inspector.OriginalText = "  Привет!  Ты   чо, сук@ , не   отвечаешь?  ";
//inspector.FindDirtyWord2();


Console.WriteLine("The END");
Console.ReadKey();

