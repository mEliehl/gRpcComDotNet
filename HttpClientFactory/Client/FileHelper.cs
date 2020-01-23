using System;
using System.IO;

namespace Client
{
    public class FileHelper
    {
        private const string PRODUCTS_TO_READ = "products_to_read";
        private const string PRODUCTS_PROCESSED = "products_processed";
        private const string PRODUCTS_CREATED = "products_created";

        public static string ProductPath { get; }
        public static string ProcessPath { get; }
        public static string ProductCreatedPath { get; }

        public static string DateFile =>
            DateTime.UtcNow.ToString("ddMMyyyyHHmmssfff");

        static FileHelper()
        {
            ProductPath = CreateDirectory(PRODUCTS_TO_READ);
            ProcessPath = CreateDirectory(PRODUCTS_PROCESSED);
            ProductCreatedPath = CreateDirectory(PRODUCTS_CREATED);

        }

        private static string CreateDirectory(string directory)
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var path = Path.Combine(currentDirectory, directory);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }
    }
}