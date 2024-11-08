using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UPCFinder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string filePath = "path_to_your_file.txt";
            ulong targetUPC = 8198354404; // Specify the UPC you're looking for
            int parallelism = Environment.ProcessorCount; // Number of concurrent tasks

            var found = new ConcurrentBag<ProductDetails>();

            Parallel.ForEach(File.ReadLines(filePath).Partition(parallelism), (lines) =>
            {
                foreach (var line in lines)
                {
                    var prodDetails = ParseLine(line);
                    if (prodDetails.UPC == targetUPC)
                    {
                        found.Add(prodDetails);
                        break; // Stop searching if found
                    }
                }
            });

            if (found.Any())
            {
                var product = found.First();
                Console.WriteLine($"UPC found: {product.UPC}, Item Name: {product.ItemName}");
                // Additional processing if needed
            }
        }

        private static ProductDetails ParseLine(string ln)
        {
            return new ProductDetails
            {
                StoreNumber = uint.Parse(ln.Substring(0, 4)),
                UPC = ulong.Parse(ln.Substring(4, 14)),
                // ... (other properties)
            };
        }
    }

    public static class PartitionExtensions
    {
        public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> source, int partitions)
        {
            var items = source.ToList();
            var partitionSize = (int)Math.Ceiling(items.Count / (double)partitions);

            for (int i = 0; i < partitions; i++)
            {
                yield return items.Skip(i * partitionSize).Take(partitionSize);
            }
        }
    }
}
