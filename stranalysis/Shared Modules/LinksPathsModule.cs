using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using stranalysis.Core;

namespace stranalysis.Shared_Modules
{
    /// <summary>
    /// The LinksPathsModule detects links and file paths in the strings
    /// </summary>
    public class LinksPathsModule : Module
    {
        public LinksPathsModule() : base ("Links & Paths") { }

        private List<string> alreadyFound;
        
        /// <summary>
        /// Runs the module
        /// </summary>
        public override void Run(List<string> strings) {
            base.Run(strings);

            alreadyFound = new List<string>();

            for (int i = 0; i < strings.Count; i++) {

                // Check for empty string
                if (string.IsNullOrEmpty(strings[i])) continue;

                // Check for whitelisted string
                if (strings[i].StartsWith("System.") && strings[i].EndsWith(".dll")) continue; // Generic .NET reference

                // Check for P/Invoke lib
                if (IsPInvokeLib(strings[i])) continue;

                // Check if already found before
                if (alreadyFound.All(p => p != strings[i])) {

                    // Check if link  
                    if ((strings[i].StartsWith("http://") || strings[i].StartsWith("https://")) && strings[i].Length > 8) {
                        Program.WriteLine($"Link: {strings[i]}");
                        alreadyFound.Add(strings[i]);
                    }

                    // Check if path
                    if (IsValidPath(strings[i])) {
                        Program.WriteLine($"Path: {strings[i]}");
                        alreadyFound.Add(strings[i]);
                    }

                }
            }
        }

        /// <summary>
        /// Determines whether or not a lib is of the P/Invoke kind
        /// </summary>
        private readonly string[] pinvokelibs = Properties.Resources.PInvokeLibraries.Split('\n');
        private bool IsPInvokeLib(string name) {
            foreach (var lib in pinvokelibs) {
                if (string.Equals(lib.Replace("\r", string.Empty), name, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if a string is a valid path to a directory of a file
        /// </summary>
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVXYZWabcdefghijklmnopqrstuvxyzw";
        private const string Validfilechars = "ABCDEFGHIJKLMNOPQRSTUVXYZWabcdefghijklmnopqrstuvxyzw.";
        private readonly string[] Validextensions = ".aif|.cda|.mid|.midi|.mp3|.mp4|.ogg|.mpa|.wav|.wma|.7z|.arj|.deb|.rar|.zip|.tar.gz|.zip|.bin|.dmg|.iso|.vcd|.csv|.dat|.db|.dbf|.log|.mdb|.sav|.sql|.xml|.apk|.bat|.bin|.exe|.jar|.py|.pyc|.fnt|.bmp|.gif|.png|.jpeg|.jpg|.psd|.svg|.tif|.tiff|.asp|.aspx|.jsp|.js|.htm|.html|.css|.php|.rss|.ppt|.pptx|.pps|.key|.class|.cpp|.cs|.java|.swift|.sh|.vb|.xls|.xlsx|.bak|.cab|.cfg|.dll|.dmp|.ico|.drv|.ini|.msi|.lnk|.sys|.tmp|.avi|.flv|.m4v|.mkv|.mov|.mpg|.mpeg|.swf|.wmv|.txt|.rtf|.pdf|.doc|.docx".Split('|');
        private readonly string[] Midstringextensions = ".exe|.rar.|.zip|.7z|.iso|.bin|.bat|.jar|.dll|.sys".Split('|');

        private string fileRegexString = @"([\w]+.EXT)";

        private bool IsValidPath(string path) {
            path = path.Replace("\\", "/");

            // If its just a file name, return true
            if (path.All(c => Validfilechars.Contains(c)) && Validextensions.Any(p => path.EndsWith(p)) && !path.StartsWith(".") && path.Length > 4)
                return true;

            // Check for containing filename
            foreach(var ext in Midstringextensions)
                if (path.Contains(ext)) {
                    Regex fileRegex = new Regex(fileRegexString.Replace(".EXT", ext));
                    string val = fileRegex.Match(path).Value;
                    if (!string.IsNullOrEmpty(val) && alreadyFound.All(p => p != val) && val.Length > 4 && !IsPInvokeLib(val)) {
                        Program.WriteLine($"Path: {val}");
                        alreadyFound.Add(path);
                    }
                    return false;
                }

            // Check for a path
            if (path.Length < 4) // the minimum length for a path
                return false;

            if (Alphabet.Any(p => p == path[0]) && path[1] == ':' && path[2] == '/' &&
                (Validextensions.Any(p => path.EndsWith(p)) || path.Split('/')[path.Split('/').Length - 1].All(p => Alphabet.Contains(p))))
                return true;

            return false;
        }
    }
}
