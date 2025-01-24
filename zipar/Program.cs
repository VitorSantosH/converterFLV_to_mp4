using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        string ffmpegPath = @"C:\Users\Usuário\Documents\workstation\zipar\zipar\extComp\ffmpeg.exe"; // Altere para o caminho do ffmpeg
        string inputFile = @"C:\Users\Usuário\Documents\001007240017_1.live (1).flv";   // Caminho do arquivo FLV
        string outputFile = @"C:\Users\Usuário\Documents\001007240017_1.live (1).mp4";  // Caminho para salvar o arquivo MP4

        try
        {
            ConvertFlvToMp4(ffmpegPath, inputFile, outputFile);
            Console.WriteLine("Conversão concluída com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro durante a conversão: {ex.Message}");
        }
    }

    static void ConvertFlvToMp4(string ffmpegPath, string inputFile, string outputFile)
    {
        ProcessStartInfo processStartInfo = new ProcessStartInfo
        {
            FileName = ffmpegPath,
            Arguments = $"-i \"{inputFile}\" -c:v libx264 -crf 23 -preset fast -c:a aac -b:a 128k \"{outputFile}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (Process process = new Process())
        {
            process.StartInfo = processStartInfo;

            process.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);
            process.ErrorDataReceived += (sender, e) => Console.WriteLine(e.Data);

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                throw new Exception("FFmpeg falhou ao processar o arquivo.");
            }
        }
    }
    /*
     *  string pastaOrigem = @"C:\Users\Usuário\Documents\workstation\arquivo.pdf";  // Corrigido o caminho com barras invertidas
     string arquivo7zDestino = @"C:\Users\Usuário\Documents\workstation\arquivo.7z"; // Corrigido o caminho com barras invertidas

     string caminho7z = @"C:\Program Files\7-Zip\7z.exe"; // Caminho para o executável 7z

     // Comando para compactar o arquivo PDF em 7z
     string argumentos = $"a \"{arquivo7zDestino}\" \"{pastaOrigem}\" -mx=9"; // Ajuste no caminho da pasta de origem

     var processo = new Process
     {
         StartInfo = new ProcessStartInfo
         {
             FileName = caminho7z,
             Arguments = argumentos,
             RedirectStandardOutput = true,
             UseShellExecute = false,
             CreateNoWindow = true
         }
     };

     // Iniciar o processo de compactação
     processo.Start();
     processo.WaitForExit();

     Console.WriteLine("Arquivo comprimido com sucesso em: " + arquivo7zDestino);
     */


}
