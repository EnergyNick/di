﻿using System;
using System.Collections.Generic;
using System.IO;
using SyntaxTextParser.Architecture;

namespace SyntaxTextParser
{
    public sealed class TextParser
    {
        private readonly TextElementParser elementParser;
        private readonly IFileReader fileReader;

        public TextParser(TextElementParser elementParser, IFileReader fileReader)
        {
            this.elementParser = elementParser;
            this.fileReader = fileReader;
        }

        /// <exception cref="ArgumentException">Thrown parser can't read that type file</exception>
        /// <exception cref="FileNotFoundException">Thrown parser can't found file</exception>
        public List<CountedTextElement> ParseElementsFromFile(string path, string fileName, string type)
        {
            if(!fileReader.CanReadThatType(type))
                throw new ArgumentException($"{nameof(fileReader)} can't read {type} file type");

            var fullPath = Path.Combine(path, fileName + '.' + type);
            if(!Directory.Exists(fullPath))
                throw new FileNotFoundException($"Path {path} isn't valid");

            return fileReader.TryReadText(fullPath, out var text)
                ? elementParser.ParseElementsFromText(text)
                : new List<CountedTextElement>();
        }
    }
}