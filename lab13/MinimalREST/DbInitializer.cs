using System.Net;
using System.Threading.Tasks;
using System.IO.Compression;

public static class DbInitializer{
    public static async Task  Initialize(IApplicationBuilder applicationBuilder){
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope()){
            var context = serviceScope.ServiceProvider.GetService<ContextDb>();

            if (!context.Measurement.Any()){

                string fileUrl = "https://request.openradiation.net/openradiation_dataset.tar.gz";
                string filePath2 = "openradiation_dataset.tar.gz";
                var filePath = Path.Combine(Environment.CurrentDirectory, filePath2);

                using (var httpClient = new HttpClient()) {
                    // Send a GET request to the specified Uri
                    using (var response = await httpClient.GetAsync(fileUrl, HttpCompletionOption.ResponseHeadersRead)){
                        response.EnsureSuccessStatusCode(); // Throw if not a success code.

                

                        // Read the content into a MemoryStream and then write to file
                        using (var ms = await response.Content.ReadAsStreamAsync())
                        using (var fs = File.Create(filePath))
                        {
                            await ms.CopyToAsync(fs);
                            fs.Flush();
                        }   
                }
                }
                string extractPath = Path.Combine(Environment.CurrentDirectory, "openradiation_dataset");
                Directory.CreateDirectory(extractPath);

                Tar.ExtractTarGz(filePath2, extractPath);
                

                string csvPAth = "out/measurements_withoutEnclosedObject.csv";
                var csvFile = Path.Combine(extractPath, csvPAth);

                var linesCSV = File.ReadAllLines(csvFile).Skip(1).Take(100);
                
                foreach (var line in linesCSV) {
                    System.Console.WriteLine(line);
                    var values = line.Split(';');
                    var measurement = new Measurement
                    {
                        
                        ApparatusId = values[0],
                        ApparatusVersion = values[1],
                        ApparatusSensorType = values[2],
                        ApparatusTubeType = values[3],
                        Temperature = values[4],
                        Value = values[5],
                        HitsNumer = values[6],
                        CalibrationFunction = values[7],
                        StartTime = values[8],
                        EndTime = values[9]
                    };

                    context.Measurement.Add(measurement);
                    
                }


                context.SaveChanges();
            }

        }

    }
}
