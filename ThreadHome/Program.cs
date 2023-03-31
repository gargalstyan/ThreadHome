using ThreadHome;

ParellelCalc parellel = new ParellelCalc(1, (long)120, 6);
long res = parellel.Run(out long elapsed);
Console.WriteLine($"res = {res} time ={elapsed}");
