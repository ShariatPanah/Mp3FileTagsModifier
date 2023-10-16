using System;
using System.IO;
using System.Linq;

namespace Mp3FileTagsModifier
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Please enter directory: ");
            string root = Console.ReadLine();
            Console.WriteLine();

            var folder = root.Substring(root.LastIndexOf('\\') + 1);
            Console.WriteLine($"Selected Folder: {folder}\n");

            var splitted = folder.Split('-');
            var artist = splitted[0];

            var albumName = folder;
            if (splitted.Length > 1)
                albumName = splitted[1].Trim();

            Console.WriteLine($"Album Name: {albumName}");
            Console.WriteLine($"Artist: {artist}");

            Console.Write("Do you want to proceed? (Y/N): ");
            var userRespond = Console.ReadLine();
            if (userRespond.ToLower() != "y")
                return;

            Console.WriteLine();

            var mp3files = Directory.GetFiles(root).Where(f => Path.GetExtension(f) == ".mp3");

            foreach (var file in mp3files)
            {
                var tagFile = TagLib.File.Create(file);
                tagFile.Tag.Album = albumName;
                tagFile.Tag.AlbumArtists = new string[] { artist };
                tagFile.Tag.Performers = new string[] { artist };

                var splittedTitle = tagFile.Name.Replace(root, "").Replace(".mp3", "").Split('-', '.');
                var title = splittedTitle[0].Trim();
                if (splittedTitle.Length > 1)
                    title = splittedTitle[splittedTitle.Length - 1].Trim();

                tagFile.Tag.Title = title;
                tagFile.Save();
            }

            Console.WriteLine("Operation finished successfully!");
            Console.ReadKey();
        }
    }
}
